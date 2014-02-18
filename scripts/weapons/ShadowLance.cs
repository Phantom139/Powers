//ShadowLance.cs
//Phantom139

//Weapon for the PhantomLord Class
function LoadPhantomLordPowers() {
   %search = "scripts/Powers/Spells/PhantomLord/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/PhantomLord/"@%type@".cs");
   }
}
LoadPhantomLordPowers(); //load these first

datablock AudioProfile(ShadowLanceSwitchSound)
{
   filename    = "fx/Bonuses/Nouns/special2.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock ShapeBaseImageData(ShadowLanceImage)
{
   className = WeaponImage;
   shapeFile = "turret_mortar_large.dts";
   item = ShadowLance;
   projectile = EnergyBolt;
   projectileType = EnergyProjectile;

   usesEnergy = true;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = ShadowLanceSwitchSound;

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

datablock ItemData(ShadowLance)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "turret_mortar_large.dts";
   image = ShadowLanceImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a Shadow Lance";
};

function CyclePhantomLordModes(%this, %data) {
   if (!(getSimTime() > (%this.mineModeTime + 100)))
      return;
   %this.mineModeTime = getSimTime();
   %this.PhantomLordMode++;
   %count = 0;
   while(%count < $Power::PowerCount["Phantom Lord"]) {    //random number, pay it no mind
      if(%this.PhantomLordMode > $Power::PowerCount["Phantom Lord"]) {
         %this.PhantomLordMode = 0;
      }
      %HasNext = CheckHasPower(%this.client, "Phantom Lord", %this.PhantomLordMode);
      if (%HasNext) {
         %count = $Power::PowerCount["Phantom Lord"] + 5;
         //nothing, were done
      }
      else {
         //increase the count and, loop again
         %this.PhantomLordMode++;
         %count++;
      }
   }
   DisplayPhantomLordInfo(%this);
   return;
}

function DisplayPhantomLordInfo(%obj) {
   switch(%obj.PhantomLordMode) {
      case 0:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Shadow Lance<spop>\n<spush><font:Arial:14>SHADOW RIFT\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 1:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Shadow Lance<spop>\n<spush><font:Arial:14>DEMATERIALIZE\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 2:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Shadow Lance<spop>\n<spush><font:Arial:14>CHAIN LIGHTNING\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 3:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Shadow Lance<spop>\n<spush><font:Arial:14>TORNADIC UPRISING\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      case 4:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Shadow Lance<spop>\n<spush><font:Arial:14>* SHADOW STORM *\nAffinity Energy: "@%obj.getAEnergy()@"/100<spop>", 3, 3 );
      default:
         BottomPrint(%obj.client, "<spush><font:Sui Generis:14>Shadow Lance<spop>\n<spush><font:Arial:14>You have no Powers, please buy some<spop>", 3, 3 );
   }
}

function ShadowLanceImage::onMount(%this,%obj,%slot) {
   Parent::onMount(%data,%obj,%slot);
   commandToClient(%obj.client, 'setWeaponsHudActive', '', "gui/hud_ret_targlaser", true);
   %obj.usingShadowLance = true;
   DisplayPhantomLordInfo(%obj);
}

function ShadowLanceImage::onunmount(%this,%obj,%slot) {
   Parent::onUnmount(%this, %obj, %slot);
   %obj.usingShadowLance = false;
}

function ShadowLanceImage::onFire(%data,%obj,%slot){
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);   // 0_o
   %aEnergy = %obj.getAEnergy();
   %maxE = FetchMaxEnergy(%client);
   %HaveIt = CheckHasPower(%obj.client, "Phantom Lord", %obj.PhantomLordMode);
   if(!%HaveIt && !%client.isAiControlled()) {
      BottomPrint(%client, "<spush><font:Sui Generis:14>Shadow Lance<spop>\n<spush><font:Arial:14>You dont have this power man. Gotta buy it.<spop>", 3, 3 );
      return;
   }
   %vector = %obj.getMuzzleVector(%slot);
   %mp = %obj.getMuzzlePoint(%slot);
   switch(%obj.PhantomLordMode) {
      case 0:
         if(%energy > (%maxE - 5)) {
            if(%aEnergy < 100) {
               initAffinityCharge(%obj);
            }
         }
      case 1:
         if(%aEnergy >= 40) {
            %obj.takeAEnergy(40);
            %p = new (LinearFlareProjectile)() {
               dataBlock        = DeMatPulse;
               initialDirection = %vector;
               initialPosition  = %mp;
               sourceObject     = %obj;
               damageFactor     = 1;
               sourceSlot       = %slot;
            };
            MissionCleanup.add(%p);
            //
            %p.ShockwaveSched = schedule(100, 0, "DematerializeShockwaves", %obj, %p);
         }
      case 2:

      case 3:

      case 4:
      
      default:
         //do nothing
   }
}
