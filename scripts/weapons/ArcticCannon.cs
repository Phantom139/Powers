//ArcticCannon.cs
//Phantom139

//Weapon for the DeepFreezer Class
function LoadDeepFreezerPowers() {
   %search = "scripts/Powers/Spells/DeepFreezer/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/DeepFreezer/"@%type@".cs");
   }
}
LoadDeepFreezerPowers(); //load these first

datablock AudioProfile(ArcticCannonSwitchSound)
{
   filename    = "fx/Bonuses/upward_passback3_crank.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock ShapeBaseImageData(ArcticCannonImage)
{
   className = WeaponImage;
   shapeFile = "turret_fusion_large.dts";
   item = ArcticCannon;
   projectile = EnergyBolt;
   projectileType = EnergyProjectile;

   usesEnergy = true;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = ArcticCannonSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.3;
   stateFire[3] = true;
   stateRecoil[3] = NoRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateSound[3] = BlasterFireSound;
   stateScript[3] = "onFire";

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6] = "DryFire";
   stateTimeoutValue[6] = 0.3;
   stateSound[6] = BlasterDryFireSound;
   stateTransitionOnTimeout[6] = "Ready";
};

datablock ItemData(ArcticCannon)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "turret_fusion_large.dts";
   image = ArcticCannonImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a Arctic Cannon";
};

function CycleDeepFreezerModes(%this, %data) {
   if (!(getSimTime() > (%this.mineModeTime + 100)))
      return;
   %this.mineModeTime = getSimTime();
   %this.DeepFreezerMode++;
   %count = 0;
   while(%count < $Power::PowerCount["Deep Freezer"]) {    //random number, pay it no mind
      if(%this.DeepFreezerMode > $Power::PowerCount["Deep Freezer"]) {
         %this.DeepFreezerMode = 0;
      }
      %HasNext = CheckHasPower(%this.client, "Deep Freezer", %this.DeepFreezerMode);
      if (%HasNext) {
         %count = $Power::PowerCount["Deep Freezer"] + 5;
         //nothing, were done
      }
      else {
         //increase the count and, loop again
         %this.DeepFreezerMode++;
         %count++;
      }
   }
   DisplayDeepFreezerInfo(%this);
   return;
}

function DisplayDeepFreezerInfo(%obj) {
   switch(%obj.DeepFreezerMode) {
      case 0:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Arctic Cannon<spop>\n<spush><font:Arial:14>SHADOW RIFT\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 1:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Arctic Cannon<spop>\n<spush><font:Arial:14>SEEKING FREEZE\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 2:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Arctic Cannon<spop>\n<spush><font:Arial:14>CRYSTAL BLAST\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 3:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Arctic Cannon<spop>\n<spush><font:Arial:14>WHITEOUT\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 4:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Arctic Cannon<spop>\n<spush><font:Arial:14>* SNOWSTORM *\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      default:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Arctic Cannon<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function ArcticCannonImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%data,%obj,%slot);
   commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
   %obj.usingArcticCannon = true;
   DisplayDeepFreezerInfo(%obj);
}

function ArcticCannonImage::onunmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   %obj.usingArcticCannon = false;
}

function ArcticCannonImage::onFire(%data, %obj, %slot){
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);   // 0_o
   %aEnergy = %obj.getAEnergy();
   %maxE = FetchMaxEnergy(%client);
   %HaveIt = CheckHasPower(%obj.client, "Deep Freezer", %obj.DeepFreezerMode);
   if(!%HaveIt && !%client.isAiControlled()) {
      BottomPrint(%client, "<spush><font:Sui Generis:14>Arctic Cannon<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
      return;
   }
   %vector = %obj.getMuzzleVector(%slot);
   %mp = %obj.getMuzzlePoint(%slot);
   switch(%obj.DeepFreezerMode) {
      case 0:
         if(%energy > (%maxE - 5)) {
            if(%aEnergy < 100) {
               initAffinityCharge(%obj);
            }
         }
      case 1:
         if(%aEnergy > 40) {
            %obj.takeAEnergy(40);
            %p = new (LinearFlareProjectile)() {
               dataBlock = SeekingFreeze;
               initialDirection = %vector;
               initialPosition  = %mp;
               sourceObject     = %obj;
               sourceSlot       = %slot;
            };
            MissionCleanup.add(%p);
            schedule(100, 0, "SeekingFreezeTargetLockCheck", %obj, %p);
         }
      case 2:

      case 3:

      case 4:
         if(%aEnergy == 100 && $CoolDownTime[%client, "Snowstorm"] == 0) {
            if(!activateMapEventPower(%obj, "Snowstorm")) {
               MessageClient(%obj.client, 'msgFailed', "\c3POWERS: A great disturbance is blocking you from using this power.");
               return;
            }
            else {
               $CoolDownTime[%client, "Snowstorm"] = 270;
               schedule(1000, 0, "CoolDownSpell", %client, "Snowstorm");
               CenterPrintAll("DEEP FREEZER ULTIMATE POWER\nSNOWSTORM\n"@%obj.client.namebase@"", 3, 3);
               %obj.takeAEnergy(100);
            }
         }
      default:
         //do nothing
   }
}
