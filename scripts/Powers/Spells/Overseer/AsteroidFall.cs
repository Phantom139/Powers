datablock GrenadeProjectileData(AsteroidFallProjo) {
	projectileShapeName  = "plasmabolt.dts";
	scale                = "80.0 80.0 80.0";
	emitterDelay         = -1;
	directDamage         = 0;
	directDamageType     = $DamageType::ShadowBrigade;
	hasDamageRadius      = true; // true;
	indirectDamage       = 5.4; // 0.5;
	damageRadius         = 35.0;
	radiusDamageType     = $DamageType::ShadowBrigade;
	kickBackStrength     = 1000;
	explosion            = MetExplosion;
	splash               = PlasmaSplash;
	baseEmitter          = JTLMeteorStormFireballEmitter;
	armingDelayMS        = 50;
	grenadeElasticity    = 0.15;
	grenadeFriction      = 0.4;
	drag                 = 0.1;
	gravityMod           = 0.0;
	sound                = GrenadeProjectileSound;

	hasLight    = true;
	lightRadius = 20.0;
	lightColor  = "1 1 0.5";
};

function DoAsteroidFall(%source, %targetPosition) {
   %pos = vectorAdd(%targetPosition, "0 0 400");
   %p = new (GrenadeProjectile)() {
      dataBlock        = AsteroidFallProjo;
      initialDirection = "0 0 -1";
      initialPosition  = %pos;
      damageFactor     = 1;
   };
   MissionCleanup.add(%p);
   %p.sourceObject = %source; //hacky way of spawning airborne projectiles
}
