//Core/Functions.cs
//Phantom139

//Handles the mod's Direct Functions

function spawnprojectile(%proj, %type, %pos, %direction, %src) {
   if(%src $= "" || !%src) {
      %src = "";
   }
   %p = new (%type)() {
       dataBlock        = %proj;
       initialDirection = %direction;
       initialPosition  = %pos;
       sourceObject     = %src;
       damageFactor     = 1;
   };
   MissionCleanup.add(%p);
   return %p;
}

function spawnprojectileOH(%proj, %type, %pos, %direction, %src) {
   if(%src $= "" || !%src) {
      %src = "";
   }
   %p = new (%type)() {
       dataBlock        = %proj;
       initialDirection = %direction;
       initialPosition  = %pos;
       damageFactor     = 1;
   };
   %p.sourceObject = %src;
   MissionCleanup.add(%p);
   return %p;
}

function SetUpClientPowersName(%client) {
   %slot = %client.slotNum;
   %class = %client.slot(%slot).class;
   %level = %client.slot(%slot).level;
   
   if(%level $= "") {
      %level = 1;
   }
   if(%class $= "") {
      %classTag = "Und";
   }
   else if(%class $= "Undecided") {
      %classTag = "Und";
   }
   else if(%class $= "Witch") {
      %classTag = "Wit";
   }
   else if(%class $= "Demon") {
      %classTag = "Dem";
   }
   else if(%class $= "Phantom") {
      %classTag = "Pha";
   }
   else if(%class $= "Hunter") {
      %classTag = "Htr";
   }
   //
   else if(%class $= "Guardian") {
      %classTag = "Grd";
   }
   else if(%class $= "Star Lighter") {
      %classTag = "Str";
   }
   else if(%class $= "Enforcer") {
      %classTag = "Enf";
   }
   else if(%class $= "Devastator") {
      %classTag = "Dev";
   }
   else if(%class $= "Overseer") {
      %classTag = "Osr";
   }
   else if(%class $= "Cryonium") {
      %classTag = "Cyo";
   }
   else if(%class $= "Nature Walker") {
      %classTag = "NWr";
   }
   //
   else if(%class $= "Gladiator") {
      %classTag = "Gld";
   }
   else if(%class $= "Star Sighter") {
      %classTag = "Sgt";
   }
   else if(%class $= "Prospector") {
      %classTag = "Pct";
   }
   else if(%class $= "Annihilator") {
      %classTag = "Ahi";
   }
   else if(%class $= "Phantom Lord") {
      %classTag = "PLd";
   }
   else if(%class $= "Deep Freezer") {
      %classTag = "Frz";
   }
   else if(%class $= "Wispwalker") {
      %classTag = "Wsp";
   }
   //
   if(%level < 100) {
      %tag = "["@%classTag@":LV"@%level@"]";
      %name = "\cp\c7" @ %tag @ "\c6" @ %client.namebase @ "\co";
   }
   else {
      %name = "\cp\c9["@%classTag@"]\c7"@%client.namebase@"\co";
   }
   MessageAll( 'MsgClientNameChanged', "", %client.name, %name, %client );
   removeTaggedString(%client.name);
   %client.name = addTaggedString(%name);
   setTargetName(%client.target, %client.name);
}

function SetUpAIClientPowersName(%client, %level, %class) {
   if(%level < 100) {
      %tag = "["@%class@":LV"@%level@"]";
      %name = "\cp\c7" @ %tag @ "\c9" @ %client.namebase @ "\co";
   }
   else {
      %name = "\cp\c8["@%class@"]\c9"@%client.namebase@"\co";
   }

   MessageAll( 'MsgClientNameChanged', "", %client.name, %name, %client );
   removeTaggedString(%client.name);
   %client.name = addTaggedString(%name);
   setTargetName(%client.target, %client.name);
   //fun thing
   //%client.setSkillLevel(%level);
}


//AI POWERS
function AIPowersLoop(%ai) {
   if($host::BotsCannotSpell) {
      schedule(1000, 0, "AIPowersLoop", %ai);
      return;
   }
   if(!%ai) {
      return;
   }
   if(!isobject(%ai.player) || %ai.player.getState() $= "dead") {

   }
   else {
      GiveAIGun(%ai);
      AiFireGun(%ai);
   }
   schedule(1000, 0, "AIPowersLoop", %ai);
}

