datablock AudioProfile(BOVHitSound) {
   filename    = "fx/misc/flag_snatch.wav";
   description = AudioClose3d;
   preload = true;
};

datablock TracerProjectileData(StabRush) {
   doDynamicClientHits = true;

   directDamage        = 0.45; //OW?
   directDamageType    = $DamageType::Stab; //already has a DM (See below)
   explosion           = ChaingunExplosion;
   splash              = ChaingunSplash;

   kickBackStrength  = 1500;
   sound             = ChaingunProjectile;

   dryVelocity       = 50.0; // z0dd - ZOD, 8-12-02. Was 425.0
   wetVelocity       = 50.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 6;
   lifetimeMS        = 400;  //shorter range
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 0.1;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.01;
   crossSize       = 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = ChaingunDecal1;
   decalData[1] = ChaingunDecal2;
   decalData[2] = ChaingunDecal3;
   decalData[3] = ChaingunDecal4;
   decalData[4] = ChaingunDecal5;
   decalData[5] = ChaingunDecal6;
};

function BOVhit::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal) {
   ServerPlay3d(BOVHitSound, %targetObject.getPosition());
   Parent::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal);
}
