//----------------------------------------------------------------------------
//----------------------------------------------------------------------------

$InvincibleTime = 6;

// Load dts shapes and merge animations
exec("scripts/light_male.cs");
exec("scripts/medium_male.cs");
exec("scripts/heavy_male.cs");
exec("scripts/light_female.cs");
exec("scripts/medium_female.cs");
exec("scripts/bioderm_light.cs");
exec("scripts/bioderm_medium.cs");
exec("scripts/bioderm_heavy.cs");

$CorpseTimeoutValue = 22 * 1000;

//Damage Rate for entering Liquid
$DamageLava       = 0.0325;
$DamageHotLava    = 0.0325;
$DamageCrustyLava = 0.0325;

$PlayerDeathAnim::TorsoFrontFallForward = 1;
$PlayerDeathAnim::TorsoFrontFallBack = 2;
$PlayerDeathAnim::TorsoBackFallForward = 3;
$PlayerDeathAnim::TorsoLeftSpinDeath = 4;
$PlayerDeathAnim::TorsoRightSpinDeath = 5;
$PlayerDeathAnim::LegsLeftGimp = 6;
$PlayerDeathAnim::LegsRightGimp = 7;
$PlayerDeathAnim::TorsoBackFallForward = 8;
$PlayerDeathAnim::HeadFrontDirect = 9;
$PlayerDeathAnim::HeadBackFallForward = 10;
$PlayerDeathAnim::ExplosionBlowBack = 11;

datablock SensorData(PlayerSensor)
{
   detects = true;
   detectsUsingLOS = true;
   detectsPassiveJammed = true;
   detectRadius = 2000;
   detectionPings = false;
   detectsFOVOnly = true;
   detectFOVPercent = 1.3;
   useObjectFOV = true;
};

//----------------------------------------------------------------------------
datablock AudioProfile(ArmorJetSound)
{
   filename    = "fx/armor/thrust.wav";
   description = CloseLooping3d;
   preload = true;
};

datablock AudioProfile(ArmorWetJetSound)
{
   filename    = "fx/armor/thrust_uw.wav";
   description = CloseLooping3d;
   preload = true;
};

