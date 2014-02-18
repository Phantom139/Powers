//Stat Potions
function StatRemove(%player, %stat) {
   if(!%player.isAlive()) {
      return;
   }
   %client = %player.client;
   switch$(%stat) {
      case "antifreeze":
         %player.antiFreeze = 0;
         messageClient(%client, 'msgInventory', "\c3Stat: Your anti-freeze effect wears off.");
      case "antiburn":
         %player.antiBurn = 0;
         messageClient(%client, 'msgInventory', "\c3Stat: Your anti-burn effect wears off.");
   }
}
