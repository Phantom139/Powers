function LoadPhantomPowers() {
   %search = "scripts/Powers/Spells/Phantom/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/Phantom/"@%type@".cs");
   }
}
LoadPhantomPowers(); //load these first

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(PhantomGunImage)
{
   className = WeaponImage;
   shapeFile = "turret_muzzlepoint.dts";
   item = PhantomGun;
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

datablock ItemData(PhantomGun)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy.dts";
   image = PhantomGunImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "The Staff of a Phantom";
};

datablock ShapeBaseImageData(PhantomStaffImage)
{
   shapeFile = "weapon_targeting.dts";
   mountPoint = 1;

   offset = "0.1 0.8 0.55"; // L/R - F/B - T/B
   rotation = "2.0 -2.0 3.0 45"; // L/R - F/B - T/B
};

function CyclePhantomModes(%this, %data) {
   if (!(getSimTime() > (%this.mineModeTime + 100)))
      return;

   %this.mineModeTime = getSimTime();
   %this.PhantomMode++;
   %count = 0;
   while(%count < $Power::PowerCount["Phantom"]) {    //random number, pay it no mind
      if(%this.PhantomMode > $Power::PowerCount["Phantom"]) {
         %this.PhantomMode = 0;
      }
      %HasNext = CheckHasPower(%this.client, "Phantom", %this.PhantomMode);
      if (%HasNext) {
         %count = $Power::PowerCount["Phantom"] + 5;
         //nothing, were done
      }
      else {
         //increase the count and, loop again
         %this.PhantomMode++;
         %count++;
      }
   }
   DisplayPhantomInfo(%this);
   return;
}

