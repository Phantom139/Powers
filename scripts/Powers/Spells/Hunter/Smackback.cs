datablock EnergyProjectileData(SmackbackProjectile) {
   hasDamageRadius     = true;
   indirectDamage      = 0.05;
   damageRadius        = 3.0;
   radiusDamageType    = $DamageType::ManaArrow;

   emitterDelay        = -1;
   kickBackStrength    = 0.0;
   bubbleEmitTime      = 1.0;

   sound = BlasterProjectileSound;
   velInheritFactor    = 0.5;

   explosion           = "BlasterExplosion";
   splash              = BlasterSplash;

   grenadeElasticity = 0.998;
   grenadeFriction   = 0.0;
   armingDelayMS     = 100;
   
   kickbackStrength = 1500;

   muzzleVelocity    = 90.0;

   drag = 0.05;

   gravityMod        = 0.0;

   dryVelocity       = 200.0;
   wetVelocity       = 150.0;

   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 500;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1.5 0.175 0.3";

   scale = "0.25 1.0 1.0";
   crossViewAng = 0.99;
   crossSize = 0.55;

   lifetimeMS     = 700;
   blurLifetime   = 0.2;
   blurWidth      = 0.25;
   blurColor = "0.5 0.0 0.0 1.0";

   texture[0] = "special/blasterBolt";
   texture[1] = "special/blasterBoltCross";
};

function SmackbackProjectile::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal) {
   if(!isPlayer(%targetObject)) {
      return;
   }
   parent::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal);
   //short stun
   schedule(1000, 0, stunObject, %targetObject, %projectile.sourceObject, 4);
}
