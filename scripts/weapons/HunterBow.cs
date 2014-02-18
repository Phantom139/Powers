function LoadHunterPowers() {
   %search = "scripts/Powers/Spells/Hunter/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/Hunter/"@%type@".cs");
   }
}
LoadHunterPowers(); //load these first

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ItemData(HunterBow) {
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_targeting.dts";
   image = HunterBowImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a HunterBow";
};

datablock ShapeBaseImageData(HunterBowImage) {
   className = WeaponImage;
   shapeFile = "weapon_targeting.dts";
   item = HunterBow;
   offset = "0 0 0";

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

function HunterBowImage::onMount(%this, %obj, %slot, %client) {
   Parent::onMount(%this, %obj, %slot);
   commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
   %obj.mountImage(HunterBow2Image, 4);
   %obj.mountImage(HunterBow3Image, 5);
   %obj.mountImage(HunterBow4Image, 6);

   %obj.usingHunterBow = true;
   DisplayHunterInfo(%obj);
   messageClient(%obj.client, 'MsgAssist', "\c3Press [MINE] key to toggle owned powers.");
   messageClient(%obj.client, 'MsgAssist', "\c3Press [F2], Click My Page, and Then Purchase Abilities to buy powers.");
}

datablock ShapeBaseImageData(HunterBow2Image) : HunterBowImage {
   offset = "0 0.6 0.9";
   rotation = "0 0 1 90";
   rotation = "1 0 0 90";
   shapeFile = "weapon_shocklance.dts";
};

datablock ShapeBaseImageData(HunterBow3Image) : HunterBowImage {
   offset = "0 0.89 -0.7";
   rotation = "0 0 1 -90";
   rotation = "1 0 0 -90";
   shapeFile = "weapon_shocklance.dts";
};

datablock ShapeBaseImageData(HunterBow4Image) : HunterBowImage {
   offset = "0 0.9 0.2";
   rotation = "0 0 0 0";
   shapeFile = "weapon_missile_projectile.dts";
};

// Unmount The Images
function HunterBowImage::onUnmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   %obj.unmountImage(4);
   %obj.unmountImage(5);
   %obj.unmountImage(6);
   
   %obj.usingHunterBow = false;
}

//-----------------------------------------------------------------------------
function CycleHunterModes(%this, %data) {
   if (!(getSimTime() > (%this.mineModeTime + 100)))
      return;

   %this.mineModeTime = getSimTime();
   %this.HunterMode++;
   %count = 0;
   while(%count < $Power::PowerCount["Hunter"]) {    //random number, pay it no mind
      if(%this.HunterMode > $Power::PowerCount["Hunter"]) {
         %this.HunterMode = 0;
      }
      %HasNext = CheckHasPower(%this.client, "Hunter", %this.HunterMode);
      if (%HasNext) {
         %count = $Power::PowerCount["Hunter"] + 5;
         //nothing, were done
      }
      else {
         //increase the count and, loop again
         %this.HunterMode++;
         %count++;
      }
   }
   DisplayHunterInfo(%this);
   return;
}

