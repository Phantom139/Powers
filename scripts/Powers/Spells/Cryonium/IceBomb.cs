datablock ParticleData(IceBombExplosionParticle)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 900;
   lifetimeVarianceMS   = 225;
   textureName          = "particleTest";
   colors[0]     = "0 1 1 1.0";
   colors[1]     = "0 1 1 0.0";
   sizes[0]      = 3;
   sizes[1]      = 5;
};

datablock ParticleEmitterData(IceBombExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 32;
   velocityVariance = 10;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "IceBombExplosionParticle";
};

datablock ShockwaveData(IceBombShockwave)
{
   width = 6.0;
   numSegments = 32;
   numVertSegments = 6;
   velocity = 16.0;
   acceleration = 40.0;
   lifetimeMS = 650;
   height = 1.0;
   verticalCurve = 0.5;
   is2D = false;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "0 1 1 0.50";
   colors[1] = "0 1 1 0.25";
   colors[2] = "0 0 1 0.0";

   mapToTerrain = true;
   orientToNormal = false;
   renderBottom = false;
};

datablock ExplosionData(IceBombSubExplosion1)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;

   delayMS = 50;

   offset = 5.0;

   playSpeed = 1.5;

   sizes[0] = "1.5 1.5 1.5";
   sizes[1] = "3.0 3.0 3.0";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(IceBombSubExplosion2)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;

   delayMS = 100;

   offset = 7.0;

   playSpeed = 1.1;

   sizes[0] = "5.0 5.0 5.0";
   sizes[1] = "8.0 8.0 8.0";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock ExplosionData(IceBombSubExplosion3)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;

   delayMS = 0;

   offset = 0.0;

   playSpeed = 0.9;


   sizes[0] = "7.0 7.0 7.0";
   sizes[1] = "10.0 10.0 10.0";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(IceBombExplosion)
{
   soundProfile   = BombExplosionSound;
   particleEmitter = IceBombExplosionEmitter;
   particleDensity = 250;
   particleRadius = 1.25;
   faceViewer = true;

   shockwave = IceBombShockwave;
   shockwaveOnTerrain = true;

   subExplosion[0] = IceBombSubExplosion1;
   subExplosion[1] = IceBombSubExplosion2;
   subExplosion[2] = IceBombSubExplosion3;

   shakeCamera = true;
   camShakeFreq = "5.0 7.0 5.0";
   camShakeAmp = "80.0 80.0 80.0";
   camShakeDuration = 1.0;
   camShakeRadius = 30.0;
};

datablock BombProjectileData(IceBomb)
{
   projectileShapeName  = "bomb.dts";
   emitterDelay         = -1;
   directDamage         = 0.0;
   hasDamageRadius      = true;
   indirectDamage       = 0.44;
   damageRadius         = 30;
   radiusDamageType     = $DamageType::BomberBombs;
   kickBackStrength     = 2500;

   explosion            = "IceBombExplosion";
   velInheritFactor     = 1.0;

   grenadeElasticity    = 0.25;
   grenadeFriction      = 0.4;
   armingDelayMS        = 2000;
   muzzleVelocity       = 0.1;
   drag                 = 0.3;

   minRotSpeed          = "60.0 0.0 0.0";
   maxRotSpeed          = "80.0 0.0 0.0";
   scale                = "2.0 2.0 2.0";

   sound                = BomberBombProjectileSound;
};

function IceBomb::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   //radius freeze
   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 30.0, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      if(CanAOEHit(%pos, 30, %ply)) {
         FreezeObject(%ply, 7500, false);
      }
   }
}

function DoIceBomb(%source, %targetPosition) {
   %pos = vectorAdd(%targetPosition, "0 0 200");
   %p = new (BombProjectile)() {
      dataBlock        = IceBomb;
      initialDirection = "0 0 -5";
      initialPosition  = %pos;
      damageFactor     = 1;
   };
   MissionCleanup.add(%p);
   %p.sourceObject = %source; //hacky way of spawning airborne projectiles
}
