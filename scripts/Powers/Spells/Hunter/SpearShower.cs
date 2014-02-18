datablock ParticleData(SpearShowerParticle) {
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

datablock ParticleEmitterData(SpearShowerEmitter) {
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

   particles = "SpearShowerParticle";
};


datablock SeekerProjectileData(SpearShower_Single) {
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

   baseEmitter         = SpearShowerEmitter;

   lifetimeMS          = 30000;
   muzzleVelocity      = 10.0;
   maxVelocity         = 150.0;
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

function SpearShower_Single::onExplode(%data, %proj, %pos, %mod) {
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

function DoSpearShower(%source, %targetPosition) {
   for(%i = 0; %i < 10; %i++) {
      %pos1 = vectorAdd(%targetPosition, "0 0 200");
      %pos2 = vectorAdd(%pos1, GetRandomPosition(7, 1));
      %final = vectorAdd(%pos2, "0 0 "@%i * 25@"");
      %p = new (SeekerProjectile)() {
         dataBlock        = SpearShower_Single;
         initialDirection = "0 0 -1";
         initialPosition  = %final;
         damageFactor     = 1;
      };
      MissionCleanup.add(%p);
      %p.sourceObject = %source; //hacky way of spawning airborne projectiles
   }
}
