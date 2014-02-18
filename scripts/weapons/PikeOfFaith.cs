//PikeOfFaith.cs
//Phantom139

//Weapon for the Cryonic Embassador Class
function LoadGuardianPowers() {
   %search = "scripts/Powers/Spells/Guardian/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/Guardian/"@%type@".cs");
   }
}
LoadGuardianPowers(); //load these first

datablock ShapeBaseImageData(PikeOfFaithImage)
{
   className = WeaponImage;
   shapeFile = "turret_muzzlepoint.dts";
   item = PikeOfFaith;
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

datablock ItemData(PikeOfFaith)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy.dts";
   image = PikeOfFaithImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a Pike Of Faith";
};

datablock ShapeBaseImageData(PikeOfFaithLookImage)
{
   shapeFile = "weapon_ELF.dts";
   mountPoint = 1;

   offset = "0.1 0.8 0.55"; // L/R - F/B - T/B
   rotation = "2.0 -2.0 3.0 45"; // L/R - F/B - T/B
};

datablock ShapeBaseImageData(PikeOfFaithLookImage2)
{
   shapeFile = "weapon_shocklance.dts";
   mountPoint = 2;

   offset = "0.1 0.8 0.99"; // L/R - F/B - T/B
   rotation = "2.0 -2.0 3.0 45"; // L/R - F/B - T/B
};

function CycleGuardianModes(%this, %data) {
   if (!(getSimTime() > (%this.mineModeTime + 100)))
      return;
   %this.mineModeTime = getSimTime();
   %this.GuardianMode++;
   %count = 0;
   while(%count < $Power::PowerCount["Guardian"]) {    //random number, pay it no mind
      if(%this.GuardianMode > $Power::PowerCount["Guardian"]) {
         %this.GuardianMode = 0;
      }
      %HasNext = CheckHasPower(%this.client, "Guardian", %this.GuardianMode);
      if (%HasNext) {
         %count = $Power::PowerCount["Guardian"] + 5;
         //nothing, were done
      }
      else {
         //increase the count and, loop again
         %this.GuardianMode++;
         %count++;
      }
   }
   DisplayGuardianInfo(%this);
   return;
}

function DisplayGuardianInfo(%obj) {
   switch(%obj.GuardianMode) {
      case 0:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Pike Of Faith<spop>\n<spush><font:Arial:14>FORCEED PUSH<spop>", 3, 3 );
      case 1:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Pike Of Faith<spop>\n<spush><font:Arial:14>ENERGY NUKE<spop>", 3, 3 );
      case 2:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Pike Of Faith<spop>\n<spush><font:Arial:14>STASIS FIELD<spop>", 3, 3 );
      case 3:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Pike Of Faith<spop>\n<spush><font:Arial:14>ENERGY BOOST<spop>", 3, 3 );
      case 4:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Pike Of Faith<spop>\n<spush><font:Arial:14>DAMAGE REDUCER<spop>", 3, 3 );
      case 5:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Pike Of Faith<spop>\n<spush><font:Arial:14>DEGAUSSIAN STRIKE<spop>", 3, 3 );
      default:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Pike Of Faith<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function PikeOfFaithImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%data,%obj,%slot);
   commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
   %obj.mountImage(PikeOfFaithLookImage, 6);
   %obj.mountImage(PikeOfFaithLookImage2, 4);
   %obj.usingPikeOfFaith = true;
   DisplayGuardianInfo(%obj);
}

function PikeOfFaithImage::onunmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   if(%obj.CastingContinuous) {
      %obj.setMoveState(false);
      %obj.CastingContinuous = 0; //stop any of these
   }
   %obj.unmountImage(6);
   %obj.unmountImage(4);
   %obj.usingPikeOfFaith = false;
}

function PikeOfFaithImage::onFire(%data,%obj,%slot){
   if(%obj.CastingContinuous) {
      %obj.CastingContinuous = 0;
      %obj.setMoveState(false);
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Pike Of Faith<spop>\n<spush><font:Arial:14>Continuous Casting Terminated.<spop>", 3, 3 );
      return;
   }
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);   // 0_o
   %maxE = FetchMaxEnergy(%client);
   %HaveIt = CheckHasPower(%obj.client, "Guardian", %obj.GuardianMode);
   if(!%HaveIt && !%client.isAiControlled()) {
      BottomPrint(%client, "<spush><font:Sui Generis:14>Pike Of Faith<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
      return;
   }
   %vector = %obj.getMuzzleVector(%slot);
   %mp = %obj.getMuzzlePoint(%slot);
   if (%obj.GuardianMode == 1) {
      if(%energy > (%maxE - 5) && $CoolDownTime[%client, "Energy Nuke"] == 0) {
         $CoolDownTime[%client, "Energy Nuke"] = 150;
         schedule(1000, 0, "CoolDownSpell", %client, "Energy Nuke");
         TakeEnergy(%client, (%maxE - 5));
         EMP(%obj, 15);
      }
   }
   else if (%obj.GuardianMode == 2) {
      if(%energy > (%maxE - 5) && $CoolDownTime[%client, "Stasis Field"] == 0) {
         $CoolDownTime[%client, "Stasis Field"] = 45;
         schedule(1000, 0, "CoolDownSpell", %client, "Stasis Field");
         TakeEnergy(%client, (%maxE - 5));
         %tPos = PowersEyeCast(%obj); // <<- Me likey :D
         initiateStatisField(%obj, %tPos);
      }
   }
   else if (%obj.GuardianMode == 3) {
      if(%energy > (%maxE - 5)) {
         TakeEnergy(%client, (%maxE - 5));
         onFireShock(EnergyBoost, %obj, %slot);
      }
   }
   else if (%obj.GuardianMode == 4) {
      if(%energy > 500) {
         TakeEnergy(%client, 500);
         %tPos = PowersEyeCast(%obj); // <<- Me likey :D
         DoEnemyMassAreaSpell(%obj, %tPos, "DamageReduction", 20);
      }
   }
   else if (%obj.GuardianMode == 5) {
      if(%energy > 600) {
         TakeEnergy(%client, 600);
         CallArtillerySpell(%obj, "DegaussianStrike", %slot, 9, 5);
      }
   }
   else {
      if(%energy > 225) {
         TakeEnergy(%client, 225);
         %p = new (LinearFlareProjectile)() {
            dataBlock        = ForceedPulse;
            initialDirection = %vector;
            initialPosition  = %mp;
            sourceObject     = %obj;
            damageFactor     = 1;
            sourceSlot       = %slot;
         };
         MissionCleanup.add(%p);
         //
         %p.ShockwaveSched = schedule(100, 0, "ForceedPushShockwaves", %obj, %p);
      }
   }
}
