function HellstormLoop(%obj, %tloc) {
   if(!isObject(%obj) || %obj.getState() $= "Dead") {
      return;
   }
   //
   if(!%obj.CastingContinuous) { //we don't continue
      %obj.setMoveState(false);
      return;
   }
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);
   if(%energy < 35) {
      CommandToClient(%client, 'BottomPrint', "<spush><font:Sui Generis:14>Staff of Embirthia<spop>\n<spush><font:Arial:14>Hellstorm out of energy, casting stopped.<spop>", 3, 3 );
      %obj.CastingContinuous = 0;
      %obj.setMoveState(false);
      return;
   }
   else {
      CommandToClient(%client, 'BottomPrint', "<spush><font:Sui Generis:14>Staff of Embirthia<spop>\n<spush><font:Arial:14>Casting Hellstorm, Press Fire To Stop \n Casting will also end if you run out of energy or die.<spop>", 3, 3 );
      TakeEnergy(%client, 35);
      //
      //unlesh hail shards, crystal shocks and all other icy evilness :D
      for(%i = 0; %i < 5; %i++) {
         %TargAierial = vectorAdd(getRandomPosition(10, 1), (getWords(%tloc, 0, 1) SPC (getWord(%tloc, 2) + 250)));
         %randt = (getRandom(1, 30) * %i);
         %proj = NapalmShot2;
         %type = LinearProjectile;
         schedule(%randt, 0, "spawnprojectileOH", %proj, %type, %TargAierial, "0 0 -10", %obj);
      }
      //
      %obj.playShieldEffect("1 1 1");
      //
      schedule(300, 0, "HellstormLoop", %obj, %tloc);
   }
}