function DisplayPhantomInfo(%obj) {
   switch(%obj.PhantomMode) {
   case 0:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>SHADOW STRIKE<spop>", 3, 3 );
   case 1:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>SHADOW RUSH<spop>", 3, 3 );
   case 2:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>FLICKER<spop>", 3, 3 );
   case 3:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>SHADOW BOMB<spop>", 3, 3 );
   case 4:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>FROST BITE<spop>", 3, 3 );
   case 5:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>SHADOW BOOST<spop>", 3, 3 );
   case 6:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>SHADOW BLAST<spop>", 3, 3 );
   case 7:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>HAIL<spop>", 3, 3 );
   case 8:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>SHADOW PULSE<spop>", 3, 3 );
   case 9:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>FLASHER<spop>", 3, 3 );
   case 10:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>SHADOW BOMB DROP<spop>", 3, 3 );
   case 11:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>NIGHTMARE<spop>", 3, 3 );
   case 12:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>SHADOW ARTILLERY<spop>", 3, 3 );
   case 13:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>HAIL STORM<spop>", 3, 3 );
   case 14:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>SHADOW BRIGADE<spop>", 3, 3 );
   case 15:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>AVALANCHE<spop>", 3, 3 );
   case 16:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>SHADOW STORM (Time To Pwn Those NEWBS!)<spop>", 3, 3 );
   default:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function PhantomGunImage::onMount(%this,%obj,%slot) {
     Parent::onMount(%data,%obj,%slot);
     commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
     %obj.mountImage(PhantomStaffImage, 5);
     %obj.usingPhantomStaff = true;
     DisplayPhantomInfo(%obj);
     messageClient(%obj.client, 'MsgAssist', "\c3Press [MINE] key to toggle owned powers.");
     messageClient(%obj.client, 'MsgAssist', "\c3Press [F2], Click My Page, and Then Purchase Abilities to buy powers.");
}

function PhantomGunImage::onunmount(%this,%obj,%slot) {
     Parent::onUnmount(%this, %obj, %slot);
     %obj.unmountImage(5);
     %obj.usingPhantomStaff = false;
}

function PhantomGunImage::onFire(%data,%obj,%slot){
    %client = %obj.client;
    %energy = FetchPowersEnergyLevel(%client);   // 0_o
    %HaveIt = CheckHasPower(%obj.client, "Phantom", %obj.PhantomMode);
    if(!%HaveIt && !%client.isAiControlled()) {
       BottomPrint(%client, "<spush><font:Sui Generis:14>Phantom Staff<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
       return;
    }
    %vector = %obj.getMuzzleVector(%slot);
    %mp = %obj.getMuzzlePoint(%slot);
    if (%obj.PhantomMode ==  1) {
       if(%energy > 80) {
          TakeEnergy(%client, 80);
          for(%i = 0; %i < 3; %i++) {
          %time = %i *500;
          schedule(%time, 0, "spawnprojectile","ShadowRushMissile","SeekerProjectile",%mp, %vector, %obj);
          }
       }
    }
    else if (%obj.PhantomMode == 2) {
       if(%energy > 75) {
       TakeEnergy(%client, 75);
       %p = new (LinearFlareProjectile)() {
       dataBlock        = FlickerBolt;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if (%obj.PhantomMode == 3) {
       if(%energy > 125) {
       TakeEnergy(%client, 125);
       %p = new (GrenadeProjectile)() {
       dataBlock        = ShadowBombShot;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if (%obj.PhantomMode == 4) {
       if(%energy > 80) {
       TakeEnergy(%client, 80);
       %p = new (LinearProjectile)() {
       dataBlock        = FrostbiteProjectile;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if(%obj.PhantomMode == 5) {
       if(%energy > 100) {
       TakeEnergy(%client, 100);
       FireBoosters(%data, %obj, %slot);
       }
    }
    else if (%obj.PhantomMode == 6) {
       if(%energy > 150) {
       TakeEnergy(%client, 150);
       %p = new (TracerProjectile)() {
       dataBlock        = ShadowBlastProjectile;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if(%obj.PhantomMode == 7) {
       if(%energy > 215) {
       TakeEnergy(%client, 215);
       CallArtillerySpell(%obj, "Hail", %slot, 2, 3);
       }
    }
    else if (%obj.PhantomMode == 8) {
       if(%energy > 190) {
       TakeEnergy(%client, 190);
       %p = new (EnergyProjectile)() {
       dataBlock        = ShadowPulse;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if (%obj.PhantomMode == 9) {
       if(%energy > 230) {
       TakeEnergy(%client, 230);
       %p = new (LinearFlareProjectile)() {
       dataBlock        = FlasherBolt1;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if(%obj.PhantomMode == 10) {
       if(%energy > 250) {
       TakeEnergy(%client, 250);
       CallArtillerySpell(%obj, "ShadowBombDrop", %slot, 5, 3);
       }
    }
    else if(%obj.PhantomMode == 11) {
       if(%energy > 250) {
       TakeEnergy(%client, 250);
       %p = new (LinearFlareProjectile)() {
       dataBlock        = NightmareShot;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if(%obj.PhantomMode == 12) {
       if(%energy > 300) {
       TakeEnergy(%client, 300);
       CallArtillerySpell(%obj, "ShadowArtillery", %slot, 5, 5);
       }
    }
    else if(%obj.PhantomMode == 13) {
       if(%energy > 295) {
       TakeEnergy(%client, 295);
       CallArtillerySpell(%obj, "Hailstorm", %slot, 7, 7);
       }
    }
    else if(%obj.PhantomMode == 14) {
       if(%energy > 375) {
       TakeEnergy(%client, 375);
       CallArtillerySpell(%obj, "ShadowBrigade", %slot, 7, 7);
       }
    }
    else if(%obj.PhantomMode == 15) {
       if(%energy > 400 && $CoolDownTime[%client, "Avalanche"] == 0) {
          $CoolDownTime[%client, "Avalanche"] = 15;
          schedule(1000, 0, "CoolDownSpell", %client, "Avalanche");
          TakeEnergy(%client, 400);
          CallArtillerySpell(%obj, "Avalanche", %slot, 10, 10);
          //EMP Block
          MessageClient(%obj.client, 'msgEMPBlock', "\c3Phantom: Energy Block Active... Cooling Down Energy.");
          %obj.setRechargeRate(0);
          %obj.schedule(15000, setRechargeRate, %obj.getDatablock().rechargeRate);
       }
    }
    else if(%obj.PhantomMode == 16) {
       if(%energy > 425 && $CoolDownTime[%client, "Shadow Storm"] == 0) {
          $CoolDownTime[%client, "Shadow Storm"] = 30;
          schedule(1000, 0, "CoolDownSpell", %client, "Shadow Storm");
          TakeEnergy(%client, 425);
          CallArtillerySpell(%obj, "ShadowStorm", %slot, 20, 23);
          //EMP Block
          MessageClient(%obj.client, 'msgEMPBlock', "\c3Phantom: Energy Block Active... Cooling Down Energy.");
          %obj.setRechargeRate(0);
          %obj.schedule(30000, setRechargeRate, %obj.getDatablock().rechargeRate);
       }
    }
    else {
       if(%energy > 20) {
       TakeEnergy(%client, 20);
       %p = new (LinearFlareProjectile)() {
       dataBlock        = ShadowStrikePulse;
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
