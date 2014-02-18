//Nightmare
function createEmitter(%pos, %emitter, %rot)
{
    %dummy = new ParticleEmissionDummy()
    {
          position = %pos;
          rotation = %rot;
          scale = "1 1 1";
          dataBlock = defaultEmissionDummy;
          emitter = %emitter;
          velocity = "1";
    };
    MissionCleanup.add(%dummy);
    //%dummy.setRotation(%rot);

    if(isObject(%dummy))
       return %dummy;
}

datablock ParticleData(NightmareGlobeSmoke)
{
dragCoefficient = 50;/////////-----------------------
gravityCoefficient = 0.0;
inheritedVelFactor = 1.0;
constantAcceleration = 0.0;
lifetimeMS = 5050;
lifetimeVarianceMS = 0;
useInvAlpha = true;
spinRandomMin = -360.0;
spinRandomMax = 360.0;
textureName = "particleTest";
colors[0] = "0.1 0.1 0.1 1.0";// ////////////////////
colors[1] = "0.1 0.1 0.1 1.0";// ////////////////////
colors[2] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\
colors[3] = "0.1 0.1 0.1 1.0";// \\\\\\\\\\\\\\\\\\\\
sizes[0] = 1.0;
sizes[1] = 1.0;
sizes[2] = 1.0;
sizes[3] = 1.0;
times[0] = 0.0;
times[1] = 0.33;
times[2] = 0.66;
times[3] = 1.0;
mass = 0.7;
elasticity = 0.2;
friction = 1;
computeCRC = true;
haslight = true;
lightType = "PulsingLight";
lightColor = "0.2 0.0 0.5 1.0";
lightTime = "200";
lightRadius = "2.0";
};

datablock ParticleEmitterData(NightmareGlobeEmitter)
{
ejectionPeriodMS = 0.004;
periodVarianceMS = 0;
ejectionVelocity = 0.0;
velocityVariance = 0.0;
ejectionOffset = 5;
thetaMin = 0;
thetaMax = 180;
overrideAdvances = false;
particles = "NightmareGlobeSmoke";
};

datablock AudioProfile(NightmareScreamSound)
{
   filename    = "voice/male1/avo.deathcry_02.wav";
   description = AudioClose3d;
   preload = true;
};

datablock LinearFlareProjectileData(NightmareShot)
{
   projectileShapeName = "weapon_missile_projectile.dts";
   scale               = "3.0 5.0 3.0";
   faceViewer          = true;
   directDamage        = 0.01;
   kickBackStrength    = 4000.0;
   DirectDamageType    = $DamageType::Nightmare;

   explosion           = "BlasterExplosion";

   dryVelocity       = 150.0;
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 10000;
   lifetimeMS        = 10000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 100;
   activateDelayMS = -1;

   baseEmitter = PulsePurpleEmitter;

   size[0]           = 0.0;
   size[1]           = 0.0;
   size[2]           = 0.0;


   numFlares         = 0;
   flareColor        = "0.0 0.0 0.0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

//implemented from my other mod (TWM-TWM2)
function NightmareShot::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal) {
   if(!isPlayer(%targetObject)) {
      return;
   }
   %targ = %targetObject.client;
   %Zombie = %projectile.sourceObject;
   %targ.nightmareticks = 0;
   Yvexnightmareloop(%zombie,%targ);
}

function Yvexnightmareloop(%zombie,%viewer) {
   %enum = getRandom(1,5);
   switch(%enum) {
   case 1:
   %emote = "sitting";
   case 2:
   %emote = "death5";
   case 3:
   %emote = "death3";
   case 4:
   %emote = "death2";
   case 5:
   %emote = "death4";
   }
   if(!isobject(%viewer.player) || %viewer.player.getState() $= "dead") {
   %viewer.nightmared = 0;
   return;
   }
   if(!isobject(%zombie)) {
   %viewer.nightmared = 0;
   %viewer.player.setMoveState(false);
   return;
   }
   if(%viewer.nightmareticks > 15) {
   %viewer.player.setMoveState(false);
   %viewer.nightmareticks = 0;
   %viewer.nightmared = 0;
   return;
   }
   %c = createEmitter(%viewer.player.getPosition(),NightmareGlobeEmitter,"1 0 0");      //Rotate it
   MissionCleanup.add(%c); // I think This should be used
   %c.schedule(500,"delete");
   %viewer.nightmareticks++;
   %viewer.player.setMoveState(true);
   %viewer.nightmared = 1;
   %viewer.player.setActionThread(%emote,true);
   %viewer.player.setWhiteout(1.8);
   %viewer.player.setDamageFlash(1.5);

   %zombie.playShieldEffect("1 1 1");
   serverPlay3D(NightmareScreamSound, %viewer.player.position);
   schedule(500,0,"Yvexnightmareloop",%zombie, %viewer);
   %viewer.player.damage(%zombie, %viewer.player.position, 0.1, $DamageType::Nightmare);
   %zombie.setDamageLevel(%zombie.getDamageLevel() - 0.1);

   schedule(1, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/fem1/avo.deathcry_02.wav");
   schedule(5, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/fem2/avo.deathcry_02.wav");
   schedule(10, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/fem3/avo.deathcry_02.wav");
   schedule(15, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/fem4/avo.deathcry_02.wav");
   schedule(20, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/fem5/avo.deathcry_02.wav");
   schedule(25, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/male1/avo.deathcry_02.wav");
   schedule(30, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/male2/avo.deathcry_02.wav");
   schedule(35, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/male3/avo.deathcry_02.wav");
   schedule(40, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/male4/avo.deathcry_02.wav");
   schedule(45, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/male5/avo.deathcry_02.wav");
   schedule(50, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/derm1/avo.deathcry_02.wav");
   schedule(55, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/derm2/avo.deathcry_02.wav");
   schedule(60, 0, "messageclient", %viewer, 'MsgClient', "~wvoice/derm3/avo.deathcry_02.wav");
}

