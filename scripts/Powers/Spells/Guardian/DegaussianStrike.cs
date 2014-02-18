datablock ParticleData(GaussSmokeParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.02;
   inheritedVelFactor   = 0.1;

   lifetimeMS           = 1200;
   lifetimeVarianceMS   = 100;

   textureName          = "particleTest";

   useInvAlpha = false;
   spinRandomMin = -90.0;
   spinRandomMax = 90.0;

   colors[0]     = "1 1 1";
   colors[1]     = "1 1 1";
   colors[2]     = "1 1 1";
   sizes[0]      = 1;
   sizes[1]      = 1.2;
   sizes[2]      = 1.4;
   times[0]      = 0.0;
   times[1]      = 0.1;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(GaussSmokeEmitter) {
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.5;
   velocityVariance = 0.3;

   thetaMin         = 0.0;
   thetaMax         = 50.0;

   particles = "GaussSmokeParticle";
};

datablock LinearFlareProjectileData(GaussBullet) {
   projectileShapeName = "weapon_missile_projectile.dts";
   scale               = "1.0 1.0 1.0";
   faceViewer          = true;
   directDamage        = 1.55;
   hasDamageRadius     = true;
   DamageRadius        = 20;
   IndirectDamage      = 1.55;
   kickBackStrength    = 6400;
   radiusDamageType    = $DamageType::Gauss;

   explosion           = "SatchelMainExplosion";
   splash              = ChaingunSplash;

   baseEmitter        = GaussSmokeEmitter;

   dryVelocity       = 500.0;
   wetVelocity       = 200.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 15000;
   lifetimeMS        = 15000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.5;
   size[2]           = 0.1;

   numFlares         = 5;   //less flares = less lag
   flareColor        = "1 0.18 0.03";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   hasLight    = true;
   lightRadius = 10.0;
   lightColor  = "0.94 0.03 0.12";
};

function DoDegaussianStrike(%source, %targetPos) {
   %p = new (LinearFlareProjectile)() {
      datablock = "GaussBullet";
      initialPosition = vectorAdd(%targetPos, "0 0 300");
      initialDirection = "0 0 -5";
   };
   %p.sourceObject = %source;
   MissionCleanup.add(%p);
}
