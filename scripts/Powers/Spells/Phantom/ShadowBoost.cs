datablock ShockwaveData(ShadowBoostShockwave)//Got this trail from scripts/weapons/admin/photonmissile.cs
{
   width = 30.0;
   numSegments = 32;
   numVertSegments = 6;
   velocity = 10;
   acceleration = 20.0;
   lifetimeMS = 900;
   height = 15.0;
   verticalCurve = 2;
   is2D = false;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 10.0;

   times[0] = 4.0;
   times[1] = 4.5;
   times[2] = 5.0;

   colors[0] = "1 0 1";
   colors[1] = "1 0 1";//0.4 0.1 1.0
   colors[2] = "1 0 1";

   mapToTerrain = false;
   orientToNormal = true;
   renderBottom = true;
};

datablock ExplosionData(ShadowBoostWaves) {
   //soundProfile   = PhotonShockwaveSound;

   shockwave = ShadowBoostShockwave;
   shockwaveOnTerrain = false;
};

datablock LinearFlareProjectileData(ShadowBoostWave) {
   projectileShapeName = "plasmabolt.dts";     //this could be whatever
   scale               = "1.0 1.0 1.0";        // Sized down from 2.0
   faceViewer          = true;
   directDamage        = 0.0;
   directDamageType    = $DamageType::ShadowStrike;
   hasDamageRadius     = true;
   indirectDamage      = 0;                   // Lets have gravity cause the damage
   damageRadius        = 12.0;                  // Anything in this radius gets sucked in
   impulse             = true;
   kickbackstrength    = -4000.0;

   explosion           = "ShadowBoostWaves";
   splash              = PlasmaSplash;

   dryVelocity       = 350;
   wetVelocity       = 350;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 0;
   lifetimeMS        = 32;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.5;
   size[2]           = 0.1;


   numFlares         = 35;
   flareColor        = "1 0 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound        = PhotonShockwaveSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 8.0;
   lightColor  = "0 1 0";
};

function boostPulse(%data, %obj, %slot) {
   if(!%obj.isAlive()) {
      return;
   }
    %vector = %obj.getMuzzleVector(%slot);
    %mp = %obj.getMuzzlePoint(%slot);

    %p = new (LinearFlareProjectile)() {
       dataBlock        = ShadowBoostWave;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
    };
    MissionCleanup.add(%p);
}

function FireBoosters(%data, %obj, %slot) {
     schedule(100, 0, boostPulse, %data, %obj, %slot);
     schedule(200, 0, boostPulse, %data, %obj, %slot);
     schedule(300, 0, boostPulse, %data, %obj, %slot);
     schedule(400, 0, boostPulse, %data, %obj, %slot);
     schedule(500, 0, boostPulse, %data, %obj, %slot);
     schedule(600, 0, boostPulse, %data, %obj, %slot);
     schedule(700, 0, boostPulse, %data, %obj, %slot);
     schedule(800, 0, boostPulse, %data, %obj, %slot);
     schedule(900, 0, boostPulse, %data, %obj, %slot);
     schedule(1000, 0, boostPulse, %data, %obj, %slot);
     schedule(1100, 0, boostPulse, %data, %obj, %slot);
     schedule(1200, 0, boostPulse, %data, %obj, %slot);
     schedule(1300, 0, boostPulse, %data, %obj, %slot);
     schedule(1400, 0, boostPulse, %data, %obj, %slot);
     schedule(1500, 0, boostPulse, %data, %obj, %slot);
}
