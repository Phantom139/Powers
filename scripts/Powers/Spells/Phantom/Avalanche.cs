datablock LinearFlareProjectileData(AvalancheSphere) {
   hasDamageRadius     = true;
   indirectDamage      = 0.5;
   damageRadius        = 10.0;
   radiusDamageType    = $DamageType::Freeze;

   explosion           = "PlasmaBarrelBoltExplosion";
   kickBackStrength  = 600.0;

   dryVelocity       = 200.0;
   wetVelocity       = 40.0;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 8000;
   lifetimeMS        = 9000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   numFlares         = 20;
   size              = 1.0;
   flareColor        = "0 0 1";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound = BlasterProjectileSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 0 1";
};

function AvalancheSphere::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   //radius freeze
   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 9.0, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      if(CanAOEHit(%pos, 9, %ply)) {
         FreezeObject(%ply, 7000, true);
      }
   }
}

function DoAvalanche(%source, %targetPosition) {
   for(%i = 0; %i < 90; %i++) {
      %pos1 = vectorAdd(%targetPosition, "0 0 300");
      %pos2 = vectorAdd(%pos1, GetRandomPosition(30, 1));
      %final = vectorAdd(%pos2, "0 0 "@%i * 7@"");
      %p = new (LinearFlareProjectile)() {
         dataBlock        = AvalancheSphere;
         initialDirection = "0 0 -2";
         initialPosition  = %final;
         damageFactor     = 1;
      };
      MissionCleanup.add(%p);
      %p.sourceObject = %source; //hacky way of spawning airborne projectiles
   }
}
