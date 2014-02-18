//Classes/PowerStore.cs
//Phantom139

//Builds the powerstore menu for F2
function CheckHasPower(%client, %class, %pos) {
   if(!isObject(%client)) {
      return 0;
   }
   if(%client.isAiControlled()) {
      return 1; // <-- We have it
   }
   else {
      %spell = $PowerSave::PowerPosition[%class, %pos];
      if(%client.slot(%client.slotNum).hasPower[%spell]) {
         return 1;
      }
      else {
         return 0;
      }
   }
}

function CheckHasPowerByName(%client, %SpellName) {
   if(!isObject(%client)) {
      return 0;
   }
   if(%client.isAiControlled()) {
      return 1; // <-- We have it
   }
   else {
      if(%client.slot(%client.slotNum).hasPower[%spellName]) {
         return 1;
      }
      else {
         return 0;
      }
   }
}

function PowerStore(%client, %tag) {
   %client.SCMPage = "SM";
   %class = %client.slot(%client.slotNum).class;
   messageClient( %client, 'ClearHud', "", %tag, %index );
   messageClient( %client, 'SetScoreHudSubheader', "", "The "@AffinityToNormal(%class)@" Power Store (Levels 1 - 35)" );
   //by Class
   GeneratePowerSelection(%client, AffinityToNormal(%class), %tag);
   //
}

function AffinityToNormal(%class) {
   switch$(%class) {
      case "Witch" or "Demon" or "Phantom" or "Hunter":
         //Do Nothing
      case "Guardian" or "Star Lighter" or "Gladiator" or "Star Sighter":
         %class = "Witch";
      case "Enforcer" or "Devastator" or "Prospector" or "Annihilator":
         %class = "Demon";
      case "Overseer" or "Cryonium" or "Phantom Lord" or "Deep Freezer":
         %class = "Phantom";
      case "Nature Walker" or "Wispwalker":
         %class = "Hunter";
   }
   return %class;
}

function AffinityBacktrace(%class) {
   switch$(%class) {
      case "Gladiator":
         %class = "Guardian";
      case "Star Sighter":
         %class = "Star Lighter";
      case "Prospector":
         %class = "Enforcer";
      case "Annihilator":
         %class = "Devastator";
      case "Phantom Lord":
         %class = "Overseer";
      case "Deep Freezer":
         %class = "Cryonium";
      case "Wispwalker":
         %class = "Nature Walker";
      default:
         //do nothing!
   }
   return %class;
}

function GeneratePowerSelection(%client, %class, %tag) {
   %Pt = %client.slot(%client.slotNum).spendPoints;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Points: "@%pt@"");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "I have these powers up for sale.");
   %index++;
   //
   messageClient( %client, 'SetLineHud', "", %tag, %index, "***********************************");
   %index++;
   %class = AffinityToNormal(%class);
   messageClient( %client, 'SetLineHud', "", %tag, %index, "* The "@%class@" Power Store *");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "***********************************");
   %index++;
   //
   %i = 1;
   while($Power::PowerMenu[%class, %i] !$= "") {
      // Power Menu String
      %str = $Power::PowerMenu[%class, %i];
      %spell = getField(%str, 0);
      %String = getField(%str, 1);
      %cost = getField(%str, 2);
      %section = getField(%str, 3);
      //
      %lReq = $Power::LevelReq[%section];
      if(%client.slot(%client.slotNum).level >= %lReq) {
         if(!CheckHasPowerByName(%client, %spell)) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\t"@%spell@"\t"@%String@"\t"@%cost@"\t"@%lReq@">"@%String@" ("@%cost@"-PTS)</a>");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\t"@%spell@"\t"@%String@"\t"@%cost@"\t"@%lReq@">"@%String@" (Owned)</a>");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Power Locked, Requires Level "@%lReq@".");
         %index++;
      }
      //
      %i++;
   }
   //
   messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Back to the Main Menu</a>");
   %index++;
}

function GetPowerChartTag(%number) {
   switch(%number) {
      case 1:
         //Weak
         %tagToSend = "<color:CD0000>Weak Againt";
      case 2:
         //Mid (Decent)
         %tagToSend = "<color:FF7F00>Moderate/Decent Against";
      case 3:
         //Strong
         %tagToSend = "<color:00FF00>Very Useful Against";
      case 4:
         //Not Applicable
         %tagToSend = "<color:ffffff>Not Applicable";
   }
   return %tagToSend;
}

