//Staff of Embirthia
//Phantom139

//Enforcer Class Weapon
function LoadEnforcerPowers() {
   %search = "scripts/Powers/Spells/Enforcer/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/Enforcer/"@%type@".cs");
   }
}
LoadEnforcerPowers(); //load these first

datablock ShapeBaseImageData(StaffofEmbirthiaImage) {
   className = WeaponImage;
   shapeFile = "turret_muzzlepoint.dts";
   item = StaffofEmbirthia;
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

datablock ItemData(StaffofEmbirthia) {
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy.dts";
   image = StaffofEmbirthiaImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a Staff of Embirthia";
};

datablock ShapeBaseImageData(StaffofEmbirthiaLookImage) {
   shapeFile = "weapon_ELF.dts";
   mountPoint = 1;

   offset = "0.1 0.8 0.55"; // L/R - F/B - T/B
   rotation = "2.0 -2.0 3.0 45"; // L/R - F/B - T/B
};

function CycleEnforcerModes(%this, %data) {
   if (!(getSimTime() > (%this.mineModeTime + 100)))
      return;
   %this.mineModeTime = getSimTime();
   %this.EnforcerMode++;
   %count = 0;
   while(%count < $Power::PowerCount["Enforcer"]) {    //random number, pay it no mind
      if(%this.EnforcerMode > $Power::PowerCount["Enforcer"]) {
         %this.EnforcerMode = 0;
      }
      %HasNext = CheckHasPower(%this.client, "Enforcer", %this.EnforcerMode);
      if (%HasNext) {
         %count = $Power::PowerCount["Enforcer"] + 5;
         //nothing, were done
      }
      else {
         //increase the count and, loop again
         %this.EnforcerMode++;
         %count++;
      }
   }
   DisplayEnforcerInfo(%this);
   return;
}

function DisplayEnforcerInfo(%obj) {
   switch(%obj.EnforcerMode) {
      case 0:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Staff of Embirthia<spop>\n<spush><font:Arial:14>DEEP FLAME SHOT<spop>", 3, 3 );
      case 1:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Staff of Embirthia<spop>\n<spush><font:Arial:14>FIRE BALL LEVEL 5<spop>", 3, 3 );
      case 2:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Staff of Embirthia<spop>\n<spush><font:Arial:14>FIRE BALL LEVEL 6<spop>", 3, 3 );
      case 3:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Staff of Embirthia<spop>\n<spush><font:Arial:14>ENRAGE<spop>", 3, 3 );
      case 4:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Staff of Embirthia<spop>\n<spush><font:Arial:14>SUPER SPLIT FIRE BOMB<spop>", 3, 3 );
      case 5:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Staff of Embirthia<spop>\n<spush><font:Arial:14>HELLSTORM<spop>", 3, 3 );
      default:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Staff of Embirthia<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function StaffofEmbirthiaImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%data,%obj,%slot);
   commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
   %obj.mountImage(StaffofEmbirthiaLookImage, 6);
   %obj.usingStaffofEmbirthia = true;
   DisplayEnforcerInfo(%obj);
}

function StaffofEmbirthiaImage::onunmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   if(%obj.CastingContinuous) {
      %obj.setMoveState(false);
      %obj.CastingContinuous = 0; //stop any of these
   }
   %obj.unmountImage(6);
   %obj.usingStaffofEmbirthia = false;
}

function StaffofEmbirthiaImage::onFire(%data,%obj,%slot){
   if(%obj.CastingContinuous) {
      %obj.CastingContinuous = 0;
      %obj.setMoveState(false);
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Staff of Embirthia<spop>\n<spush><font:Arial:14>Continuous Casting Terminated.<spop>", 3, 3 );
      return;
   }
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);   // 0_o
   %maxE = FetchMaxEnergy(%client);
   %HaveIt = CheckHasPower(%obj.client, "Enforcer", %obj.EnforcerMode);
   if(!%HaveIt && !%client.isAiControlled()) {
      BottomPrint(%client, "<spush><font:Sui Generis:14>Staff of Embirthia<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
      return;
   }
   %vector = %obj.getMuzzleVector(%slot);
   %mp = %obj.getMuzzlePoint(%slot);
   if (%obj.EnforcerMode == 1) {
      if(%energy > 450) {
         TakeEnergy(%client, 450);
         %p = new (LinearProjectile)() {
            dataBlock        = FireballShotLv5;
            initialDirection = %vector;
            initialPosition  = %mp;
            sourceObject     = %obj;
            damageFactor     = 1;
            sourceSlot       = %slot;
         };
         MissionCleanup.add(%p);
      }
   }
   else if (%obj.EnforcerMode == 2) {
      if(%energy > 500) {
         TakeEnergy(%client, 500);
         %p = new (LinearProjectile)() {
            dataBlock        = FireballShotLv6;
            initialDirection = %vector;
            initialPosition  = %mp;
            sourceObject     = %obj;
            damageFactor     = 1;
            sourceSlot       = %slot;
         };
         MissionCleanup.add(%p);
      }
   }
   else if (%obj.EnforcerMode == 3) {
      if(%energy > %maxE-5) {
         TakeEnergy(%client, %maxE-5);
         makeObjectInvincible(%obj, 8);
      }
   }
   else if (%obj.EnforcerMode == 4) {
      if(%energy > 600) {
         TakeEnergy(%client, 600);
         %p = new (LinearProjectile)() {
            dataBlock        = PowerfulNapalmShot;
            initialDirection = %vector;
            initialPosition  = %mp;
            sourceObject     = %obj;
            damageFactor     = 1;
            sourceSlot       = %slot;
         };
         MissionCleanup.add(%p);
      }
   }
   else if (%obj.EnforcerMode == 5) {
      if(%energy > 450) {        //We require 50 more energy to cast at least 2 more
         TakeEnergy(%client, 400);
         %obj.CastingContinuous = 1;
         %obj.setMoveState(true); //no movement please...
         %tPos = PowersEyeCast(%obj); // <<- Me likey :D
         HellstormLoop(%obj, %tPos);
      }
   }
   else {
      if(%energy > 80) {
         TakeEnergy(%client, 80);
         %p = new (LinearFlareProjectile)() {
            dataBlock        = DeepFlame;
            initialDirection = %vector;
            initialPosition  = %mp;
            sourceObject     = %obj;
            damageFactor     = 1;
            sourceSlot       = %slot;
         };
         MissionCleanup.add(%p);
      }
   }
}
