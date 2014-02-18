datablock ParticleData( VapExploCrescentParticle )
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 000;
   textureName          = "special/crescent3";
   colors[0]     = "0.7 0.8 1.0 0.0";
   colors[1]     = "0.7 0.8 1.0 0.4";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 4.0;
   sizes[1]      = 8.0;
   sizes[2]      = 9.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( VapExploCrescentEmitter )
{
   ejectionPeriodMS = 25;
   periodVarianceMS = 0;
   ejectionVelocity = 40;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 200;
   particles = "VapExploCrescentParticle";
};


datablock ParticleData(VapExploExplosionSmoke)
{
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = -0.30;   // rises slowly
   inheritedVelFactor   = 0.025;

   lifetimeMS           = 1250;
   lifetimeVarianceMS   = 500;

   textureName          = "particleTest";

   useInvAlpha =  true;
   spinRandomMin = -100.0;
   spinRandomMax =  100.0;

   textureName = "special/Smoke/bigSmoke";

   colors[0]     = "0.7 0.8 1.0 0.0";
   colors[1]     = "0.7 0.8 1.0 0.4";
   colors[2]     = "0.7 0.8 1.0 0.0";
   
   sizes[0]      = 5.0;
   sizes[1]      = 6.0;
   sizes[2]      = 10.0;

   times[0]      = 0.0;
   times[1]      = 0.333;
   times[2]      = 0.666;



};

datablock ParticleEmitterData(VapExploExplosionSmokeEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionOffset = 8.0;


   ejectionVelocity = 1.25;
   velocityVariance = 1.2;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   lifetimeMS       = 500;

   particles = "VapExploExplosionSmoke";

};


datablock ExplosionData(VapExploSubExplosion1)
{
   explosionShape = "mortar_explosion.dts";
   faceViewer           = true;

   delayMS = 100;

   offset = 5.0;

   playSpeed = 1.5;

   sizes[0] = "0.5 0.5 0.5";
   sizes[1] = "0.5 0.5 0.5";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(VapExploSubExplosion2)
{
   explosionShape = "mortar_explosion.dts";
   faceViewer           = true;

   delayMS = 50;

   offset = 5.0;

   playSpeed = 1.0;

   sizes[0] = "1.0 1.0 1.0";
   sizes[1] = "1.0 1.0 1.0";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock ExplosionData(VapExploSubExplosion3)
{
   explosionShape = "mortar_explosion.dts";
   faceViewer           = true;
   delayMS = 0;
   offset = 0.0;
   playSpeed = 0.7;

   sizes[0] = "1.0 1.0 1.0";
   sizes[1] = "2.0 2.0 2.0";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(VapExploExplosion)
{
   soundProfile   = MortarExplosionSound;

   shockwave = MortarShockwave;
   shockwaveOnTerrain = true;

   subExplosion[0] = VapExploSubExplosion1;
   subExplosion[1] = VapExploSubExplosion2;
   subExplosion[2] = VapExploSubExplosion3;

   emitter[0] = VapExploExplosionSmokeEmitter;
   emitter[1] = VapExploCrescentEmitter;

   shakeCamera = true;
   camShakeFreq = "8.0 9.0 7.0";
   camShakeAmp = "100.0 100.0 100.0";
   camShakeDuration = 1.3;
   camShakeRadius = 25.0;
};

//---------------------------------------------------------------------------
// Smoke particles
//---------------------------------------------------------------------------
datablock ParticleData(VapExploSmokeParticle)
{
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = -0.3;   // rises slowly
   inheritedVelFactor   = 0.125;

   lifetimeMS           =  1200;
   lifetimeVarianceMS   =  200;
   useInvAlpha          =  true;
   spinRandomMin        = -100.0;
   spinRandomMax        =  100.0;

   animateTexture = false;

   textureName = "special/Smoke/bigSmoke";

   colors[0] = "0.5 0.1 0.9 1.0";
   colors[1] = "0.5 0.1 0.9 1.0";
   colors[2] = "0.5 0.1 0.9";
   sizes[0]      = 1.0;
   sizes[1]      = 2.0;
   sizes[2]      = 4.5;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(VapExploSmokeEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 3;

   ejectionVelocity = 2.25;
   velocityVariance = 0.55;

   thetaMin         = 0.0;
   thetaMax         = 40.0;

   particles = "VapExploSmokeParticle";
};

datablock TracerProjectileData(VapExploProjectile) {
   doDynamicClientHits = true;

   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.9;
   damageRadius        = 15.0;
   radiusDamageType    = $DamageType::Grenade;
   kickBackStrength    = 2500;

   explosion           = "VapExploExplosion";
   splash              = ChaingunSplash;

   kickBackStrength  = 0.0;
   sound 				= ChaingunProjectile;

   dryVelocity       = 700.0;
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


