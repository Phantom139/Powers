datablock ExplosionData(FissureExplosion) {
   explosionShape = "effect_plasma_explosion.dts";
   soundProfile   = plasmaExpSound;
   particleEmitter = FireballExplosionEmitter;
   particleDensity = 100;
   particleRadius = 1.25;
   faceViewer = true;

   sizes[0] = "15.0 15.0 15.0";
   sizes[1] = "15.0 15.0 15.0";
   times[0] = 0.0;
   times[1] = 1.5;
};


datablock LinearProjectileData(FissureBurstExplosionProjectile) {
   projectileShapeName = "mortar_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.8;
   damageRadius        = 20.0;
   radiusDamageType    = $DamageType::FireBall;
   kickBackStrength    = 3000;

   explosion           = "FissureExplosion";
//   underwaterExplosion = "UnderwaterFireballExplosion";
   velInheritFactor    = 0.5;
//   splash              = FireballSplash;
   depthTolerance      = 10.0; // depth at which it uses underwater explosion

   grenadeElasticity = 0.15;
   grenadeFriction   = 0.4;
   armingDelayMS     = 5;
   muzzleVelocity    = 63.7;
   drag              = 0.1;

   sound          = MortarProjectileSound;

   dryVelocity       = 80;
   wetVelocity       = 50;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 5000;
   lifetimeMS        = 2700;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 5000;

};

datablock SeekerProjectileData(FissurePointer) {
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   kickBackStrength    = 2000;

   explosion           = "MissileExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;

   baseEmitter         = MissileSmokeEmitter;
   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = 30000;
   muzzleVelocity      = 10.0;
   maxVelocity         = 50.0;
   turningSpeed        = 40.0;
   acceleration        = 2.5;

   proximityRadius     = 3;

   terrainAvoidanceSpeed         = 180;
   terrainScanAhead              = 25;
   terrainHeightFail             = 12;
   terrainAvoidanceRadius        = 100;

   flareDistance = 200;
   flareAngle    = 30;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 550;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = false;
};

function createFissurePulseOne(%sourceObject) {
   if(!%sourceObject.isAlive()) {
      return;
   }
   %mp = %sourceObject.getMuzzlePoint(0);
   %vec = %sourceObject.getMuzzleVector(0);
   // This projectile is the pointer for the explosions on the surface.
   %TPos = getWords(%mp, 0, 1) SPC (getTerrainHeight(%mp) + 3000);
   //
   %p = new SeekerProjectile() {
      datablock = FissurePointer;
      InitialPosition = %TPos;
      InitialDirection = %vec;
   };
   %p.sourceObject = %sourceObject;
   MissionCleanup.add(%p);
   //
   schedule(1500, 0, "FissureBurstLoop", %sourceObject, %p, 0);
}

function FissureBurstLoop(%src, %proj, %count) {
   if(!%src.isAlive()) {
      if(isObject(%proj)) {
         %proj.delete();
         return;
      }
      return;
   }
   if(%count > 20) {
      if(isObject(%proj)) {
         %proj.delete();
         return;
      }
      return;
   }
   else {
      %count++;
      //
      %grd = getTerrainHeight(%proj.getPosition());
      %explo = getWords(%proj.getPosition(), 0, 1) SPC %grd;
      //
      %fisBurst = new LinearProjectile() {
         datablock = FissureBurstExplosionProjectile;
         InitialPosition = vectorAdd(%explo, "0 0 1");
         InitialDirection = "0 0 -1";
      };
      %fisBurst.sourceObject = %src;
      MissionCleanup.add(%fisBurst);
      //
      schedule(500, 0, "FissureBurstLoop", %src, %proj, %count);
   }
}
