//CryonicSpear.cs
//Phantom139

//Weapon for the Cryonium Class
//Formerly the Cryonic Embassador
function LoadCryoniumPowers() {
   %search = "scripts/Powers/Spells/Cryonium/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/Cryonium/"@%type@".cs");
   }
}
LoadCryoniumPowers(); //load these first

datablock ShapeBaseImageData(CryonicSpearImage)
{
   className = WeaponImage;
   shapeFile = "turret_muzzlepoint.dts";
   item = CryonicSpear;
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

datablock ItemData(CryonicSpear)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy.dts";
   image = CryonicSpearImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a Cryonic Spear";
};

datablock ShapeBaseImageData(CryonicSpearLookImage)
{
   shapeFile = "weapon_sniper.dts";
   mountPoint = 1;

   offset = "0.1 0.8 0.55"; // L/R - F/B - T/B
   rotation = "2.0 -2.0 3.0 45"; // L/R - F/B - T/B
};

datablock ShapeBaseImageData(CryonicSpearLookImage2)
{
   shapeFile = "weapon_shocklance.dts";
   mountPoint = 2;

   offset = "0.1 0.8 0.99"; // L/R - F/B - T/B
   rotation = "2.0 -2.0 3.0 45"; // L/R - F/B - T/B
};

function CycleCryoniumModes(%this, %data) {
   if (!(getSimTime() > (%this.mineModeTime + 100)))
      return;
   %this.mineModeTime = getSimTime();
   %this.CryoniumMode++;
   %count = 0;
   while(%count < $Power::PowerCount["Cryonium"]) {    //random number, pay it no mind
      if(%this.CryoniumMode > $Power::PowerCount["Cryonium"]) {
         %this.CryoniumMode = 0;
      }
      %HasNext = CheckHasPower(%this.client, "Cryonium", %this.CryoniumMode);
      if (%HasNext) {
         %count = $Power::PowerCount["Cryonium"] + 5;
         //nothing, were done
      }
      else {
         //increase the count and, loop again
         %this.CryoniumMode++;
         %count++;
      }
   }
   DisplayCryoniumInfo(%this);
   return;
}

function DisplayCryoniumInfo(%obj) {
   switch(%obj.CryoniumMode) {
      case 0:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Cryonic Spear<spop>\n<spush><font:Arial:14>ICE RUSH<spop>", 3, 3 );
      case 1:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Cryonic Spear<spop>\n<spush><font:Arial:14>ICE BOMB<spop>", 3, 3 );
      case 2:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Cryonic Spear<spop>\n<spush><font:Arial:14>FROZEN TWISTER<spop>", 3, 3 );
      case 3:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Cryonic Spear<spop>\n<spush><font:Arial:14>CRYSTAL SHOCK<spop>", 3, 3 );
      case 4:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Cryonic Spear<spop>\n<spush><font:Arial:14>DEEP FREEZE<spop>", 3, 3 );
      case 5:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Cryonic Spear<spop>\n<spush><font:Arial:14>BLIZZARD<spop>", 3, 3 );
      default:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Cryonic Spear<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function CryonicSpearImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%data,%obj,%slot);
   commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
   %obj.mountImage(CryonicSpearLookImage, 6);
   %obj.mountImage(CryonicSpearLookImage2, 4);
   %obj.usingCryonicSpear = true;
   DisplayCryoniumInfo(%obj);
}

function CryonicSpearImage::onunmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   if(%obj.CastingContinuous) {
      %obj.setMoveState(false);
      %obj.CastingContinuous = 0; //stop any of these
   }
   %obj.unmountImage(6);
   %obj.unmountImage(4);
   %obj.usingCryonicSpear = false;
}

function CryonicSpearImage::onFire(%data,%obj,%slot){
   if(%obj.CastingContinuous) {
      %obj.CastingContinuous = 0;
      %obj.setMoveState(false);
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Cryonic Spear<spop>\n<spush><font:Arial:14>Continuous Casting Terminated.<spop>", 3, 3 );
      return;
   }
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);   // 0_o
   %HaveIt = CheckHasPower(%obj.client, "Cryonium", %obj.CryoniumMode);
   if(!%HaveIt && !%client.isAiControlled()) {
      BottomPrint(%client, "<spush><font:Sui Generis:14>Cryonic Spear<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
      return;
   }
   %vector = %obj.getMuzzleVector(%slot);
   %mp = %obj.getMuzzlePoint(%slot);
   if (%obj.CryoniumMode == 1) {
      if(%energy > 200) {
         TakeEnergy(%client, 200);
         CallArtillerySpell(%obj, "IceBomb", %slot, 10, 6);
      }
   }
   else if (%obj.CryoniumMode == 2) {
      if(%energy > 300) {        //We require 50 more energy to cast at least 2 more
         TakeEnergy(%client, 250);
         %obj.CastingContinuous = 1;
         %obj.setMoveState(true); //no movement please...
         FrozenTwisterLoop(%obj);
      }
   }
   else if (%obj.CryoniumMode == 3) {
      if(%energy > 200) {
         TakeEnergy(%client, 200);
         %p = new (LinearFlareProjectile)() {
            dataBlock        = CrystalShockPulse;
            initialDirection = %vector;
            initialPosition  = %mp;
            sourceObject     = %obj;
            damageFactor     = 1;
            sourceSlot       = %slot;
         };
         MissionCleanup.add(%p);
      }
   }
   else if (%obj.CryoniumMode == 4) {
      if(%energy > 500) {
         TakeEnergy(%client, 500);
         CallArtillerySpell(%obj, "DeepFreeze", %slot, 15, 10);
      }
   }
   else if (%obj.CryoniumMode == 5) {
      if(%energy > 450) {        //We require 50 more energy to cast at least 2 more
         TakeEnergy(%client, 400);
         %obj.CastingContinuous = 1;
         %obj.setMoveState(true); //no movement please...
         %tPos = PowersEyeCast(%obj); // <<- Me likey :D
         BlizzardLoop(%obj, %tPos);
      }
   }
   else {
      if(%energy > 80) {
         TakeEnergy(%client, 80);
         for(%i = 0; %i < 3; %i++) {
            %time = %i *500;
            schedule(%time, 0, "spawnprojectile","IceRushMissile","SeekerProjectile",%mp, %vector, %obj);
         }
      }
   }
}
