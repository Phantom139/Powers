datablock LinearFlareProjectileData(EBallBoltLV3) {
   hasDamageRadius     = true;
   indirectDamage      = 1.0;
   damageRadius        = 14.0;
   radiusDamageType    = $DamageType::EnergyBall;

   explosion           = "PlasmaBarrelBoltExplosion";
   kickBackStrength  = 600.0;

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
   size              = 0.50;
   flareColor        = "0 0 1";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound = BlasterProjectileSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 0 1";
};

