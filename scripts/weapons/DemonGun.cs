function LoadDemonPowers() {
   %search = "scripts/Powers/Spells/Demon/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/Demon/"@%type@".cs");
   }
}
LoadDemonPowers(); //load these first

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(DemonWeaponImage)
{
   className = WeaponImage;
   shapeFile = "turret_muzzlepoint.dts";
   item = DemonWeapon;
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

datablock ItemData(DemonWeapon)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy.dts";
   image = DemonWeaponImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a demon's bane";
};

function CycleDemonModes(%this, %data) {
if (!(getSimTime() > (%this.mineModeTime + 100)))
return;
%this.mineModeTime = getSimTime();
%this.DemonMode++;
%count = 0;
while(%count < $Power::PowerCount["Demon"]) {    //random number, pay it no mind
      if(%this.DemonMode > $Power::PowerCount["Demon"]) {
      %this.DemonMode = 0;
      }
   %HasNext = CheckHasPower(%this.client, "Demon", %this.DemonMode);
   if (%HasNext) {
   %count = $Power::PowerCount["Demon"] + 5;
   //nothing, were done
   }
   else {
   //increase the count and, loop again
   %this.DemonMode++;
   %count++;
   }
}
DisplayDemonInfo(%this);
return;
}

function DisplayDemonInfo(%obj) {
   switch(%obj.DemonMode) {
   case 0:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>FLAME SHOT<spop>", 3, 3 );
   case 1:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>FIRE BALL LEVEL 1<spop>", 3, 3 );
   case 2:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>ENERGY BALL LEVEL 1<spop>", 3, 3 );
   case 3:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>LOW LEVEL SHOCK<spop>", 3, 3 );
   case 4:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>LOW LEVEL DRAIN<spop>", 3, 3 );
   case 5:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>FIRE BALL LEVEL 2<spop>", 3, 3 );
   case 6:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>PARALYZE FOE<spop>", 3, 3 );
   case 7:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>ENERGY BALL LEVEL 2<spop>", 3, 3 );
   case 8:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>ELECTRIC STRIKE<spop>", 3, 3 );
   case 9:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>VAMPIRE DRAINER<spop>", 3, 3 );
   case 10:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>FIRE BALL LEVEL 3<spop>", 3, 3 );
   case 11:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>ENERGY BALL LEVEL 3<spop>", 3, 3 );
   case 12:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>THUNDER STRIKE<spop>", 3, 3 );
   case 13:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>SPLIT FIRE BOMB<spop>", 3, 3 );
   case 14:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>OH-MEG-AH ENERGY DRAIN<spop>", 3, 3 );
   case 15:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>FIRE BALL LEVEL 4<spop>", 3, 3 );
   case 16:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>LIGHTNING STRIKE<spop>", 3, 3 );
   default:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function DemonWeaponImage::onMount(%this,%obj,%slot) {
     Parent::onMount(%data,%obj,%slot);
     commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
     DisplayDemonInfo(%obj);
     %obj.usingDemonWeapon = true;
     messageClient(%obj.client, 'MsgAssist', "\c3Press [MINE] key to toggle owned powers.");
     messageClient(%obj.client, 'MsgAssist', "\c3Press [F2], Click My Page, and Then Purchase Abilities to buy powers.");
}

function DemonWeaponImage::onunmount(%this,%obj,%slot) {
     Parent::onUnmount(%this, %obj, %slot);
     %obj.usingDemonWeapon = false;
}

function DemonWeaponImage::onFire(%data,%obj,%slot){
    %client = %obj.client;
    %energy = FetchPowersEnergyLevel(%client);
    %HaveIt = CheckHasPower(%obj.client, "Demon", %obj.DemonMode);
    if(!%HaveIt && !%client.isAiControlled()) {
       BottomPrint(%client, "<spush><font:Sui Generis:14>Demon<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
       return;
    }
    %vector = %obj.getMuzzleVector(%slot);
    %mp = %obj.getMuzzlePoint(%slot);
    if (%obj.DemonMode ==  1) {
       if(%energy > 50) {
       TakeEnergy(%client, 50);
       %p = new (LinearProjectile)() {
       dataBlock        = FireballShot;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if (%obj.DemonMode ==  2) {
       if(%energy > 75) {
       TakeEnergy(%client, 75);
       %p = new (LinearFlareProjectile)() {
       dataBlock        = EballBolt;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if(%obj.DemonMode ==  3) {
       if(%energy > 50) {
       TakeEnergy(%client, 50);
       onFireShock(BasicShockSpell, %obj, %slot);
       }
    }
    else if(%obj.DemonMode ==  4) {
       if(%energy > 75) {
       TakeEnergy(%client, 75);
       onFireShock(BasicDrainSpell, %obj, %slot);
       }
    }
    else if (%obj.DemonMode ==  5) {
       if(%energy > 90) {
       TakeEnergy(%client, 90);
       %p = new (LinearProjectile)() {
       dataBlock        = FireballShotLV2;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if(%obj.DemonMode ==  6) {
       if(%energy > 90) {
       TakeEnergy(%client, 90);
       onFireShock(ParalyzeShocker, %obj, %slot);
       }
    }
    else if(%obj.DemonMode ==  7) {
       if(%energy > 95) {
       TakeEnergy(%client, 95);
       %p = new (LinearFlareProjectile)() {
       dataBlock        = EBallBoltLV2;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if(%obj.DemonMode ==  8) {
       if(%energy > 150) {
       TakeEnergy(%client, 150);
       onFireShock(ModShockSpell, %obj, %slot);
       }
    }
    else if(%obj.DemonMode ==  9) {
       if(%energy > 175) {
       TakeEnergy(%client, 175);
       onFireShock(EnergyVamp, %obj, %slot);
       }
    }
    else if (%obj.DemonMode ==  10) {
       if(%energy > 190) {
       TakeEnergy(%client, 190);
       %p = new (LinearProjectile)() {
       dataBlock        = FireballShotLV3;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if(%obj.DemonMode ==  11) {
       if(%energy > 215) {
       TakeEnergy(%client, 215);
       %p = new (LinearFlareProjectile)() {
       dataBlock        = EBallBoltLV3;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if(%obj.DemonMode ==  12) {
       if(%energy > 200) {
       TakeEnergy(%client, 200);
       onFireShock(HeavyShockSpell, %obj, %slot);
       }
    }
    else if (%obj.DemonMode ==  13) {
       if(%energy > 275) {
       TakeEnergy(%client, 275);
       %p = new (LinearProjectile)() {
       dataBlock        = NapalmShot;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if(%obj.DemonMode ==  14) {
       if(%energy > 250) {
       TakeEnergy(%client, 250);
       onFireShock(EnergyGod, %obj, %slot);
       }
    }
    else if (%obj.DemonMode ==  15) {
       if(%energy > 300) {
       TakeEnergy(%client, 300);
       %p = new (LinearProjectile)() {
       dataBlock        = FireballShotLV4;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if(%obj.DemonMode == 16) {
       if(%energy > 400 && $CoolDownTime[%client, "Lightning"] == 0) {
          $CoolDownTime[%client, "Lightning"] = 15;
          schedule(1000, 0, "CoolDownSpell", %client, "Lightning");
          TakeEnergy(%client, 400);
          CallArtillerySpell(%obj, "Lightning", %slot, 15, 5);
          //EMP Block
          MessageClient(%obj.client, 'msgEMPBlock', "\c3Demon: Energy Block Active... Cooling Down Energy.");
          %obj.setRechargeRate(0);
          %obj.schedule(15000, setRechargeRate, %obj.getDatablock().rechargeRate);
       }
    }
    else {
       if(%energy > 20) {
       TakeEnergy(%client, 20);
       %p = new (LinearFlareProjectile)() {
       dataBlock        = FireBolt;
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
