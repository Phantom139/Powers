datablock ParticleData(SunStormBaseParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 3;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 8000;
   lifetimeVarianceMS   = 500;

   useInvAlpha = false;
   spinRandomMin = -1.0;
   spinRandomMax = 1.0;

   animateTexture = true;
   framesPerSec = 15;

   textureName = "special/cloudflash";

   colors[0]     = "1.0 0.6 0.4 1.0";
   colors[1]     = "54 100 139 1.0";
   colors[2]     = "1.0 0.25 0.1 0.0";

   sizes[0]      = 10.5;
   sizes[1]      = 10.7;
   sizes[2]      = 11.0;

   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(SunStormBaseEmitter) {
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.5;
   velocityVariance = 0.3;

   thetaMin         = 0.0;
   thetaMax         = 30.0;

   particles = "SunStormBaseParticle";
};

datablock ParticleData(SunStormExplosionParticle) {
   dragCoefficient      = 2;
   gravityCoefficient   = -0.05;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 10000;
   lifetimeVarianceMS   = 0;
   textureName          = "special/cloudflash";
   colors[0]     = "1 0.18 0.03 0.6";
   colors[1]     = "1 0.18 0.03 0.0";
   sizes[0]      = 35;
   sizes[1]      = 38.5;
};

datablock ParticleEmitterData(SunStormExplosionEmitter) {
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionOffset = 2.0;
   ejectionVelocity = 4.0;
   velocityVariance = 0.0;
   thetaMin         = 60.0;
   thetaMax         = 90.0;
   lifetimeMS       = 6000;

   particles = "SunStormExplosionParticle";
};

datablock ShockwaveData(SunStormShockwave) {
   width = 10.0;
   numSegments = 32;
   numVertSegments = 6;
   velocity = 10;
   acceleration = 20.0;
   lifetimeMS = 3000;
   height = 3.0;
   verticalCurve = 0.5;
   is2D = false;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0]     = "1.0 0.6 0.4 1.0";
   colors[1]     = "54 100 139 1.0";
   colors[2]     = "1.0 0.25 0.1 0.0";

   mapToTerrain = true;
   orientToNormal = false;
   renderBottom = false;
};

datablock ParticleData( SunStormCrescentParticle ) {
   dragCoefficient      = 2;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 000;
   textureName          = "special/crescent3";
   colors[0]     = "1.0 0.6 0.4 1.0";
   colors[1]     = "54 100 139 1.0";
   colors[2]     = "1.0 0.25 0.1 0.0";
   sizes[0]      = 4.0;
   sizes[1]      = 8.0;
   sizes[2]      = 9.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( SunStormCrescentEmitter )
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
   particles = "SunStormCrescentParticle";
};


datablock ExplosionData(SunStormExplosion) {
   soundProfile   = BombExplosionSound;

   shockwave = SunStormShockwave;
   shockwaveOnTerrain = true;

   emitter[0] = SunStormExplosionEmitter;
   emitter[1] = MortarCrescentEmitter;
   
   particleDensity = 2;
   particleRadius = 15.0;
   faceViewer = true;
};

datablock LinearFlareProjectileData(SunStormFirst) {
   projectileShapeName = "turret_muzzlepoint.dts";
   scale               = "1.0 1.0 1.0";
   faceViewer          = true;
   hasDamageRadius     = true;
   indirectDamage      = 0.5;
   damageRadius        = 50.0;
   kickBackStrength    = 1250.0;
   radiusDamageType    = $DamageType::SunRay;

   explosion           = "SunStormExplosion";
   splash              = PlasmaSplash;

   baseEmitter        = SunStormBaseEmitter;

   dryVelocity       = 350.0; // z0dd - ZOD, 7/20/02. Faster plasma projectile. was 55
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 10000;
   lifetimeMS        = 11000;
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
   flareColor        = "1 0.18 0.03";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = FlamethrowerFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 50.0;
   lightColor  = "0.94 0.03 0.12";
};

function SunStormFirst::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   //boom!
   //radius burn
   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 25.0, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      if(CanAOEHit(%pos, 25, %ply)) {
         applyBurn(%proj.sourceObject, %ply, 50);
      }
   }
   doSunStormLingerDamage(%proj.sourceObject, %pos, 25, 0);
}

function doSunStormLingerDamage(%source, %position, %radius, %counter) {
   if(%counter > 60) {
      return;
   }
   %counter++;
   InitContainerRadiusSearch(%position, %radius, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      //%ply.damage(%source, %ply.getposition(), (0.005), $DamageType::SunRay);
      if(CanAOEHit(%position, %radius, %ply)) {
         applyBurn(%source, %ply, 15);
      }
   }
   schedule(100, 0, "doSunStormLingerDamage", %source, %position, %radius, %counter);
}

function DoSunStorm(%source, %targetPosition) {
   %pos = vectorAdd(%targetPosition, "0 0 500");
   %final = vectorAdd(%pos, "0 0 "@%i * 10@"");
   %p = new (LinearFlareProjectile)() {
      dataBlock        = SunStormFirst;
      initialDirection = "0 0 -5";
      initialPosition  = %final;
      damageFactor     = 1;
   };
   %p.sourceObject = %source; //hacky way of spawning airborne projectiles
}
