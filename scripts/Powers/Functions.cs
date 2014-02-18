//Functions.cs
//Phantom139

//Exterior Function Set For the Mod
function LoadSpellEffects() {
   %search = "scripts/Powers/Spells/SpellEffects/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file);
      exec("scripts/Powers/Spells/SpellEffects/"@%type@".cs");
   }
}

function reload(%script) {
   compile(%script); // Added by JackTL - Duh!!
   exec(%script);
   %count = ClientGroup.getCount();
   for(%i = 0; %i < %count; %i++) {
      %cl = ClientGroup.getObject(%i);
      if(!%cl.isAIControlled()) // no sending bots datablocks.. LOL
         %cl.transmitDataBlocks(0); // all clients on server
   }
}

function spawnprojectile(%proj,%type,%pos,%direction,%src) {
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

function servercmdCheckHTilt(%client) {}
function servercmdCheckendTilt(%client) {}

function Player::IsAlive(%player) {
   //fuck off terrain
   if(%player.squareSize || %player.interiorFile) {
      return;
   }
   if(isObject(%player) && !%player.squareSize) {
      if(%player.getState() $= "dead") {
         return false;
      }
      else {
         return true;
      }
   }
   else {
      return false;
   }
}

function isPlayer(%obj) {
   return %obj.getType() & $TypeMasks::PlayerObjectType;
}

//more
datablock ShockLanceProjectileData(FXZap) {
   directDamage        = 0;
   radiusDamageType    = $DamageType::Default;
   kickBackStrength    = 0;
   velInheritFactor    = 0;
   sound               = "";

   zapDuration = 1.0;
   impulse = 0;
   boltLength = 14.0;
   extension = 14.0;            // script variable indicating distance you can shock people from
   lightningFreq = 50.0;
   lightningDensity = 18.0;
   lightningAmp = 0.25;
   lightningWidth = 0.05;


   boltSpeed[0] = 2.0;
   boltSpeed[1] = -0.5;

   texWrap[0] = 1.5;
   texWrap[1] = 1.5;

   startWidth[0] = 0.3;
   endWidth[0] = 0.6;
   startWidth[1] = 0.3;
   endWidth[1] = 0.6;

   texture[0] = "special/shockLightning01";
   texture[1] = "special/shockLightning02";
   texture[2] = "special/shockLightning03";
   texture[3] = "special/ELFBeam";
};

function SimObject::isPlayer(%obj) {
     isPlayer(%obj);
}
function ShapeBase::zapObject(%obj) {
     zapObject(%obj);
}
function ShapeBase::stopZap(%obj) {
     stopZap(%obj);
}

function zapObject(%obj) {
   %obj.zap = new ShockLanceProjectile() {
       dataBlock        = "fxzap";
       initialDirection = "0 0 0";
       initialPosition  = %obj.getWorldBoxCenter();
       sourceObject     = %obj;
       sourceSlot       = 0;
       targetId         = %obj;
   };
   MissionCleanup.add(%obj.zap);
}

function stopZap(%obj) {
   if(isObject(%obj)) {
      if(isObject(%obj.zap))
         %obj.zap.delete();
   }
}

function datablockInfo() {
   %blocks = DataBlockGroup.getCount();
   %effects = 0;

   for(%i = 0; %i < %blocks; %i++) {
      if(DataBlockGroup.getObject(%i).getClassName() $= "EffectProfile") {
         %n = DataBlockGroup.getObject(%i).getName();
         echo(""@%i@". "@%n@"");
         %effects++;
      }
   }
   echo("Number of Datablocks:");
   error(%blocks);
   echo("Current Datablock Capacity:");
   error(mCeil((%blocks / 2048)*100)@"%");
   echo("Number of EffectProfile datablocks:");
   error(%effects);
   echo("Percentage of EffectProfile usage on datablock pool:");
   error(mCeil((%effects / 2048)*100)@"%");

   if(%effects) {
      echo("You have some EffectProfiles remaining. Eliminate them to free up unused datablock space. EffectProfiles are unused Force Feedback datablocks, often attached to sounds. These can be safely removed from all SoundProfile datablocks and removed.");
   }
}

function isSet(%s) {
   return (%s !$= "");
}
