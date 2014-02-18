datablock ParticleData(IceRushSmokeParticle)
{
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = -0.3;   // rises slowly
   inheritedVelFactor   = 0.125;

   lifetimeMS           =  1200;
   lifetimeVarianceMS   =  200;
   useInvAlpha          =  true;
   spinRandomMin        = -100.0;
   spinRandomMax        =  100.0;

   animateTexture = false;

   textureName = "special/Smoke/bigSmoke";

   colors[0] = "0 0 1 1.0";
   colors[1] = "0 0 1 1.0";
   colors[2] = "0 0 1 1.0";
   sizes[0]      = 1.0;
   sizes[1]      = 2.0;
   sizes[2]      = 4.5;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(IceRushSmokeEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 3;

   ejectionVelocity = 2.25;
   velocityVariance = 0.55;

   thetaMin         = 0.0;
   thetaMax         = 40.0;

   particles = "IceRushSmokeParticle";
};

datablock SeekerProjectileData(IceRushMissile)
{
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 0.33;
   damageRadius        = 10.0;
   radiusDamageType    = $DamageType::IceRush;
   kickBackStrength    = 2000;

   explosion           = "MissileExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
                                 // is cranked up to full so the missile doesn't start
                                 // out behind the player when the player is moving
                                 // very quickly - bramage

   baseEmitter         = IceRushSmokeEmitter;
   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = 20000; // z0dd - ZOD, 4/14/02. Was 6000
   muzzleVelocity      = 10.0;
   maxVelocity         = 400.0; // z0dd - ZOD, 4/14/02. Was 80.0
   turningSpeed        = 23.0;
   acceleration        = 125.0;

   proximityRadius     = 2.5;

   terrainAvoidanceSpeed = 10;
   terrainScanAhead      = 7;
   terrainHeightFail     = 1;
   terrainAvoidanceRadius = 3;

   flareDistance = 40;
   flareAngle    = 20;
   minSeekHeat   = 0.0;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 250;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = false;
};

function IceRushMissile::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   //radius freeze
   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 10.0, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      FreezeObject(%ply, 5000, true);
   }
}