function GiveAIGun(%ai) {
   %info = $AIInfo[%ai];
   %class = getWord(%info, 0);
   %level = getWord(%info, 1);
   if(%level < 15) {
      %ai.LevClass = 1;
   }
   else if(%level >= 27) {
      %ai.LevClass = 3;
   }
   else {
      %ai.LevClass = 2;
   }
   //
   //echo(%class);
   %getMount = %ai.player.getMountedImage($WeaponSlot);
   if(%getMount !$= "WitchWeaponImage" && %getMount !$= "DemonWeaponImage" && %getMount !$= "PhantomGunImage") {
      if(%class == 1 || %class == 4 || %class == 5 || %class == 10 || %class == 11) {
         %ai.player.setInventory(Blaster, 0, true);
         %ai.player.setInventory(WitchWeapon, 1, true);
         %ai.player.use(WitchWeapon);
         if(%ai.LevClass == 1) {
            %ai.player.WitchMode = getRandom(1, 5);
         }
         else if(%ai.LevClass == 2) {
            %ai.player.WitchMode = getRandom(1, 10);
         }
         else if(%ai.LevClass == 3) {
            %ai.player.WitchMode = getRandom(1, 17);
         }
      }
      else if(%class == 2 || %class == 6 || %class == 7 || %class == 12 || %class == 13) {
         %ai.player.setInventory(Blaster, 0, true);
         %ai.player.setInventory(DemonWeapon, 1, true);
         %ai.player.use(DemonWeapon);
         if(%ai.LevClass == 1) {
            %ai.player.DemonMode = getRandom(1, 5);
         }
         else if(%ai.LevClass == 2) {
            %ai.player.DemonMode = getRandom(1, 10);
         }
         else if(%ai.LevClass == 3) {
            %ai.player.DemonMode = getRandom(1, 16);
         }
      }
      else if(%class == 3 || %class == 8 || %class = 9 || %class = 14 || %class == 15) {
         %ai.player.setInventory(Blaster, 0, true);
         %ai.player.setInventory(PhantomGun, 1, true);
         %ai.player.use(PhantomGun);
         if(%ai.LevClass == 1) {
            %ai.player.PhantomMode = getRandom(1, 5);
         }
         else if(%ai.LevClass == 2) {
            %ai.player.PhantomMode = getRandom(1, 9);
         }
         else if(%ai.LevClass == 3) {
            %ai.player.PhantomMode = getRandom(1, 16);
         }
      }
   }
}

function AiFireGun(%ai) {
   if(%ai.shouldEngage) {
     //echo("fire");
     %ai.player.setImageTrigger(0, true);
   }
}

function PowersModPostDeath(%player) {
   if(%player.client.isAIControlled()) {
      return;
   }
   %client = %player.client;
   %slot = %client.slotNum;
   if(%slot $= "") {
      CenterPrint(%client, "You appear to not have a slot selected.\n\nPlease press F2 to select a slot.", 3, 3);
      return;
   }
   
   if(%client.slot(%slot).class $= "" || %client.slot(%slot).class $= "Undecided") {
      BottomPrint(%client, "Tired Of Dying? Maybe you need to pick a class!\n\nPress [F2] then click My Page to choose!", 3, 3);
   }
}

