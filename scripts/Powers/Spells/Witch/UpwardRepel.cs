datablock LinearFlareProjectileData(UpwardRepelPulse) {
   scale               = "0.1 0.1 0.1";
   faceViewer          = false;
   directDamage        = 0.00001;
   hasDamageRadius     = false;
   indirectDamage      = 0.6;
   damageRadius        = 10.0;
   kickBackStrength    = 100.0;
   directDamageType    = $DamageType::Admin;
   indirectDamageType  = $DamageType::Admin;

   explosion           = "BlasterExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 200.0;
   wetVelocity       = 10;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 30000;
   lifetimeMS        = 30000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //baseEmitter         = PulsePurpleEmitter;
   //delayEmitter        = PulsePurpleEmitter;
   //bubbleEmitter       = PulsePurpleEmitter;

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

function UpwardRepelPulse::onCollision(%data,%projectile,%targetObject,%modifier,%position,%normal) {
   if (%targetObject.getClassName() $= "Player" && %targetObject.getstate() !$= "Dead") {
      LeviUpLoop(%targetObject);
   }
}

function LeviUpLoop(%target) {
   if(%target.leviloop >= 20) {
      %target.leviloop = 0;
      return;
   }
   if(%target.leviloop < 20) {
      if(%target.getstate() $="Dead") {
         %target.leviloop = 0;
         return;
      }
      %target.applyImpulse(%target.getPosition(), "0 0 10");
   }
   schedule(100,0,"LeviUpLoop",%target);
}
