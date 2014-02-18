datablock AudioProfile(MultishotChargeFireSound) {
   filename    = "fx/weapons/targetinglaser_paint.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock ParticleData(MultishotParticle) {
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

   colors[0]     = "1.0 0.0 0.0 0.5";
   colors[1]     = "1.0 0.0 0.0 0.3";
   colors[2]     = "0.1 0.0 0.0 0.1";
   sizes[0]      = 1;
   sizes[1]      = 1;
   sizes[2]      = 1;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(MultishotEmitter) {
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

   particles = "MultishotParticle";
};

datablock LinearFlareProjectileData(MultishotArrow) {
   scale               = "1.0 1.0 1.0";
   faceViewer          = false;
   hasDamageRadius     = true;
   indirectDamage      = 0.53;
   damageRadius        = 10.0;
   radiusDamageType    = $DamageType::ManaArrow;

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

   baseEmitter         = MultishotEmitter;
   delayEmitter        = MultishotEmitter;
   bubbleEmitter       = MultishotEmitter;

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

function FireMultishot(%player) {
   //quick charge up
   serverPlay3D(MultishotChargeFireSound, %player.getPosition());
   //fire 3 bursts
   schedule(500, 0, ThreeWideBurst, %player);
   schedule(1250, 0, ThreeWideBurst, %player);
   schedule(2000, 0, ThreeWideBurst, %player);
}

function ThreeWideBurst(%player) {
   %muzzle = %player.getMuzzlePoint($WeaponSlot);
   %mVect = %player.getMuzzleVector($WeaponSlot);
   //three bolts
   %pVec[1] = getWord(%mVect, 0) * mSin(0.5 * 3.1415926) SPC getWord(%mVect, 1) * mCos(0.5 * 3.1415926) SPC getWord(%mVect, 2);
   %pVec[2] = %mVect;
   %pVec[1] = -1 * getWord(%mVect, 0) * mSin(0.5 * 3.1415926) SPC -1 * getWord(%mVect, 1) * mCos(0.5 * 3.1415926) SPC getWord(%mVect, 2);
   for(%i = 1; %i <= 3; %i++) {
      %p = new (LinearFlareProjectile)() {
         dataBlock        = MultishotArrow;
         initialDirection = %pVec[%i];
         initialPosition  = %muzzle;
         sourceObject     = %player;
         damageFactor     = 1;
         sourceSlot       = $WeaponSlot;
      };
      MissionCleanup.add(%p);
   }
}
