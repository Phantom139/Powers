// JTLmeteorStorm.cs
//
// This script (C) 2002 by JackTL
//
// Use, modify, but give credit

datablock ParticleData(JTLMeteorStormFireballParticle) {
	dragCoeffiecient     = 0.0;
	gravityCoefficient   = -0.2;
	inheritedVelFactor   = 0.0;

	lifetimeMS           = 700;
	lifetimeVarianceMS   = 0;

	textureName          = "particleTest";

	useInvAlpha = false;
	spinRandomMin = -160.0;
	spinRandomMax = 160.0;

	animateTexture = true;
	framesPerSec = 15;

	animTexName[0]       = "special/Explosion/exp_0016";
	animTexName[1]       = "special/Explosion/exp_0018";
	animTexName[2]       = "special/Explosion/exp_0020";
	animTexName[3]       = "special/Explosion/exp_0022";
	animTexName[4]       = "special/Explosion/exp_0024";
	animTexName[5]       = "special/Explosion/exp_0026";
	animTexName[6]       = "special/Explosion/exp_0028";
	animTexName[7]       = "special/Explosion/exp_0030";
	animTexName[8]       = "special/Explosion/exp_0032";

	colors[0]     = "1.0 0.7 0.5 1.0";
	colors[1]     = "1.0 0.5 0.2 1.0";
	colors[2]     = "1.0 0.25 0.1 0.0";
	sizes[0]      = 19.0;
	sizes[1]      = 11.0;
	sizes[2]      = 6.5;
	times[0]      = 0.0;
	times[1]      = 0.2;
	times[2]      = 1.0;
};

datablock ParticleEmitterData(JTLMeteorStormFireballEmitter) {
	ejectionPeriodMS = 5;
	periodVarianceMS = 1;

	ejectionVelocity = 0.25;
	velocityVariance = 0.0;

	thetaMin         = 0.0;
	thetaMax         = 30.0;

	particles = "JTLMeteorStormFireballParticle";
};

//--------------------------------------------------------------------------
// Explosion
//--------------------------------------

datablock ParticleData(MetExplosionSmoke)
{
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = -0.30;   // rises slowly
   inheritedVelFactor   = 0.025;

   lifetimeMS           = 3500;
   lifetimeVarianceMS   = 400;

   useInvAlpha =  true;
   spinRandomMin = -100.0;
   spinRandomMax =  100.0;

   textureName = "special/Smoke/bigSmoke";

   colors[0]     = "0.7 0.7 0.7 1.0";
   colors[1]     = "0.2 0.2 0.2 1.0";
   colors[2]     = "0.2 0.2 0.2 1.0";
   colors[3]     = "0.2 0.2 0.2 1.0";
   colors[4]     = "0.1 0.1 0.1 0.0";
   colors[5]     = "0.1 0.1 0.1 0.0";
   sizes[0]      = 5.0;
   sizes[1]      = 8.0;
   sizes[2]      = 11.0;
   sizes[3]      = 14.0;
   sizes[4]      = 17.0;
   sizes[5]      = 20.0;
   times[0]      = 0.0;
   times[1]      = 0.333;
   times[2]      = 0.666;
   times[3]      = 1.0;
   times[4]      = 1.333;
   times[5]      = 1.666;
};

datablock ParticleEmitterData(MetExplosionSmokeEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;

   ejectionOffset = 10.0;

   ejectionVelocity = 15.0;
   velocityVariance = 5.0;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   lifetimeMS       = 1500;

   particles = "MetExplosionSmoke";
};

datablock ShockwaveData(MetShockwave)
{
   width = 30.0;
   numSegments = 32;
   numVertSegments = 6;
   velocity = 500;
   acceleration = 100.0;
   lifetimeMS = 500;
   height = 12.0;
   verticalCurve = 0.5;
   is2D = false;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "0.4 1.0 0.4 0.50";
   colors[1] = "0.4 1.0 0.4 0.25";
   colors[2] = "0.4 1.0 0.4 0.0";

   mapToTerrain = true;
   orientToNormal = false;
   renderBottom = false;
};

datablock ExplosionData(MetSubExplosion)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;
   delayMS = 0;
   offset = 0.0;
   playSpeed = 0.15;

   sizes[0] = "35.0 35.0 35.0";
   sizes[1] = "35.0 35.0 35.0";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock ExplosionData(MetExplosion)
{
   explosionShape = "effect_plasma_explosion.dts";
   soundProfile   = plasmaExpSound;

   particleEmitter = MetExplosionSmokeEmitter;

   shockwave = MetShockwave;
   shockwaveOnTerrain = false;

   subExplosion[0] = MetSubExplosion;
   particleDensity = 150;
   particleRadius = 1.25;
   faceViewer = true;

   sizes[0] = "55.0 55.0 55.0";
   sizes[1] = "55.0 55.0 55.0";
   times[0] = 0.0;
   times[1] = 1.5;
};

//---------------------------------------
//   Projectile
//---------------------------------------

datablock GrenadeProjectileData(ShadowBrigadeProjectile) {
	projectileShapeName  = "plasmabolt.dts";
	scale                = "40.0 40.0 40.0";
	emitterDelay         = -1;
	directDamage         = 0;
	directDamageType     = $DamageType::ShadowBrigade;
	hasDamageRadius      = true; // true;
	indirectDamage       = 0.4; // 0.5;
	damageRadius         = 35.0;
	radiusDamageType     = $DamageType::ShadowBrigade;
	kickBackStrength     = 1000;
	explosion            = MetExplosion;
	splash               = PlasmaSplash;
	baseEmitter          = JTLMeteorStormFireballEmitter;
	armingDelayMS        = 50;
	grenadeElasticity    = 0.15;
	grenadeFriction      = 0.4;
	drag                 = 0.1;
	gravityMod           = 0.0;
	sound                = GrenadeProjectileSound;

	hasLight    = true;
	lightRadius = 20.0;
	lightColor  = "1 1 0.5";
};

function DoShadowBrigade(%source, %targetPosition) {
   for(%i = 0; %i < 5; %i++) {
      %pos1 = vectorAdd(%targetPosition, "0 0 300");
      %pos2 = vectorAdd(%pos1, GetRandomPosition(15, 1));
      %final = vectorAdd(%pos2, "0 0 "@%i * 30@"");
      %p = new (GrenadeProjectile)() {
         dataBlock        = ShadowBrigadeProjectile;
         initialDirection = "0 0 -2";
         initialPosition  = %final;
         damageFactor     = 1;
      };
      MissionCleanup.add(%p);
      %p.sourceObject = %source; //hacky way of spawning airborne projectiles
   }
}
