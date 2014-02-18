datablock ParticleData(InvinciblePierParticle) {
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.1;
   inheritedVelFactor   = 0.1;

   lifetimeMS           = 500;
   lifetimeVarianceMS   = 50;

   textureName          = "special/cloudflash";

   spinRandomMin = -10.0;
   spinRandomMax = 10.0;

   colors[0]     = "1 0.18 0.03 0.4";
   colors[1]     = "0 134 139 0.3";
   colors[2]     = "1 0.18 0.03 0.0";
   sizes[0]      = 2.0;
   sizes[1]      = 1.0;
   sizes[2]      = 0.8;
   times[0]      = 0.0;
   times[1]      = 0.6;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(InvinciblePierEmitter) {
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;

   ejectionOffset = 0.2;
   ejectionVelocity = 10.0;
   velocityVariance = 0.0;

   thetaMin         = 0.0;
   thetaMax         = 10.0;

   particles = "InvinciblePierParticle";
};

function makeObjectInvincible(%obj, %duration) {
   if(!%obj.isAlive()) {
      return;
   }
   //
   cancel(%obj.invincibleCancelLoop);
   //
   messageClient(%obj.client, 'msgClient', "\c0You are now invincible.");
   %obj.isPlayerInvincible = true;
   DoInvinciblePier(%obj);
   %obj.invincibleCancelLoop = schedule((1000 * %duration), 0, "clearInvincibility", %obj);
}

function clearInvincibility(%obj) {
   %obj.isPlayerInvincible = false;
   messageClient(%obj.client, 'msgClient', "\c0You are no longer invincible.");
}

function DoInvinciblePier(%obj) {
   if(!%obj.isAlive()) {
      return;
   }
   //
   if(!%obj.isPlayerInvincible) {
      return;
   }
   //
   %fire = new ParticleEmissionDummy(){
      position = vectoradd(%obj.getPosition(),"0 0 0.5");
      dataBlock = "defaultEmissionDummy";
      emitter = "InvinciblePierEmitter";
   };
   MissionCleanup.add(%fire);
   %fire.schedule(150, "delete");
   //
   schedule(100, 0, "DoInvinciblePier", %obj);
}
