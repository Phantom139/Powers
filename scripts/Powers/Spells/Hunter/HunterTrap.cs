// ----------------------------------------------
// mine script
// ----------------------------------------------

$TeamDeployableMax[HunterTrapDeployed]		= 9999;

// ----------------------------------------------
// Item datablocks
// ----------------------------------------------

datablock ItemData(HunterTrapDeployed) {
   className = Weapon;
   shapeFile = "mine.dts";
   mass = 0.75;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 3;

   //maxDamage = 0.2;
   //explosion = MineExplosion;
   //underwaterExplosion = UnderwaterMineExplosion;
   //indirectDamage = 0.55;
   //damageRadius = 6.0;
   //radiusDamageType = $DamageType::Mine;
   //kickBackStrength = 1500;
   aiAvoidThis = true;
   dynamicType = $TypeMasks::DamagableItemObjectType;

   spacing = 0.1; // how close together mines can be
   proximity = 5; // how close causes a detonation (by player/vehicle)
   armTime = 1500; // 2.2 seconds to arm a mine after it comes to rest
   maxDepCount = 15; // try to deploy this many times before detonating

   computeCRC = true;
};

//
function LaunchHunterTrap(%player, %type, %mp, %vector) {
   %client = %player.client;
   
   %mine = new Item() {
      datablock = HunterTrapDeployed;
   };
   //apply the velocity
   %vec = vectorScale(%vector, 20);
   
   %mine.setTransform(%mp);
   %mine.applyImpulse(%mp, %vec);
   
   %mine.type = %type;

   %mine.armed = false;
   %mine.damaged = 0;
   %mine.detonated = false;
   %mine.depCount = 0;
   %mine.theClient = %client;
   schedule(1500, %mine, "deployHunterTrapCheck", %mine, %player);
}

function deployHunterTrapCheck(%mineObj, %player) {
   if(%mineObj.depCount > %mineObj.getDatablock().maxDepCount) {
      explodeTrap(%mineObj, true);
   }

   // wait until the mine comes to rest
   if(%mineObj.getVelocity() $= "0 0 0") {
      // 2-second delay before mine is armed -- let deploy thread play out etc.
      schedule(%mineObj.getDatablock().armTime, %mineObj, "armDeployedMine", %mineObj);

      // play "deploy" thread
      %mineObj.playThread(0, "deploy");
      serverPlay3D(MineDeploySound, %mineObj.getTransform());
      %mineTeam = %mineObj.sourceObject.team;
      %mineObj.team = %player.team;

      //start the thread that keeps checking for objects near the mine...
      hunterTrapCheckVicinity(%mineObj);

      //let the AI know *after* it's come to rest...
      AIDeployMine(%mineObj);

      //let the game know there's a deployed mine
      Game.notifyMineDeployed(%mineObj);
      
      //If the player has the night traps upgrade, cloak the mine now
      if(CheckHasPowerByName(%player.client, "NightTrap")) {
         %mineObj.setCloaked(true);
      }

      switch$(%mineObj.type) {
         case "Quake":
            %mineObj.proximity = 5;
         case "Net":
            %mineObj.proximity = 5;
         case "Blast":
            %mineObj.proximity = 7;
         case "Toxic":
            %mineObj.proximity = 7;
         case "Freeze":
            %mineObj.proximity = 7;
         case "Burn":
            %mineObj.proximity = 7;
         case "Burst":
            %mineObj.proximity = 8;
      }
      
   }
   else {
      //schedule this deploy check again a little later
      %mineObj.depCount++;
      schedule(500, %mineObj, "deployHunterTrapCheck", %mineObj, %player);
   }
}

function hunterTrapCheckVicinity(%mine) {
   if(%mine.armed) {
      if(!%mine.boom) {
         %mineLoc = %mine.getWorldBoxCenter();
         %masks = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType;
         %detonateRange = %mine.proximity;
         if(!%detonateRange) {
            %detonateRange = 5;
         }

         InitContainerRadiusSearch(%mineLoc, %detonateRange, %masks);
         while((%tgt = containerSearchNext()) != 0) {
            if(%tgt.team != %mine.team) {
               %mine.detonated = true;
               schedule(50, %mine, "explodeTrap", %mine, false);
               break;
            }
         }
      }
   }
   // if nothing set off the mine, schedule another check
   if(!%mine.detonated) {
      schedule(300, %mine, "hunterTrapCheckVicinity", %mine);
   }
}

