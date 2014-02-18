datablock ShockLanceProjectileData(BasicDrainSpell) {
   directDamage        = 0.01;
   radiusDamageType    = $DamageType::ShockLance;
   kickBackStrength    = 2500;
   velInheritFactor    = 0;
   sound               = "";

   zapDuration = 1.0;
   impulse = 1800;
   boltLength = 50.0;
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

function BasicDrainSpell::DoShockEffect(%projDB, %proj, %source, %target) {
   if(!isPlayer(%target)) {
      return;
   }
   if(!%target.IsAlive()) {
      return;
   }
   if(!%target.beingDrained) {
      %target.beingDrained = 1;
      %target.drainLoop = DrainELoop(%target, %source, false);
      schedule(8000, 0, "cancelEDrain", %target);
   }
}
