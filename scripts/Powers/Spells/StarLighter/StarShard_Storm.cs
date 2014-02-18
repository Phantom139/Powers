function DoShardStorm(%source, %targetPosition) {
   for(%i = 0; %i < 20; %i++) {
      %pos1 = vectorAdd(%targetPosition, "0 0 100");
      %pos2 = vectorAdd(%pos1, GetRandomPosition(15, 1));
      %final = vectorAdd(%pos2, "0 0 "@%i * 16@"");
      %p = new (LinearFlareProjectile)() {
         dataBlock        = StarShard;
         initialDirection = "0 0 -3";
         initialPosition  = %final;
         damageFactor     = 1;
      };
      MissionCleanup.add(%p);
      %p.sourceObject = %source; //hacky way of spawning airborne projectiles
   }
}
