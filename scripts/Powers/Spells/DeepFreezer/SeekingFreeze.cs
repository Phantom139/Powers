datablock LinearFlareProjectileData(SeekingFreeze) {
   scale               = "4 4 4";//6
   sound      = PlasmaProjectileSound;

   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.4;
   damageRadius        = 10.0;
   kickBackStrength    = 4000;
   radiusDamageType    = $DamageType::Freeze; //obviously change this

   explosion           = "DiscExplosion";
   underwaterExplosion = "DiscExplosion";
   splash              = BlasterSplash;

   dryVelocity       = 40.0;
   wetVelocity       = 40.0;
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
   flareColor        = "0 206 209";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   size[0]           = 1;
   size[1]           = 10;
   size[2]           = 2;


   hasLight    = true;
   lightRadius = 1.0;
   lightColor  = "0.6 1.1 0";
};

function SeekingFreeze::onExplode(%data, %proj, %pos, %mod) {
   Parent::onExplode(%data, %proj, %pos, %mod);
   cancel(%proj.seeksched);
   cancel(%proj.seekschedcheck);
   //
   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 10.0, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      if(CanAOEHit(%pos, 10, %ply)) {
         FreezeObject(%ply, 7000, true);
      }
   }
}

//Seeking Projectile Code
//Credit To Abrikcham
//-abirikcham@yahoo.com
//Cleaned up and modified for my needs.
function SeekingFreezeTargetLockCheck(%obj, %proj) {
   if(!isObject(%proj)) {
      return;
   }
   %searchmask = $TypeMasks::PlayerObjectType;

   InitContainerRadiusSearch(%proj.getPosition(), 20, %searchmask);
   while (%target = containerSearchNext()) {
      if(%target != %obj) {
         if(%target.team == %obj.team) {
            %proj.seekschedcheck = schedule(50, 0, "SeekingFreezeTargetLockCheck", %obj, %Proj);
            return;
         }
         SeekingFreezeSeek(%obj, %proj, %target);
         return;
      }
   }
   %proj.seekschedcheck = schedule(50, 0, "SeekingFreezeTargetLockCheck", %obj, %Proj);
}


function SeekingFreezeSeek(%obj, %Proj, %target) {
   %projpos = %proj.getPosition();
   %projdir = %proj.initialdirection;
   %type = %target.getClassName();

   if(!isobject(%proj)) {
      return;
   }

   if(isobject(%proj)) {
      %proj.delete();
   }
   if(!isobject(%obj)) {
      return;
   }
   if(!isobject(%target)) {
      return;
   }
   if(%type $= "Player") {
      if(%target.getState() $= "Dead") {
         return;
      }
   }

   //( ... )the projs now have a max turn angle like real missile ub3r l33t;;;; nm wtf afasdf
   %test = getWord(%projdir, 0);
   %test2 = getWord(%projdir, 1);
   %test3 = getWord(%projdir, 2);

   %projdir = vectornormalize(vectorsub(%target.getPosition(), %projpos));
   %testa = getWord(%projdir, 0);
   %testa2 = getWord(%projdir, 1);
   %testa3 = getWord(%projdir, 2);

   %testthing = %test - %testa; //oh u can rename all this test stuff but make sure u get it right =/ dont break plz
   %testfin = %testthing / 16;  //!!!!!!!!!! OK HERE!!!! is where the max angle thing is... increase for lower turn angle and reduce for a higher turn angle
   %testfinal = %testfin * -1;   //^^^^^ *side note for the one above this* dont div by zero unless yer dumb (...) div by i think 1 if you want it to seek with a 360 max turn angle angle... kinda gay though if u do that
   %testfinale = %testfinal + %test;

   %testthing2 = %test2 - %testa2;
   %testfin2 = %testthing2 / 10; //change here too .. this is for the y axis  btw it's best if u leave my setting of 10 on ... it's the most balanced well nm change it to what u want but you really should leave it around this number like 9ish
   %testfinal2 = %testfin2 * -1;
   %testfinale2 = %testfinal2 + %test2;

   %testthing3 = %test3 - %testa3;
   %testfin3 = %testthing3 / 10; //z- axis this one is for i think.. mmm idea... you try playing with dif max angles for xyz for maybe like a sidewinder effect =?
   %testfinal3 = %testfin3 * -1;
   %testfinale3 = %testfinal3 + %test3;

   %haxordir = %testfinale SPC %testfinale2 SPC %testfinale3; //final dir.. .....

   %proj = new (linearflareprojectile)() {
      dataBlock        = SeekingFreeze;
      initialDirection = %haxordir;
      initialPosition  = %projpos;
      sourceslot = %obj;
   };
   %proj.sourceobject = %obj;
   MissionCleanup.add(%proj);

   %proj.seeksched = schedule(80, 0, "SeekingFreezeSeek", %obj, %Proj, %target);
}