function DisplayHunterInfo(%obj) {
   switch(%obj.HunterMode) {
   case 0:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>MANA ARROW<spop>", 3, 3 );
   case 1:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>SMACKBACK<spop>", 3, 3 );
   case 2:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>CONCUSSION BLOW<spop>", 3, 3 );
   case 3:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>[D] QUAKE TRAP<spop>", 3, 3 );
   case 4:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>[D] NET TRAP<spop>", 3, 3 );
   case 5:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>ACID ARROW<spop>", 3, 3 );
   case 6:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>[D] BLAST TRAP<spop>", 3, 3 );
   case 7:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>[D] TOXIC TRAP<spop>", 3, 3 );
   case 8:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>[D] FREEZE TRAP<spop>", 3, 3 );
   case 9:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>[D] BURN TRAP<spop>", 3, 3 );
   case 10:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>SPEAR SHOWER<spop>", 3, 3 );
   case 11:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>AMBUSH<spop>", 3, 3 );
   case 12:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>MULTISHOT<spop>", 3, 3 );
   case 13:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>SHINE SHOT<spop>", 3, 3 );
   case 14:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>COMPOSITE<spop>", 3, 3 );
   case 15:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>[D] BURST TRAP<spop>", 3, 3 );
   case 16:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>STRIKE DOWN<spop>", 3, 3 );
   default:
      BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function HunterBowImage::onFire(%data, %obj, %slot){
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);   // 0_o
   %HaveIt = CheckHasPower(%obj.client, "Hunter", %obj.HunterMode);
   if(!%HaveIt && !%client.isAiControlled()) {
      BottomPrint(%client, "<spush><font:Sui Generis:14>Hunter Bow<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
      return;
   }
   %vector = %obj.getMuzzleVector(%slot);
   %mp = %obj.getMuzzlePoint(%slot);
   //
   switch(%obj.HunterMode) {
      //Mana Arrow
      case 0:
         if(%energy > 50) {
            TakeEnergy(%client, 50);
            %p = new (LinearFlareProjectile)() {
               dataBlock        = ManaArrow;
               initialDirection = %vector;
               initialPosition  = %mp;
               sourceObject     = %obj;
               damageFactor     = 1;
               sourceSlot       = %slot;
            };
            MissionCleanup.add(%p);
         }
      //Smackback
      case 1:
         if(%energy > 75) {
            TakeEnergy(%client, 75);
            %p = new (EnergyProjectile)() {
               dataBlock        = SmackbackProjectile;
               initialDirection = %vector;
               initialPosition  = %mp;
               sourceObject     = %obj;
               damageFactor     = 1;
               sourceSlot       = %slot;
            };
            MissionCleanup.add(%p);
         }
      //Concussion Blow
      case 2:
         if(%energy > 75) {
            TakeEnergy(%client, 75);
            %p = new (LinearProjectile)() {
               dataBlock        = ConcussionBlowProjectile;
               initialDirection = %vector;
               initialPosition  = %mp;
               sourceObject     = %obj;
               damageFactor     = 1;
               sourceSlot       = %slot;
            };
            MissionCleanup.add(%p);
         }
      //Quake Trap
      case 3:
         if(%energy > 125) {
            TakeEnergy(%client, 125);
            LaunchHunterTrap(%obj, "Quake", %mp, %vector);
         }
      //Net Trap
      case 4:
         if(%energy > 125) {
            TakeEnergy(%client, 125);
            LaunchHunterTrap(%obj, "Net", %mp, %vector);
         }
      //Acid Arrow
      case 5:
         if(%energy > 175) {
            TakeEnergy(%client, 175);
            %p = new (LinearFlareProjectile)() {
               dataBlock        = AcidArrow;
               initialDirection = %vector;
               initialPosition  = %mp;
               sourceObject     = %obj;
               damageFactor     = 1;
               sourceSlot       = %slot;
            };
            MissionCleanup.add(%p);
         }
      //Blast Trap
      case 6:
         if(%energy > 200) {
            TakeEnergy(%client, 200);
            LaunchHunterTrap(%obj, "Blast", %mp, %vector);
         }
      //Toxic Trap
      case 7:
         if(%energy > 215) {
            TakeEnergy(%client, 215);
            LaunchHunterTrap(%obj, "Toxic", %mp, %vector);
         }
      //Freeze Trap
      case 8:
         if(%energy > 215) {
            TakeEnergy(%client, 215);
            LaunchHunterTrap(%obj, "Freeze", %mp, %vector);
         }
      //Burn Trap
      case 9:
         if(%energy > 215) {
            TakeEnergy(%client, 215);
            LaunchHunterTrap(%obj, "Burn", %mp, %vector);
         }
      //Spear Shower
      case 10:
         if(%energy > 275) {
            TakeEnergy(%client, 275);
            CallArtillerySpell(%obj, "SpearShower", %slot, 6, 5);
         }
      //Ambush
      case 11:
         if(%energy > 250) {
            TakeEnergy(%client, 250);
            %obj.setCloaked(true);
            schedule(15000, 0, "WitchUncloak", %obj);
         }
      //Multishot
      case 12:
         if(%energy > 400) {
            TakeEnergy(%client, 400);
            %obj.setMoveState(true);
            %obj.schedule(2500, setMoveState, false);
            FireMultishot(%obj);
         }
      //Shine Shot
      case 13:
         if(%energy > 350) {
            TakeEnergy(%client, 350);
            %p = new (LinearFlareProjectile)() {
               dataBlock        = ShineShot;
               initialDirection = %vector;
               initialPosition  = %mp;
               sourceObject     = %obj;
               damageFactor     = 1;
               sourceSlot       = %slot;
            };
            MissionCleanup.add(%p);
         }
      //Composite
      case 14:
         if(%energy > 430) {
            TakeEnergy(%client, 430);
            %p = new (LinearProjectile)() {
               dataBlock        = CompositeProjectile;
               initialDirection = %vector;
               initialPosition  = %mp;
               sourceObject     = %obj;
               damageFactor     = 1;
               sourceSlot       = %slot;
            };
            MissionCleanup.add(%p);
         }
      //Burst Trap
      case 15:
         if(%energy > 400) {
            TakeEnergy(%client, 400);
            LaunchHunterTrap(%obj, "Burst", %mp, %vector);
         }
      //Strike Down
      case 16:
         if(%energy > 475 && $CoolDownTime[%client, "StrikeDown"] == 0) {
            $CoolDownTime[%client, "StrikeDown"] = 30;
            schedule(1000, 0, "CoolDownSpell", %client, "StrikeDown");
            TakeEnergy(%client, 475);
            %p = new (LinearFlareProjectile)() {
               dataBlock        = StrikeDownArrow;
               initialDirection = %vector;
               initialPosition  = %mp;
               sourceObject     = %obj;
               damageFactor     = 1;
               sourceSlot       = %slot;
            };
            MissionCleanup.add(%p);
            //EMP Block
            MessageClient(%obj.client, 'msgEMPBlock', "\c3Hunter: Energy Block Active... Cooling Down Energy.");
            %obj.setRechargeRate(0);
            %obj.schedule(30000, setRechargeRate, %obj.getDatablock().rechargeRate);
         }
      default:
   }
}
