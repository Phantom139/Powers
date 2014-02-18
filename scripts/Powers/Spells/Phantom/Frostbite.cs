datablock LinearProjectileData(FrostbiteProjectile) {
   projectileShapeName = "disc.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.2;
   damageRadius        = 7.5;
   radiusDamageType    = $DamageType::Freeze;
   kickBackStrength    = 1750;

   sound 				= discProjectileSound;
   explosion           = "DiscExplosion";
   underwaterExplosion = "UnderwaterDiscExplosion";
   splash              = DiscSplash;

   dryVelocity       = 300;
   wetVelocity       = 300;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 5000;
   lifetimeMS        = 5000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = 200;

   hasLight    = true;
   lightRadius = 6.0;
   lightColor  = "0.175 0.175 0.5";
};

function FrostbiteProjectile::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal) {
   if(!isPlayer(%targetObject)) {
      return;
   }
   if(!%targetObject.IsAlive()) {
      return;
   }
   Parent::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal);
   FreezeObject(%targetobject, 5000, false);
}

