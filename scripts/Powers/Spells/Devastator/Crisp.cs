//FIRE BOLT
datablock ParticleData(CrispBaseParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 800;
   lifetimeVarianceMS   = 500;

   useInvAlpha = false;
   spinRandomMin = -160.0;
   spinRandomMax = 160.0;

   animateTexture = true;
   framesPerSec = 15;

   textureName = "special/cloudflash";

   colors[0]     = "1.0 0.6 0.4 1.0";
   colors[1]     = "54 100 139 1.0";
   colors[2]     = "1.0 0.25 0.1 0.0";

   sizes[0]      = 3.5;
   sizes[1]      = 3.7;
   sizes[2]      = 3.0;

   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(CrispBaseEmitter) {
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.5;
   velocityVariance = 0.3;

   thetaMin         = 0.0;
   thetaMax         = 30.0;

   particles = "CrispBaseParticle";
};

datablock ParticleData(CrispExplosionParticle) {
   dragCoefficient      = 2;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 4500;
   lifetimeVarianceMS   = 0;
   textureName          = "special/cloudflash";
   colors[0]     = "1 0.18 0.03 0.6";
   colors[1]     = "1 0.18 0.03 0.0";
   sizes[0]      = 2;
   sizes[1]      = 2.5;
};

datablock ParticleEmitterData(CrispExplosionEmitter) {
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionOffset = 2.0;
   ejectionVelocity = 4.0;
   velocityVariance = 0.0;
   thetaMin         = 60.0;
   thetaMax         = 90.0;
   lifetimeMS       = 4500;

   particles = "CrispExplosionParticle";
};

datablock ExplosionData(CrispExplosion) {
   particleEmitter = CrispExplosionEmitter;
   particleDensity = 75;
   particleRadius = 1.25;
   faceViewer = true;
};

//--------------------------------------------------------------------------
// Explosion
//--------------------------------------

datablock LinearFlareProjectileData(Crisp) {
   projectileShapeName = "turret_muzzlepoint.dts";
   scale               = "1.0 1.0 1.0";
   faceViewer          = true;
   hasDamageRadius     = true;
   indirectDamage      = 0.3;
   damageRadius        = 25.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Crisp;

   explosion           = "CrispExplosion";
   splash              = PlasmaSplash;

   baseEmitter        = CrispBaseEmitter;

   dryVelocity       = 150.0; // z0dd - ZOD, 7/20/02. Faster plasma projectile. was 55
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
   lightRadius = 10.0;
   lightColor  = "0.94 0.03 0.12";
};

function Crisp::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   //radius freeze
   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 15.0, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      if(CanAOEHit(%pos, 15, %ply)) {
         applyBurn(%proj.sourceObject, %ply, 55);
      }
   }
   doCrispLingerDamage(%proj.sourceObject, %pos, 15, 0);
}

function doCrispLingerDamage(%source, %position, %radius, %counter) {
   if(%counter > 55) {
      return;
   }
   %counter++;
   
   AOEDamage(%source, %position, %radius, 0.02, $DamageType::Crisp);

   schedule(100, 0, "doCrispLingerDamage", %source, %position, %radius, %counter);
}
