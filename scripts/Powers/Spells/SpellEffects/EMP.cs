function EMP(%obj, %duration) {
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      messageClient(%cl, 'msgSound', "~wfx/weapons/mortar_explode.wav");
      if(isObject(%cl.player) && %cl.player.isAlive()) {
         ApplyEMP(%cl.player);
         %cl.player.EMPed = 1;
         %cl.player.setWhiteout(1.0);
         schedule((%duration * 1000), 0, "KillEMP", %cl.player);
         messageClient(%cl, 'msgAlert', "\c0Your energy has been wiped by a discharge of energy");
      }
   }
}

function ApplyEMP(%player) {
   if(!%player.isAlive()) {
      return;
   }
   if(!%player.EMPed) {
      return;
   }
   %player.setEnergyLevel(0);
   %player.zapObject();
   schedule(250, 0, "ApplyEMP", %player);
}

function killEMP(%player) {
   if(!%player.isAlive()) {
      return;
   }
   %player.stopZap();
   %player.EMPed = 0;
   messageClient(%player.client, 'msgAlert', "\c0The discharge effect wears off");
}
