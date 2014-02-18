datablock ParticleData(DesolationInitExpFlameParticle) {
   dragCoefficient      = 0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -1.1;
   lifetimeMS           = 2000;
   lifetimeVarianceMS   = 0;
   textureName          = "special/cloudflash";
   colors[0]     = "1 0.18 0.03 0.6";
   colors[1]     = "1 0.18 0.03 0.0";
   sizes[0]      = 7;
   sizes[1]      = 8;
};

datablock ParticleEmitterData(DesolationInitExpFlameEmitter) {
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionOffset = 2.0;
   ejectionVelocity = 30.0;
   velocityVariance = 10.0;
   thetaMin         = 0.0;
   thetaMax         = 90.0;
   lifetimeMS       = 250;

   particles = "DesolationInitExpFlameParticle";
};

datablock ParticleData(DesolationExpGroundBurnParticle) {
   dragCoefficient      = 2;
   gravityCoefficient   = -0.4;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 3000;
   lifetimeVarianceMS   = 0;
   textureName          = "special/cloudflash";
   colors[0]     = "1 0.18 0.03 0.6";
   colors[1]     = "1 0.18 0.03 0.0";
   sizes[0]      = 6;
   sizes[1]      = 6.75;
};

datablock ParticleEmitterData(DesolationExpGroundBurnEmitter) {
   ejectionPeriodMS = 4;
   periodVarianceMS = 0;
   ejectionOffset = 0.0;
   ejectionVelocity = 10.0;
   velocityVariance = 10.0;
   thetaMin         = 87.0;
   thetaMax         = 88.0;
   lifetimeMS       = 5000;

   particles = "DesolationExpGroundBurnParticle";
};

datablock ExplosionData(DesolationExplosion) {
   soundProfile   = MortarExplosionSound;
   emitter[0] = DesolationInitExpFlameEmitter;
   emitter[1] = DesolationExpGroundBurnEmitter;

   explosionShape = "effect_plasma_explosion.dts";
   faceViewer = true;
   lifetimeMS = 5000;
   playSpeed = 0.7;

   sizes[0] = "7.0 7.0 7.0";
   sizes[1] = "7.0 7.0 7.0";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock TracerProjectileData(DesolationSubExplosion) {
   doDynamicClientHits = true;

   directDamage        = 0.0;
   directDamageType    = $DamageType::Plasma;
   explosion           = DesolationExplosion;
   splash              = ChaingunSplash;

   hasDamageRadius     = true;
   indirectDamage      = 0.1;
   damageRadius        = 5;
   radiusDamageType    = $DamageType::Plasma;

   kickBackStrength  = 5;
   sound             = ChaingunProjectile;

   dryVelocity       = 30.0;
   wetVelocity       = 30.0;
   velInheritFactor  = 0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 6000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 1.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.20;
   crossSize       = 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = ChaingunDecal1;
   decalData[1] = ChaingunDecal2;
   decalData[2] = ChaingunDecal3;
   decalData[3] = ChaingunDecal4;
   decalData[4] = ChaingunDecal5;
   decalData[5] = ChaingunDecal6;

   hasLight    = true;
   lightRadius = 1.0;
   lightColor  = "0.5 0.5 0.175";
};

function DesolationSubExplosion::onExplode(%data, %proj, %pos, %mod) {
   if(%proj.count < 1) {
      %vec = vectorscale(vectornormalize(%proj.vector), 24);
      %result = containerRayCast(vectoradd(%pos,"0 0 10"), vectoradd(%pos,%vec), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType, %proj);
      if(%result) {
         schedule(5, 0, "DesolationFindNewDir", %pos, %vec, %proj.sourceobject, %proj.count, 0);
      }
	  else {
	     %rndvec = (getRandom(1, 10) - 5)@" "@(getRandom(1, 10) - 5)@" "@((getRandom() * 5) + 5);
	     %newvec = vectoradd(%vec,%rndvec);
	     %newvec = vectoradd(%pos,%newvec);
	     %p = new TracerProjectile() {
		     dataBlock        = DesolationSubExplosion;
		     initialDirection = "0 0 -1";
		     initialPosition  = %newvec;
		     sourceObject     = %proj.sourceobject;
             sourceSlot       = 5;
	     };
	     %p.sourceobject = %proj.sourceobject;
	     %p.vector = %vec;
	     %p.count = %proj.count + 1;
	  }
   }
   if (%data.hasDamageRadius) {
      RadiusExplosion(%proj, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %proj.sourceObject, %data.radiusDamageType);
      InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 20.0, $TypeMasks::PlayerObjectType);
      while ((%ply = ContainerSearchNext()) != 0) {
         if(CanAOEHit(%pos, 20, %ply)) {
            applyBurn(%proj.sourceObject, %ply, 15);
         }
      }
   }
}

