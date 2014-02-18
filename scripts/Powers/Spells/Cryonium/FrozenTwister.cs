datablock LinearProjectileData(HailShardProjectile) {
   projectileShapeName = "disc.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.45;
   damageRadius        = 4.5;
   radiusDamageType    = $DamageType::Freeze;
   kickBackStrength    = 1750;

   sound 				= discProjectileSound;
   explosion           = "DiscExplosion";
   underwaterExplosion = "UnderwaterDiscExplosion";
   splash              = DiscSplash;

   dryVelocity       = 300;
   wetVelocity       = 300;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 5000;
   lifetimeMS        = 5000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = 200;

   hasLight    = true;
   lightRadius = 6.0;
   lightColor  = "0.175 0.175 0.5";
};

function HailShardProjectile::onExplode(%data, %proj, %pos, %mod) {
   parent::onExplode(%data, %proj, %pos, %mod);
   //radius freeze
   InitContainerRadiusSearch(%proj.getWorldBoxCenter(), 5.5, $TypeMasks::PlayerObjectType);
   while ((%ply = ContainerSearchNext()) != 0) {
      if(%ply != %proj.sourceObject) {
         FreezeObject(%ply, 1000, true);
      }
   }
}

function FrozenTwisterLoop(%obj) {
   if(!isObject(%obj) || %obj.getState() $= "Dead") {
      return;
   }
   if(!%obj.CastingContinuous) { //we don't continue
      %obj.setMoveState(false);
      return;
   }
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);
   if(%energy < 35) {
      CommandToClient(%client, 'BottomPrint', "<spush><font:Sui Generis:14>Cryonic Spear<spop>\n<spush><font:Arial:14>Frozen Twister out of energy, casting stopped.<spop>", 3, 3 );
      %obj.CastingContinuous = 0;
      %obj.setMoveState(false);
      return;
   }
   else {
      CommandToClient(%client, 'BottomPrint', "<spush><font:Sui Generis:14>Cryonic Spear<spop>\n<spush><font:Arial:14>Casting Frozen Twister, Press Fire To Stop \n Casting will also end if you run out of energy or die.<spop>", 3, 3 );
      TakeEnergy(%client, 35);
      //
      DoUnboundedVortex(%obj, %obj.getPosition(), 25, 6, 1000);
      DropHailShards(%obj);
      %obj.playShieldEffect("1 1 1");
      //
      schedule(300, 0, "FrozenTwisterLoop", %obj);
   }
}

function DropHailShards(%obj) {
   %objPos = %obj.getPosition();
   %startPos = "0 0 100";
   %hitLoc[0] = "6 0 0";
   %hitLoc[1] = "-6 0 0";
   %hitLoc[2] = "0 -6 0";
   %hitLoc[3] = "0 6 0";
   for(%i = 0; %i < 4; %i++) {
      %p = new (LinearProjectile)() {
         dataBlock        = HailShardProjectile;
         initialDirection = "0 0 -3";
         initialPosition  = vectorAdd(vectorAdd(%objPos, %hitLoc[%i]), %startPos);
         damageFactor     = 1;
      };
      MissionCleanup.add(%p);
      %p.sourceObject = %obj;
   }
}
