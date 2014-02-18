//StarforceCannon.cs
//Phantom139

//Weapon for the Gladiator Class
function LoadStarSighterPowers() {
   %search = "scripts/Powers/Spells/StarSighter/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/StarSighter/"@%type@".cs");
   }
}
LoadStarSighterPowers(); //load these first

datablock AudioProfile(StarforceCannonSwitchSound)
{
   filename    = "fx/Bonuses/down_perppass3_bunnybump.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock ShapeBaseImageData(StarforceCannonImage)
{
   className = WeaponImage;
   shapeFile = "turret_aa_large.dts";
   item = StarforceCannon;
   projectile = EnergyBolt;
   projectileType = EnergyProjectile;

   usesEnergy = true;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = StarforceCannonSwitchSound;

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

datablock ItemData(StarforceCannon)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "turret_aa_large.dts";
   image = StarforceCannonImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a Starforce Cannon";
};

function CycleStarSighterModes(%this, %data) {
   if (!(getSimTime() > (%this.mineModeTime + 100)))
      return;
   %this.mineModeTime = getSimTime();
   %this.StarSighterMode++;
   %count = 0;
   while(%count < $Power::PowerCount["Star Sighter"]) {    //random number, pay it no mind
      if(%this.StarSighterMode > $Power::PowerCount["Star Sighter"]) {
         %this.StarSighterMode = 0;
      }
      %HasNext = CheckHasPower(%this.client, "Star Sighter", %this.StarSighterMode);
      if (%HasNext) {
         %count = $Power::PowerCount["Star Sighter"] + 5;
         //nothing, were done
      }
      else {
         //increase the count and, loop again
         %this.StarSighterMode++;
         %count++;
      }
   }
   DisplayStarSighterInfo(%this);
   return;
}

function DisplayStarSighterInfo(%obj) {
   switch(%obj.StarSighterMode) {
      case 0:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Starforce Cannon<spop>\n<spush><font:Arial:14>CHARGE THE LIGHT\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 1:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Starforce Cannon<spop>\n<spush><font:Arial:14>CATALYST EXPLOSIVE\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 2:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Starforce Cannon<spop>\n<spush><font:Arial:14>SHARD RAIN\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 3:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Starforce Cannon<spop>\n<spush><font:Arial:14>SWIFT VENGEANCE\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 4:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Starforce Cannon<spop>\n<spush><font:Arial:14>STAR LIGHT\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 5:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Starforce Cannon<spop>\n<spush><font:Arial:14>* RIFT *\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      default:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Starforce Cannon<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function StarforceCannonImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%data,%obj,%slot);
   commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
   %obj.usingStarforceCannon = true;
   DisplayStarSighterInfo(%obj);
}

function StarforceCannonImage::onunmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   %obj.usingStarforceCannon = false;
}

function StarforceCannonImage::onFire(%data,%obj,%slot){
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);   // 0_o
   %aEnergy = %obj.getAEnergy();
   %maxE = FetchMaxEnergy(%client);
   %HaveIt = CheckHasPower(%obj.client, "Star Sighter", %obj.StarSighterMode);
   if(!%HaveIt && !%client.isAiControlled()) {
      BottomPrint(%client, "<spush><font:Sui Generis:14>Starforce Cannon<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
      return;
   }
   %vector = %obj.getMuzzleVector(%slot);
   %mp = %obj.getMuzzlePoint(%slot);
   switch(%obj.StarSighterMode) {
      case 0:
         if(%energy > (%maxE - 5)) {
            if(%aEnergy < 100) {
               initAffinityCharge(%obj);
            }
         }
      case 1:

      case 2:

      case 3:

      case 4:

      default:
         //do nothing
   }
}
