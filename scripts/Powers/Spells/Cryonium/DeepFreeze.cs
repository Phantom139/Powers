datablock ParticleData(PrebeamParticle) {
	dragCoefficient = 0;
	gravityCoefficient = -0.9;
	windCoefficient = 0;
	inheritedVelFactor = 0;
	constantAcceleration = 0;
	lifetimeMS = 12000;
	lifetimeVarianceMS = 6000;
	useInvAlpha = 0;
	spinRandomMin = 0;
	spinRandomMax = 0;
	textureName = "special/bluespark.PNG";
	times[0] = 0.75;
	times[1] = 2;
	colors[0] = "0.5 0.5 0.9 1";
	colors[1] = " 0 0 0 0";
	sizes[0] = 1;
	sizes[1] = 1;
};

datablock ParticleEmitterData(PrebeamEmitter) {
	ejectionPeriodMS = 45;
	periodVarianceMS = 10;
	ejectionVelocity = 2;
	velocityVariance = 2;
	ejectionOffset   = 35;
	thetaMin = 140;
	thetaMax = 160;
	phiReferenceVel = 0;
	phiVariance = 360;
	overrideAdvances = 0;
	orientParticles= 1;
	orientOnVelocity = 1;
	particles = "PrebeamParticle";
};

function DoDeepFreeze(%obj, %position) {
   %c = createEmitter(%position,PrebeamEmitter,"1 0 0");      //Rotate it
   MissionCleanup.add(%c); // I think This should be used
   %c.schedule(10000,"delete");
   schedule(10000, 0, "UnleashDeepFreeze", %position);
}

function UnleashDeepFreeze(%position) {
   InitContainerRadiusSearch(%position, 30.0, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      FreezeObject(%ply, 12500, true);
   }
   //
   %Icefield = new ForceFieldBare() {
        rotation = "0 0 0 1";
        scale = "30 30 6";
        position =  vectorAdd(%position, "-15 -15 -2");
        dataBlock = "IceField";
        team = 99;
   };
   MissionCleanup.add(%Icefield);
   %IceField.schedule(3000, "delete");
   if (isObject(%IceField.pzone)) {
      %IceField.pzone.delete();
   }
}
