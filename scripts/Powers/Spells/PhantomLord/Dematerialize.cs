datablock ShockwaveData(DematerializePulseShockwave) {
   width = 5.0;
   numSegments = 30;
   numVertSegments = 2;
   velocity = 8;
   verticalcurve = 0;
   acceleration = -17.0;
   lifetimeMS = 600;
   height = 0.00001;
   is2D = false;
   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 10.0;
   mapToTerrain = false;
   orientToNormal = true;
   renderBottom = true;
   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;
   colors[0] = "0.5 0.0 0.8 1";
   colors[1] = "0.5 0.0 0.8 0.60";//0.4 0.1 1.0
   colors[2] = "0.5 0.0 0.8 0.0";
};

datablock ExplosionData(DematerializePulseShockwaveExp) {
	faceViewer           = false;
	shockwave = DematerializePulseShockwave;

	shakeCamera = true;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 3.0;
};

datablock LinearProjectileData(DematerializePulseShockwaveProj) {
	projectileShapeName = "turret_muzzlepoint.dts";
	scale               = "0.1 0.1 0.1";
	faceViewer          = true;
	directDamage        = 0;
	hasDamageRadius     = false;
	indirectDamage      = 0.5;
	damageRadius        = 10;
	kickBackStrength    = 1;
	radiusDamageType    = $DamageType::Dematerialize;
	explosion           = "DematerializePulseShockwaveExp";
	dryVelocity       = 0.0001;
	wetVelocity       = 0.00001;
	velInheritFactor  = 0.0;
	lifetimeMS        = 0.00000001;
	explodeOnDeath    = true;
	reflectOnWaterImpactAngle = 0.0;
	explodeOnWaterImpact      = true;
	deflectionOnWaterImpact   = 0.0;
};

datablock LinearFlareProjectileData(DeMatPulse) {
   scale               = ".1 .1 .1";//6
   sound      = PlasmaProjectileSound;

   faceViewer          = true;
   directDamage        = 0.05;
   hasDamageRadius     = true;
   indirectDamage      = 0.3;
   damageRadius        = 10.0;
   kickBackStrength    = 4000;
   radiusDamageType    = $DamageType::Dematerialize;

   explosion           = "PlasmaExplosion";
   underwaterExplosion = "PlasmaExplosion";
   splash              = BlasterSplash;

   dryVelocity       = 100.0;
   wetVelocity       = 100.0;
   velInheritFactor  = 0.6;
   fizzleTimeMS      = 8000;
   lifetimeMS        = 8000;
   explodeOnDeath    = true;

   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 15000;

   activateDelayMS = 0;
   numFlares         = 35;
   flareColor        = "1.0 0 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   size[0]           = 1;
   size[1]           = 10;
   size[2]           = 2;


   hasLight    = true;
   lightRadius = 1.0;
   lightColor  = "1.0 0 0";
};

function DeMatPulse::onExplode(%data, %proj, %pos, %mod) {
   parent::onexplode(%data, %proj, %pos, %mod);
   cancel(%proj.ShockwaveSched);
}

function DematerializeShockwaves(%obj, %proj) {
   if(isobject(%proj)) {
      %fake = new (linearprojectile)() {
          dataBlock        = "DematerializePulseShockwaveProj";
		  initialDirection = %proj.initialDirection;
		  initialPosition  = %proj.position;
		  sourceSlot       = %obj;
  	  };
      %fake.sourceobject = %obj;
      MissionCleanup.add(%fake);

      //
      InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 10.0, $TypeMasks::PlayerObjectType);
      while ((%ply = ContainerSearchNext()) != 0) {
         if(%ply != %obj) {
            //kill!
            %ply.damage(%obj, %ply.getPosition(), 100, $DamageType::Dematerialize);
            //boom!
            if(%ply.getState() $= "dead") {
               %ply.blowup();
            }
         }
      }
      //

      %proj.ShockwaveSched = schedule(100, 0, "DematerializeShockwaves", %obj, %Proj);
   }
}

function DeMatPulse::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal) {
   if(!isObject(%targetObject) || %targetObject $= "" || %targetObject <= 0) {
      cancel(%proj.ShockwaveSched);
      return;
   }
   if ((!%targetObject.getType() & $AllObjMask) || %targetObject.getClassName() $= TerrainBlock || %targetObject.getClassName() $= InteriorInstance) {
      cancel(%proj.ShockwaveSched);
      return;
   }
   %targetObject.damage(%projectile.sourceObject, %position, %data.directDamage, %data.directDamageType);
}
