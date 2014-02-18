//VolcanicCannon.cs
//Phantom139

//Weapon for the Annihilator Class
function LoadAnnihilatorPowers() {
   %search = "scripts/Powers/Spells/Annihilator/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/Annihilator/"@%type@".cs");
   }
}
LoadAnnihilatorPowers(); //load these first

datablock AudioProfile(VolcanicCannonSwitchSound)
{
   filename    = "fx/Bonuses/upward_perppass3_juggletoss.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock ShapeBaseImageData(VolcanicCannonImage)
{
   className = WeaponImage;
   shapeFile = "weapon_mortar.dts";
   item = VolcanicCannon;
   projectile = EnergyBolt;
   projectileType = EnergyProjectile;

   usesEnergy = true;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = VolcanicCannonSwitchSound;

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

datablock ItemData(VolcanicCannon)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_mortar.dts";
   image = VolcanicCannonImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a Volcanic Cannon";
};

function CycleAnnihilatorModes(%this, %data) {
   if (!(getSimTime() > (%this.mineModeTime + 100)))
      return;
   %this.mineModeTime = getSimTime();
   %this.AnnihilatorMode++;
   %count = 0;
   while(%count < $Power::PowerCount["Annihilator"]) {    //random number, pay it no mind
      if(%this.AnnihilatorMode > $Power::PowerCount["Annihilator"]) {
         %this.AnnihilatorMode = 0;
      }
      %HasNext = CheckHasPower(%this.client, "Annihilator", %this.AnnihilatorMode);
      if (%HasNext) {
         %count = $Power::PowerCount["Annihilator"] + 5;
         //nothing, were done
      }
      else {
         //increase the count and, loop again
         %this.AnnihilatorMode++;
         %count++;
      }
   }
   DisplayAnnihilatorInfo(%this);
   return;
}

function DisplayAnnihilatorInfo(%obj) {
   switch(%obj.AnnihilatorMode) {
      case 0:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Volcanic Cannon<spop>\n<spush><font:Arial:14>DARK VOID\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 1:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Volcanic Cannon<spop>\n<spush><font:Arial:14>ENERGY BALL LV 4\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 2:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Volcanic Cannon<spop>\n<spush><font:Arial:14>UNBEARABLE FIRESTORM\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 3:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Volcanic Cannon<spop>\n<spush><font:Arial:14>SUN STORM\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 4:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Volcanic Cannon<spop>\n<spush><font:Arial:14>* DRAGON'S REVENGE *\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      default:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Volcanic Cannon<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function VolcanicCannonImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%data,%obj,%slot);
   commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
   %obj.usingVolcanicCannon = true;
   DisplayAnnihilatorInfo(%obj);
}

function VolcanicCannonImage::onunmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   %obj.usingVolcanicCannon = false;
}

function VolcanicCannonImage::onFire(%data,%obj,%slot){
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);   // 0_o
   %aEnergy = %obj.getAEnergy();
   %maxE = FetchMaxEnergy(%client);
   %HaveIt = CheckHasPower(%obj.client, "Annihilator", %obj.AnnihilatorMode);
   if(!%HaveIt && !%client.isAiControlled()) {
      BottomPrint(%client, "<spush><font:Sui Generis:14>Volcanic Cannon<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
      return;
   }
   %vector = %obj.getMuzzleVector(%slot);
   %mp = %obj.getMuzzlePoint(%slot);
   switch(%obj.AnnihilatorMode) {
      case 0:
         if(%energy > (%maxE - 5)) {
            if(%aEnergy < 100) {
               initAffinityCharge(%obj);
            }
         }
      case 1:

      case 2:

      case 3:
         if(%aEnergy > 80) {
            %obj.takeAEnergy(80);
            CallArtillerySpell(%obj, "SunStorm", %slot, 15, 8);
         }
      case 4:

      default:
         //do nothing
   }
}