datablock AudioProfile(MountVehicleSound)
{
   filename    = "fx/vehicles/mount_dis.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(UnmountVehicleSound)
{
   filename    = "fx/vehicles/mount.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(CorpseLootingSound)
{
   fileName = "fx/weapons/generic_switch.wav";
   description = AudioClosest3d;
   preload = true;
};
          
datablock AudioProfile(ArmorMoveBubblesSound)
{
   filename    = "fx/armor/bubbletrail2.wav";
   description = CloseLooping3d;
   preload = true;
};

datablock AudioProfile(WaterBreathMaleSound)
{
   filename    = "fx/armor/breath_uw.wav";
   description = ClosestLooping3d;
   preload = true;
};

datablock AudioProfile(WaterBreathFemaleSound)
{
   filename    = "fx/armor/breath_fem_uw.wav";
   description = ClosestLooping3d;
   preload = true;
};

datablock AudioProfile(waterBreathBiodermSound)
{
   filename    = "fx/armor/breath_bio_uw.wav";
   description = ClosestLooping3d;
   preload = true;
};

datablock AudioProfile(SkiAllSoftSound)
{
   filename    = "fx/armor/ski_soft.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(SkiAllHardSound)
{
   filename    = "fx/armor/ski_soft.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(SkiAllMetalSound)
{
   filename    = "fx/armor/ski_soft.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(SkiAllSnowSound)
{
   filename    = "fx/armor/ski_soft.wav";
   description = AudioClosest3d;
   preload = true;
};

//SOUNDS ----- LIGHT ARMOR--------
datablock AudioProfile(LFootLightSoftSound)
{
   filename    = "fx/armor/light_LF_soft.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(RFootLightSoftSound)
{
   filename    = "fx/armor/light_RF_soft.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(LFootLightHardSound)
{
   filename    = "fx/armor/light_LF_hard.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootLightHardSound)
{
   filename    = "fx/armor/light_RF_hard.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(LFootLightMetalSound)
{
   filename    = "fx/armor/light_LF_metal.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootLightMetalSound)
{
   filename    = "fx/armor/light_RF_metal.wav";
   description = AudioClose3d;
   preload = true;
};
datablock AudioProfile(LFootLightSnowSound)
{
   filename    = "fx/armor/light_LF_snow.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(RFootLightSnowSound)
{
   filename    = "fx/armor/light_RF_snow.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(LFootLightShallowSplashSound)
{
   filename    = "fx/armor/light_LF_water.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootLightShallowSplashSound)
{
   filename    = "fx/armor/light_RF_water.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(LFootLightWadingSound)
{
   filename    = "fx/armor/light_LF_wade.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootLightWadingSound)
{
   filename    = "fx/armor/light_RF_wade.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(LFootLightUnderwaterSound)
{
   filename    = "fx/armor/light_LF_uw.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(RFootLightUnderwaterSound)
{
   filename    = "fx/armor/light_RF_uw.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(LFootLightBubblesSound)
{
   filename    = "fx/armor/light_LF_bubbles.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(RFootLightBubblesSound)
{
   filename    = "fx/armor/light_RF_bubbles.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactLightSoftSound)
{
   filename    = "fx/armor/light_land_soft.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactLightHardSound)
{
   filename    = "fx/armor/light_land_hard.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactLightMetalSound)
{
   filename    = "fx/armor/light_land_metal.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactLightSnowSound)
{
   filename    = "fx/armor/light_land_snow.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ImpactLightWaterEasySound)
{
   filename    = "fx/armor/general_water_smallsplash2.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactLightWaterMediumSound)
{
   filename    = "fx/armor/general_water_medsplash.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ImpactLightWaterHardSound)
{
   filename    = "fx/armor/general_water_bigsplash.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(ExitingWaterLightSound)
{
   filename    = "fx/armor/general_water_exit2.wav";
   description = AudioClose3d;
   preload = true;
};
//----------------------------------------------------------------------------
// Splash
//----------------------------------------------------------------------------

datablock ParticleData(PlayerSplashMist)
{
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.05;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.5;
   sizes[2]      = 0.8;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(PlayerSplashMistEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 3.0;
   velocityVariance = 2.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 250;
   particles = "PlayerSplashMist";
};


datablock ParticleData(PlayerBubbleParticle)
{
   dragCoefficient      = 0.0;
   gravityCoefficient   = -0.50;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   textureName          = "special/bubbles";
   colors[0]     = "0.7 0.8 1.0 0.4";
   colors[1]     = "0.7 0.8 1.0 0.4";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.1;
   sizes[1]      = 0.3;
   sizes[2]      = 0.3;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(PlayerBubbleEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 2.0;
   ejectionOffset   = 0.5;
   velocityVariance = 0.5;
   thetaMin         = 0;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "PlayerBubbleParticle";
};

datablock ParticleData(PlayerFoamParticle)
{
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.05;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.7 0.8 1.0 0.20";
   colors[1]     = "0.7 0.8 1.0 0.20";
   colors[2]     = "0.7 0.8 1.0 0.00";
   sizes[0]      = 0.2;
   sizes[1]      = 0.4;
   sizes[2]      = 1.6;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(PlayerFoamEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 3.0;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "PlayerFoamParticle";
};


datablock ParticleData( PlayerFoamDropletsParticle )
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 0;
   textureName          = "special/droplet";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.8;
   sizes[1]      = 0.3;
   sizes[2]      = 0.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( PlayerFoamDropletsEmitter )
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 2;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 60;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   particles = "PlayerFoamDropletsParticle";
};



datablock ParticleData( PlayerSplashParticle )
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.2;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 0;
   textureName          = "special/droplet";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.5;
   sizes[2]      = 0.5;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( PlayerSplashEmitter )
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 3;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 60;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 100;
   particles = "PlayerSplashParticle";
};

datablock SplashData(PlayerSplash)
{
   numSegments = 15;
   ejectionFreq = 15;
   ejectionAngle = 40;
   ringLifetime = 0.5;
   lifetimeMS = 300;
   velocity = 4.0;
   startRadius = 0.0;
   acceleration = -3.0;
   texWrap = 5.0;

   texture = "special/water2";

   emitter[0] = PlayerSplashEmitter;
   emitter[1] = PlayerSplashMistEmitter;

   colors[0] = "0.7 0.8 1.0 0.0";
   colors[1] = "0.7 0.8 1.0 0.3";
   colors[2] = "0.7 0.8 1.0 0.7";
   colors[3] = "0.7 0.8 1.0 0.0";
   times[0] = 0.0;
   times[1] = 0.4;
   times[2] = 0.8;
   times[3] = 1.0;
};

//----------------------------------------------------------------------------
// Jet data
//----------------------------------------------------------------------------
datablock ParticleData(HumanArmorJetParticle)
{
   dragCoefficient      = 0.0;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 100;
   lifetimeVarianceMS   = 0;
   textureName          = "particleTest";
   colors[0]     = "0.32 0.47 0.47 1.0";
   colors[1]     = "0.32 0.47 0.47 0";
   sizes[0]      = 0.40;
   sizes[1]      = 0.15;
};

datablock ParticleEmitterData(HumanArmorJetEmitter)
{
   ejectionPeriodMS = 3;
   periodVarianceMS = 0;
   ejectionVelocity = 3;
   velocityVariance = 2.9;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 5;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "HumanArmorJetParticle";
};

datablock JetEffectData(HumanArmorJetEffect)
{
   texture        = "special/jetExhaust02";
   coolColor      = "0.0 0.0 1.0 1.0";
   hotColor       = "0.2 0.4 0.7 1.0";
   activateTime   = 0.2;
   deactivateTime = 0.05;
   length         = 0.75;
   width          = 0.2;
   speed          = -15;
   stretch        = 2.0;
   yOffset        = 0.2;
};

datablock JetEffectData(HumanMediumArmorJetEffect)
{
   texture        = "special/jetExhaust02";
   coolColor      = "0.0 0.0 1.0 1.0";
   hotColor       = "0.2 0.4 0.7 1.0";
   activateTime   = 0.2;
   deactivateTime = 0.05;
   length         = 0.75;
   width          = 0.2;
   speed          = -15;
   stretch        = 2.0;
   yOffset        = 0.4;
};

datablock JetEffectData(HumanLightFemaleArmorJetEffect)
{
   texture        = "special/jetExhaust02";
   coolColor      = "0.0 0.0 1.0 1.0";
   hotColor       = "0.2 0.4 0.7 1.0";
   activateTime   = 0.2;
   deactivateTime = 0.05;
   length         = 0.75;
   width          = 0.2;
   speed          = -15;
   stretch        = 2.0;
   yOffset        = 0.2;
};

datablock JetEffectData(BiodermArmorJetEffect)
{
   texture        = "special/jetExhaust02";
   coolColor      = "0.0 0.0 1.0 1.0";
   hotColor       = "0.8 0.6 0.2 1.0";
   activateTime   = 0.2;
   deactivateTime = 0.05;
   length         = 0.75;
   width          = 0.2;
   speed          = -15;
   stretch        = 2.0;
   yOffset        = 0.0;
};

//----------------------------------------------------------------------------
// Foot puffs
//----------------------------------------------------------------------------
datablock ParticleData(LightPuff)
{
   dragCoefficient      = 2.0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -35.0;
   spinRandomMax        = 35.0;
   textureName          = "particleTest";
   colors[0]     = "0.46 0.36 0.26 0.4";
   colors[1]     = "0.46 0.46 0.36 0.0";
   sizes[0]      = 0.4;
   sizes[1]      = 1.0;
};

datablock ParticleEmitterData(LightPuffEmitter)
{
   ejectionPeriodMS = 35;
   periodVarianceMS = 10;
   ejectionVelocity = 0.1;
   velocityVariance = 0.05;
   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 20;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   useEmitterColors = true;
   particles = "LightPuff";
};

//----------------------------------------------------------------------------
// Liftoff dust
//----------------------------------------------------------------------------
datablock ParticleData(LiftoffDust)
{
   dragCoefficient      = 1.0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "0.46 0.36 0.26 0.0";
   colors[1]     = "0.46 0.46 0.36 0.4";
   colors[2]     = "0.46 0.46 0.36 0.0";
   sizes[0]      = 0.2;
   sizes[1]      = 0.6;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(LiftoffDustEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 2.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 90;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   useEmitterColors = true;
   particles = "LiftoffDust";
};

//----------------------------------------------------------------------------

datablock ParticleData(BiodermArmorJetParticle) : HumanArmorJetParticle
{
   colors[0]     = "0.50 0.48 0.36 1.0";
   colors[1]     = "0.50 0.48 0.36 0";
};

datablock ParticleEmitterData(BiodermArmorJetEmitter) : HumanArmorJetEmitter
{
   particles = "BiodermArmorJetParticle";
};


//----------------------------------------------------------------------------
datablock DecalData(LightMaleFootprint)
{
   sizeX       = 0.125;
   sizeY       = 0.25;
   textureName = "special/footprints/L_male";
};

datablock DebrisData( PlayerDebris )
{
   explodeOnMaxBounce = false;

   elasticity = 0.35;
   friction = 0.5;

   lifetime = 4.0;
   lifetimeVariance = 0.0;

   minSpinSpeed = 60;
   maxSpinSpeed = 600;

   numBounces = 5;
   bounceVariance = 0;

   staticOnMaxBounce = true;
   gravModifier = 1.0;

   useRadiusMass = true;
   baseRadius = 1;

   velocity = 18.0;
   velocityVariance = 12.0;
};             

datablock PlayerData(LightMaleHumanArmor) : LightPlayerDamageProfile
{
   emap = true;
   
   className = Armor;
   shapeFile = "light_male.dts";
   cameraMaxDist = 3;
   computeCRC = true;
  
   canObserve = true;
   cmdCategory = "Clients";
   cmdIcon = CMDPlayerIcon;
   cmdMiniIconName = "commander/MiniIcons/com_player_grey";

   hudImageNameFriendly[0] = "gui/hud_playertriangle";
   hudImageNameEnemy[0] = "gui/hud_playertriangle_enemy";
   hudRenderModulated[0] = true;

   hudImageNameFriendly[1] = "commander/MiniIcons/com_flag_grey";
   hudImageNameEnemy[1] = "commander/MiniIcons/com_flag_grey";
   hudRenderModulated[1] = true;
   hudRenderAlways[1] = true;
   hudRenderCenter[1] = true;
   hudRenderDistance[1] = true;

   hudImageNameFriendly[2] = "commander/MiniIcons/com_flag_grey";
   hudImageNameEnemy[2] = "commander/MiniIcons/com_flag_grey";
   hudRenderModulated[2] = true;
   hudRenderAlways[2] = true;
   hudRenderCenter[2] = true;
   hudRenderDistance[2] = true;

   cameraDefaultFov = 90.0;
   cameraMinFov = 5.0;
   cameraMaxFov = 120.0;
   
   debrisShapeName = "debris_player.dts";
   debris = playerDebris;

   aiAvoidThis = true;

   minLookAngle = -1.5;
   maxLookAngle = 1.5;
   maxFreelookAngle = 3.0;

   mass = 90;
   drag = 0.275;
   maxdrag = 0.4;
   density = 10;
   maxDamage = 1.5;
   maxEnergy =  100;
   repairRate = 0.0033;
   energyPerDamagePoint = 75.0; // shield energy required to block one point of damage

   rechargeRate = 0.256;
   jetForce = 26.21 * 90;
   underwaterJetForce = 26.21 * 90 * 1.5;
   underwaterVertJetFactor = 1.5;
   jetEnergyDrain =  0.8;
   underwaterJetEnergyDrain = 0.6;
   minJetEnergy = 1;
   maxJetHorizontalPercentage = 0.8;

   runForce = 55.20 * 90;
   runEnergyDrain = 0;
   minRunEnergy = 0;
   maxForwardSpeed = 15;
   maxBackwardSpeed = 13;
   maxSideSpeed = 13;

   maxUnderwaterForwardSpeed = 11;
   maxUnderwaterBackwardSpeed = 10;
   maxUnderwaterSideSpeed = 10;


   jumpForce = 8.3 * 90;
   jumpEnergyDrain = 0;
   minJumpEnergy = 0;
   jumpDelay = 0;


   recoverDelay = 9;
   recoverRunForceScale = 1.2;

   minImpactSpeed = 45;
   speedDamageScale = 0.004;

   jetSound = ArmorJetSound;
   wetJetSound = ArmorJetSound;
   jetEmitter = HumanArmorJetEmitter;
   jetEffect = HumanArmorJetEffect;
   
   boundingBox = "1.2 1.2 2.3";
   pickupRadius = 0.75;
   
   // damage location details
   boxNormalHeadPercentage       = 0.83;
   boxNormalTorsoPercentage      = 0.49;
   boxHeadLeftPercentage         = 0;
   boxHeadRightPercentage        = 1;
   boxHeadBackPercentage         = 0;
   boxHeadFrontPercentage        = 1;

   //Foot Prints
   decalData   = LightMaleFootprint;
   decalOffset = 0.25;
   
   footPuffEmitter = LightPuffEmitter;
   footPuffNumParts = 15;
   footPuffRadius = 0.25;

   dustEmitter = LiftoffDustEmitter;

   splash = PlayerSplash;
   splashVelocity = 4.0;
   splashAngle = 67.0;
   splashFreqMod = 300.0;
   splashVelEpsilon = 0.60;
   bubbleEmitTime = 0.4;
   splashEmitter[0] = PlayerFoamDropletsEmitter;
   splashEmitter[1] = PlayerFoamEmitter;
   splashEmitter[2] = PlayerBubbleEmitter;
   mediumSplashSoundVelocity = 10.0;   
   hardSplashSoundVelocity = 20.0;   
   exitSplashSoundVelocity = 5.0;

   // Controls over slope of runnable/jumpable surfaces
   runSurfaceAngle  = 70;
   jumpSurfaceAngle = 80;

   minJumpSpeed = 20;
   maxJumpSpeed = 30;

   horizMaxSpeed = 68;
   horizResistSpeed = 33;
   horizResistFactor = 0.35;
   maxJetForwardSpeed = 30;

   upMaxSpeed = 80;
   upResistSpeed = 25;
   upResistFactor = 0.3;
   
   // heat inc'ers and dec'ers
   heatDecayPerSec      = 1.0 / 4.0; // takes 4 seconds to clear heat sig.
   heatIncreasePerSec   = 1.0 / 3.0; // takes 3.0 seconds of constant jet to get full heat sig.

   footstepSplashHeight = 0.35;
   //Footstep Sounds
   LFootSoftSound       = LFootLightSoftSound;
   RFootSoftSound       = RFootLightSoftSound;
   LFootHardSound       = LFootLightHardSound;
   RFootHardSound       = RFootLightHardSound;
   LFootMetalSound      = LFootLightMetalSound;
   RFootMetalSound      = RFootLightMetalSound;
   LFootSnowSound       = LFootLightSnowSound;
   RFootSnowSound       = RFootLightSnowSound;
   LFootShallowSound    = LFootLightShallowSplashSound;
   RFootShallowSound    = RFootLightShallowSplashSound;
   LFootWadingSound     = LFootLightWadingSound;
   RFootWadingSound     = RFootLightWadingSound;
   LFootUnderwaterSound = LFootLightUnderwaterSound;
   RFootUnderwaterSound = RFootLightUnderwaterSound;
   LFootBubblesSound    = LFootLightBubblesSound;
   RFootBubblesSound    = RFootLightBubblesSound;
   movingBubblesSound   = ArmorMoveBubblesSound;
   waterBreathSound     = WaterBreathMaleSound;

   impactSoftSound      = ImpactLightSoftSound;
   impactHardSound      = ImpactLightHardSound;
   impactMetalSound     = ImpactLightMetalSound;
   impactSnowSound      = ImpactLightSnowSound;
   
   skiSoftSound         = SkiAllSoftSound;
   skiHardSound         = SkiAllHardSound;
   skiMetalSound        = SkiAllMetalSound;
   skiSnowSound         = SkiAllSnowSound;

   impactWaterEasy      = ImpactLightWaterEasySound;
   impactWaterMedium    = ImpactLightWaterMediumSound;
   impactWaterHard      = ImpactLightWaterHardSound;
   
   groundImpactMinSpeed    = 10.0;
   groundImpactShakeFreq   = "4.0 4.0 4.0";
   groundImpactShakeAmp    = "1.0 1.0 1.0";
   groundImpactShakeDuration = 0.8;
   groundImpactShakeFalloff = 10.0;
   
   exitingWater         = ExitingWaterLightSound;
   
   maxWeapons = 3;           // Max number of different weapons the player can have
   maxGrenades = 1;          // Max number of different grenades the player can have
   maxMines = 1;             // Max number of different mines the player can have
   
   // Inventory restrictions
   max[RepairKit]          = 1;
   max[Mine]               = 3;
   max[Grenade]            = 5;
   max[Blaster]            = 1;
   max[Plasma]             = 1;
   max[PlasmaAmmo]         = 20;
   max[Disc]               = 1;
   max[DiscAmmo]           = 15;
   max[SniperRifle]        = 1;
   max[GrenadeLauncher]    = 1;
   max[GrenadeLauncherAmmo]= 10;
   max[Mortar]             = 0;
   max[MortarAmmo]         = 0;
   max[MissileLauncher]    = 0;
   max[MissileLauncherAmmo]= 0;
   max[Chaingun]           = 1;
   max[ChaingunAmmo]       = 100;
   max[RepairGun]          = 1;
   max[CloakingPack]       = 1;
   max[SensorJammerPack]   = 1;
   max[EnergyPack]         = 1;
   max[RepairPack]         = 1;
   max[ShieldPack]         = 1;
   max[AmmoPack]           = 1;
   max[SatchelCharge]      = 1;
   max[MortarBarrelPack]   = 0;
   max[MissileBarrelPack]  = 0;
   max[AABarrelPack]       = 0;
   max[PlasmaBarrelPack]   = 0;
   max[ELFBarrelPack]      = 0;
   max[InventoryDeployable]= 0;
   max[MotionSensorDeployable]   = 1;
   max[PulseSensorDeployable]    = 1;
   max[TurretOutdoorDeployable]  = 0;
   max[TurretIndoorDeployable]   = 0;
   max[FlashGrenade]       = 5;
   max[ConcussionGrenade]  = 5;
   max[FlareGrenade]       = 5;
   max[TargetingLaser]     = 1;
   max[ELFGun]             = 1;
   max[ShockLance]         = 1;
   max[CameraGrenade]      = 2;
   max[Beacon]             = 3;
   
   observeParameters = "0.5 4.5 4.5";

   shieldEffectScale = "0.7 0.7 1.0";

};

//----------------------------------------------------------------------------
datablock PlayerData(MaleHumanArmor) : LightMaleHumanArmor
{
   shapeFile = "light_male.dts";
   waterBreathSound = WaterBreathMaleSound;
   jetEffect =  HumanMediumArmorJetEffect;
};

datablock PlayerData(LightFemaleHumanArmor) : LightMaleHumanArmor
{
   shapeFile = "light_female.dts";
   waterBreathSound = WaterBreathFemaleSound;
   jetEffect =  HumanMediumArmorJetEffect;
};

datablock DecalData(LightBiodermFootprint)
{
   sizeX       = 0.25;
   sizeY       = 0.25;
   textureName = "special/footprints/L_bioderm";
};

datablock PlayerData(LightMaleBiodermArmor) : LightMaleHumanArmor
{
   shapeFile = "bioderm_light.dts";
   jetEmitter = BiodermArmorJetEmitter;
   jetEffect =  BiodermArmorJetEffect;


   debrisShapeName = "bio_player_debris.dts";
   
   //Foot Prints
   decalData   = LightBiodermFootprint;
   decalOffset = 0.3;

   waterBreathSound = WaterBreathBiodermSound;
};

//------------------------------------------------------------
function Armor::onAdd(%data,%obj)
{
   Parent::onAdd(%data, %obj);
   // Vehicle timeout
   %obj.mountVehicle = true;

   // Default dynamic armor stats
   %obj.setRechargeRate(%data.rechargeRate);
   %obj.setRepairRate(0);

   %obj.setSelfPowered();
}

function Armor::onRemove(%this, %obj)
{
   //Frohny asked me to remove this - all players are deleted now on mission cycle...
   //if(%obj.getState() !$= "Dead")
   //{
   //   error("ERROR PLAYER REMOVED WITHOUT DEATH - TRACE:");
   //   trace(1);
   //   schedule(0,0,trace,0);
   //}

   if (%obj.client.player == %obj)
      %obj.client.player = 0;
}

function Armor::onNewDataBlock(%this,%obj)
{
}

function Armor::onDisabled(%this, %obj, %state) {
   if(!isSet(%obj.client)) {
      freeclienttarget(%obj);

      %dodeath = (getrandom() * 3);
	  if(%dodeath <= 1)
         %obj.setActionThread("Death" @ $PlayerDeathAnim::HeadFrontDirect);
	  else if(%dodeath <= 2)
         %obj.setActionThread("Death" @ $PlayerDeathAnim::HeadBackFallForward);
      else
         %obj.setActionThread("Death" @ $PlayerDeathAnim::TorsoBackFallForward);
   }
   %fadeTime = 1000;
   %obj.startFade( %fadeTime, ($CorpseTimeoutValue) - %fadeTime, true );
   %obj.schedule($CorpseTimeoutValue, "delete");
}

function Armor::shouldApplyImpulse(%data, %obj)
{
   return true;
}

$wasFirstPerson = true;

function Armor::onMount(%this,%obj,%vehicle,%node)
{
   if (%node == 0)
   {
      // Node 0 is the pilot's pos.
      %obj.setTransform("0 0 0 0 0 1 0");
      %obj.setActionThread(%vehicle.getDatablock().mountPose[%node],true,true);
   
      if(!%obj.inStation)
         %obj.lastWeapon = (%obj.getMountedImage($WeaponSlot) == 0 ) ? "" : %obj.getMountedImage($WeaponSlot).getName().item;
         
       %obj.unmountImage($WeaponSlot);
   
      if(!%obj.client.isAIControlled())
      {
         %obj.setControlObject(%vehicle);
         %obj.client.setObjectActiveImage(%vehicle, 2);
      }
      
      //E3 respawn...
 
      if(%obj == %obj.lastVehicle.lastPilot && %obj.lastVehicle != %vehicle)
      {
         schedule(15000, %obj.lastVehicle,"vehicleAbandonTimeOut", %obj.lastVehicle);
          %obj.lastVehicle.lastPilot = "";
      }
      if(%vehicle.lastPilot !$= "" && %vehicle == %vehicle.lastPilot.lastVehicle)
            %vehicle.lastPilot.lastVehicle = "";
            
      %vehicle.abandon = false;
      %vehicle.lastPilot = %obj;
      %obj.lastVehicle = %vehicle;

      // update the vehicle's team
      if((%vehicle.getTarget() != -1) && %vehicle.getDatablock().cantTeamSwitch $= "")
      {   
         setTargetSensorGroup(%vehicle.getTarget(), %obj.client.getSensorGroup());
         if( %vehicle.turretObject > 0 )
            setTargetSensorGroup(%vehicle.turretObject.getTarget(), %obj.client.getSensorGroup());
      }

      // Send a message to the client so they can decide if they want to change view or not:
      commandToClient( %obj.client, 'VehicleMount' );

   }
   else
   {
      // tailgunner/passenger positions
      if(%vehicle.getDataBlock().mountPose[%node] !$= "")
         %obj.setActionThread(%vehicle.getDatablock().mountPose[%node]);
      else
         %obj.setActionThread("root", true);
   }
   // -------------------------------------------------------------------------
   // z0dd - ZOD, 10/06/02. announce to any other passengers that you've boarded
   if(%vehicle.getDatablock().numMountPoints > 1)
   {
      %nodeName = findNodeName(%vehicle, %node); // function in vehicle.cs
      for(%i = 0; %i < %vehicle.getDatablock().numMountPoints; %i++)
      {
         if (%vehicle.getMountNodeObject(%i) > 0)
         {
            if(%vehicle.getMountNodeObject(%i).client != %obj.client)
            {
               %team = (%obj.team == %vehicle.getMountNodeObject(%i).client.team ? 'Teammate' : 'Enemy');
               messageClient( %vehicle.getMountNodeObject(%i).client, 'MsgShowPassenger', '\c2%3: \c3%1\c2 has boarded in the \c3%2\c2 position.', %obj.client.name, %nodeName, %team );
            }
            commandToClient( %vehicle.getMountNodeObject(%i).client, 'showPassenger', %node, true);
         }
      }
   }
   //make sure they don't have any packs active
//    if ( %obj.getImageState( $BackpackSlot ) $= "activate")
//       %obj.use("Backpack");
   if ( %obj.getImageTrigger( $BackpackSlot ) )
      %obj.setImageTrigger( $BackpackSlot, false );

   //AI hooks
   %obj.client.vehicleMounted = %vehicle;
   AIVehicleMounted(%vehicle);
   if(%obj.client.isAIControlled())
      %this.AIonMount(%obj, %vehicle, %node);
}


function Armor::onUnmount( %this, %obj, %vehicle, %node )
{
   if ( %node == 0 )
   {
      commandToClient( %obj.client, 'VehicleDismount' );
      commandToClient(%obj.client, 'removeReticle');

      if(%obj.inv[%obj.lastWeapon])
         %obj.use(%obj.lastWeapon);
      
      if(%obj.getMountedImage($WeaponSlot) == 0) 
         %obj.selectWeaponSlot( 0 );
                                
      //Inform gunner position when pilot leaves...
      //if(%vehicle.getDataBlock().showPilotInfo !$= "")
      //   if((%gunner = %vehicle.getMountNodeObject(1)) != 0)
      //      commandToClient(%gunner.client, 'PilotInfo', "PILOT EJECTED", 6, 1);
   }
   // ----------------------------------------------------------------------
   // z0dd - ZOD, 10/06/02. announce to any other passengers that you've left
   if(%vehicle.getDatablock().numMountPoints > 1)
   {
      %nodeName = findNodeName(%vehicle, %node); // function in vehicle.cs
      for(%i = 0; %i < %vehicle.getDatablock().numMountPoints; %i++)
      {
         if (%vehicle.getMountNodeObject(%i) > 0)
         {
            if(%vehicle.getMountNodeObject(%i).client != %obj.client)
            {
               %team = (%obj.team == %vehicle.getMountNodeObject(%i).client.team ? 'Teammate' : 'Enemy');
               messageClient( %vehicle.getMountNodeObject(%i).client, 'MsgShowPassenger', '\c2%3: \c3%1\c2 has ejected from the \c3%2\c2 position.', %obj.client.name, %nodeName, %team );
            }
            commandToClient( %vehicle.getMountNodeObject(%i).client, 'showPassenger', %node, false);
         }
      }
   }
   //AI hooks
   %obj.client.vehicleMounted = "";
   if(%obj.client.isAIControlled())
      %this.AIonUnMount(%obj, %vehicle, %node);
}

function Armor::onCollision(%this,%obj,%col,%forceVehicleNode)
{
   if (%obj.getState() $= "Dead")
      return;

   %dataBlock = %col.getDataBlock();
   %className = %dataBlock.className;
   %client = %obj.client;
   // player collided with a vehicle
   %node = -1;
   if (%forceVehicleNode !$= "" || (%className $= WheeledVehicleData || %className $= FlyingVehicleData || %className $= HoverVehicleData) &&
         %obj.mountVehicle && %obj.getState() $= "Move" && %col.mountable && !%obj.inStation && %col.getDamageState() !$= "Destroyed") {

      //if the player is an AI, he should snap to the mount points in node order,
      //to ensure they mount the turret before the passenger seat, regardless of where they collide...
      if (%obj.client.isAIControlled())
      {
         %transform = %col.getTransform();   

         //either the AI is *required* to pilot, or they'll pick the first available passenger seat
         if (%client.pilotVehicle)
         {
            //make sure the bot is in light armor
            if (%client.player.getArmorSize() $= "Light")
            {
               //make sure the pilot seat is empty
               if (!%col.getMountNodeObject(0))
                  %node = 0;
            }
         }
         else
            %node = findAIEmptySeat(%col, %obj);
      }
      else
         %node = findEmptySeat(%col, %obj, %forceVehicleNode);

      //now mount the player in the vehicle
      if(%node >= 0)
      {
         // players can't be pilots, bombardiers or turreteers if they have
         // "large" packs -- stations, turrets, turret barrels
         if(hasLargePack(%obj)) {
            // check to see if attempting to enter a "sitting" node
            if(nodeIsSitting(%datablock, %node)) {
               // send the player a message -- can't sit here with large pack
               if(!%obj.noSitMessage)
               {
                  %obj.noSitMessage = true;
                  %obj.schedule(2000, "resetSitMessage");
                  messageClient(%obj.client, 'MsgCantSitHere', '\c2Pack too large, can\'t occupy this seat.~wfx/misc/misc.error.wav');
               }
               return;
            }
         }
         if(%col.noEnemyControl && %obj.team != %col.team)
            return;
            
         commandToClient(%obj.client,'SetDefaultVehicleKeys', true);
         //If pilot or passenger then bind a few extra keys
         if(%node == 0)
            commandToClient(%obj.client,'SetPilotVehicleKeys', true);
         else
            commandToClient(%obj.client,'SetPassengerVehicleKeys', true);

         if(!%obj.inStation)
            %col.lastWeapon = ( %col.getMountedImage($WeaponSlot) == 0 ) ? "" : %col.getMountedImage($WeaponSlot).getName().item;
         else
            %col.lastWeapon = %obj.lastWeapon;
         
         %col.mountObject(%obj,%node);
         %col.playAudio(0, MountVehicleSound);
         %obj.mVehicle = %col;

			// if player is repairing something, stop it
			if(%obj.repairing)
				stopRepairing(%obj);

         //this will setup the huds as well...
         %dataBlock.playerMounted(%col,%obj, %node);
      }
   }
   else if (%className $= "Armor") {
      // player has collided with another player
      if(%col.getState() $= "Dead") {
         %gotSomething = false;
         // it's corpse-looting time!
         // weapons -- don't pick up more than you are allowed to carry!
         for(%i = 0; ( %obj.weaponCount < %obj.getDatablock().maxWeapons ) && $InvWeapon[%i] !$= ""; %i++) 
         {
            %weap = $NameToInv[$InvWeapon[%i]];
            if ( %col.hasInventory( %weap ) )
            {
               if ( %obj.incInventory(%weap, 1) > 0 )
               {
                  %col.decInventory(%weap, 1);
                  %gotSomething = true;
                  messageClient(%obj.client, 'MsgItemPickup', '\c0You picked up %1.', %weap.pickUpName);
               }
            }
         }
         // targeting laser:
         if ( %col.hasInventory( "TargetingLaser" ) )
         {
            if ( %obj.incInventory( "TargetingLaser", 1 ) > 0 )
            {
               %col.decInventory( "TargetingLaser", 1 );
               %gotSomething = true;
               messageClient( %obj.client, 'MsgItemPickup', '\c0You picked up a targeting laser.' );
            }
         }
         // ammo
         for(%j = 0; $ammoType[%j] !$= ""; %j++)
         {
            %ammoAmt = %col.inv[$ammoType[%j]];
            if(%ammoAmt)
            {
               // incInventory returns the amount of stuff successfully grabbed
               %grabAmt = %obj.incInventory($ammoType[%j], %ammoAmt);
               if(%grabAmt > 0)
               {
                  %col.decInventory($ammoType[%j], %grabAmt);
                  %gotSomething = true;
                  messageClient(%obj.client, 'MsgItemPickup', '\c0You picked up %1.', $ammoType[%j].pickUpName);
                  %obj.client.setWeaponsHudAmmo($ammoType[%j], %obj.getInventory($ammoType[%j]));
               }
            }
         }
         // figure out what type, if any, grenades the (live) player has
         %playerGrenType = "None";
         for(%x = 0; $InvGrenade[%x] !$= ""; %x++) {
            %gren = $NameToInv[$InvGrenade[%x]];
            %playerGrenAmt = %obj.inv[%gren];
            if(%playerGrenAmt > 0)
            {
               %playerGrenType = %gren;
               break;
            }
         }
         // grenades
         for(%k = 0; $InvGrenade[%k] !$= ""; %k++)
         {
            %gren = $NameToInv[$InvGrenade[%k]];
            %corpseGrenAmt = %col.inv[%gren];
            // does the corpse hold any of this grenade type?
            if(%corpseGrenAmt)
            {
               // can the player pick up this grenade type?
               if((%playerGrenType $= "None") || (%playerGrenType $= %gren))
               {
                  %taken = %obj.incInventory(%gren, %corpseGrenAmt);
                  if(%taken > 0)
                  {
                     %col.decInventory(%gren, %taken);
                     %gotSomething = true;
                     messageClient(%obj.client, 'MsgItemPickup', '\c0You picked up %1.', %gren.pickUpName);
                     %obj.client.setInventoryHudAmount(%gren, %obj.getInventory(%gren));
                  }
               }
               break;
            }
         }
         // figure out what type, if any, mines the (live) player has
         %playerMineType = "None";
         for(%y = 0; $InvMine[%y] !$= ""; %y++)
         {
            %mType = $NameToInv[$InvMine[%y]];
            %playerMineAmt = %obj.inv[%mType];
            if(%playerMineAmt > 0)
            {
               %playerMineType = %mType;
               break;
            }
         }
         // mines
         for(%l = 0; $InvMine[%l] !$= ""; %l++)
         {
            %mine = $NameToInv[$InvMine[%l]];
            %mineAmt = %col.inv[%mine];
            if(%mineAmt) {
               if((%playerMineType $= "None") || (%playerMineType $= %mine))
               {
                  %grabbed = %obj.incInventory(%mine, %mineAmt);
                  if(%grabbed > 0)
                  {
                     %col.decInventory(%mine, %grabbed);
                     %gotSomething = true;
                     messageClient(%obj.client, 'MsgItemPickup', '\c0You picked up %1.', %mine.pickUpName);
                     %obj.client.setInventoryHudAmount(%mine, %obj.getInventory(%mine));
                  }
               }
               break;
            }
         }
         // beacons
         %beacAmt = %col.inv[Beacon];
         if(%beacAmt)
         {
            %bTaken = %obj.incInventory(Beacon, %beacAmt);
            if(%bTaken > 0)
            {
               %col.decInventory(Beacon, %bTaken);
               %gotSomething = true;
               messageClient(%obj.client, 'MsgItemPickup', '\c0You picked up %1.', Beacon.pickUpName);
               %obj.client.setInventoryHudAmount(Beacon, %obj.getInventory(Beacon));
            }
         }
         // repair kit
         %rkAmt = %col.inv[RepairKit];
         if(%rkAmt)
         {
            %rkTaken = %obj.incInventory(RepairKit, %rkAmt);
            if(%rkTaken > 0)
            {
               %col.decInventory(RepairKit, %rkTaken);
               %gotSomething = true;
               messageClient(%obj.client, 'MsgItemPickup', '\c0You picked up %1.', RepairKit.pickUpName);
               %obj.client.setInventoryHudAmount(RepairKit, %obj.getInventory(RepairKit));
            }
         }
      }
      if(%gotSomething)
         %col.playAudio(0, CorpseLootingSound);
   }
}

function Player::resetSitMessage(%obj)
{
   %obj.noSitMessage = false;   
}

function Player::setInvincible(%this, %val)
{
   %this.invincible = %val;
}

function Player::causedRecentDamage(%this, %val)
{
   %this.causedRecentDamage = %val; 
}
   
function hasLargePack(%player)
{
   %pack = %player.getMountedImage($BackpackSlot);
   if(%pack.isLarge)
      return true;
   else
      return false;
}

function nodeIsSitting(%vehDBlock, %node)
{
   // pilot == always a "sitting" node
   if(%node == 0)
      return true;
   else {
      switch$ (%vehDBlock.getName())
      {
         // note: for assault tank -- both nodes are sitting
         // for any single-user vehicle -- pilot node is sitting
         case "BomberFlyer":
            // bombardier == sitting; tailgunner == not sitting
            if(%node == 1)
               return true;
            else
               return false;
         case "HAPCFlyer":
            // only the pilot node is sitting
            return false;
         default:
            return true;
      }
   }
}

//----------------------------------------------------------------------------
function Player::setMountVehicle(%this, %val)
{
   %this.mountVehicle = %val;
}

function Armor::doDismount(%this, %obj, %forced)
{
   // This function is called by player.cc when the jump trigger
   // is true while mounted
   if (!%obj.isMounted())
      return;

   if(isObject(%obj.getObjectMount().shield))
      %obj.getObjectMount().shield.delete();

   commandToClient(%obj.client,'SetDefaultVehicleKeys', false);

   // Position above dismount point

   %pos    = getWords(%obj.getTransform(), 0, 2);
   %oldPos = %pos;
   %vec[0] = " 0  0  1";
   %vec[1] = " 0  0  1";
   %vec[2] = " 0  0 -1";
   %vec[3] = " 1  0  0";
   %vec[4] = "-1  0  0";
   %numAttempts = 5;
   %success     = -1;
   %impulseVec  = "0 0 0";
   if (%obj.getObjectMount().getDatablock().hasDismountOverrides() == true)
   {
      %vec[0] = %obj.getObjectMount().getDatablock().getDismountOverride(%obj.getObjectMount(), %obj);
      %vec[0] = MatrixMulVector(%obj.getObjectMount().getTransform(), %vec[0]);
   }
   else
   {
      %vec[0] = MatrixMulVector( %obj.getTransform(), %vec[0]);
   }

   %pos = "0 0 0";
   for (%i = 0; %i < %numAttempts; %i++)
   {
      %pos = VectorAdd(%oldPos, VectorScale(%vec[%i], 3));
      if (%obj.checkDismountPoint(%oldPos, %pos))
      {
         %success = %i;
         %impulseVec = %vec[%i];
         break;
      }
   }
   if (%forced && %success == -1)
   {
      %pos = %oldPos;
   }

   // hide the dashboard HUD and delete elements based on node
   commandToClient(%obj.client, 'setHudMode', 'Standard', "", 0);
   // Unmount and control body
   if(%obj.vehicleTurret)
      %obj.vehicleTurret.getDataBlock().playerDismount(%obj.vehicleTurret);
   %obj.unmount();
   if(%obj.mVehicle)
      %obj.mVehicle.getDataBlock().playerDismounted(%obj.mVehicle, %obj);
   
   // bots don't change their control objects when in vehicles
   if(!%obj.client.isAIControlled())
   {
      %vehicle = %obj.getControlObject();
      %obj.setControlObject(0);
   }

   %obj.mountVehicle = false;
   %obj.schedule(4000, "setMountVehicle", true);

   // Position above dismount point
   %obj.setTransform(%pos);
   %obj.playAudio(0, UnmountVehicleSound);
   %obj.applyImpulse(%pos, VectorScale(%impulseVec, %obj.getDataBlock().mass * 3));
   %obj.setPilot(false);
   %obj.vehicleTurret = "";
}

function resetObserveFollow( %client, %dismount )
{
   if( %dismount )
   {
      if( !isObject( %client.player ) )
         return;

      for( %i = 0; %i < %client.observeCount; %i++ )
      {
         %client.observers[%i].camera.setOrbitMode( %client.player, %client.player.getTransform(), 0.5, 4.5, 4.5); 
      }
   }
   else
   {
      if( !%client.player.isMounted() )
         return;

      // grab the vehicle...
      %mount = %client.player.getObjectMount();
      if( %mount.getDataBlock().observeParameters $= "" )
         %params = %client.player.getTransform();
      else
         %params = %mount.getDataBlock().observeParameters;
      
      for( %i = 0; %i < %client.observeCount; %i++ )
      {
         %client.observers[%i].camera.setOrbitMode(%mount, %mount.getTransform(), getWord( %params, 0 ), getWord( %params, 1 ), getWord( %params, 2 ));
      }
   }   
}


//----------------------------------------------------------------------------

function Player::scriptKill(%player, %damageType) {
   %player.scriptKilled = 1; 
   %player.setInvincible(false);
   %player.SpellShieldTicks = 0;
   %player.damage(0, %player.getPosition(), 10000, %damageType);
   TakeHP(%player, FetchMaxHP(%player));
}

function Armor::damageObject(%data, %targetObject, %sourceObject, %position, %amount, %damageType, %momVec, %mineSC) {
//error("Armor::damageObject( "@%data@", "@%targetObject@", "@%sourceObject@", "@%position@", "@%amount@", "@%damageType@", "@%momVec@" )");
   if(%targetObject.getState() $= "Dead")
      return;
      
   if(%targetObject.isPlayerInvincible && %damageType != $DamageType::Suicide) {
      return;
   }
      
   //shield
   if(%targetObject.SpellShieldTicks > 0) {
      if(%damageType != $DamageType::Suicide) {
         %targetObject.SpellShieldTicks--;
         %targetObject.playShieldEffect("1 1 1");
         if(%targetObject.SpellShieldTicks == 0) {
            messageClient(%targetObject.client, 'MsgShieldIsOff', "\c2Your Shield has disipated from too much damage.");
         }
         return;
      }
      else {
         //obviously they just die
      }
   }

   //----------------------------------------------------------------
   // z0dd - ZOD, 6/09/02. Check to see if this vehcile is destroyed, 
   // if it is do no damage. Fixes vehicle ghosting bug. We do not
   // check for isObject here, destroyed objects fail it even though
   // they exist as objects, go figure.
   if(%damageType == $DamageType::Impact)
      if(%sourceObject.getDamageState() $= "Destroyed")
         return;

   if (%targetObject.isMounted() && %targetObject.scriptKilled $= "")
   {
      %mount = %targetObject.getObjectMount();
      if(%mount.team == %targetObject.team)
      {
         %found = -1;
         for (%i = 0; %i < %mount.getDataBlock().numMountPoints; %i++)
         {
            if (%mount.getMountNodeObject(%i) == %targetObject)
            {
               %found = %i;
               break;
            }
         }

         if (%found != -1)
         {
            if (%mount.getDataBlock().isProtectedMountPoint[%found])
            {
               %mount.getDataBlock().damageObject(%mount, %sourceObject, %position, %amount, %damageType);
               return;
            }
         }
      }
   }

   %targetClient = %targetObject.getOwnerClient();
   if(isObject(%mineSC))
      %sourceClient = %mineSC;   
   else
      %sourceClient = isObject(%sourceObject) ? %sourceObject.getOwnerClient() : 0;

   %targetTeam = %targetClient.team;

   //if the source object is a player object, player's don't have sensor groups
   // if it's a turret, get the sensor group of the target
   // if its a vehicle (of any type) use the sensor group
   if (%sourceClient)
      %sourceTeam = %sourceClient.getSensorGroup();
   else if(%damageType == $DamageType::Suicide) {
      %sourceTeam = 0;
      TakeHP(%targetObject, FetchMaxHP(%targetObject));
   }

   //--------------------------------------------------------------------------------------------------------------------
   // z0dd - ZOD, 5/8/02. Check to see if this turret has a valid owner, if not clear the owner varible.
   //else if(isObject(%sourceObject) && %sourceObject.getClassName() $= "Turret")
   //   %sourceTeam = getTargetSensorGroup(%sourceObject.getTarget());
   else if(isObject(%sourceObject) && %sourceObject.getClassName() $= "Turret")
   {
      %sourceTeam = getTargetSensorGroup(%sourceObject.getTarget());
      if(%sourceObject.owner !$="" && (%sourceObject.owner.team != %sourceObject.team || !isObject(%sourceObject.owner)))
         %sourceObject.owner = "";
   }
   // End z0dd - ZOD
   //--------------------------------------------------------------------------------------------------------------------
   else if( isObject(%sourceObject) &&
   	( %sourceObject.getClassName() $= "FlyingVehicle" || %sourceObject.getClassName() $= "WheeledVehicle" || %sourceObject.getClassName() $= "HoverVehicle"))
      %sourceTeam = getTargetSensorGroup(%sourceObject.getTarget());
   else
   {
      if (isObject(%sourceObject) && %sourceObject.getTarget() >= 0 )
      {
         %sourceTeam = getTargetSensorGroup(%sourceObject.getTarget());
      }
      else
      {
         %sourceTeam = -1;
      }
   }

   // if teamdamage is off, and both parties are on the same team
   // (but are not the same person), apply no damage
   if(!$teamDamage && (%targetClient != %sourceClient) && (%targetTeam == %sourceTeam))
      return;

   if(%targetObject.isShielded && %damageType != $DamageType::Blaster)
      %amount = %data.checkShields(%targetObject, %position, %amount, %damageType);

   if(%amount == 0)
      return;

   // Set the damage flash
   %damageScale = %data.damageScale[%damageType];
   if(%damageScale !$= "")
      %amount *= %damageScale;
   
   %flash = %targetObject.getDamageFlash() + (%amount * 2);
   if (%flash > 0.75)
      %flash = 0.75;
   
   %previousDamage = %targetObject.getDamagePercent();
   %targetObject.setDamageFlash(%flash);

   //%targetObject.applyDamage(%amount);
   %deal = DoDamageModifiers(%sourceClient, %targetClient, %amount);
   TakeHP(%targetObject, %deal); //<- Powers

   Game.onClientDamaged(%targetClient, %sourceClient, %damageType, %sourceObject);

   %targetClient.lastDamagedBy = %damagingClient;
   %targetClient.lastDamaged = getSimTime();
   
   //now call the "onKilled" function if the client was... you know...  
   if(%targetObject.getState() $= "Dead")
   {
      // where did this guy get it?
      %damLoc = %targetObject.getDamageLocation(%position);
      
      // should this guy be blown apart?
      if( %damageType == $DamageType::Explosion || 
          %damageType == $DamageType::TankMortar ||
          %damageType == $DamageType::Mortar ||
          %damageType == $DamageType::MortarTurret ||
          %damageType == $DamageType::BomberBombs ||
          %damageType == $DamageType::SatchelCharge ||
          %damageType == $DamageType::Missile )     
      {
         if( %previousDamage >= 0.35 ) // only if <= 35 percent damage remaining
         {
            %targetObject.setMomentumVector(%momVec);
            %targetObject.blowup(); 
         }
      }
   
      // this should be funny...
      if( %damageType == $DamageType::VehicleSpawn )
      {   
         %targetObject.setMomentumVector("0 0 1");
         %targetObject.blowup();
      }
      
      //are we undecided?
      PowersModPostDeath(%targetObject);
      
      // If we were killed, max out the flash
      %targetObject.setDamageFlash(0.75);
      
      %damLoc = %targetObject.getDamageLocation(%position);
      Game.onClientKilled(%targetClient, %sourceClient, %damageType, %sourceObject, %damLoc);
   }
   else if ( %amount > 0.1 )
   {   
      if( %targetObject.station $= "" && %targetObject.isCloaked() )
      {
         %targetObject.setCloaked( false );
         %targetObject.reCloak = %targetObject.schedule( 500, "setCloaked", true ); 
      }
      
      playPain( %targetObject );
   }
}

function Armor::onImpact(%data, %playerObject, %collidedObject, %vec, %vecLen)
{
   %data.damageObject(%playerObject, 0, VectorAdd(%playerObject.getPosition(),%vec),
      %vecLen * %data.speedDamageScale, $DamageType::Ground);
}

function Armor::applyConcussion( %this, %dist, %radius, %sourceObject, %targetObject )
{
   %percentage = 1 - ( %dist / %radius );
   %random = getRandom();
   
   if( %sourceObject == %targetObject )
   {   
      %flagChance = 1.0;
      %itemChance = 1.0;
   }
   else
   {   
      %flagChance = 0.7;
      %itemChance = 0.7;   
   }
   
   %probabilityFlag = %flagChance * %percentage;
   %probabilityItem = %itemChance * %percentage;   
   
   if( %random <= %probabilityFlag )
   {   
      Game.applyConcussion( %targetObject );
   }    
   
   if( %random <= %probabilityItem )
   {
      %player = %targetObject;
      %numWeapons = 0;
      
      // blaster 0
      // plasma 1
      // chain 2
      // disc 3   
      // grenade 4
      // snipe 5
      // elf 6
      // mortar 7
      
      //get our inventory
      if( %weaps[0] = %player.getInventory("Blaster") > 0 ) %numWeapons++; 
      
      %foundWeapon = false;
      %attempts = 0;
      
      if( %numWeapons > 0 )   
      {   
         while( !%foundWeapon )
         {
            %rand = mFloor( getRandom() * 8 );
            if( %weaps[ %rand ] )
            {
               %foundWeapon = true;
               
               switch ( %rand )
               {
                  case 0:
                     %player.use("Blaster");     
               }
               
               %image = %player.getMountedImage( $WeaponSlot );
               %player.throw( %image.item );
               %player.client.setWeaponsHudItem( %image.item, 0, 0 );
               %player.throwPack();
            }
            else
            {   
               %attempts++;
               if( %attempts > 10 )
                  %foundWeapon = true;
            }    
         }
      }
      else
      {
         %targetObject.throwPack();
         %targetObject.throwWeapon();
      }   
   }
}             

//----------------------------------------------------------------------------

$DeathCry[1] = 'avo.deathCry_01';
$DeathCry[2] = 'avo.deathCry_02';
$PainCry[1] = 'avo.grunt';
$PainCry[2] = 'avo.pain';

function playDeathCry( %obj ) {
   if(isSet(%obj.client)) {
      %client = %obj.client;
      %random = getRandom(1) + 1;
      %desc = AudioClosest3d;

      playTargetAudio( %client.target, $DeathCry[%random], %desc, false );
   }
}

function playPain( %obj ) {
   if(isSet(%obj.client)) {
      %client = %obj.client;
      %random = getRandom(1) + 1;
      %desc = AudioClosest3d;
   
      playTargetAudio( %client.target, $PainCry[%random], %desc, false);
   }
}

//----------------------------------------------------------------------------

//$DefaultPlayerArmor = LightMaleHumanArmor;
$DefaultPlayerArmor = Light;

function Player::setArmor(%this,%size) {
   // Takes size as "Light","Medium", "Heavy"
   %client = %this.client;
   if (%client.race $= "Bioderm")
      // Only have male bioderms.
      %armor = %size @ "Male" @ %client.race @ Armor;
   else
      %armor = %size @ %client.sex @ %client.race @ Armor;
   //echo("Player::armor: " @ %armor);
   %this.setDataBlock(%armor);
   %client.armor = %size;
//   echo("TEST: Energy "@%client.player.getEnergyLevel()@"");
}

function getDamagePercent(%maxDmg, %dmgLvl)
{
   return (%dmgLvl / %maxDmg);
}

function Player::getArmorSize(%this)
{
   // return size as "Light","Medium", "Heavy"
   %dataBlock = %this.getDataBlock().getName();
   //Only light armor anymore
   if (getSubStr(%dataBlock, 0, 5) $= "Light")
      return "Light";
   else
      return "Unknown";
}

function Player::pickup(%this,%obj,%amount)
{
   %data = %obj.getDataBlock();
   // Don't pick up a pack if we already have one mounted
   if (%data.className $= Pack &&
         %this.getMountedImage($BackpackSlot) != 0)
      return 0;
	// don't pick up a weapon (other than targeting laser) if player's at maxWeapons
   else if(%data.className $= Weapon 
     && %data.getName() !$= "TargetingLaser"  // Special case 
     && %this.weaponCount >= %this.getDatablock().maxWeapons)
      return 0;
	// don't allow players to throw large packs at pilots (thanks Wizard)
   else if(%data.className $= Pack && %data.image.isLarge && %this.isPilot())
		return 0;
   return ShapeBase::pickup(%this,%obj,%amount);
}

function Player::use( %this,%data )
{
   // If player is in a station then he can't use any items
   if(%this.station !$= "")
      return false;

   // Convert the word "Backpack" to whatever is in the backpack slot.
   if ( %data $= "Backpack" ) 
   {
      if ( %this.inStation )
         return false;

      if ( %this.isPilot() )
      {
         messageClient( %this.client, 'MsgCantUsePack', '\c2You can\'t use your pack while piloting.~wfx/misc/misc.error.wav' );
         return( false );
      }
      else if ( %this.isWeaponOperator() )
      {
         messageClient( %this.client, 'MsgCantUsePack', '\c2You can\'t use your pack while in a weaponry position.~wfx/misc/misc.error.wav' );
         return( false );
      }
      
      %image = %this.getMountedImage( $BackpackSlot );
      if ( %image )
         %data = %image.item;
   }

   // Can't use some items when piloting or your a weapon operator
   if ( %this.isPilot() || %this.isWeaponOperator() ) 
      if ( %data.getName() !$= "RepairKit" )
         return false;
   
   return ShapeBase::use( %this, %data );
}

function Player::maxInventory(%this,%data)
{
   %max = ShapeBase::maxInventory(%this,%data);
   if (%this.getInventory(AmmoPack))
      %max += AmmoPack.max[%data.getName()];
   return %max;
}

function Player::isPilot(%this)
{
   %vehicle = %this.getObjectMount();
   // There are two "if" statements to avoid a script warning.
   if (%vehicle)
      if (%vehicle.getMountNodeObject(0) == %this)
         return true;
   return false;
}

function Player::isWeaponOperator(%this)
{
   %vehicle = %this.getObjectMount();
   if ( %vehicle )
   {
      %weaponNode = %vehicle.getDatablock().weaponNode;
      if ( %weaponNode > 0 && %vehicle.getMountNodeObject( %weaponNode ) == %this )
         return( true );
   }

   return( false );
}   

function Player::liquidDamage(%obj, %data, %damageAmount, %damageType)
{
   if(%obj.getState() !$= "Dead")
   {
      %data.damageObject(%obj, 0, "0 0 0", %damageAmount, %damageType); 
      %obj.lDamageSchedule = %obj.schedule(50, "liquidDamage", %data, %damageAmount, %damageType);
   }
   else
      %obj.lDamageSchedule = "";
}

function Armor::onEnterLiquid(%data, %obj, %coverage, %type)
{
   switch(%type)
   {
      case 0:
         //Water
      case 1:
         //Ocean Water
      case 2:
         //River Water
      case 3:
         //Stagnant Water
      case 4:
         //Lava
         %obj.liquidDamage(%data, $DamageLava, $DamageType::Lava);
      case 5:
         //Hot Lava
         %obj.liquidDamage(%data, $DamageHotLava, $DamageType::Lava);
      case 6:    
         //Crusty Lava
         %obj.liquidDamage(%data, $DamageCrustyLava, $DamageType::Lava);
      case 7:
         //Quick Sand
   }
}

function Armor::onLeaveLiquid(%data, %obj, %type)
{
   switch(%type)
   {
      case 0:
         //Water
      case 1:
         //Ocean Water
      case 2:
         //River Water
      case 3:
         //Stagnant Water
      case 4:
         //Lava
      case 5:
         //Hot Lava
      case 6:
         //Crusty Lava
      case 7:
         //Quick Sand
   }

   if(%obj.lDamageSchedule !$= "")
   {
      cancel(%obj.lDamageSchedule);
      %obj.lDamageSchedule = "";
   }
}

function Armor::onTrigger(%data, %player, %triggerNum, %val)
{
   if (%triggerNum == 4)
   {
      // Throw grenade
      if (%val == 1)
      {
         %player.grenTimer = 1;
      }
      else
      {
         if (%player.grenTimer == 0)
         {
            // Bad throw for some reason
         }
         else
         {
            %player.use(Grenade);
            %player.grenTimer = 0;
         }
      }
   }
   else if (%triggerNum == 5)
   {
      // Throw mine
      if (%val == 1)
      {
         %player.mineTimer = 1;
      }
      else
      {
         if (%player.mineTimer == 0)
         {
            // Bad throw for some reason
         }
         else
         {
            %player.use(Mine);
            %player.mineTimer = 0;
         }
      }
   }
   else if (%triggerNum == 3)
   {
      // val = 1 when jet key (LMB) first pressed down
      // val = 0 when jet key released
      // MES - do we need this at all any more?
      if(%val == 1)
         %player.isJetting = true;
      else
         %player.isJetting = false;
   }
}

function Player::setMoveState(%obj, %move)
{
   %obj.disableMove(%move);
}

function Armor::onLeaveMissionArea(%data, %obj)
{
   Game.leaveMissionArea(%data, %obj);
}

function Armor::onEnterMissionArea(%data, %obj)
{
   Game.enterMissionArea(%data, %obj);
}

function Armor::animationDone(%data, %obj)
{
   if(%obj.animResetWeapon !$= "")
   {
      if(%obj.getMountedImage($WeaponSlot) == 0)
         if(%obj.inv[%obj.lastWeapon])
            %obj.use(%obj.lastWeapon);
      %obj.animSetWeapon = "";
   }
}

function playDeathAnimation(%player, %damageLocation, %type)
{
   %vertPos = firstWord(%damageLocation);
   %quadrant = getWord(%damageLocation, 1);
   
   //echo("vert Pos: " @ %vertPos);
   //echo("quad: " @ %quadrant);
   
   if( %type == $DamageType::Explosion || %type == $DamageType::Mortar || %type == $DamageType::Grenade) 
   {
      if(%quadrant $= "front_left" || %quadrant $= "front_right") 
         %curDie = $PlayerDeathAnim::ExplosionBlowBack;
      else
         %curDie = $PlayerDeathAnim::TorsoBackFallForward;
   }
   else if(%vertPos $= "head") 
   {
      if(%quadrant $= "front_left" ||  %quadrant $= "front_right" ) 
         %curDie = $PlayerDeathAnim::HeadFrontDirect;
      else 
         %curDie = $PlayerDeathAnim::HeadBackFallForward;
   }
   else if(%vertPos $= "torso") 
   {
      if(%quadrant $= "front_left" ) 
         %curDie = $PlayerDeathAnim::TorsoLeftSpinDeath;
      else if(%quadrant $= "front_right") 
         %curDie = $PlayerDeathAnim::TorsoRightSpinDeath;
      else if(%quadrant $= "back_left" ) 
         %curDie = $PlayerDeathAnim::TorsoBackFallForward;
      else if(%quadrant $= "back_right") 
         %curDie = $PlayerDeathAnim::TorsoBackFallForward;
   }
   else if (%vertPos $= "legs") 
   {
      if(%quadrant $= "front_left" ||  %quadrant $= "back_left") 
         %curDie = $PlayerDeathAnim::LegsLeftGimp;
      if(%quadrant $= "front_right" || %quadrant $= "back_right") 
         %curDie = $PlayerDeathAnim::LegsRightGimp;
   }
   
   if(%curDie $= "" || %curDie < 1 || %curDie > 11)
      %curDie = 1;
   
   %player.setActionThread("Death" @ %curDie);
}

function Armor::onDamage(%data, %obj)
{
   if(%obj.station !$= "" && %obj.getDamageLevel() == 0)
      %obj.station.getDataBlock().endRepairing(%obj.station);
}
