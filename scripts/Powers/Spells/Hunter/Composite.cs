datablock LinearProjectileData(CompositeProjectile) {
   projectileShapeName = "pack_upgrade_satchel.dts";
   emitterDelay        = -1;
   kickBackStrength    = 1750;

   explosion           = "SatchelMainExplosion";
   underwaterExplosion = "SatchelMainExplosion";

   hasDamageRadius     = true;
   indirectDamage      = 1.9;
   damageRadius        = 8.0;
   radiusDamageType    = $DamageType::Composite;

   dryVelocity       = 90;
   wetVelocity       = 50;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 500;
   lifetimeMS        = 500;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 500;

   activateDelayMS = 500;

   hasLight    = true;
   lightRadius = 6.0;
   lightColor  = "0.175 0.175 0.5";
};

function CompositeProjectile::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
}
