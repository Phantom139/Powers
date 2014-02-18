datablock LinearFlareProjectileData(SnowstormPulse) {
   projectileShapeName = "plasmabolt.dts";
   scale               = "0.1 0.1 0.1";
   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.2;
   damageRadius        = 2500.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Freeze;

   explosion           = "BlasterExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 55.0;
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 300;
   lifetimeMS        = 300;
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
   flareColor        = "1 0.75 0.25";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

function SnowstormPulse::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 2500.0, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      if(CanAOEHit(%pos, 2500, %ply)) {
         if(%ply.team != %proj.sourceObject.team) {
            FreezeObject(%ply, 20000, true);
         }
      }
   }
}

//
function BeginSnowstorm() {
   %snowstorm = new Precipitation() {
      position = "0 0 0";
      rotation = "1 0 0 0";
      scale = "100 100 100";
      dataBlock = "Snow";
      percentage = "1";
      color1 = "1 1 1 1.000000";
      color2 = "-1 -1 -1 1";
      color3 = "-1 -1 -1 1.000000";
      offsetSpeed = "0.25";
      minVelocity = "0.75";//0.25
      maxVelocity = "1.5";  //1.5
      maxNumDrops = "20000";
      maxRadius = "50";
   };
   
   return %snowstorm;
}

function BeginFreezeBlast(%mem, %player) {
   %pos = %player.getPosition();
   %s_pos = vectorAdd(%pos, "0 0 -3");
   
   %p = new (LinearFlareProjectile)() {
      dataBlock        = SnowstormPulse;
      initialDirection = "0 0 -5";
      initialPosition  = %s_pos;
      damageFactor     = 1;
   };
   %p.sourceObject = %player;
   
   %mem.freezePulses = schedule(500, 0, BeginFreezeBlast, %mem, %player);
}

function BeginWhiteout(%mem, %player) {
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %test = ClientGroup.getObject(%i);
      if(%test != %player.client) {
         if(isObject(%test.player) && %test.player.getState() !$= "dead") {
            if(%test.player.team != %player.team) {
               %test.player.setWhiteout(0.8);
            }
         }
      }
   }
   %mem.whiteout = schedule(500, 0, BeginWhiteout, %mem, %player);
}

function MapEventManager::useMapPower_Snowstorm(%this, %player) {
   %this.beginHolderCheckup(%player, 20000);

   %player.lastPosition = %player.getTransform();
   %player.setTransform(vectorAdd(%player.getTransform(), "0 0 150"));
   HoverPlayer(%player);
   
   %player.isPlayerInvincible = true;
   %player.snowstormInvReset = schedule(20000, 0, eval, ""@%player@".isPlayerInvincible = false;");

   %this.mapSnow = BeginSnowstorm();
   %this.freezePulses = BeginFreezeBlast(%this, %player);
   %this.whiteout = BeginWhiteout(%this, %player);
   
   %this.snowstormEnd = schedule(20000, 0, endSnowstorm, %this, %player);
}

function MapEventManager::handleDeath_Snowstorm(%this, %player) {
   //suicide much?
   cancel(%player.snowstormInvReset); //no need to eval on dead guys
   cancel(%this.snowstormEnd);
   
   endSnowstorm(%this, %player);
}

function endSnowstorm(%mem, %player) {
   //remove the snow
   %mem.mapSnow.schedule(250, delete);

   cancel(%mem.freezePulses);
   cancel(%mem.whiteout);
   if(isObject(%player) && %player.getState() !$= "dead") {
      KillHoverpad(%player, true);
      %player.setTransform(%player.lastPosition);
   }
}
