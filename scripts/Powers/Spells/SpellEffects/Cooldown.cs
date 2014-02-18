function CoolDownSpell(%client, %spell) {
   if($CoolDownTime[%client, %spell] <= 0) {
      messageClient(%client, 'MsgCool', "\c5COOLDOWN COMPLETE: "@%spell@"");
      return;
   }
   else {
      $CoolDownTime[%client, %spell]--;
      schedule(1000, 0, "CoolDownSpell", %client, %spell);
   }
}
