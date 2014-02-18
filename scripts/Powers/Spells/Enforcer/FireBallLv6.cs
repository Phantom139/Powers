datablock ExplosionData(FireballExplosionLv6)
{
   explosionShape = "effect_plasma_explosion.dts";
   soundProfile   = plasmaExpSound;
   particleEmitter = FireballExplosionEmitter;
   particleDensity = 100;
   particleRadius = 1.25;
   faceViewer = true;

   sizes[0] = "25.0 25.0 25.0";
   sizes[1] = "25.0 25.0 25.0";
   times[0] = 0.0;
   times[1] = 1.5;
};


datablock LinearProjectileData(FireballShotLv6)
{
   projectileShapeName = "mortar_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 1.8;
   damageRadius        = 25.0;
   radiusDamageType    = $DamageType::FireBall;
   kickBackStrength    = 3000;

   explosion           = "FireballExplosionLv6";
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

   dryVelocity       = 80;
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

datablock LinearProjectileData(FireballShotLv6_ExploFinal) {
   projectileShapeName = "mortar_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 2.1;
   damageRadius        = 25.0;
   radiusDamageType    = $DamageType::FireBall;
   kickBackStrength    = 3000;

   explosion           = "FireballExplosionLv6";
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

   dryVelocity       = 80;
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

function FireballShotLv6::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   DoBoundedVortex(%proj.sourceObject, %pos, 35, 5, 250, 45, 0);
   schedule(4500, 0, "nthExplosion", %proj.sourceObject, %pos, FireballShotLv6_ExploFinal);
}
