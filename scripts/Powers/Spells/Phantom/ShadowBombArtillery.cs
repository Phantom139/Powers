function DoShadowArtillery(%source, %targetPosition) {
   for(%i = 0; %i < 8; %i++) {
      %pos1 = vectorAdd(%targetPosition, "0 0 200");
      %pos2 = vectorAdd(%pos1, GetRandomPosition(15, 1));
      %final = vectorAdd(%pos2, "0 0 "@%i * 25@"");
      %p = new (GrenadeProjectile)() {
         dataBlock        = ShadowBombShot;
         initialDirection = "0 0 -1";
         initialPosition  = %final;
         damageFactor     = 1;
      };
      MissionCleanup.add(%p);
      %p.sourceObject = %source; //hacky way of spawning airborne projectiles
   }
}
