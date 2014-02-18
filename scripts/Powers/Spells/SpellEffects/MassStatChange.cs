//MASS STAT CHANGE
//These are your team assist spells
//Team Heal, Team Shield, Ect.

function DoFriendlyMassAreaSpell(%source, %position, %spell, %radius) {
   %teamAffect = %source.team;
   %TargetSearchMask = $TypeMasks::PlayerObjectType;
   %affect = 0;

   InitContainerRadiusSearch(%position, %radius, %TargetSearchMask);
   while ((%potentialTarget = ContainerSearchNext()) != 0) {
      if (%potentialtarget && %potentialTarget.team == %teamAffect) {
         switch$(%spell) {
            case "Shielder":
               %potentialtarget.SpellShieldTicks = 10;
               MessageClient(%potentialtarget, 'MsgShield', "\c3A Magical Shield from "@%source.client.namebase@" now protects you.");
               %affect++;
            case "Healer":
               %potentialtarget.applyRepair(2.0);
               MessageClient(%potentialtarget, 'MsgShield', "\c3"@%source.client.namebase@"'s power is healing you.");
               %affect++;
         }
      }
   }
   %source.client.slot(%source.client.curSlot).EXP += %affect;
   messageClient(%source.client, 'msgAward', "\c3Radius Spell Affected "@%affect@" Players. (+"@%affect@" EXP)");
   doEXPAward(%source.client, "");
}

function DoEnemyMassAreaSpell(%source, %position, %spell, %radius) {
   %teamAffect = %source.team;
   %TargetSearchMask = $TypeMasks::PlayerObjectType;
   %affect = 0;

   InitContainerRadiusSearch(%position, %radius, %TargetSearchMask);
   while ((%potentialTarget = ContainerSearchNext()) != 0) {
      if (%potentialtarget && %potentialTarget.team != %teamAffect) {
         switch$(%spell) {
            //case "Shielder":
            //   %potentialtarget.SpellShieldTicks = 10;
            //   MessageClient(%potentialtarget, 'MsgShield', "\c3A Magical Shield from "@%source.client.namebase@" now protects you.");
            //   %affect++;
            case "DamageReduction":
               cancel(%potentialtarget.reset_damage_Sched); //kill it if he is scheduled to be reset
               %newDM = %potentialtarget.damageMod / 2;
               %potentialtarget.damageMod = %newDM;
               MessageClient(%potentialtarget, 'MsgShield', "\c3A magical force from "@%source.client.namebase@" has reduced your destructive power.");
               %affect++;
               %potentialtarget.reset_damage_Sched = schedule(15000, 0, "statReset", %potentialTarget, "damage", 1);
            default:
         }
      }
   }
   %source.client.slot(%source.client.curSlot).EXP += %affect;
   messageClient(%source.client, 'msgAward', "\c3Radius Spell Affected "@%affect@" Players. (+"@%affect@" EXP)");
   doEXPAward(%source.client, "");
}

function statReset(%obj, %stat, %setTo) {
   if(!isObject(%obj) || !%obj.isAlive()) {
      return;
   }
   %cli = %obj.client;
   switch$(%stat) {
      case "damage":
         %obj.damageMod = %setTo;
         MessageClient(%cli, 'MsgShield', "\c3your destructive power has been fully restored.");
   }
}