function DesolationFindNewDir(%pos, %vec, %source, %count, %count2) {
   if(%count2 == 2) {
	  %rndvec = getRandom(1, 10)@" "@getRandom(1, 10)@" "@((getRandom() * 5) + 4);
	  %newvec = vectoradd(%pos,%rndvec);
	  %p = new TracerProjectile() {
	      dataBlock        = DesolationSubExplosion;
	      initialDirection = "0 0 -1";
	      initialPosition  = %newvec;
	      sourceObject     = %source;
          sourceSlot       = 5;
      };
	  %p.sourceobject = %source;
	  %p.vector = %vec;
	  %p.count = %count+1;
	  return;
   }
   if(%count2 == 1) {
	  %vec = vectorscale(%vec,-1);
      %result = containerRayCast(vectoradd(%pos,"0 0 10"), vectoradd(%pos,%vec), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType, 0);
	  if(!(%result)) {
	     %rndvec = getRandom(1, 10)@" "@getRandom(1, 10)@" "@((getRandom() * 5) + 4);
	     %newvec = vectoradd(%vec,%rndvec);
	     %newvec = vectoradd(%pos,%newvec);
	     %p = new TracerProjectile() {
		     dataBlock        = DesolationSubExplosion;
		     initialDirection = "0 0 -1";
		     initialPosition  = %newvec;
		     sourceObject     = %source;
   		     sourceSlot       = 5;
	     };
	     %p.sourceobject = %source;
	     %p.vector = %vec;
	     %p.count = %count+1;
	     return;
	  }
   }
   else {
	  %chance = getrandom(1,4);
	  if(%chance <= 2){
	     %nv2 = (getword(%vec, 0) * -1);
	     %nv1 = getword(%vec, 1);
	     %vec = %nv1@" "@%nv2@" 0";
	  }
      else{
	     %nv2 = getword(%vec, 0);
	     %nv1 = (getword(%vec, 1) * -1);
	     %vec = %nv1@" "@%nv2@" 0";
	  }
      %result = containerRayCast(vectoradd(%pos,"0 0 10"), vectoradd(%pos,%vec), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType, 0);
	  if(!(%result)) {
	     %rndvec = getRandom(1, 10)@" "@getRandom(1, 10)@" "@((getRandom() * 5) + 4);
	     %newvec = vectoradd(%vec,%rndvec);
	     %newvec = vectoradd(%pos,%newvec);
	     %p = new TracerProjectile() {
		     dataBlock        = DesolationSubExplosion;
		     initialDirection = "0 0 -1";
		     initialPosition  = %newvec;
		     sourceObject     = %source;
   		     sourceSlot       = 5;
	     };
	     %p.sourceobject = %source;
	     %p.vector = %vec;
	     %p.count = %count+1;
	     return;
	  }
   }
   %count2++;
   schedule(2, 0, "DesolationFindNewDir", %pos, %vec, %source, %count, %count2);
}

function DropDesolation(%g, %target) {
   if(!isObject(%g) || %g.getState() $= "dead") {
      return;
   }
   //First, Specify All Directions
   %vec[1] = vectorscale(vectornormalize("1 0 0"), 24);  // +X 0Y
   %vec[2] = vectorscale(vectornormalize("1 1 0"), 24);  // +X +Y
   %vec[3] = vectorscale(vectornormalize("1 -1 0"), 24); // +X -Y
   %vec[4] = vectorscale(vectornormalize("-1 0 0"), 24); // -X 0Y
   %vec[5] = vectorscale(vectornormalize("-1 1 0"), 24); // -X +Y
   %vec[6] = vectorscale(vectornormalize("-1 -1 0"), 24); //-X -Y
   %vec[7] = vectorscale(vectornormalize("0 1 0"), 24);  // 0X +Y
   %vec[8] = vectorscale(vectornormalize("0 -1 0"), 24); // 0X -Y
   //Oh.. long crap
   for(%i = 1; %i <= 8; %i++) {
      %p = new TracerProjectile() {
  	      dataBlock        = DesolationSubExplosion;
  	      initialDirection = "0 0 30";
     	  initialPosition  = vectorAdd(%target, "0 0 -25");
  	      sourceSlot       = 5;
      };
      %p.sourceObject = %g;
      %p.vector = %vec[%i];
      %p.count = 2;
   }
}
