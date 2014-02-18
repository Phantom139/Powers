datablock LinearProjectileData(ConcussionBlowProjectile) {
   projectileShapeName = "grenade.dts";
   emitterDelay        = -1;
   kickBackStrength    = 1750;
   
   explosion           = "ConcussionGrenadeExplosion";
   underwaterExplosion = "ConcussionGrenadeExplosion";

   hasDamageRadius     = true;
   indirectDamage      = 0.2;
   damageRadius        = 8.0;
   radiusDamageType    = $DamageType::Grenade;

   dryVelocity       = 90;
   wetVelocity       = 50;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 1000;
   lifetimeMS        = 1000;
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

function ConcussionBlowProjectile::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
}
