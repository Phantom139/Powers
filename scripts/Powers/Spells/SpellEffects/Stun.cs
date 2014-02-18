function stunObject(%target, %source, %duration) {
   if(!isPlayer(%target)) {
      return;
   }
   if(!%target.IsAlive()) {
      return;
   }
   %target.stunned = 1;
   cancel(%target.stunReset);
   //
   %target.setMoveState(true);
   messageClient(%target.client, 'MsgFroze', "\c0You have been stunned.");
   %target.stunReset = schedule((%duration * 1000), 0, "clearStun", %target);
}

function clearStun(%target) {
   %target.stunned = 0;
   %target.setMoveState(false);
   messageClient(%target.client, 'MsgFroze', "\c0The stun effect wears off.");
}
