//Asthenie Sword
//Phantom139

//Devastator Class Weapon
function LoadDevastatorPowers() {
   %search = "scripts/Powers/Spells/Devastator/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/Devastator/"@%type@".cs");
   }
}
LoadDevastatorPowers(); //load these first

datablock ShapeBaseImageData(AsthenieSwordImage)
{
   className = WeaponImage;
   shapeFile = "weapon_shocklance.dts";
   item = AsthenieSword;
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

datablock ItemData(AsthenieSword)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_shocklance.dts";
   image = AsthenieSwordImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a Asthenie Sword";
};

function CycleDevastatorModes(%this, %data) {
   if (!(getSimTime() > (%this.mineModeTime + 100)))
      return;
   %this.mineModeTime = getSimTime();
   %this.DevastatorMode++;
   %count = 0;
   while(%count < $Power::PowerCount["Devastator"]) {    //random number, pay it no mind
      if(%this.DevastatorMode > $Power::PowerCount["Devastator"]) {
         %this.DevastatorMode = 0;
      }
      %HasNext = CheckHasPower(%this.client, "Devastator", %this.DevastatorMode);
      if (%HasNext) {
         %count = $Power::PowerCount["Devastator"] + 5;
         //nothing, were done
      }
      else {
         //increase the count and, loop again
         %this.DevastatorMode++;
         %count++;
      }
   }
   DisplayDevastatorInfo(%this);
   return;
}

function DisplayDevastatorInfo(%obj) {
   switch(%obj.DevastatorMode) {
      case 0:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Asthenie Sword<spop>\n<spush><font:Arial:14>BURN FORCE<spop>", 3, 3 );
      case 1:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Asthenie Sword<spop>\n<spush><font:Arial:14>FISSURE BURST<spop>", 3, 3 );
      case 2:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Asthenie Sword<spop>\n<spush><font:Arial:14>MICROWAVE<spop>", 3, 3 );
      case 3:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Asthenie Sword<spop>\n<spush><font:Arial:14>CRISP<spop>", 3, 3 );
      case 4:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Asthenie Sword<spop>\n<spush><font:Arial:14>DESOLATION<spop>", 3, 3 );
      case 5:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Asthenie Sword<spop>\n<spush><font:Arial:14>SUN RAY<spop>", 3, 3 );
      default:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Asthenie Sword<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function AsthenieSwordImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%data,%obj,%slot);
   commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
   %obj.usingAsthenieSword = true;
   DisplayDevastatorInfo(%obj);
}

function AsthenieSwordImage::onunmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   if(%obj.CastingContinuous) {
      %obj.setMoveState(false);
      %obj.CastingContinuous = 0; //stop any of these
   }
   %obj.usingAsthenieSword = false;
}

function AsthenieSwordImage::onFire(%data,%obj,%slot){
   if(%obj.CastingContinuous) {
      %obj.CastingContinuous = 0;
      %obj.setMoveState(false);
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Asthenie Sword<spop>\n<spush><font:Arial:14>Continuous Casting Terminated.<spop>", 3, 3 );
      return;
   }
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);   // 0_o
   %maxE = FetchMaxEnergy(%client);
   %HaveIt = CheckHasPower(%obj.client, "Devastator", %obj.DevastatorMode);
   if(!%HaveIt && !%client.isAiControlled()) {
      BottomPrint(%client, "<spush><font:Sui Generis:14>Asthenie Sword<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
      return;
   }
   %vector = %obj.getMuzzleVector(%slot);
   %mp = %obj.getMuzzlePoint(%slot);
   if (%obj.DevastatorMode == 1) {
      if(%energy > 500) {
         TakeEnergy(%client, 500);
         createFissurePulseOne(%obj);
      }
   }
   else if (%obj.DevastatorMode == 2) {
      if(%energy > 400) {
         TakeEnergy(%client, 400);
         %p = new (LinearFlareProjectile)() {
            dataBlock        = MicroPulse;
            initialDirection = %vector;
            initialPosition  = %mp;
            sourceObject     = %obj;
            damageFactor     = 1;
            sourceSlot       = %slot;
         };
         MissionCleanup.add(%p);
         //
         %p.ShockwaveSched = schedule(100, 0, "MicrowaveShockwaves", %obj, %p);
      }
   }
   else if (%obj.DevastatorMode == 3) {
      if(%energy > 450) {
         TakeEnergy(%client, 450);
         %p = new (LinearFlareProjectile)() {
            dataBlock        = Crisp;
            initialDirection = %vector;
            initialPosition  = %mp;
            sourceObject     = %obj;
            damageFactor     = 1;
            sourceSlot       = %slot;
         };
         MissionCleanup.add(%p);
      }
   }
   else if (%obj.DevastatorMode == 4) {
      if(%energy > 600 && $CoolDownTime[%client, "Desolation"] == 0) {
         $CoolDownTime[%client, "Desolation"] = 5;
         schedule(1000, 0, "CoolDownSpell", %client, "Desolation");
         TakeEnergy(%client, 600);
         CallArtillerySpell(%obj, "Desolation", %slot, 7, 5);
      }
   }
   else if (%obj.DevastatorMode == 5) {
      if(%energy > %maxE-5) {
         TakeEnergy(%client, %maxE-5);
         CallArtillerySpell(%obj, "SunRay", %slot, 7, 8);
      }
   }
   else {
      if(%energy > 65) {
         TakeEnergy(%client, 65);
         %p = new (LinearFlareProjectile)() {
            dataBlock        = BurnForce;
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
