datablock LinearFlareProjectileData(CatalystBurst_PilotProjo) {
   directDamage        = 0.5; //direct hit = bye bye :D
   hasDamageRadius     = true;
   indirectDamage      = 0.7;
   damageRadius        = 20.0;
   kickBackStrength    = 100.0;
   directDamageType    = $DamageType::Catalyst;
   indirectDamageType  = $DamageType::Catalyst;

   kickBackStrength    = 0.0;
   bubbleEmitTime      = 1.0;

   sound = MortarProjectileSound;
   velInheritFactor    = 0.5;

   explosion           = "VehicleBombExplosion";
   splash              = BlasterSplash;

   grenadeElasticity = 0.998;
   grenadeFriction   = 0.0;
   armingDelayMS     = 500;

   muzzleVelocity    = 100.0;

   drag = 0.05;

   gravityMod        = 0.0;

   dryVelocity       = 100.0;
   wetVelocity       = 80.0;

   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 6000;

   lifetimeMS     = 30000;

   scale             = "7 7 7";
   numFlares         = 48;
   flareColor        = "100 0 0";
   flareModTexture   = "special/shrikeBoltCross";
   flareBaseTexture  = "special/shrikeBolt";
};

datablock LinearFlareProjectileData(CatalystBurst_Explosion) {
   directDamage        = 0.1;
   hasDamageRadius     = true;
   indirectDamage      = 0.75;
   damageRadius        = 10.0;
   kickBackStrength    = 100.0;
   directDamageType    = $DamageType::Catalyst;
   indirectDamageType  = $DamageType::Catalyst;

   kickBackStrength    = 0.0;
   bubbleEmitTime      = 1.0;

   sound = MortarProjectileSound;
   velInheritFactor    = 0.5;

   explosion           = "VehicleBombExplosion";
   splash              = BlasterSplash;

   grenadeElasticity = 0.998;
   grenadeFriction   = 0.0;
   armingDelayMS     = 500;

   muzzleVelocity    = 100.0;

   drag = 0.05;

   gravityMod        = 0.0;

   dryVelocity       = 100.0;
   wetVelocity       = 80.0;

   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 6000;

   lifetimeMS     = 30000;

   scale             = "7 7 7";
   numFlares         = 48;
   flareColor        = "100 0 0";
   flareModTexture   = "special/shrikeBoltCross";
   flareBaseTexture  = "special/shrikeBolt";
};

function CatalystBurst_PilotProjo::onExplode(%data, %proj, %pos, %mod) {
   Parent::onExplode(%data, %proj, %pos, %mod);
   InitContainerRadiusSearch(%pos, 25, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      schedule(4500, 0, "doChainExplosions", %ply, %proj.sourceObject);
   }
}

function doChainExplosions(%target, %source) {
   if(!isObject(%target) || !%target.isAlive()) {
      return;
   }
   %position = %target.getPosition();
   %spawn = vectorAdd(%position, "0 0 .5");
   %boom = new (LinearFlareProjectile)() {
      datablock = CatalystBurst_Explosion;
      initialPosition = %spawn;
      initialDirection = "0 0 -10";
   };
   %boom.sourceObject = %source;
   missionCleanup.add(%boom);
}
