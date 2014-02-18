datablock ParticleData(EnviousDownpourBaseParticle) {
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

   colors[0]     = "0 128 0 1.0";
   colors[1]     = "0 128 0 1.0";
   colors[2]     = "1.0 0.25 0.1 0.0";

   sizes[0]      = 10.5;
   sizes[1]      = 10.7;
   sizes[2]      = 11.0;

   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(EnviousDownpourBaseEmitter) {
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.5;
   velocityVariance = 0.3;

   thetaMin         = 0.0;
   thetaMax         = 30.0;

   particles = "EnviousDownpourBaseParticle";
};

datablock ParticleData(EnviousDownpourExplosionParticle) {
   dragCoefficient      = 2;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 10000;
   lifetimeVarianceMS   = 0;
   textureName          = "special/cloudflash";
   colors[0]     = "1 0.18 0.03 0.6";
   colors[1]     = "1 0.18 0.03 0.0";
   sizes[0]      = 12;
   sizes[1]      = 12.5;
};

datablock ParticleEmitterData(EnviousDownpourExplosionEmitter) {
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionOffset = 2.0;
   ejectionVelocity = 4.0;
   velocityVariance = 0.0;
   thetaMin         = 60.0;
   thetaMax         = 90.0;
   lifetimeMS       = 6000;

   particles = "EnviousDownpourExplosionParticle";
};

datablock ExplosionData(EnviousDownpourExplosion) {
   particleEmitter = EnviousDownpourExplosionEmitter;
   particleDensity = 5;
   particleRadius = 2.0;
   faceViewer = true;
};

datablock LinearFlareProjectileData(EnviousDownpourFirst) {
   projectileShapeName = "turret_muzzlepoint.dts";
   scale               = "1.0 1.0 1.0";
   faceViewer          = true;
   hasDamageRadius     = true;
   indirectDamage      = 0.55;
   damageRadius        = 25.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::EnviousDownpour;

   explosion           = "EnviousDownpourExplosion";
   splash              = PlasmaSplash;

   baseEmitter        = EnviousDownpourBaseEmitter;

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
   lightRadius = 10.0;
   lightColor  = "0.94 0.03 0.12";
};

datablock LinearFlareProjectileData(EnviousDownpour2) {
   projectileShapeName = "turret_muzzlepoint.dts";
   scale               = "1.0 1.0 1.0";
   faceViewer          = true;
   hasDamageRadius     = true;
   indirectDamage      = 0.55;
   damageRadius        = 25.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::EnviousDownpour;

   explosion           = "EnviousDownpourExplosion";
   splash              = PlasmaSplash;

   baseEmitter        = EnviousDownpourBaseEmitter;

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
   lightRadius = 10.0;
   lightColor  = "0.94 0.03 0.12";
};

function EnviousDownpourFirst::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   doEnviousDownpourLingerDamage(%proj.sourceObject, %pos, 35, 0);
}

function doEnviousDownpourLingerDamage(%source, %position, %radius, %counter) {
   if(%counter > 60) {
      return;
   }
   %counter++;
   InitContainerRadiusSearch(%position, %radius, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      %ply.damage(%source, %ply.getposition(), (0.005), $DamageType::EnviousDownpour);
   }
   schedule(100, 0, "doEnviousDownpourLingerDamage", %source, %position, %radius, %counter);
}

function DoEnviousDownpour(%source, %targetPosition) {
   %pos = vectorAdd(%targetPosition, "0 0 300");
   %final = vectorAdd(%pos, "0 0 "@%i * 10@"");
   %p = new (LinearFlareProjectile)() {
      dataBlock        = EnviousDownpourFirst;
      initialDirection = "0 0 -5";
      initialPosition  = %final;
      damageFactor     = 1;
   };
   %p.sourceObject = %source; //hacky way of spawning airborne projectiles
   for(%i = 0; %i < 15; %i++) {
      %pos = vectorAdd(%targetPosition, "0 0 300");
      %final = vectorAdd(%pos, "0 0 "@%i * 10@"");
      %p = new (LinearFlareProjectile)() {
         dataBlock        = EnviousDownpour2;
         initialDirection = "0 0 -5";
         initialPosition  = %final;
         damageFactor     = 1;
      };
      MissionCleanup.add(%p);
      %p.sourceObject = %source; //hacky way of spawning airborne projectiles
   }
}
