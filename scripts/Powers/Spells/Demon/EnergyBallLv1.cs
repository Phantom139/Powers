//Energy Ball
datablock LinearFlareProjectileData(EBallBolt) {
   hasDamageRadius     = true;
   indirectDamage      = 0.5;
   damageRadius        = 7.0;
   radiusDamageType    = $DamageType::EnergyBall;

   explosion           = "PlasmaBarrelBoltExplosion";
   kickBackStrength  = 350.0;

   dryVelocity       = 120.0;
   wetVelocity       = 40.0;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 2000;
   lifetimeMS        = 3000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   numFlares         = 20;
   size              = 0.20;
   flareColor        = "0 0 1";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound = BlasterProjectileSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 0 1";
};
