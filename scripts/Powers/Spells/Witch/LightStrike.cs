//LIGHT STRIKE
datablock ParticleData(LightStrikeParticle)
{
   dragCoeffiecient     = 1;
   gravityCoefficient   = -0.3;   // rises slowly
   inheritedVelFactor   = 0;

   lifetimeMS           =  300;
   lifetimeVarianceMS   =  0;
   useInvAlpha          =  false;
   spinRandomMin        = 0.0;
   spinRandomMax        = 0.0;

   animateTexture = false;

   textureName = "flareBase"; // "special/Smoke/bigSmoke"

   colors[0]     = "0.7 0.4 0.1 1";
   colors[1]     = "0.8 0.5 0.5 1";
   colors[2]     = "0.2 0.2 0.1 0.5";

   sizes[0] = 0.75;
   sizes[1] = 0.75;
   sizes[2] = 0.75;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;
};

datablock ParticleEmitterData(LightStrikeEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 1;

   ejectionVelocity = 10;
   velocityVariance = 0;

   thetaMin         = 89.0;
   thetaMax         = 90.0;

   orientParticles = false;

   particles = "LightStrikeParticle";
};

datablock LinearFlareProjectileData(LightStrikeProj)
{
   scale               = "1.0 1.0 1.0";
   faceViewer          = false;
   directDamage        = 0.4;
   hasDamageRadius     = false;
   indirectDamage      = 0.333;
   damageRadius        = 3.0;
   kickBackStrength    = 100.0;
   directDamageType    = $DamageType::LightStrike;
   indirectDamageType  = $DamageType::LightStrike;

   explosion           = "BlasterExplosion";
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

   baseEmitter         = LightStrikeEmitter;
   delayEmitter        = LightStrikeEmitter;
   bubbleEmitter       = LightStrikeEmitter;

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

