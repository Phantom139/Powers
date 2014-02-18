function BlizzardLoop(%obj, %tloc) {
   if(!isObject(%obj) || %obj.getState() $= "Dead") {
      if(isObject(%obj.blizzardBeamEmitter)) {
         %obj.blizzardBeamEmitter.delete();
      }
      return;
   }
   //
   if(!isObject(%obj.blizzardBeamEmitter)) {
      %obj.blizzardBeamEmitter = createEmitter(%obj.getPosition(), PrebeamEmitter, "1 0 0");
      MissionCleanup.add(%obj.blizzardBeamEmitter);
   }
   //
   if(!%obj.CastingContinuous) { //we don't continue
      if(isObject(%obj.blizzardBeamEmitter)) {
         %obj.blizzardBeamEmitter.delete();
      }
      %obj.setMoveState(false);
      return;
   }
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);
   if(%energy < 35) {
      CommandToClient(%client, 'BottomPrint', "<spush><font:Sui Generis:14>Cryonic Spear<spop>\n<spush><font:Arial:14>Blizzard out of energy, casting stopped.<spop>", 3, 3 );
      if(isObject(%obj.blizzardBeamEmitter)) {
         %obj.blizzardBeamEmitter.delete();
      }
      %obj.CastingContinuous = 0;
      %obj.setMoveState(false);
      return;
   }
   else {
      CommandToClient(%client, 'BottomPrint', "<spush><font:Sui Generis:14>Cryonic Spear<spop>\n<spush><font:Arial:14>Casting Blizzard, Press Fire To Stop \n Casting will also end if you run out of energy or die.<spop>", 3, 3 );
      TakeEnergy(%client, 35);
      //
      //unlesh hail shards, crystal shocks and all other icy evilness :D
      for(%i = 0; %i < 7; %i++) {
         %TargAierial = vectorAdd(getRandomPosition(10, 1), (getWords(%tloc, 0, 1) SPC (getWord(%tloc, 2) + 250)));
         %randt = (getRandom(1, 30) * %i);
         %proj = HailstormProjectile;
         %type = LinearProjectile;
         schedule(%randt, 0, "spawnprojectileOH", %proj, %type, %TargAierial, "0 0 -5", %obj);
      }
      //a few missiles ftw :D
      for(%i = 0; %i < 2; %i++) {
         %TargAierial = vectorAdd(getRandomPosition(10, 1), (getWords(%tloc, 0, 1) SPC (getWord(%tloc, 2) + 250)));
         %randt = (getRandom(1, 50) * %i);
         %proj = IceRushMissile;
         %type = SeekerProjectile;
         schedule(%randt, 0, "spawnprojectileOH", %proj, %type, %TargAierial, "0 0 -5", %obj);
      }
      //
      for(%i = 0; %i < 7; %i++) {
         %TargAierial = vectorAdd(getRandomPosition(10, 1), (getWords(%tloc, 0, 1) SPC (getWord(%tloc, 2) + 250)));
         %randt = (getRandom(1, 30) * %i);
         %proj = CrystalShockPulse;
         %type = LinearFlareProjectile;
         schedule(%randt, 0, "spawnprojectileOH", %proj, %type, %TargAierial, "0 0 -5", %obj);
      }
      //
      %obj.playShieldEffect("1 1 1");
      //
      schedule(300, 0, "BlizzardLoop", %obj, %tloc);
   }
}
