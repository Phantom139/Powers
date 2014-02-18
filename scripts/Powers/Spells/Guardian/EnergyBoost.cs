//shocklance like functions
datablock ShockLanceProjectileData(EnergyBoost) {
   directDamage        = 0.0;
   radiusDamageType    = $DamageType::ShockLance;
   kickBackStrength    = 2500;
   velInheritFactor    = 0;
   sound               = "";

   zapDuration = 1.0;
   impulse = 1800;
   boltLength = 55.0;
   extension = 50.0;            // script variable indicating distance you can shock people from
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

function EnergyBoost::DoShockEffect(%dat, %p, %obj, %hitObj) {
   if(%hitObj.team == %obj.team) {
      messageClient(%obj.client, 'msgClient', "\c0Fully restored the energy of "@%hitObj.client.namebase@".");
      messageClient(%hitObj.client, 'msgClient', "\c0Energy fully restored by "@%obj.client.namebase@".");
      %hitObj.setEnergyLevel(100);
   }
   else {
      %hitObj.setEnergyLevel(0);
   }
}
