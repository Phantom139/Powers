datablock ParticleData(StarShardExplosionParticle) {
   dragCoefficient      = 2;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 1500;
   lifetimeVarianceMS   = 10;
   textureName          = "special/underwaterSpark";
   colors[0]     = "1.0 1.0 1.0 1.0";
   colors[1]     = "1.0 1.0 1.0 0.0";
   sizes[0]      = 0.1;
   sizes[1]      = 0.3;
};

datablock ParticleEmitterData(StarShardExplosionEmitter) {
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 2;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 0;
   overrideAdvances = false;
   particles = "StarShardExplosionParticle";
};

datablock ExplosionData(StarShardExplosion) {
   explosionShape = "energy_explosion.dts";
   soundProfile   = plasmaExpSound;

   Emitter[0] = StarShardExplosionEmitter;
   faceViewer = false;

   sizes[0] = "0.5 0.5 0.5";
   sizes[1] = "0.5 0.5 0.5";
   times[0] = 0.0;
   times[1] = 1.5;
};

datablock ParticleData(StarShardParticle) {
   dragCoeffiecient     = 2.0;
   gravityCoefficient   = 0.0;   // rises slowly
   inheritedVelFactor   = 0.0;
   windCoeffiecient     = 0.0;

   lifetimeMS           =  1400;
   lifetimeVarianceMS   =  0;
   useInvAlpha          =  false;
   spinRandomMin        = -150.0;
   spinRandomMax        = 360.0;

   animateTexture = false;

   textureName = "skins/flaregreen";//Ret_RepairGun";

   colors[0]     = "0.9 0.9 0.9 0.5";
   colors[1]     = "0.9 0.9 0.9 0.3";
   colors[2]     = "0.9 0.9 0.9 0.1";
   sizes[0]      = 1;
   sizes[1]      = 1;
   sizes[2]      = 1;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(StarShardSmokeEmitter) {
	lifetimeMS    = -1;
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 0.1;
	velocityVariance = 0.0;
	ejectionoffset = 0;
	thetaMin     = 0.0;
	thetaMax     = 0.0;

	orientParticles = false;
	orientOnVelocity = false;

   particles = "StarShardParticle";
};

datablock LinearFlareProjectileData(StarShard) {
   scale               = "1.0 1.0 1.0";
   faceViewer          = false;
   directDamage        = 0.9;
   hasDamageRadius     = true;
   indirectDamage      = 0.9;
   damageRadius        = 10.0;
   kickBackStrength    = 100.0;
   directDamageType    = $DamageType::StarShard;
   indirectDamageType  = $DamageType::StarShard;

   explosion           = "StarShardExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 400.0;
   wetVelocity       = 10;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 30000;
   lifetimeMS        = 30000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   baseEmitter         = StarShardSmokeEmitter;
   delayEmitter        = StarShardSmokeEmitter;
   bubbleEmitter       = StarShardSmokeEmitter;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.2;
   size[2]           = 0.2;


   numFlares         = 15;
   flareColor        = "0 1 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound        = MissileProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 1 0";

};
