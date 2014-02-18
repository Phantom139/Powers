//Flicker
datablock AudioProfile(FlickerBoomSound)
{
   filename    = "fx/misc/MA1.WAV";
   description = AudioClosest3d;
   preload = true;
};

datablock ShockwaveData(FlickerBoomShockwave)
{
   width = 10.0;
   numSegments = 150;
   numVertSegments = 2;
   velocity = 5;
   acceleration = 30.0;
   lifetimeMS = 2000;
   height = 1.0;
   is2D = true;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "0.5 0.0 0.8 1.00";
   colors[1] = "0.8 0.0 0.5 0.20";
   colors[2] = "0.5 0.0 0.8 0.0";
};

datablock ExplosionData(FlickerExplosion)
{
   soundProfile   = FlickerBoomSound;
   shockwave[0] =  FlickerBoomShockwave;

   faceViewer           = false;
};

datablock LinearFlareProjectileData(FlickerBolt) {
   emitterDelay        = -1;
   directDamage        = 0;
   radiusDamageType    = $DamageType::Flicker;
   hasDamageRadius     = true;
   indirectDamage      = 0.8;
   damageRadius        = 10.0;
   kickBackStrength    = 0.0;
   bubbleEmitTime      = 1.0;

   sound = PlasmaProjectileSound;
   velInheritFactor    = 0.5;

   explosion           = "FlickerExplosion";
   splash              = BlasterSplash;

   grenadeElasticity = 0.998;
   grenadeFriction   = 0.0;
   armingDelayMS     = 500;

   muzzleVelocity    = 100.0;

   drag = 0.05;

   gravityMod        = 0.0;

   dryVelocity       = 100.0;
   wetVelocity       = 80.0;

   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 6000;

   lifetimeMS     = 6000;

   scale             = "1 1 1";
   numFlares         = 48;
   flareColor        = "0.5 0 1";
   flareModTexture   = "special/shrikeBoltCross";
   flareBaseTexture  = "special/shrikeBolt";
};

