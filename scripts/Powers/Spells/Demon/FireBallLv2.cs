datablock ExplosionData(FireballExplosionLv2)
{
   explosionShape = "effect_plasma_explosion.dts";
   soundProfile   = plasmaExpSound;
   particleEmitter = FireballExplosionEmitter;
   particleDensity = 100;
   particleRadius = 1.25;
   faceViewer = true;

   sizes[0] = "8.0 8.0 8.0";
   sizes[1] = "8.0 8.0 8.0";
   times[0] = 0.0;
   times[1] = 1.5;
};


datablock LinearProjectileData(FireballShotLV2)
{
   projectileShapeName = "mortar_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.72;
   damageRadius        = 16.0;
   radiusDamageType    = $DamageType::FireBall;
   kickBackStrength    = 3000;

   explosion           = "FireballExplosionLv2";
//   underwaterExplosion = "UnderwaterFireballExplosion";
   velInheritFactor    = 0.5;
//   splash              = FireballSplash;
   depthTolerance      = 10.0; // depth at which it uses underwater explosion

   baseEmitter         = MissileFireEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;

   grenadeElasticity = 0.15;
   grenadeFriction   = 0.4;
   armingDelayMS     = 2000;
   muzzleVelocity    = 63.7;
   drag              = 0.1;

   sound          = MortarProjectileSound;

   hasLight    = true;
   lightRadius = 4;
   lightColor  = "1.00 0.9 1.00";

   hasLightUnderwaterColor = true;
   underWaterLightColor = "0.05 0.075 0.2";

   dryVelocity       = 90;
   wetVelocity       = 50;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 5000;
   lifetimeMS        = 2700;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 5000;

};
