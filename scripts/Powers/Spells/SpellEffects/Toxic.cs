datablock ParticleData(toxicParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.1;
   inheritedVelFactor   = 0.1;

   lifetimeMS           = 500;
   lifetimeVarianceMS   = 50;

   textureName          = "special/cloudflash";

   spinRandomMin = -10.0;
   spinRandomMax = 10.0;

   colors[0]     = "0 1 0 0.4";
   colors[1]     = "0 1 0 0.3";
   colors[2]     = "0 1 0 0.0";
   sizes[0]      = 1.3;
   sizes[1]      = 1.0;
   sizes[2]      = 0.8;
   times[0]      = 0.0;
   times[1]      = 0.6;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(toxicEmitter) {
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;

   ejectionOffset = 0.2;
   ejectionVelocity = 10.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 10.0;

   particles = "toxicParticle";
};

function applytoxic(%source, %obj, %hitCT) {
   if(!%source.isPlayer()) {
      return;
   }
   if(%obj.antitoxic == 1) {
      return;
   }
   %obj.toxicCounter = 0;
   %obj.istoxiced = 1;
   if(%hitCT > %obj.maxtoxicCount) {
      %obj.maxtoxicCount = %hitCT;
   }
   toxicLoop(%source, %obj);
}

function toxicLoop(%source, %obj) {
   if(%obj.antitoxic == 1) {
      %obj.istoxiced = 0;
      %obj.toxicCounter = 0;
      %obj.maxtoxicCount = 0;
      return;
   }
   %obj.damage(%source, %obj.getposition(), 0.01, $DamageType::toxic);
   %fire = new ParticleEmissionDummy(){
      position = vectoradd(%obj.getPosition(),"0 0 0.5");
      dataBlock = "defaultEmissionDummy";
      emitter = "toxicEmitter";
   };
   MissionCleanup.add(%fire);
   %fire.schedule(150, "delete");
   //
   %obj.toxicCounter++;
   if(%obj.toxicCounter >= %obj.maxtoxicCount) {
      %obj.istoxiced = 0;
      %obj.toxicCounter = 0;
      %obj.maxtoxicCount = 0;
      return;
   }
   else {
      schedule(100, 0, "toxicLoop", %source, %obj);
   }
}