function GeneratePowerBuyInfo(%client, %index, %tag, %powerTag) {
   //
   %str = $Power::PowerInfo[%powerTag];
   //
   %info = getField(%str, 0);
   %vsWitch = getField(%str, 1);
   %vsDemon = getField(%str, 2);
   %vsPhantom = getField(%str, 3);
   %vsHunter = getField(%str, 4);
   //
   messageClient( %client, 'SetLineHud', "", %tag, %index, ""@%info@"");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "\n\n\n");
   %index += 3;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "******************");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "*USEFULNESS TABLE*");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "******************");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "*VS WITCH: "@GetPowerChartTag(%vsWitch)@"");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "*VS DEMON: "@GetPowerChartTag(%vsDemon)@"");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "*VS PHANTOM: "@GetPowerChartTag(%vsPhantom)@"");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "*VS HUNTER: "@GetPowerChartTag(%vsHunter)@"");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "");
   %index++;
   //
   return %index;
}

function BuyPower(%client, %power, %lReq) {
   if(%client.slot(%client.slotNum).level < %lReq) {
      MessageClient(%client, 'msgFailed', "\c3POWERS: You lack the required level to purchase this power.");
      return;
   }
   %client.slot(%client.slotNum).hasPower[%power] = 1;
   %client.saveData();
   //
   echo("Power "@%power@", added to "@%client.namebase@"'s Slot "@%client.slotNum@".");
}

function GenerateAffinity1Shop(%client, %tag) {
   %Pt = %client.slot(%client.slotNum).spendPoints;
   %clClass = %client.slot(%client.slotNum).class;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "*** THE AFFINITY STORE (LEVELS 35 - 70) ***");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Your Class: "@%clClass@"");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Points: "@%pt@"");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Upgrades:");
   %index++;
   %clClass = AffinityBacktrace(%clClass);
   
   if(!CheckHasPowerByName(%client, "HPBoost1")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\tHPBoost1\tHealth Increase 1\t3\t"@$Power::LevelReq["Affinity"]@">Health Increase 1 [+50HP] (3-PTS)</a>");
      %index++;
   }
   else {
      if(!CheckHasPowerByName(%client, "HPBoost2")) {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\tHPBoost2\tHealth Increase 2\t5\t"@$Power::LevelReq["Affinity"]@">Health Increase 2 [+50HP] (5-PTS)</a>");
         %index++;
      }
      else {
         if(!CheckHasPowerByName(%client, "HPBoost3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\tHPBoost3\tHealth Increase 3\t10\t"@$Power::LevelReq["Affinity"]@">Health Increase 3 [+50HP] (10-PTS)</a>");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Maximum Health Increase For Levels 35 - 70 Purchased");
            %index++;
         }
      }
   }
   //
   if(!CheckHasPowerByName(%client, "EnergyBoost1")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\tEnergyBoost1\tEnergy Increase 1\t3\t"@$Power::LevelReq["Affinity"]@">Energy Increase 1 [+50Energy] (3-PTS)</a>");
      %index++;
   }
   else {
      if(!CheckHasPowerByName(%client, "EnergyBoost2")) {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\tEnergyBoost2\tEnergy Increase 2\t5\t"@$Power::LevelReq["Affinity"]@">Energy Increase 2 [+50Energy] (5-PTS)</a>");
         %index++;
      }
      else {
         if(!CheckHasPowerByName(%client, "EnergyBoost3")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\tEnergyBoost3\tEnergy Increase 3\t10\t"@$Power::LevelReq["Affinity"]@">Energy Increase 3 [+50Energy] (10-PTS)</a>");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "Maximum Energy Increase For Levels 35 - 70 Purchased");
            %index++;
         }
      }
   }
   //----- Special Abilities
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Special Items for "@%clClass@":");
   %index++;
   %i = 1;
   while($Power::SpecialMenu[%clClass, %i] !$= "") {
      // Power Menu String
      %str = $Power::SpecialMenu[%clClass, %i];
      %spell = getField(%str, 0);
      %String = getField(%str, 1);
      %cost = getField(%str, 2);
      %section = getField(%str, 3);
      //
      %lReq = $Power::LevelReq[%section];
      if(%client.slot(%client.slotNum).level >= %lReq) {
         if(!CheckHasPowerByName(%client, %spell)) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\t"@%spell@"\t"@%String@"\t"@%cost@"\t"@%lReq@">"@%String@" ("@%cost@"-PTS)</a>");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, ""@%String@" (Owned)");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Power Locked, Requires Level "@%lReq@".");
         %index++;
      }
      //
      %i++;
   }
   //-----------------------
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Affinity Powers:");
   %index++;
   %i = 1;
   while($Power::PowerMenu[%clClass, %i] !$= "") {
      // Power Menu String
      %str = $Power::PowerMenu[%clClass, %i];
      %spell = getField(%str, 0);
      %String = getField(%str, 1);
      %cost = getField(%str, 2);
      %section = getField(%str, 3);
      //
      %lReq = $Power::LevelReq[%section];
      if(%client.slot(%client.slotNum).level >= %lReq) {
         if(!CheckHasPowerByName(%client, %spell)) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\t"@%spell@"\t"@%String@"\t"@%cost@"\t"@%lReq@">"@%String@" ("@%cost@"-PTS)</a>");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, ""@%String@" (Owned)");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Power Locked, Requires Level "@%lReq@".");
         %index++;
      }
      //
      %i++;
   }
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Exit</a>');
   %index++;
}

