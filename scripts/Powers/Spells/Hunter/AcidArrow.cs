datablock ParticleData(AcidArrowExplosionParticle) {
   dragCoefficient      = 2;
   gravityCoefficient   = 0.2;
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

datablock ParticleEmitterData(AcidArrowExplosionEmitter) {
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionOffset = 2.0;
   ejectionVelocity = 4.0;
   velocityVariance = 0.0;
   thetaMin         = 60.0;
   thetaMax         = 90.0;
   lifetimeMS       = 6000;

   particles = "AcidArrowExplosionParticle";
};

datablock ExplosionData(AcidArrowExplosion) {
   particleEmitter = AcidArrowExplosionEmitter;
   particleDensity = 3;
   particleRadius = 2.0;
   faceViewer = true;
};

datablock ParticleData(AcidArrowParticle) {
   dragCoeffiecient     = 2.0;
   gravityCoefficient   = 0.0;   // rises slowly
   inheritedVelFactor   = 0.0;
   windCoeffiecient     = 0.0;

   lifetimeMS           =  1400;
   lifetimeVarianceMS   =  0;
   useInvAlpha          =  false;
   spinRandomMin        = -150.0;
   spinRandomMax        = 360.0;

   animateTexture = false;

   textureName = "skins/jetflare00";//Ret_RepairGun";

   colors[0]     = "0.0 1.0 0.0 0.5";
   colors[1]     = "0.0 1.0 0.0 0.3";
   colors[2]     = "0.0 0.1 0.0 0.1";
   sizes[0]      = 1;
   sizes[1]      = 1;
   sizes[2]      = 1;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(AcidArrowSmokeEmitter) {
	lifetimeMS    = -1;
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 0.1;
	velocityVariance = 0.0;
	ejectionoffset = 0;
	thetaMin     = 0.0;
	thetaMax     = 0.0;

	orientParticles = false;
	orientOnVelocity = false;

   particles = "AcidArrowParticle";
};

datablock LinearFlareProjectileData(AcidArrow) {
   scale               = "1.0 1.0 1.0";
   faceViewer          = false;
   directDamage        = 0.5;
   hasDamageRadius     = true;
   indirectDamage      = 0.4;
   damageRadius        = 10.0;
   radiusDamageType    = $DamageType::Toxic;
   directDamageType    = $DamageType::Toxic;

   splash              = PlasmaSplash;
   explosion           = "AcidArrowExplosion";

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

   baseEmitter         = AcidArrowSmokeEmitter;
   delayEmitter        = AcidArrowSmokeEmitter;
   bubbleEmitter       = AcidArrowSmokeEmitter;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.2;
   size[2]           = 0.2;

   numFlares         = 15;
   flareColor        = "0 1 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound        = MissileProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 1 0";
};

function AcidArrow::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   
   //---------------------------------------------
   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 5.0, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      if(getRandom(0, 10) > 6)  {
         applytoxic(%proj.sourceObject, %ply, 250);
      }
   }
}
