function LoadWitchPowers() {
   %search = "scripts/Powers/Spells/Witch/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/Witch/"@%type@".cs");
   }
}
LoadWitchPowers(); //load these first

//--------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(WitchWeaponImage)
{
   className = WeaponImage;
   shapeFile = "turret_muzzlepoint.dts";
   item = WitchWeapon;
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

datablock ItemData(WitchWeapon)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy.dts";
   image = WitchWeaponImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a witch's potion (lol)";
};

function CycleWitchModes(%this, %data) {
if (!(getSimTime() > (%this.mineModeTime + 100)))
return;
%this.mineModeTime = getSimTime();
%this.WitchMode++;
%count = 0;
while(%count < $Power::PowerCount["Witch"]) {    //random number, pay it no mind
      if(%this.WitchMode > $Power::PowerCount["Witch"]) {
      %this.WitchMode = 0;
      }
   %HasNext = CheckHasPower(%this.client, "Witch", %this.WitchMode);
   if (%HasNext) {
   %count = $Power::PowerCount["Witch"] + 5;
   //nothing, were done
   }
   else {
   //increase the count and, loop again
   %this.WitchMode++;
   %count++;
   }
}
DisplayWitchInfo(%this);
return;
}

function DisplayWitchInfo(%obj) {
   switch(%obj.WitchMode) {
   case 0:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>LIGHT STRIKE<spop>", 3, 3 );
   case 1:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>LOW LEVEL CAMO<spop>", 3, 3 );
   case 2:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>DISPELL WEAPON<spop>", 3, 3 );
   case 3:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>LOW LEVEL HEAL<spop>", 3, 3 );
   case 4:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>BASIC SHIELDING<spop>", 3, 3 );
   case 5:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>REPEL FORCE<spop>", 3, 3 );
   case 6:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>SHIFT<spop>", 3, 3 );
   case 7:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>LOW LEVEL BLAST<spop>", 3, 3 );
   case 8:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>MID LEVEL HEAL<spop>", 3, 3 );
   case 9:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>MID LEVEL CAMO<spop>", 3, 3 );
   case 10:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>MODERATE SHIELDING<spop>", 3, 3 );
   case 11:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>STUN ENEMY<spop>", 3, 3 );
   case 12:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>VAPOR EXPLOSION<spop>", 3, 3 );
   case 13:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>HIGH LEVEL CAMO<spop>", 3, 3 );
   case 14:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>EXTREME SHIELDING<spop>", 3, 3 );
   case 15:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>TEAM HEALER<spop>", 3, 3 );
   case 16:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>VAPORIZE<spop>", 3, 3 );
   case 17:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>TO THE HEAVENS<spop>", 3, 3 );
   default:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function WitchWeaponImage::onMount(%this,%obj,%slot) {
     Parent::onMount(%data,%obj,%slot);
     commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
     %obj.usingWitchWeapon = true;
     DisplayWitchInfo(%obj);
     messageClient(%obj.client, 'MsgAssist', "\c3Press [MINE] key to toggle owned powers.");
     messageClient(%obj.client, 'MsgAssist', "\c3Press [F2], Click My Page, and Then Purchase Abilities to buy powers.");
}

function WitchWeaponImage::onunmount(%this,%obj,%slot) {
     Parent::onUnmount(%this, %obj, %slot);
     %obj.usingWitchWeapon = false;
}

function WitchUncloak(%obj) {
   %obj.setCloaked(false);
}

function WitchWeaponImage::onFire(%data,%obj,%slot){
    %client = %obj.client;
    %energy = FetchPowersEnergyLevel(%client);
    %maxE = FetchMaxEnergy(%client);
    %HaveIt = CheckHasPower(%obj.client, "Witch", %obj.WitchMode);
    if(!%HaveIt && !%client.isAiControlled()) {
       BottomPrint(%client, "<spush><font:Sui Generis:14>Witch<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
       return;
    }
    %vector = %obj.getMuzzleVector(%slot);
    %mp = %obj.getMuzzlePoint(%slot);
    if (%obj.WitchMode ==  1) {     // 5 sec camo
       if(%energy > 30) {
       TakeEnergy(%client, 30);
       %obj.setCloaked(true);
       schedule(5000,0,"WitchUncloak",%obj);
       }
    }
    else if(%obj.WitchMode == 2) {
       if(%energy > 60) {
       TakeEnergy(%client, 60);
       %p = new (LinearFlareProjectile)() {
       dataBlock        = DisarmPulse;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if (%obj.WitchMode == 3) {     //low heal
       if(%energy > 60) {
       TakeEnergy(%client, 60);
       %obj.applyRepair(0.5);
       }
    }
    else if (%obj.WitchMode == 4) {     //basic shield (3 hits)
       if(%energy > %maxE-5) {
       TakeEnergy(%client, %maxE-5);
       %obj.SpellShieldTicks = 3;
       }
    }
    else if(%obj.WitchMode == 5) {
       if(%energy > 75) {
       TakeEnergy(%client, 75);
       %p = new (LinearFlareProjectile)() {
       dataBlock        = RepelPulse;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if (%obj.WitchMode == 6) {
       if(%energy > %maxE-35) {
       TakeEnergy(%client, %maxE-35);
       Teleport(%obj, %slot);
       }
    }
    else if (%obj.WitchMode == 7) {
       if(%energy > 90) {
       TakeEnergy(%client, 90);
       %p = new (TracerProjectile)() {
       dataBlock        = BasicExploProjectile;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if (%obj.WitchMode == 8) {     //med heal
       if(%energy > 100) {
       TakeEnergy(%client, 100);
       %obj.applyRepair(1.2);
       }
    }
    else if (%obj.WitchMode == 9) {     // 10 sec camo
       if(%energy > 75) {
       TakeEnergy(%client, 75);
       %obj.setCloaked(true);
       schedule(10000,0,"WitchUncloak",%obj);
       }
    }
    else if (%obj.WitchMode == 10) {     //moderate shield (5 hits)
       if(%energy > %maxE-5) {
       TakeEnergy(%client, %maxE-5);
       %obj.SpellShieldTicks = 5;
       }
    }
    else if(%obj.WitchMode ==  11) {
       if(%energy > 150) {
       TakeEnergy(%client, 150);
       onFireShock(StunShocker, %obj, %slot);
       }
    }
    else if (%obj.WitchMode == 12) {
       if(%energy > 170) {
       TakeEnergy(%client, 170);
       %p = new (TracerProjectile)() {
       dataBlock        = VapExploProjectile;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if (%obj.WitchMode == 13) {     // 15 sec camo
       if(%energy > 150) {
       TakeEnergy(%client, 150);
       %obj.setCloaked(true);
       schedule(15000,0,"WitchUncloak",%obj);
       }
    }
    else if (%obj.WitchMode == 14) {
       if(%energy > %maxE-5 && $CoolDownTime[%client, "TeamShield"] == 0) {
          $CoolDownTime[%client, "TeamShield"] = 30;
          schedule(1000, 0, "CoolDownSpell", %client, "TeamShield");
          TakeEnergy(%client, %maxE-5);
          DoFriendlyMassAreaSpell(%obj, %obj.getPosition(), "Shielder", 20);
          //EMP Block
          MessageClient(%obj.client, 'msgEMPBlock', "\c3Witch: Energy Block Active... Cooling Down Energy.");
          %obj.setRechargeRate(0);
          %obj.schedule(25000, setRechargeRate, %obj.getDatablock().rechargeRate);
       }
    }
    else if (%obj.WitchMode == 15) {
       if(%energy > %maxE-5 && $CoolDownTime[%client, "TeamHeal"] == 0) {
          $CoolDownTime[%client, "TeamHeal"] = 30;
          schedule(1000, 0, "CoolDownSpell", %client, "TeamHeal");
          TakeEnergy(%client, %maxE-5);
          DoFriendlyMassAreaSpell(%obj, %obj.getPosition(), "Healer", 20);
          //EMP Block
          MessageClient(%obj.client, 'msgEMPBlock', "\c3Witch: Energy Block Active... Cooling Down Energy.");
          %obj.setRechargeRate(0);
          %obj.schedule(25000, setRechargeRate, %obj.getDatablock().rechargeRate);
       }
    }
    else if (%obj.WitchMode == 16) {
       if(%energy > 270) {
       TakeEnergy(%client, 270);
       %p = new (TracerProjectile)() {
       dataBlock        = VaporizeProjectile;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
       };
       MissionCleanup.add(%p);
       }
    }
    else if(%obj.WitchMode == 17) {
       if(%energy > 350) {
       TakeEnergy(%client, 350);
       %p = new (LinearFlareProjectile)() {
       dataBlock        = UpwardRepelPulse;
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
       if(%energy > 20) {
       TakeEnergy(%client, 20);
       %p = new (LinearFlareProjectile)() {
       dataBlock        = LightStrikeProj;
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
