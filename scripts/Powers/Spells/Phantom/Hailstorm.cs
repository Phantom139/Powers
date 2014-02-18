datablock LinearProjectileData(HailstormProjectile) {
   projectileShapeName = "disc.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.45;
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

function HailstormProjectile::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   //radius freeze
   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 9.0, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      if(CanAOEHit(%pos, 9, %ply)) {
         FreezeObject(%ply, 6000, true);
      }
   }
}

function DoHailstormArtillery(%source, %targetPosition) {
   for(%i = 0; %i < 55; %i++) {
      %pos1 = vectorAdd(%targetPosition, "0 0 350");
      %pos2 = vectorAdd(%pos1, GetRandomPosition(30, 1));
      %final = vectorAdd(%pos2, "0 0 "@%i * 7@"");
      %p = new (LinearProjectile)() {
         dataBlock        = HailstormProjectile;
         initialDirection = "0 0 -2";
         initialPosition  = %final;
         damageFactor     = 1;
      };
      MissionCleanup.add(%p);
      %p.sourceObject = %source; //hacky way of spawning airborne projectiles
   }
}
