function DrainELoop(%target, %source, %stealE) {
   if(!isPlayer(%target)) {
      return;
   }
   if(!%target.IsAlive()) {
      return;
   }
   if(!%target.beingDrained) {
      return;
   }
   DoPowersStuff(%target.client, (FetchPowersEnergyLevel(%target.client) - 7));
   zapObject(%target);
   if(%stealE) {
      DoPowersStuff(%source.client, (FetchPowersEnergyLevel(%source.client) + 3));
      if(!%source.IsAlive()) {
         cancelEDrain(%target);
         return;
      }
   }
   %target.drainLoop = schedule(100, 0, "DrainELoop", %target, %source, %stealE);
}

function cancelEDrain(%target) {
   %target.beingDrained = 0;
   cancel(%target.drainLoop);
}