function HunterTrapDeployed::onCollision(%data, %obj, %col) {
   if(!%obj.armed) {
      return;
   }
   if(%obj.boom) {
      return;
   }

   //check to see what it is that collided with the mine
   %struck = %col.getClassName();
   if(%struck $= "Player" || %struck $= "WheeledVehicle" || %struck $= "FlyingVehicle") {
      if(%col.team != %obj.team) {
         explodeTrap(%obj, false);
      }
   }
}

function explodeTrap(%mo, %noDamage) {
   %mo.noDamage = %noDamage;
   %mo.setDamageState(Destroyed);
}

function HunterTrapDeployed::damageObject(%data, %targetObject, %sourceObject, %position, %amount, %damageType) {
   if(!%targetObject.armed) {
      return;
   }

   if(%targetObject.boom) {
      return;
   }

   %targetObject.damaged += %amount;

   if(%targetObject.damaged >= %data.maxDamage) {
      %targetObject.setDamageState(Destroyed);
   }
}

function HunterTrapDeployed::onDestroyed(%data, %obj, %lastState) {
   %obj.boom = true;
   %mineTeam = %obj.team;

   if(!%obj.noDamage) {
      RadiusExplosion(%obj, %obj.getPosition(), %obj.damageRadius, %obj.indirectDamage,
                      %obj.kickBackStrength, %obj.sourceObject, %obj.radiusDamageType);
      //
      ApplyTrapEffect(%obj);
   }

   %obj.schedule(600, "delete");
}

function ApplyTrapEffect(%trap) {
   %position = %trap.getPosition();
   %sourceCL = %trap.theClient;
   switch$(%trap.type) {
      case "Quake":
         %p = new (TracerProjectile)() {
            dataBlock        = QuakeTrapDown;
            initialDirection = "0 0 -1";
            initialPosition  = vectorAdd(%position, "0 0 1");
            sourceObject     = %sourceCL.player;
            sourceClient     = %sourceCL;
            damageFactor     = 1;
         };
         MissionCleanup.add(%p);
         
      case "Net":
         //net trap - hit all within 5m
         %TargetSearchMask = $TypeMasks::PlayerObjectType;
         InitContainerRadiusSearch(%position, 5, %TargetSearchMask);
         while ((%potentialTarget = ContainerSearchNext()) != 0) {
            //stun them for a short period of time
            %potentialTarget.setMoveState(true);
            %potentialTarget.schedule(5000, setMoveState, false);
            if(!%potentialTarget.client.isAIControlled()) {
               MessageClient(%potentialTarget.client, 'msgTrap', "\c2You have been ensnared by a net trap.");
               schedule(5000, 0, MessageClient, %potentialTarget.client, 'msgTrap', "\c2You have escaped the net trap.");
            }
         }
         //

      case "Blast":
         %p = new (TracerProjectile)() {
            dataBlock        = BlastTrapDown;
            initialDirection = "0 0 -1";
            initialPosition  = vectorAdd(%position, "0 0 1");
            sourceObject     = %sourceCL.player;
            sourceClient     = %sourceCL;
            damageFactor     = 1;
         };
         MissionCleanup.add(%p);
      
      case "Toxic":
         %p = new (TracerProjectile)() {
            dataBlock        = AcidTrapDown;
            initialDirection = "0 0 -1";
            initialPosition  = vectorAdd(%position, "0 0 1");
            sourceObject     = %sourceCL.player;
            sourceClient     = %sourceCL;
            damageFactor     = 1;
         };
         MissionCleanup.add(%p);
      
      case "Freeze":
         %p = new (TracerProjectile)() {
            dataBlock        = FreezeTrapDown;
            initialDirection = "0 0 -1";
            initialPosition  = vectorAdd(%position, "0 0 1");
            sourceObject     = %sourceCL.player;
            sourceClient     = %sourceCL;
            damageFactor     = 1;
         };
         MissionCleanup.add(%p);
      
      case "Burn":
         %p = new (TracerProjectile)() {
            dataBlock        = BurnTrapDown;
            initialDirection = "0 0 -1";
            initialPosition  = vectorAdd(%position, "0 0 1");
            sourceObject     = %sourceCL.player;
            sourceClient     = %sourceCL;
            damageFactor     = 1;
         };
         MissionCleanup.add(%p);
      
      case "Burst":
         BurstTrapExplode(%trap);
   }
}
