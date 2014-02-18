//The Spell... to end all
function DoShadowStorm(%source, %targetPosition) {
   for(%i = 0; %i < 25; %i++) {
      %pos1 = vectorAdd(%targetPosition, "0 0 300");
      %pos2 = vectorAdd(%pos1, GetRandomPosition(15, 1));
      %final = vectorAdd(%pos2, "0 0 "@%i * 300@"");
      %p = new (GrenadeProjectile)() {
         dataBlock        = ShadowBombShot;
         initialDirection = "0 0 -2";
         initialPosition  = %final;
         damageFactor     = 1;
      };
      MissionCleanup.add(%p);
      %p.sourceObject = %source; //hacky way of spawning airborne projectiles
   }
   for(%i = 0; %i < 30; %i++) {
      %pos1 = vectorAdd(%targetPosition, "0 0 75");
      %pos2 = vectorAdd(%pos1, GetRandomPosition(25, 1));
      %final = vectorAdd(%pos2, "0 0 "@%i*2@"");
      %p = new (LinearFlareProjectile)() {
         dataBlock = FlasherBolt1;
         initialDirection = "0 0 -5";
         initialPosition = %final;
         damageFactor = 1;
      };
      MissionCleanup.add(%p);
      %p.sourceObject = %source;
   }
   for(%i = 0; %i < 30; %i++) {
      %pos1 = vectorAdd(%targetPosition, "0 0 50");
      %pos2 = vectorAdd(%pos1, GetRandomPosition(25, 1));
      %final = vectorAdd(%pos2, "0 0 "@%i*2@"");
      %p = new (LinearFlareProjectile)() {
         dataBlock = FlickerBolt;
         initialDirection = "0 0 -5";
         initialPosition = %final;
         damageFactor = 1;
      };
      MissionCleanup.add(%p);
      %p.sourceObject = %source;
   }
}
