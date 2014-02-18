//OverseerSword.cs
//Phantom139

//Weapon for the Overseer Class
function LoadOverseerPowers() {
   %search = "scripts/Powers/Spells/Overseer/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/Overseer/"@%type@".cs");
   }
}
LoadOverseerPowers(); //load these first

datablock ShapeBaseImageData(OverseerSwordImage)
{
   className = WeaponImage;
   shapeFile = "weapon_shocklance.dts";
   item = OverseerSword;
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

datablock ItemData(OverseerSword)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_shocklance.dts";
   image = OverseerSwordImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a Overseer Sword";
};

function CycleOverseerModes(%this, %data) {
   if (!(getSimTime() > (%this.mineModeTime + 100)))
      return;
   %this.mineModeTime = getSimTime();
   %this.OverseerMode++;
   %count = 0;
   while(%count < $Power::PowerCount["Overseer"]) {    //random number, pay it no mind
      if(%this.OverseerMode > $Power::PowerCount["Overseer"]) {
         %this.OverseerMode = 0;
      }
      %HasNext = CheckHasPower(%this.client, "Overseer", %this.OverseerMode);
      if (%HasNext) {
         %count = $Power::PowerCount["Overseer"] + 5;
         //nothing, were done
      }
      else {
         //increase the count and, loop again
         %this.OverseerMode++;
         %count++;
      }
   }
   DisplayOverseerInfo(%this);
   return;
}

function DisplayOverseerInfo(%obj) {
   switch(%obj.OverseerMode) {
      case 0:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Overseer Sword<spop>\n<spush><font:Arial:14>SHADOW SWIPE<spop>", 3, 3 );
      case 1:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Overseer Sword<spop>\n<spush><font:Arial:14>STAB RUSH<spop>", 3, 3 );
      case 2:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Overseer Sword<spop>\n<spush><font:Arial:14>BARRIER FIELD<spop>", 3, 3 );
      case 3:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Overseer Sword<spop>\n<spush><font:Arial:14>MARVOLIC LIGHTNING<spop>", 3, 3 );
      case 4:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Overseer Sword<spop>\n<spush><font:Arial:14>SHADOW HEAL<spop>", 3, 3 );
      case 5:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Overseer Sword<spop>\n<spush><font:Arial:14>ASTEROID FALL<spop>", 3, 3 );
      default:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Overseer Sword<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function OverseerSwordImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%data,%obj,%slot);
   commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
   %obj.usingOverseerSword = true;
   DisplayOverseerInfo(%obj);
}

function OverseerSwordImage::onunmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   if(%obj.CastingContinuous) {
      %obj.setMoveState(false);
      %obj.CastingContinuous = 0; //stop any of these
   }
   %obj.usingOverseerSword = false;
}

function OverseerSwordImage::onFire(%data,%obj,%slot){
   if(%obj.CastingContinuous) {
      %obj.CastingContinuous = 0;
      %obj.setMoveState(false);
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Overseer Sword<spop>\n<spush><font:Arial:14>Continuous Casting Terminated.<spop>", 3, 3 );
      return;
   }
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);   // 0_o
   %maxE = FetchMaxEnergy(%client);
   %HaveIt = CheckHasPower(%obj.client, "Overseer", %obj.OverseerMode);
   if(!%HaveIt && !%client.isAiControlled()) {
      BottomPrint(%client, "<spush><font:Sui Generis:14>Overseer Sword<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
      return;
   }
   %vector = %obj.getMuzzleVector(%slot);
   %mp = %obj.getMuzzlePoint(%slot);
   if (%obj.OverseerMode == 1) {
      if(%energy > 10) {
         TakeEnergy(%client, 10);
         %p = new (TracerProjectile)() {
            dataBlock        = StabRush;
            initialDirection = %vector;
            initialPosition  = %mp;
            sourceObject     = %obj;
            damageFactor     = 1;
            sourceSlot       = %slot;
         };
         MissionCleanup.add(%p);
      }
   }
   else if (%obj.OverseerMode == 2) {
      if(%energy > 300) {        //We require 50 more energy to cast at least 2 more
         TakeEnergy(%client, 250);
         %obj.CastingContinuous = 1;
         %obj.setMoveState(true); //no movement please...
         BarrierFieldLoop(%obj);
      }
   }
   else if (%obj.OverseerMode == 3) {
       if(%energy > 450) {
          TakeEnergy(%client, 450);
          onFireShock(AvadusLightningShock, %obj, %slot);
      }
   }
   else if (%obj.OverseerMode == 4) {
       if(%energy > 450) {
          TakeEnergy(%client, 450);
          %obj.applyRepair(1.8);
       }
   }
   else if (%obj.OverseerMode == 5) {
       if(%energy > %maxE-5) {
          TakeEnergy(%client, %maxE-5);
          CallArtillerySpell(%obj, "AsteroidFall", %slot, 30, 10);
       }
   }
   else {
      if(%energy > 300) {
         TakeEnergy(%client, 300);
         FireShadowSwipe(%data, %obj, %slot);
      }
   }
}
