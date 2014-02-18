datablock ParticleData(burnParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.1;
   inheritedVelFactor   = 0.1;

   lifetimeMS           = 500;
   lifetimeVarianceMS   = 50;

   textureName          = "special/cloudflash";

   spinRandomMin = -10.0;
   spinRandomMax = 10.0;

   colors[0]     = "1 0.18 0.03 0.4";
   colors[1]     = "1 0.18 0.03 0.3";
   colors[2]     = "1 0.18 0.03 0.0";
   sizes[0]      = 2.0;
   sizes[1]      = 1.0;
   sizes[2]      = 0.8;
   times[0]      = 0.0;
   times[1]      = 0.6;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(burnEmitter) {
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;

   ejectionOffset = 0.2;
   ejectionVelocity = 10.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 10.0;

   particles = "burnParticle";
};

function applyBurn(%source, %obj, %hitCT) {
   if(%obj.antiBurn == 1) {
      return;
   }
   %obj.burnCounter = 0;
   %obj.isBurned = 1;
   if(%hitCT > %obj.maxBurnCount) {
      %obj.maxBurnCount = %hitCT;
   }
   burnLoop(%source, %obj);
}

function burnLoop(%source, %obj) {
   if(%obj.antiBurn == 1) {
      %obj.isBurned = 0;
      %obj.burnCounter = 0;
      %obj.maxBurnCount = 0;
      return;
   }
   %obj.damage(%source, %obj.getposition(), 0.025, $DamageType::Burn);
   %fire = new ParticleEmissionDummy(){
      position = vectoradd(%obj.getPosition(),"0 0 0.5");
      dataBlock = "defaultEmissionDummy";
      emitter = "BurnEmitter";
   };
   MissionCleanup.add(%fire);
   %fire.schedule(150, "delete");
   //
   %obj.burnCounter++;
   if(%obj.burnCounter >= %obj.maxBurnCount) {
      %obj.isBurned = 0;
      %obj.burnCounter = 0;
      %obj.maxBurnCount = 0;
      return;
   }
   else {
      schedule(100, 0, "burnLoop", %source, %obj);
   }
}
