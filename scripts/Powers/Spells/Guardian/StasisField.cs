datablock ParticleData(StasisField) {
    dragCoefficient = 1;
    gravityCoefficient = 0.2;
    windCoefficient = 1;
    inheritedVelFactor = 0.2;
    constantAcceleration = -0;
    lifetimeMS = 600;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    spinRandomMin = 0;
    spinRandomMax = 0;
    textureName = "special/cloudFlash";

    colors[0]     = "0.1 0.7 10.0 1.0";
    colors[1]     = "0.1 0.7 10.0 1.0";
    colors[2]     = "0.1 0.7 10.0 1.0";
    
    sizes[0]      = 3.0;
    sizes[1]      = 3.0;
    sizes[2]      = 3.0;
    
    times[0]      = 0.0;
    times[1]      = 0.5;
    times[2]      = 1.0;
};

datablock ParticleEmitterData(StasisFieldEmitter) {
    ejectionPeriodMS = 40;
    periodVarianceMS = 0;
    ejectionVelocity = 1;
    velocityVariance = 10;
    ejectionOffset =   25.0;
    thetaMin = 60;
    thetaMax = 80;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
	  // lifeTimeMS = 1000;
    orientParticles= 1;
    orientOnVelocity = 1;

    particles = "StasisField";
};

function initiateStatisField(%obj, %pos) {
   stasisLoop(%obj, %pos, 1);
}

function stasisLoop(%obj, %pos, %counter) {
   if(%counter > 10) {
      return;
   }
   //
   InitContainerRadiusSearch(%pos, 15.0, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      cancel(%ply.stasisClear);
      %ply.zapObject();
      %ply.setMoveState(true);
      %ply.isPlayerInvincible = true;
      %ply.stasisClear = schedule(1010, 0, "clearStasisEffect", %ply);
   }
   //
   %stasis = new ParticleEmissionDummy(){
      position = vectoradd(%pos,"0 0 0.5");
      dataBlock = "defaultEmissionDummy";
      emitter = "StasisFieldEmitter";
   };
   MissionCleanup.add(%stasis);
   %stasis.schedule(1000, "delete");
   //
   %counter++;
   schedule(1000, 0, "stasisLoop", %obj, %pos, %counter);
}

function clearStasisEffect(%ply) {
   %ply.stopZap();
   %ply.setMoveState(false);
   %ply.isPlayerInvincible = false;
}
