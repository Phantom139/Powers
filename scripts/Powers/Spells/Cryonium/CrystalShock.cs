datablock LinearFlareProjectileData(CrystalShockPulse) {
   projectileShapeName = "plasmabolt.dts";
   scale               = "2.0 2.0 2.0";
   faceViewer          = true;
   directDamage        = 0.45;
   hasDamageRadius     = false;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::CrysShock;

   explosion           = "PlasmaBoltExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 75.0;
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 29000;
   lifetimeMS        = 30000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.5;
   size[2]           = 0.1;


   numFlares         = 35;
   flareColor        = "1 0.75 0.25";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

function CrystalShockPulse::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal) {
   if(!isPlayer(%targetObject)) {
      return;
   }
   if(!%targetObject.squareSize && !%targetObject.IsAlive()) {
      return;
   }
   Parent::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal);
   if(%targetObject.isFrozen) {
      messageClient(%targetObject.client, 'MsgBoom', "\c3The Ice Crystals Explode and shred on your armor.");
      %targetObject.getDataBlock().damageObject(%targetObject, %projectile.sourceObject,
         %targetObject.getPosition(), (0.9), $DamageType::CrysShock);
      Thaw(%targetObject);
   }
}
