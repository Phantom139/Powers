function spawndropprojectile(%proj,%type,%pos,%direction,%src) {
if(%src $= "" || !%src) {
%src = "";
}
%p = new (%type)() {
dataBlock        = %proj;
initialDirection = %direction;
initialPosition  = %pos;
damageFactor     = 1;
};
MissionCleanup.add(%p);
%p.sourceObject = %src;
return %p;
}


datablock ShockwaveData(FlasherBoomShockwaveInitial)
{
   width = 10.0;
   numSegments = 150;
   numVertSegments = 2;
   velocity = 5;
   acceleration = 10.0;
   lifetimeMS = 5000;
   height = 1.0;
   is2D = true;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "1 0 0 1.00";
   colors[1] = "1 0 0 0.20";
   colors[2] = "1 0 0 0.0";
};

datablock ExplosionData(FlasherExplosionInitial)
{
   soundProfile   = FlasherBoomSound;
   shockwave[0] =  FlasherBoomShockwaveInitial;

   faceViewer           = false;
};

datablock LinearFlareProjectileData(FlasherBolt1) {
   emitterDelay        = -1;
   directDamage        = 0;
   radiusDamageType    = $DamageType::Flasher;
   hasDamageRadius     = true;
   indirectDamage      = 0.4;
   damageRadius        = 10.0;
   kickBackStrength    = 0.0;
   bubbleEmitTime      = 1.0;

   sound = PlasmaProjectileSound;
   velInheritFactor    = 0.5;

   explosion           = "FlasherExplosionInitial";
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

//the main explosion
datablock ShockwaveData(FlasherBoomShockwaveFinal)
{
   width = 30.0;
   numSegments = 150;
   numVertSegments = 2;
   velocity = 5;
   acceleration = 20.0;
   lifetimeMS = 2000;
   height = 1.0;
   is2D = true;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "1 0 0 1.00";
   colors[1] = "1 0 0 0.20";
   colors[2] = "1 0 0 0.0";
};

datablock ExplosionData(FlasherExplosionFinal)
{
   soundProfile   = FlasherBoomSound;
   shockwave[0] =  FlasherBoomShockwaveFinal;

   faceViewer           = false;
};

datablock LinearFlareProjectileData(FlasherBolt2) {
   emitterDelay        = -1;
   directDamage        = 0;
   radiusDamageType    = $DamageType::Flasher;
   hasDamageRadius     = true;
   indirectDamage      = 0.9;
   damageRadius        = 30.0;
   kickBackStrength    = 450.0;
   bubbleEmitTime      = 1.0;

   sound = PlasmaProjectileSound;
   velInheritFactor    = 0.5;

   explosion           = "FlasherExplosionFinal";
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

//flasher code
function FlasherBolt1::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   //prepare to drop the second one
   schedule(1750, 0, "spawndropprojectile", FlasherBolt2, LinearFlareProjectile,
      vectorAdd(%pos, "0 0 1"), "0 0 -5", %proj.sourceObject);
}
