datablock ParticleData(BurstTrapSpearParticle) {
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

   colors[0]     = "176 176 176 0.5";
   colors[1]     = "176 176 176 0.3";
   colors[2]     = "176 176 176 0.1";
   sizes[0]      = 1;
   sizes[1]      = 1;
   sizes[2]      = 1;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(BurstTrapSpearEmitter) {
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

   particles = "BurstTrapSpearParticle";
};


datablock SeekerProjectileData(BurstTrap_Single) {
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 0.6;
   damageRadius        = 4.0;
   radiusDamageType    = $DamageType::SpearShower;
   kickBackStrength    = 2000;

   explosion           = "MissileExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;

   baseEmitter         = BurstTrapSpearEmitter;

   lifetimeMS          = 30000;
   muzzleVelocity      = 10.0;
   maxVelocity         = 85.0;
   turningSpeed        = 110.0;
   acceleration        = 350.0;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 550;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = false;
};

function BurstTrap_Single::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);

   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 3.5, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      %ply.setMoveState(true);
      %ply.schedule(3000, setMoveState, false);
      if(!%ply.client.isAIControlled()) {
         MessageClient(%ply.client, 'msgTrap', "\c2The spear shard stuns you.");
         schedule(3000, 0, MessageClient, %ply.client, 'msgTrap', "\c2The stun effect wears off.");
      }
   }
}

function BurstTrapExplode(%trap) {
   %position = %trap.getPosition();
   %sourceCL = %trap.theClient;
   
   %spawnPos = vectorAdd(%position, "0 0 0.5");
   %counter = 0;
   
   //This for loop creates all vectors along the unit circle in 30 deg. intervals
   for(%x = 0; %x < 360; %x += 30) {
      %y = (90 * mCeil(%x / 90)) - %x;
      
      %vec = mCos(mDegToRad(%x)) SPC mSin(mDegToRad(%y)) SPC 0;
      %p = new (SeekerProjectile)() {
         dataBlock        = BurstTrap_Single;
         initialDirection = %vec;
         initialPosition  = %spawnPos;
         damageFactor     = 1;
      };
      %p.sourceObject = %sourceCL.player;
      MissionCleanup.add(%p);
   }
}
