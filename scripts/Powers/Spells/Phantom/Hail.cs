datablock LinearProjectileData(HailProjectile) {
   projectileShapeName = "disc.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.4;
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

function HailProjectile::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   //radius freeze
   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 5.0, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      FreezeObject(%ply, 8000, false);
   }
}

function DoHailArtillery(%source, %targetPosition) {
   for(%i = 0; %i < 20; %i++) {
      %pos1 = vectorAdd(%targetPosition, "0 0 350");
      %pos2 = vectorAdd(%pos1, GetRandomPosition(15, 1));
      %final = vectorAdd(%pos2, "0 0 "@%i * 16@"");
      %p = new (LinearProjectile)() {
         dataBlock        = HailProjectile;
         initialDirection = "0 0 -1";
         initialPosition  = %final;
         damageFactor     = 1;
      };
      MissionCleanup.add(%p);
      %p.sourceObject = %source; //hacky way of spawning airborne projectiles
   }
}
