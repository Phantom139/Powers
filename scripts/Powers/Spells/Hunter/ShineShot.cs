datablock LinearFlareProjectileData(ShineShot) {
   scale               = "3.0 3.0 3.0";
   faceViewer          = false;
   hasDamageRadius     = true;
   indirectDamage      = 0.05;
   damageRadius        = 10.0;
   radiusDamageType    = $DamageType::ManaArrow;

   splash              = PlasmaSplash;

   dryVelocity       = 400.0;
   wetVelocity       = 10;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 30000;
   lifetimeMS        = 30000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   baseEmitter         = ManaArrowSmokeEmitter;
   delayEmitter        = ManaArrowSmokeEmitter;
   bubbleEmitter       = ManaArrowSmokeEmitter;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.2;
   size[2]           = 0.2;


   numFlares         = 15;
   flareColor        = "1 1 1";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound        = MissileProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "1 1 1";

};

function ShineShot::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);

   //---------------------------------------------
   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 7.5, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      %ply.setWhiteOut(100);
   }
}