function GivePowersWeapon(%client) {
   %slot = %client.slotNum;
   
   if(%client.player.affinityEnergy $= "") {
      %client.player.affinityEnergy = 0;
   }
   
   if(%client.slot(%slot).class $= "Witch") {
      %client.player.setInventory( WitchWeapon, 1, true );
      %client.player.WitchMode = 0;
   }
   else if(%client.slot(%slot).class $= "Demon") {
      %client.player.setInventory( DemonWeapon, 1, true);
      %client.player.DemonMode = 0;
   }
   else if(%client.slot(%slot).class $= "Phantom") {
      %client.player.setInventory( PhantomGun, 1, true);
      %client.player.PhantomMode = 0;
   }
   else if(%client.slot(%slot).class $= "Hunter") {
      %client.player.setInventory( HunterBow, 1, true);
      %client.player.HunterMode = 0;
   }
   //
   else if(%client.slot(%slot).class $= "Guardian") {
      %client.player.setInventory( WitchWeapon, 1, true );
      %client.player.setInventory( PikeOfFaith, 1, true );
      %client.player.WitchMode = 0;
      %client.player.GuardianMode = 0;
   }
   else if(%client.slot(%slot).class $= "Star Lighter") {
      %client.player.setInventory( WitchWeapon, 1, true );
      %client.player.setInventory( HarbingerofWar, 1, true );
      %client.player.WitchMode = 0;
      %client.player.StarLighterMode = 0;
   }
   else if(%client.slot(%slot).class $= "Enforcer") {
      %client.player.setInventory( DemonWeapon, 1, true);
      %client.player.setInventory( StaffofEmbirthia, 1, true);
      %client.player.DemonMode = 0;
      %client.player.EnforcerMode = 0;
   }
   else if(%client.slot(%slot).class $= "Devastator") {
      %client.player.setInventory( DemonWeapon, 1, true);
      %client.player.setInventory( AsthenieSword, 1, true);
      %client.player.DemonMode = 0;
      %client.player.DevastatorMode = 0;
   }
   else if(%client.slot(%slot).class $= "Overseer") {
      %client.player.setInventory( PhantomGun, 1, true);
      %client.player.setInventory( OverseerSword, 1, true);
      %client.player.PhantomMode = 0;
      %client.player.OverseerMode = 0;
   }
   else if(%client.slot(%slot).class $= "Cryonium") {
      %client.player.setInventory( PhantomGun, 1, true);
      %client.player.setInventory( CryonicSpear, 1, true);
      %client.player.PhantomMode = 0;
      %client.player.CryonicEmbMode = 0;
   }
   else if(%client.slot(%slot).class $= "Nature Walker") {
      %client.player.setInventory( HunterBow, 1, true);
      %client.player.HunterMode = 0;
      %client.player.NatureWalkerMode = 0;
   }
   //
   else if(%client.slot(%slot).class $= "Gladiator") {
      %client.player.setInventory( WitchWeapon, 1, true );
      %client.player.setInventory( PikeOfFaith, 1, true );
      %client.player.setInventory( HolyCannon, 1, true );
      %client.player.WitchMode = 0;
      %client.player.GuardianMode = 0;
      %client.player.GladiatorMode = 0;
   }
   else if(%client.slot(%slot).class $= "Star Sighter") {
      %client.player.setInventory( WitchWeapon, 1, true );
      %client.player.setInventory( HarbingerofWar, 1, true );
      %client.player.setInventory( StarforceCannon, 1, true );
      %client.player.WitchMode = 0;
      %client.player.StarLighterMode = 0;
      %client.player.StarSighterMode = 0;
   }
   else if(%client.slot(%slot).class $= "Prospector") {
      %client.player.setInventory( DemonWeapon, 1, true);
      %client.player.setInventory( StaffofEmbirthia, 1, true);
      %client.player.setInventory( CrystalLance, 1, true);
      %client.player.DemonMode = 0;
      %client.player.EnforcerMode = 0;
      %client.player.ProspectorMode = 0;
   }
   else if(%client.slot(%slot).class $= "Annihilator") {
      %client.player.setInventory( DemonWeapon, 1, true);
      %client.player.setInventory( AsthenieSword, 1, true);
      %client.player.setInventory( VolcanicCannon, 1, true);
      %client.player.DemonMode = 0;
      %client.player.DevastatorMode = 0;
      %client.player.AnnihilatorMode = 0;
   }
   else if(%client.slot(%slot).class $= "Phantom Lord") {
      %client.player.setInventory( PhantomGun, 1, true);
      %client.player.setInventory( OverseerSword, 1, true);
      %client.player.setInventory( ShadowLance, 1, true);
      %client.player.PhantomMode = 0;
      %client.player.OverseerMode = 0;
      %client.player.PhantomLordMode = 0;
   }
   else if(%client.slot(%slot).class $= "Deep Freezer") {
      %client.player.setInventory( PhantomGun, 1, true);
      %client.player.setInventory( CryonicSpear, 1, true);
      %client.player.setInventory( ArcticCannon, 1, true);
      %client.player.PhantomMode = 0;
      %client.player.CryonicEmbMode = 0;
      %client.player.DeepFreezerMode = 0;
   }
   else if(%client.slot(%slot).class $= "Wispwalker") {
      %client.player.setInventory( HunterBow, 1, true);
      %client.player.HunterMode = 0;
      %client.player.NatureWalkerMode = 0;
      %client.player.WispwalkerMode = 0;
   }
}

function RMPG() {
   %X = getWord(MissionArea.getArea(), 0);
   %Y = getWord(MissionArea.getArea(), 1);
   %W = getWord(MissionArea.getArea(), 2);
   %H = getWord(MissionArea.getArea(), 3);

   %OppX = ((%X) + (%W));
   %OppY = ((%Y) + (%H));
   %Position = getRandom((%X+(0.1*%W)), %OppX - (0.1*%W)) SPC getRandom((%Y+(0.1*%H)), %OppY - (0.1*%H)) SPC 0;

   %Z = getTerrainHeight(%position);
   %PositionF = getWord(%Position, 0) SPC getWord(%Position, 1) SPC %Z;

   return %PositionF;
}

function ForceSlot(%client, %num) {
   %client.SlotNum = %num;
   LoadClientRankfile(%client);
   if(isObject(%client.player)) {
      buyFavorites(%client);
   }
   SetUpClientPowersName(%client);
   %client.SelectingSlot = 0;
   //
   MessageClient(%client, 'MsgAdminForce', "\c5POWERS: You have been forced into slot "@%num);
}
