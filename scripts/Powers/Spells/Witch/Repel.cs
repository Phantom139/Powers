datablock LinearFlareProjectileData(RepelPulse) {
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

function RepelPulse::onCollision(%data,%projectile,%targetObject,%modifier,%position,%normal) {
   if (%targetObject.getClassName() $= "Player" && %targetObject.getstate() !$= "Dead") {
      LeviLoop(%projectile.sourceObject,%targetObject);
   }
}

function LeviLoop(%orig,%target) {
   if(%target.leviloop >= 20) {
      %target.leviloop = 0;
      return;
   }
   if(%target.leviloop < 20) {
      if(%orig.getstate() $="Dead") {
         %target.leviloop = 0;
         return;
      }
      if(%target.getstate() $="Dead") {
         %target.leviloop = 0;
         return;
      }
      %tmp[1] = vectorsub(vectorsub(%orig.getforwardvector(), "0 0 0"), %target.getposition());
      %tmp[2] = vectornormalize(%tmp[1]);
      %tmp[3] = vectorscale(%tmp[2], 30 * %orig.getdatablock().mass);
      %target.applyimpulse(%target.getposition(), %tmp[3]);
      %target.leviloop++;
   }
   schedule(100,0,"LeviLoop",%orig,%target);
}
