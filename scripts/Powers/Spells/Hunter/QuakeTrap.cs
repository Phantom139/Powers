datablock ExplosionData(QuakeTrapExplosion) {
   soundProfile   = ConcussionGrenadeExplosionSound;
   shockwave =  ConcussionGrenadeShockwave;

   emitter[0] = ConcussionGrenadeSparkEmitter;
   emitter[1] = ConcussionGrenadeCrescentEmitter;

   shakeCamera = true;
   camShakeFreq = "4.0 5.0 4.5";
   camShakeAmp = "140.0 140.0 140.0";
   camShakeDuration = 10.0;
   camShakeRadius = 15.0;
};

datablock TracerProjectileData(QuakeTrapDown) {
   doDynamicClientHits = true;

   hasDamageRadius     = true;
   indirectDamage      = 0.2;
   damageRadius        = 8.0;
   radiusDamageType    = $DamageType::HunterTrap;
   explosion           = "QuakeTrapExplosion";
   splash              = ChaingunSplash;

   kickBackStrength  = 0.0;
   sound 				= MineExplosionSound;

   dryVelocity       = 425.0;
   wetVelocity       = 100.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 15.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.10;
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
