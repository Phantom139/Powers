datablock ShockLanceProjectileData(AvadusLightningShock) {
   directDamage        = 0.01;
   radiusDamageType    = $DamageType::ShockLance;
   kickBackStrength    = 2500;
   velInheritFactor    = 0;
   sound               = "";

   zapDuration = 1.0;
   impulse = 1800;
   boltLength = 80.0;
   extension = 75.0;            // script variable indicating distance you can shock people from
   lightningFreq = 25.0;
   lightningDensity = 3.0;
   lightningAmp = 0.25;
   lightningWidth = 0.05;

   shockwave = ShocklanceHit;

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

   emitter[0] = ShockParticleEmitter;
};

datablock ELFProjectileData(AvadusELFBeam) {
   beamRange         = 9000;
   numControlPoints  = 8;
   restorativeFactor = 3.75;
   dragFactor        = 4.5;
   endFactor         = 2.25;
   randForceFactor   = 2;
   randForceTime     = 0.125;
	drainEnergy			= 0.0;
	drainHealth			= 0.0;
   directDamageType  = $DamageType::ELF;
	mainBeamWidth     = 0.1;           // width of blue wave beam
	mainBeamSpeed     = 9.0;            // speed that the beam travels forward
	mainBeamRepeat    = 0.25;           // number of times the texture repeats
   lightningWidth    = 0.5;
   lightningDist      = 0.5;           // distance of lightning from main beam

   fireSound    = ElfGunFireSound;
   wetFireSound = ElfFireWetSound;

   textures[0] = "special/Sniper";
   textures[1] = "special/FlareSpark";
   textures[2] = "special/Redflare";

   emitter = FlareEmitter;
};

function AvadusLightningShock::DoShockEffect(%dat, %p, %obj, %hitObj) {
   if(!isPlayer(%hitObj)) {
      %hitObj.getDataBlock().damageObject(%hitobj, %obj, %hitObj.getPosition(),
         99999, $DamageType::AvadusKill);
      return;
   }
   %hitObj.setMoveState(true);
   %obj.setMoveState(true);
   DoAvadusKill(%obj, %hitObj, 25);
}

function DoAvadusKill(%obj, %hitObj, %counter) {
   if(!isObject(%obj) || %obj.getState() $= "Dead") {
      if(isObject(%obj.avadusLightning)) {
         %obj.avadusLightning.delete();
      }
      %hitObj.setMoveState(false);
      return;
   }
   if(!isObject(%hitObj) || %hitObj.getState() $= "Dead") {
      %obj.setMoveState(false);
      if(isObject(%obj.avadusLightning)) {
         %obj.avadusLightning.delete();
      }
      return;
   }
   %counter--;
   //
   %enum = getRandom(1,5);
   switch(%enum) {
      case 1:
         %emote = "sitting";
      case 2:
         %emote = "death5";
      case 3:
         %emote = "death3";
      case 4:
         %emote = "death2";
      case 5:
         %emote = "death4";
   }
   %hitObj.setActionThread(%emote,true);
   //
   if(%counter == 0) {
      %hitObj.getDataBlock().damageObject(%hitobj, %obj, %hitObj.getPosition(),
         99999, $DamageType::AvadusKill);
      %hitObj.stopZap();
      //
      if(isObject(%obj.avadusLightning)) {
         %obj.avadusLightning.delete();
      }
      %obj.setMoveState(false);
      return;
   }
   // Show the ELF beam
   %vec = vectorsub(%hitObj.getworldboxcenter(),%obj.getMuzzlePoint(0));
   %vec = vectoradd(%vec, vectorscale(%hitObj.getvelocity(),vectorlen(%vec)/100));
   if(isObject(%obj.avadusLightning)) {
      %obj.avadusLightning.delete();
   }
   %obj.avadusLightning = new ELFProjectile() {
      dataBlock        = AvadusELFBeam;
      initialDirection = %vec;
      initialPosition  = %obj.getMuzzlePoint(0);
      sourceObject     = %obj;
      sourceSlot       = 0;
   };
   MissionCleanup.add(%obj.avadusLightning);
   //
   %hitObj.playAudio(0, ShockLanceHitSound);
   %hitObj.zapObject();
   schedule(100, 0, "DoAvadusKill", %obj, %hitObj, %counter);
}
