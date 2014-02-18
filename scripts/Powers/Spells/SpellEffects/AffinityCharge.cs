//AffinityCharge.cs
//Phantom139

datablock ParticleData(WitchChargeParticle) {
   dragCoefficient = 0;
   gravityCoefficient = 0;
   windCoefficient = 0;
   inheritedVelFactor = 0.2;
   constantAcceleration = -2;
   lifetimeMS = 10000;
   lifetimeVarianceMS = 400;
   useInvAlpha = 0;
   spinRandomMin = -79.0323;
   spinRandomMax = 175.403;
   textureName = "special/cloudflash2.png";
   times[0] = 0;
   times[1] = 0.25;
   times[2] = 1;
   colors[0] = "1 1 1 0.25";
   colors[1] = "1 1 1 0.169";
   colors[2] = "1 1 1 0.411";
   sizes[0] = 0.33;
   sizes[1] = 0.65;
   sizes[2] = 0;
};

datablock ParticleEmitterData(WitchChargeEmitter) {
   ejectionPeriodMS = 5;
   periodVarianceMS = 1;
   ejectionVelocity = 10;
   velocityVariance = 5;
   ejectionOffset =   0;

   lifetimeMS = 0;
   thetaMin = 0;
   thetaMax = 360;
   phiReferenceVel = 0;
   phiVariance = 360;
   overrideAdvances = 0;
   orientParticles= 0;
   orientOnVelocity = 0;
   particles = "WitchChargeParticle";
};

datablock ParticleData(DemonChargeParticle) {
   dragCoefficient = 0;
   gravityCoefficient = 0;
   windCoefficient = 0;
   inheritedVelFactor = 0.2;
   constantAcceleration = -2;
   lifetimeMS = 10000;
   lifetimeVarianceMS = 400;
   useInvAlpha = 0;
   spinRandomMin = -79.0323;
   spinRandomMax = 175.403;
   textureName = "special/cloudflash2.png";
   times[0] = 0;
   times[1] = 0.25;
   times[2] = 1;
   colors[0] = "1 0 0 0.25";
   colors[1] = "1 0 0 0.169";
   colors[2] = "1 1 1 0.411";
   sizes[0] = 0.33;
   sizes[1] = 0.65;
   sizes[2] = 0;
};

datablock ParticleEmitterData(DemonChargeEmitter) {
   ejectionPeriodMS = 5;
   periodVarianceMS = 1;
   ejectionVelocity = 10;
   velocityVariance = 5;
   ejectionOffset =   0;

   lifetimeMS = 0;
   thetaMin = 0;
   thetaMax = 360;
   phiReferenceVel = 0;
   phiVariance = 360;
   overrideAdvances = 0;
   orientParticles= 0;
   orientOnVelocity = 0;
   particles = "DemonChargeParticle";
};

datablock ParticleData(PhantomChargeParticle) {
   dragCoefficient = 0;
   gravityCoefficient = 0;
   windCoefficient = 0;
   inheritedVelFactor = 0.2;
   constantAcceleration = -2;
   lifetimeMS = 10000;
   lifetimeVarianceMS = 400;
   useInvAlpha = 0;
   spinRandomMin = -79.0323;
   spinRandomMax = 175.403;
   textureName = "special/cloudflash2.png";
   times[0] = 0;
   times[1] = 0.25;
   times[2] = 1;
   colors[0] = "0.5 0.1 0.9 0.25";
   colors[1] = "0.5 0.1 0.9 0.169";
   colors[2] = "0.5 0.1 0.9 0.411";
   sizes[0] = 0.33;
   sizes[1] = 0.65;
   sizes[2] = 0;
};

datablock ParticleEmitterData(PhantomChargeEmitter) {
   ejectionPeriodMS = 5;
   periodVarianceMS = 1;
   ejectionVelocity = 10;
   velocityVariance = 5;
   ejectionOffset =   0;

   lifetimeMS = 0;
   thetaMin = 0;
   thetaMax = 360;
   phiReferenceVel = 0;
   phiVariance = 360;
   overrideAdvances = 0;
   orientParticles= 0;
   orientOnVelocity = 0;
   particles = "PhantomChargeParticle";
};
//------------------------------------------------------------

function initAffinityCharge(%player) {
   %player.setRechargeRate(0);
   TakeEnergy(%player.client, FetchPowersEnergyLevel(%player.client));

   doAffinityCharge(%player);
}

function Player::getAEnergy(%this) {
   return %this.affinityEnergy;
}

function Player::takeAEnergy(%this, %amount) {
   if(%amount > %this.getAEnergy()) {
      %amount = %this.getAEnergy();
   }
   
   %this.affinityEnergy -= %amount;
}

function doAffinityCharge(%player) {
   if(!isObject(%player) || %player.getState() $= "Dead") {
      if(isObject(%player.AffinityChargeEmitter)) {
         %player.AffinityChargeEmitter.delete();
      }
      return;
   }
   %client = %player.client;
   %class = %client.slot(%client.slotNum).class;
   %mp = %player.getMuzzlePoint($WeaponSlot);
   
   %aEnergy = %player.getAEnergy();
   if(%aEnergy >= 100) {
      %player.setMoveState(false);
      %player.setRechargeRate(%player.getDatablock().rechargeRate);
      %player.affinityEnergy = 100;
      if(isObject(%player.AffinityChargeEmitter)) {
         %player.AffinityChargeEmitter.delete();
      }
      return;
   }

   if(!isObject(%player.AffinityChargeEmitter)) {
      switch$(AffinityToNormal(%class)) {
         case "Witch":
            %player.AffinityChargeEmitter = createEmitter(%mp, WitchChargeEmitter, "1 0 0");
            MissionCleanup.add(%player.AffinityChargeEmitter);
         case "Demon":
            %player.AffinityChargeEmitter = createEmitter(%mp, DemonChargeEmitter, "1 0 0");
            MissionCleanup.add(%player.AffinityChargeEmitter);
         case "Phantom":
            %player.AffinityChargeEmitter = createEmitter(%mp, PhantomChargeEmitter, "1 0 0");
            MissionCleanup.add(%player.AffinityChargeEmitter);
         case "Hunter":
      }
   }
   else {
      //%player.AffinityChargeEmitter.setPosition(%mp);
   }
   
   %player.setMoveState(true);
   %player.affinityEnergy += 5;
   %player.playShieldEffect("1 1 1");
   
   BottomPrint(%player.client, "Affinity Energy: "@%aEnergy@" / 100", 1, 1);
   
   schedule(275, 0, doAffinityCharge, %player);
}

