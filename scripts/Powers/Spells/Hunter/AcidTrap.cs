datablock ParticleData(AcidTrapExplosionParticle) {
   dragCoefficient      = 2;
   gravityCoefficient   = 0.1;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 10000;
   lifetimeVarianceMS   = 0;
   textureName          = "special/cloudflash";
   colors[0]     = "0 1 0 0.6";
   colors[1]     = "0 1 0 0.0";
   sizes[0]      = 12;
   sizes[1]      = 12.5;
};

datablock ParticleEmitterData(AcidTrapExplosionEmitter) {
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionOffset = 2.0;
   ejectionVelocity = 4.0;
   velocityVariance = 0.0;
   thetaMin         = 60.0;
   thetaMax         = 90.0;
   lifetimeMS       = 6000;

   particles = "AcidTrapExplosionParticle";
};

datablock ExplosionData(AcidTrapExplosion) {
   particleEmitter = AcidTrapExplosionEmitter;
   particleDensity = 3;
   particleRadius = 2.0;
   faceViewer = true;
};

datablock TracerProjectileData(AcidTrapDown) {
   doDynamicClientHits = true;

   hasDamageRadius     = true;
   indirectDamage      = 0.65;
   damageRadius        = 10.0;
   radiusDamageType    = $DamageType::HunterTrap;
   explosion           = "AcidTrapExplosion";
   splash              = ChaingunSplash;

   kickBackStrength    = 1750.0;
   sound 			   = MineExplosionSound;

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

function AcidTrapDown::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   
   //---------------------------------------------
   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 7.0, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      applytoxic(%proj.sourceObject, %ply, 250);
   }
}
