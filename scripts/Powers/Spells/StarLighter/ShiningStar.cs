datablock ParticleData(ShiningStarProjoParticle) {
   dragCoefficient      = 5;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = -1.3;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 150;
   textureName          = "special/cloudflash";
   useInvAlpha          =  false;
   colors[0]     = "1 1 1";
   colors[1]     = "1 1 1";
   colors[2]     = "1 1 1";
   sizes[0]      = 3.501;
   sizes[1]      = 4.001;
   sizes[2]      = 5.001;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ShiningStarProjoEmitter) {
   ejectionPeriodMS = 15;
   periodVarianceMS = 5;

   ejectionVelocity = 42.7;  // A little oomph at the back end
   velocityVariance = 0.0;
   ejectionoffset = 0;
   thetaMin         = 0.0;
   thetaMax         = 180.0;
   phiReferenceVel = 0;
   phiVariance = "360";
   particles = "ShiningStarProjoParticle";
   overrideAdvances = true;
   orientParticles  = true;
};

datablock LinearFlareProjectileData(ShiningStar_StarProjo) {
   directDamage        = 5.0; //direct hit = bye bye :D
   hasDamageRadius     = true;
   indirectDamage      = 0.9;
   damageRadius        = 10.0;
   kickBackStrength    = 100.0;
   directDamageType    = $DamageType::StarRadiation;
   indirectDamageType  = $DamageType::StarRadiation;
   
   kickBackStrength    = 0.0;
   bubbleEmitTime      = 1.0;

   sound = MortarProjectileSound;
   velInheritFactor    = 0.5;

   explosion           = "VehicleBombExplosion";
   splash              = BlasterSplash;

   grenadeElasticity = 0.998;
   grenadeFriction   = 0.0;
   armingDelayMS     = 500;

   muzzleVelocity    = 100.0;

   drag = 0.05;

   gravityMod        = 0.0;

   dryVelocity       = 100.0;
   wetVelocity       = 80.0;

   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 6000;

   lifetimeMS     = 30000;

   scale             = "7 7 7";
   numFlares         = 48;
   flareColor        = "255 255 255";
   flareModTexture   = "special/shrikeBoltCross";
   flareBaseTexture  = "special/shrikeBolt";
};

datablock LinearFlareProjectileData(ShiningStar_Boom) {
   directDamage        = 5.0; //direct hit = bye bye :D
   hasDamageRadius     = true;
   indirectDamage      = 0.9;
   damageRadius        = 25.0;
   kickBackStrength    = 100.0;
   directDamageType    = $DamageType::StarRadiation;
   indirectDamageType  = $DamageType::StarRadiation;

   kickBackStrength    = 0.0;
   bubbleEmitTime      = 1.0;

   sound = MortarProjectileSound;
   velInheritFactor    = 0.5;

   explosion           = "VehicleBombExplosion";
   splash              = BlasterSplash;

   grenadeElasticity = 0.998;
   grenadeFriction   = 0.0;
   armingDelayMS     = 500;

   muzzleVelocity    = 100.0;

   drag = 0.05;

   gravityMod        = 0.0;

   dryVelocity       = 100.0;
   wetVelocity       = 80.0;

   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 6000;

   lifetimeMS     = 30000;

   scale             = "7 7 7";
   numFlares         = 48;
   flareColor        = "255 255 255";
   flareModTexture   = "special/shrikeBoltCross";
   flareBaseTexture  = "special/shrikeBolt";
};

function ShiningStar_StarProjo::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   engageMiniStar(%proj.sourceObject, %pos, 0);
}

function engageMiniStar(%source, %position, %counter) {
   if(%counter > 100) {
      nthExplosion(%source, %position, ShiningStar_Boom);
      return;
   }
   %counter++;
   if(%counter % 10 == 0 || %counter == 1 && %counter < 100) {
      %star = new ParticleEmissionDummy(){
         position = vectoradd(%position, "0 0 0.5");
         dataBlock = "defaultEmissionDummy";
         emitter = "ShiningStarProjoEmitter";
      };
      MissionCleanup.add(%star);
      %star.schedule(1000, "delete");
   }
   AOEDamage(%source, %position, 20, 0.05, $DamageType::StarRadiation);

   schedule(100, 0, "engageMiniStar", %source, %position, %counter);
}