function GenerateAffinity2Shop(%client, %tag) {
   %Pt = %client.slot(%client.slotNum).spendPoints;
   %clClass = %client.slot(%client.slotNum).class;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "*** THE AFFINITY STORE (Levels 70+) ***");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Your Class: "@%clClass@"");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Points: "@%pt@"");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Upgrades:");
   %index++;
   if(!CheckHasPowerByName(%client, "HPBoost3")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Health Increase 4 - Requires Health Increase 3");
      %index++;
   }
   else {
      if(!CheckHasPowerByName(%client, "HPBoost4")) {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\tHPBoost4\tHealth Increase 4\t5\t"@$Power::LevelReq["Affinity2"]@">Health Increase 4 [+75HP] (5-PTS)</a>");
         %index++;
      }
      else {
         if(!CheckHasPowerByName(%client, "HPBoost5")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\tHPBoost5\tHealth Increase 5\t7\t"@$Power::LevelReq["Affinity2"]@">Health Increase 5 [+75HP] (7-PTS)</a>");
            %index++;
         }
         else {
            if(!CheckHasPowerByName(%client, "HPBoost6")) {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\tHPBoost6\tHealth Increase 6\t10\t"@$Power::LevelReq["Affinity2"]@">Health Increase 6 [+100HP] (10-PTS)</a>");
               %index++;
            }
            else {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "Maximum Health Increase Purchased");
               %index++;
            }
         }
      }
   }
   //
   if(!CheckHasPowerByName(%client, "EnergyBoost3")) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Energy Increase 4 - Requires Energy Increase 3");
      %index++;
   }
   else {
      if(!CheckHasPowerByName(%client, "EnergyBoost4")) {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\tEnergyBoost4\tEnergy Increase 4\t5\t"@$Power::LevelReq["Affinity2"]@">Energy Increase 4 [+75Energy] (5-PTS)</a>");
         %index++;
      }
      else {
         if(!CheckHasPowerByName(%client, "EnergyBoost5")) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\tEnergyBoost5\tEnergy Increase 5\t5\t"@$Power::LevelReq["Affinity2"]@">Energy Increase 5 [+75Energy] (5-PTS)</a>");
            %index++;
         }
         else {
            if(!CheckHasPowerByName(%client, "EnergyBoost6")) {
               messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\tEnergyBoost6\tEnergy Increase 6\t7\t"@$Power::LevelReq["Affinity2"]@">Energy Increase 6 [+100Energy] (7-PTS)</a>");
               %index++;
            }
            else {
               if(!CheckHasPowerByName(%client, "EnergyBoost7")) {
                  messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\tEnergyBoost7\tEnergy Increase 7\t10\t"@$Power::LevelReq["Affinity2"]@">Energy Increase 7 [+150Energy] (10-PTS)</a>");
                  %index++;
               }
               else {
                  messageClient( %client, 'SetLineHud', "", %tag, %index, "Maximum Energy Increase Purchased");
                  %index++;
               }
            }
         }
      }
   }
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Second Affinity Powers:");
   %index++;
   %i = 1;
   
   if(true) {
      //Phantom: TO-DO: Remove this when affinity 2 powers are done.
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<Color:990000>Second Affinity Powers are currently in development, Sorry!");
      %index++;
   }
   else {
   while($Power::PowerMenu[%clClass, %i] !$= "") {
      // Power Menu String
      %str = $Power::PowerMenu[%clClass, %i];
      %spell = getField(%str, 0);
      %String = getField(%str, 1);
      %cost = getField(%str, 2);
      %section = getField(%str, 3);
      //
      %lReq = $Power::LevelReq[%section];
      if(%client.slot(%client.slotNum).level >= %lReq) {
         if(!CheckHasPowerByName(%client, %spell)) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tConfirmPurchase\t"@%spell@"\t"@%String@"\t"@%cost@"\t"@%lReq@">"@%String@" ("@%cost@"-PTS)</a>");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, ""@%String@" (Owned)");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Power Locked, Requires Level "@%lReq@".");
         %index++;
      }
      //
      %i++;
   }
   }
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Exit</a>');
   %index++;
}
