datablock ParticleData(StrikeDownArrowParticle) {
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

   textureName = "special/cloudflash";//Ret_RepairGun";

   colors[0]     = "250 152 6 0.5";
   colors[1]     = "250 152 6 0.3";
   colors[2]     = "250 152 6 0.1";
   sizes[0]      = 1;
   sizes[1]      = 1;
   sizes[2]      = 1;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(StrikeDownArrowEmitter) {
	lifetimeMS    = -1;
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 0.15;
	velocityVariance = 0.0;
	ejectionoffset = 0;
	thetaMin     = 0.0;
	thetaMax     = 0.0;

	orientParticles = false;
	orientOnVelocity = false;

   particles = "StrikeDownArrowParticle";
};

datablock LinearFlareProjectileData(StrikeDownArrow) {
   scale               = "1.0 1.0 1.0";
   faceViewer          = false;
   directDamage        = 0.75;
   hasDamageRadius     = true;
   indirectDamage      = 0.95;
   damageRadius        = 17.5;
   directDamageType    = $DamageType::StrikeDown;
   radiusDamageType    = $DamageType::StrikeDown;

   explosion           = SatchelMainExplosion;
   splash              = PlasmaSplash;

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

   baseEmitter         = StrikeDownArrowEmitter;
   delayEmitter        = StrikeDownArrowEmitter;
   bubbleEmitter       = StrikeDownArrowEmitter;

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

function StrikeDownArrow::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);

   //---------------------------------------------
   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 12.5, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      applyBurn(%proj.sourceObject, %ply, 50);
   }
}
