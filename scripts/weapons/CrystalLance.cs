//CrystalLance.cs
//Phantom139

//Weapon for the Prospector Class
function LoadProspectorPowers() {
   %search = "scripts/Powers/Spells/Prospector/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/Prospector/"@%type@".cs");
   }
}
LoadProspectorPowers(); //load these first

datablock AudioProfile(CrystalLanceSwitchSound)
{
   filename    = "fx/Bonuses/upward_passback1_bomb.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock ShapeBaseImageData(CrystalLanceImage)
{
   className = WeaponImage;
   shapeFile = "turret_elf_large.dts";
   item = CrystalLance;
   projectile = EnergyBolt;
   projectileType = EnergyProjectile;

   usesEnergy = true;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = CrystalLanceSwitchSound;

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

datablock ItemData(CrystalLance)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "turret_elf_large.dts";
   image = CrystalLanceImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a Crystal Lance";
};

function CycleProspectorModes(%this, %data) {
   if (!(getSimTime() > (%this.mineModeTime + 100)))
      return;
   %this.mineModeTime = getSimTime();
   %this.ProspectorMode++;
   %count = 0;
   while(%count < $Power::PowerCount["Prospector"]) {    //random number, pay it no mind
      if(%this.ProspectorMode > $Power::PowerCount["Prospector"]) {
         %this.ProspectorMode = 0;
      }
      %HasNext = CheckHasPower(%this.client, "Prospector", %this.ProspectorMode);
      if (%HasNext) {
         %count = $Power::PowerCount["Prospector"] + 5;
         //nothing, were done
      }
      else {
         //increase the count and, loop again
         %this.ProspectorMode++;
         %count++;
      }
   }
   DisplayProspectorInfo(%this);
   return;
}

function DisplayProspectorInfo(%obj) {
   switch(%obj.ProspectorMode) {
      case 0:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Crystal Lance<spop>\n<spush><font:Arial:14>DARK VOID\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 1:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Crystal Lance<spop>\n<spush><font:Arial:14>FLAMETHROWER\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 2:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Crystal Lance<spop>\n<spush><font:Arial:14>DARK HEAL<spop>", 3, 3 );
      case 3:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Crystal Lance<spop>\n<spush><font:Arial:14>HELL'S FURY\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 4:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Crystal Lance<spop>\n<spush><font:Arial:14>* SPIRIT OF POWER *\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      default:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Crystal Lance<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function CrystalLanceImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%data,%obj,%slot);
   commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
   %obj.usingCrystalLance = true;
   DisplayProspectorInfo(%obj);
}

function CrystalLanceImage::onunmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   %obj.usingCrystalLance = false;
}

function CrystalLanceImage::onFire(%data,%obj,%slot){
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);   // 0_o
   %aEnergy = %obj.getAEnergy();
   %maxE = FetchMaxEnergy(%client);
   %HaveIt = CheckHasPower(%obj.client, "Prospector", %obj.ProspectorMode);
   if(!%HaveIt && !%client.isAiControlled()) {
      BottomPrint(%client, "<spush><font:Sui Generis:14>Crystal Lance<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
      return;
   }
   %vector = %obj.getMuzzleVector(%slot);
   %mp = %obj.getMuzzlePoint(%slot);
   switch(%obj.ProspectorMode) {
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
