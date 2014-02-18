function LoadStarLighterPowers() {
   %search = "scripts/Powers/Spells/StarLighter/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/StarLighter/"@%type@".cs");
   }
}
LoadStarLighterPowers(); //load these first

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(HarbingerofWarImage)
{
   className = WeaponImage;
   shapeFile = "turret_muzzlepoint.dts";
   item = HarbingerofWar;
   projectile = EnergyBolt;
   projectileType = EnergyProjectile;

   usesEnergy = true;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = BlasterSwitchSound;

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

datablock ItemData(HarbingerofWar)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy.dts";
   image = HarbingerofWarImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "wut?";
};

datablock ShapeBaseImageData(StarLighterStaffImage)
{
   shapeFile = "weapon_disc.dts";
   mountPoint = 1;

   offset = "0.1 0.8 0.55"; // L/R - F/B - T/B
   rotation = "2.0 -2.0 3.0 45"; // L/R - F/B - T/B
};

function CycleStarLighterModes(%this, %data) {
   if (!(getSimTime() > (%this.mineModeTime + 100)))
      return;
   %this.mineModeTime = getSimTime();
   %this.StarLighterMode++;
   %count = 0;
   while(%count < $Power::PowerCount["StarLighter"]) {    //random number, pay it no mind
      if(%this.StarLighterMode > $Power::PowerCount["StarLighter"]) {
         %this.StarLighterMode = 0;
      }
      %HasNext = CheckHasPower(%this.client, "StarLighter", %this.StarLighterMode);
      if (%HasNext) {
         %count = $Power::PowerCount["StarLighter"] + 5;
         //nothing, were done
      }
      else {
         //increase the count and, loop again
         %this.StarLighterMode++;
         %count++;
      }
   }
   DisplayStarLighterInfo(%this);
   return;
}

function DisplayStarLighterInfo(%obj) {
   switch(%obj.StarLighterMode) {
   case 0:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Harbinger of War<spop>\n<spush><font:Arial:14>STAR SHARD<spop>", 3, 3 );
   case 1:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Harbinger of War<spop>\n<spush><font:Arial:14>REPEL SHIFT<spop>", 3, 3 );
   case 2:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Harbinger of War<spop>\n<spush><font:Arial:14>SHINING STAR<spop>", 3, 3 );
   case 3:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Harbinger of War<spop>\n<spush><font:Arial:14>SHARD STORM<spop>", 3, 3 );
   case 4:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Harbinger of War<spop>\n<spush><font:Arial:14>ENVIOUS DOWNPOUR<spop>", 3, 3 );
   case 5:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Harbinger of War<spop>\n<spush><font:Arial:14>CATALYST BURST<spop>", 3, 3 );
   default:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Harbinger of War<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function HarbingerofWarImage::onMount(%this,%obj,%slot) {
     Parent::onMount(%data,%obj,%slot);
     commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
     %obj.mountImage(StarLighterStaffImage, 5);
     %obj.usingStarLighterStaff = true;
     DisplayStarLighterInfo(%obj);
}

function HarbingerofWarImage::onunmount(%this,%obj,%slot) {
     Parent::onUnmount(%this, %obj, %slot);
     %obj.unmountImage(5);
     %obj.usingStarLighterStaff = false;
}

function HarbingerofWarImage::onFire(%data,%obj,%slot){
    %client = %obj.client;
    %energy = FetchPowersEnergyLevel(%client);   // 0_o
    %maxE = FetchMaxEnergy(%client);
    %HaveIt = CheckHasPower(%obj.client, "StarLighter", %obj.StarLighterMode);
    if(!%HaveIt && !%client.isAiControlled()) {
       BottomPrint(%client, "<spush><font:Sui Generis:14>Harbinger of War<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
       return;
    }
    %vector = %obj.getMuzzleVector(%slot);
    %mp = %obj.getMuzzlePoint(%slot);
    if (%obj.StarLighterMode ==  1) {
       if(%energy > (%maxE - 5)) {
          TakeEnergy(%client, (%maxE - 5));
          RiftingTeleport(%obj, %slot);
       }
    }
    else if (%obj.StarLighterMode == 2) {
       if(%energy > 550) {
          TakeEnergy(%client, 550);
          %p = new (LinearFlareProjectile)() {
              dataBlock        = ShiningStar_StarProjo;
              initialDirection = %vector;
              initialPosition  = %mp;
              sourceObject     = %obj;
              damageFactor     = 1;
              sourceSlot       = %slot;
          };
          MissionCleanup.add(%p);
       }
    }
    else if (%obj.StarLighterMode == 3) {
       if(%energy > 650) {
          TakeEnergy(%client, 650);
          CallArtillerySpell(%obj, "ShardStorm", %slot, 5, 7);
       }
    }
    else if (%obj.StarLighterMode == 4) {
      if(%energy > %maxE-5) {
         TakeEnergy(%client, %maxE-5);
         CallArtillerySpell(%obj, "EnviousDownpour", %slot, 10, 8);
      }
    }
    else if(%obj.StarLighterMode == 5) {
       if(%energy > 700) {
          TakeEnergy(%client, 700);
          %p = new (LinearFlareProjectile)() {
              dataBlock        = CatalystBurst_PilotProjo;
              initialDirection = %vector;
              initialPosition  = %mp;
              sourceObject     = %obj;
              damageFactor     = 1;
              sourceSlot       = %slot;
          };
          MissionCleanup.add(%p);
       }
    }
    else {
       if(%energy > 150) {
          TakeEnergy(%client, 150);
          %p = new (LinearFlareProjectile)() {
              dataBlock        = StarShard;
              initialDirection = %vector;
              initialPosition  = %mp;
              sourceObject     = %obj;
              damageFactor     = 1;
              sourceSlot       = %slot;
          };
          MissionCleanup.add(%p);
       }
    }
//    DoPowersStuff(%obj.client, 1);
}
