function BarrierFieldLoop(%obj) {
   if(!isObject(%obj) || %obj.getState() $= "Dead") {
      return;
   }
   if(!%obj.CastingContinuous) { //we don't continue
      %obj.setMoveState(false);
      return;
   }
   %client = %obj.client;
   %energy = FetchPowersEnergyLevel(%client);
   if(%energy < (35/3)*(3/4)) {
      CommandToClient(%client, 'BottomPrint', "<spush><font:Sui Generis:14>Overseer Sword<spop>\n<spush><font:Arial:14>Barrier Field out of energy, casting stopped.<spop>", 3, 3 );
      %obj.CastingContinuous = 0;
      %obj.setMoveState(false);
      return;
   }
   else {
      CommandToClient(%client, 'BottomPrint', "<spush><font:Sui Generis:14>Overseer Sword<spop>\n<spush><font:Arial:14>Casting Barrier Field, Press Fire To Stop \n Casting will also end if you run out of energy or die.<spop>", 3, 3 );
      TakeEnergy(%client, (35/3)*(3/4));
      //
      BarrierFieldShield(%obj);
      //
      schedule(75, 0, "BarrierFieldLoop", %obj);
   }
}

function BarrierFieldShield(%obj) {
   %TargetSearchMask = $TypeMasks::ProjectileObjectType;
   InitContainerRadiusSearch(%obj.getPosition(), 35 ,%TargetSearchMask);

   while ((%potentialTarget = ContainerSearchNext()) != 0) {
      if (%potentialTarget.getPosition() != %obj.getPosition()) {
         %potentialTarget.delete();
         %obj.playShieldEffect("1 1 1");
      }
   }
}
