datablock ParticleData(LightningSmokeParticle) {
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = -0.3;   // rises slowly
   inheritedVelFactor   = 0.125;

   lifetimeMS           =  1200;
   lifetimeVarianceMS   =  200;
   useInvAlpha          =  true;
   spinRandomMin        = -100.0;
   spinRandomMax        =  100.0;

   animateTexture = false;

   textureName = "special/textures/shockLightning";

   colors[0] = "0 0 1 1.0";
   colors[1] = "0 0 1 1.0";
   colors[2] = "0 0 1";
   sizes[0]      = 0.3;
   sizes[1]      = 0.7;
   sizes[2]      = 1.5;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(LightningSmokeEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 3;

   ejectionVelocity = 2.25;
   velocityVariance = 0.55;

   thetaMin         = 0.0;
   thetaMax         = 40.0;

   particles = "LightningSmokeParticle";
};

datablock GrenadeProjectileData(LightningBoltProjo) {
   projectileShapeName = "mortar_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.8;
   damageRadius        = 15.0;
   radiusDamageType    = $DamageType::Lightning;
   kickBackStrength    = 150;

   velInheritFactor    = 0.5;

   baseEmitter         = LightningSmokeEmitter;

   grenadeElasticity = 0.15;
   grenadeFriction   = 0.4;
   armingDelayMS     = 100;
   muzzleVelocity    = 63.7;
   drag              = 0.1;

   sound			 = LightningHitSound;

   hasLight    = true;
   lightRadius = 4;
   lightColor  = "0.05 0.2 0.05";

   hasLightUnderwaterColor = true;
   underWaterLightColor = "0.05 0.075 0.2";

};

function DoLightning(%source, %targetPosition) {
   %ThunderSound[0] = thunderCrash1;
   %ThunderSound[1] = thunderCrash2;
   %ThunderSound[2] = thunderCrash3;
   %ThunderSound[3] = thunderCrash4;
   %ThunderSoundCount = 4;
   
   for(%i = 0; %i < 10; %i++) {
      schedule(375*%i, 0, "Serverplay3D", %ThunderSound[mFloor(getRandom() * %ThunderSoundCount)], %targetPosition);
      %pos1 = vectorAdd(%targetPosition, "0 0 375");
      %pos2 = vectorAdd(%pos1, GetRandomPosition(15, 1));
      %final = vectorAdd(%pos2, "0 0 "@%i * 75@"");
      %p = new (GrenadeProjectile)() {
         dataBlock        = LightningBoltProjo;
         initialDirection = "0 0 -3";
         initialPosition  = %final;
         damageFactor     = 1;
      };
      MissionCleanup.add(%p);
      %p.sourceObject = %source; //hacky way of spawning airborne projectiles
   }
}
