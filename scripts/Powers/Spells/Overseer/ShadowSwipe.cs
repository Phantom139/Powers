datablock ShockwaveData(ShadowSwipeShockwave) {
   width = 10.0;
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

datablock ExplosionData(ShadowSwipeWaves) {
   //soundProfile   = PhotonShockwaveSound;

   shockwave = ShadowSwipeShockwave;
   shockwaveOnTerrain = false;
};

datablock LinearFlareProjectileData(ShadowSwipeWave) {
   projectileShapeName = "plasmabolt.dts";     //this could be whatever
   scale               = "1.0 1.0 1.0";        // Sized down from 2.0
   faceViewer          = true;
   directDamage        = 0.8;
   directDamageType    = $DamageType::ShadowSwipe;
   hasDamageRadius     = true;
   indirectDamage      = 0;                   // Lets have gravity cause the damage
   damageRadius        = 10.0;                  // Anything in this radius gets sucked in
   impulse             = true;
   kickbackstrength    = -2000.0;

   explosion           = "ShadowSwipeWaves";
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

function ShadowSwipePulse(%data, %obj, %slot) {
   if(!%obj.isAlive()) {
      return;
   }
    %vector = %obj.getMuzzleVector(%slot);
    %mp = %obj.getMuzzlePoint(%slot);

    %p = new (LinearFlareProjectile)() {
       dataBlock        = ShadowSwipeWave;
       initialDirection = %vector;
       initialPosition  = %mp;
       sourceObject     = %obj;
       damageFactor     = 1;
       sourceSlot       = %slot;
    };
    MissionCleanup.add(%p);
}

function FireShadowSwipe(%data, %obj, %slot) {
     schedule(100, 0, ShadowSwipePulse, %data, %obj, %slot);
     schedule(200, 0, ShadowSwipePulse, %data, %obj, %slot);
     schedule(300, 0, ShadowSwipePulse, %data, %obj, %slot);
     schedule(400, 0, ShadowSwipePulse, %data, %obj, %slot);
     schedule(500, 0, ShadowSwipePulse, %data, %obj, %slot);
     schedule(600, 0, ShadowSwipePulse, %data, %obj, %slot);
     schedule(700, 0, ShadowSwipePulse, %data, %obj, %slot);
     schedule(800, 0, ShadowSwipePulse, %data, %obj, %slot);
     schedule(900, 0, ShadowSwipePulse, %data, %obj, %slot);
}
