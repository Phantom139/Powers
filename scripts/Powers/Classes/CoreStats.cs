//Classes/CoreStats.cs
//Phantom139

//Handles all math based operations and the stat system

//used in multiple functions
//Credit to the RPG mod/Bionic Construction for these (S[H]A[D]O[W] (Stuff180)).

function round(%n) {
	if(%n < 0) {
		%t = -1;
		%n = -%n;
	}
	else if(%n >= 0) {
		%t = 1;
    }

	%f = mfloor(%n);
	%a = %n - %f;
	if(%a < 0.5)
		%b = 0;
	else if(%a >= 0.5)
		%b = 1;

	return mfloor((%f + %b) * %t);
}

function FetchMaxEnergy(%client) {
   %max = 0;
   if(%client.isAiControlled()) {
      %max = 500;
      return %max;
   }
   %slot = %client.slotNum;
   %lv = %client.slot(%slot).level;
   %class = %client.slot(%slot).class;
   switch$ (%class) {
      case "Witch" or "Demon" or "Phantom" or "Hunter":  //basic classes
         if(%lv < 10) {
            %max += (100 + (%lv * 4));
         }
         //boost t3h energy
         else if(%lv >= 10 && %lv < 15) {
            %max += (150 + (%lv * 5));
         }
         else if(%lv >= 15 && %lv < 27) {
            %max += (200 + (%lv * 6));
         }
         else if(%lv >= 27) {
            %max += (250 + (%lv * 7));
         }
      case "Guardian" or "Star Lighter" or "Enforcer" or "Devastator" or "Overseer" or "Cryonic Embassador" or "Nature Walker":
         if(%lv < 45) {
            %max += (250 + (%lv * 8));
         }
         else if(%lv >= 45 && %lv < 55) {
            %max += (300 + (%lv * 9));
         }
         else if(%lv >= 55 && %lv < 70) {
            %max += (350 + (%lv * 10));
         }
      case "Gladiator" or "Star Sighter" or "Prospector" or "Annihilator" or "Phantom Lord" or "Deep Freezer" or "Wispwalker":
         if(%lv < 80) {
            %max += (350 + (%lv * 11));
         }
         else if(%lv >= 80 && %lv < 90) {
            %max += (450 + (%lv * 12));
         }
         else if(%lv >= 90 && %lv < 100) {
            %max += (550 + (%lv * 13));
         }
         else {
            %max += (600 + (%lv * 15));
         }
      default: //if all else fails, return to basic
         if(%lv < 10) {
            %max += (100 + (%lv * 4));
         }
         //boost t3h energy
         else if(%lv >= 10 && %lv < 15) {
            %max += (150 + (%lv * 5));
         }
         else if(%lv >= 15 && %lv < 27) {
            %max += (200 + (%lv * 6));
         }
         else if(%lv >= 27) {
            %max += (250 + (%lv * 7));
         }
   }
   //Apply Boosts
   if(CheckHasPowerByName(%client, "EnergyBoost1") == 1) {
      %max += 50;
   }
   if(CheckHasPowerByName(%client, "EnergyBoost2") == 1) {
      %max += 50;
   }
   if(CheckHasPowerByName(%client, "EnergyBoost3") == 1) {
      %max += 50;
   }
   if(CheckHasPowerByName(%client, "EnergyBoost4") == 1) {
      %max += 75;
   }
   if(CheckHasPowerByName(%client, "EnergyBoost5") == 1) {
      %max += 75;
   }
   if(CheckHasPowerByName(%client, "EnergyBoost6") == 1) {
      %max += 100;
   }
   if(CheckHasPowerByName(%client, "EnergyBoost7") == 1) {
      %max += 150;
   }
   //
   return %max;
}

function DoPowersStuff(%client, %setStat) {
    if(!isObject(%client.player) || %client.player.getState() $= "dead") {
       return;
    }
   	%armor = %client.player.getDataBlock();

    if(%setStat $= "")
       %setStat = FetchMaxEnergy(%client);

	%max = %setStat * %armor.maxEnergy;
	%set = %max / FetchMaxEnergy(%client);

    //Can we use this E-Level?
	if(%set < 0)
		%set = 0;
	else if(%set > %armor.maxEnergy)
		%set = %armor.maxEnergy;

	%client.player.setEnergyLevel(%set);
}

//remember, powers goes over max energy
function FetchPowersEnergyLevel(%client) {
   %armor = %client.player.getDataBlock();

   %get = %client.player.getEnergyLevel() * FetchMaxEnergy(%client);
   %final = %get / %armor.maxEnergy;

   return %final;
}

function TakeEnergy(%client, %varible) {
   DoPowersStuff(%client, (FetchPowersEnergyLevel(%client) - %varible));
}

function GiveEnergy(%client, %varible) {
   DoPowersStuff(%client, (FetchPowersEnergyLevel(%client) + %varible));
}



//---------------------------------------
//HP

function FetchMaxHP(%player) {
   if(!isPlayer(%player)) {
      return;
   }
   %client = %player.client;
   if(%client.isAIControlled()) {
      return 150;
   }
   %max = 150;
   //Apply Boosts
   if(CheckHasPowerByName(%client, "HPBoost1") == 1) {
      %max += 50;
   }
   if(CheckHasPowerByName(%client, "HPBoost2") == 1) {
      %max += 50;
   }
   if(CheckHasPowerByName(%client, "HPBoost3") == 1) {
      %max += 50;
   }
   if(CheckHasPowerByName(%client, "HPBoost4") == 1) {
      %max += 75;
   }
   if(CheckHasPowerByName(%client, "HPBoost5") == 1) {
      %max += 75;
   }
   if(CheckHasPowerByName(%client, "HPBoost6") == 1) {
      %max += 100;
   }
   //
   //echo(%max);
   return %max;
}

function DoPowersHPStuff(%player, %setStat) {
   if(!isPlayer(%player)) {
      return;
   }
    if(!isObject(%player) || %player.getState() $= "dead") {
       return;
    }
	%armor = %player.getDataBlock();

	if(%setStat < 0)
		%setStat = 0;
	if(%setStat $= "")
		%setStat = FetchMaxHP(%player);

	%a = %setStat * %armor.maxDamage;
	%b = %a / FetchMaxHP(%player);
	%c = %armor.maxDamage - %b;

	if(%c < 0)
		%c = 0;
	else if(%c > %armor.maxDamage)
		%c = %armor.maxDamage;

	%player.setDamageLevel(%c);

	return %val;
}

//remember, powers goes over max energy
function DoDamageModifiers(%sourceClient, %targetClient, %amount) {
   if(!isSet(%sourceClient.player.damageMod)) {
      %sourceClient.player.damageMod = 1;
   }
   if(!isSet(%targetClient.player.damageVar)) {
      %targetClient.player.damageVar = 1;
   }
   %firstModifier = %amount * %sourceClient.player.damageMod;
   %final = %firstModifier * %targetClient.player.damageVar;
   return %final;
}

function FetchPowersHPLevel(%player) {
   if(!isPlayer(%player)) {
      return;
   }
   if(!isObject(%player)) {
      return 0;
   }
   %armor = %player.getDataBlock();

   %c = %armor.maxDamage - %player.getDamageLevel();
   %a = %c * FetchMaxHP(%player);
   %b = %a / %armor.maxDamage;

   return round(%b);
}

function TakeHP(%player, %varible) {
   DoPowersHPStuff(%player, FetchPowersHPLevel(%player) - round(%varible * $TribesDamageToNumericDamage));
}

function GiveHP(%player, %varible) {
   DoPowersHPStuff(%player, FetchPowersHPLevel(%player) + round(%varible * $TribesDamageToNumericDamage));
}

//---------------------------------------


//Stats Page
//For Score Menu
function SendInfoPage(%arg2, %client, %index, %tag) {
   %OldIndex = %index;
   %slot = %arg2.slotNum;
   %lv = %arg2.slot(%slot).level;
   %class = %arg2.slot(%slot).class;
   if(%client.StatView $= "Yes") {
      messageClient( %client, 'ClearHud', "", %tag);
      messageClient( %client, 'SetScoreHudSubheader', "", ""@%arg2.namebase@"'s Stats Card" );
      %client.SCMPage = "SM";
      %xp = %arg2.slot(%slot).exp;
      %prst = %arg2.slot(%slot).affinity;
      if(!%prst) {
         if(%lv == 35) {
            %nextLV = "First Class Affinity Promotion";
            %nextLVXP = %xp;
         }
         else {
            %nextLV = %lv+1;
            %nextLVXP = $PowerSave::MinEXP[%lv];
         }
      }
      else if(%prst == 1) {
         if(%lv == 70) {
            %nextLV = "Second Class Affinity Promotion";
            %nextLVXP = %xp;
         }
         else {
            %nextLV = %lv+1;
            %nextLVXP = $PowerSave::Affinity1MinEXP[%lv-35];
         }
      }
      else if(%prst == 2) {
         if(%lv == 100) {
            %nextLV = "Maximum Level Reachead";
            %nextLVXP = %xp;
         }
         else {
            %nextLV = %lv+1;
            %nextLVXP = $PowerSave::Affinity2MinEXP[%lv-70];
         }
      }
      //Card
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Class : "@%class@".");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "EXP : "@%xp@".");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Level : "@%lv@".");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Next Level : "@%nextLV@".");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "XP Needed : "@%nextLVXP - %xp@".");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "---.");
      %index++;
      if(!isObject(%arg2.player) || %arg2.player.getState() $= "dead") {
         messageClient( %client, 'SetLineHud', "", %tag, %index, ""@%arg2.namebase@" is currently dead.");
         %index++;
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "HP: "@FetchPowersHPLevel(%arg2.player)@" / "@FetchMaxHP(%arg2.player)@"");        //"@((%arg2.player.getDatablock().maxDamage - %arg2.player.getDamageLevel()) * 100)@" / "@((%arg2.player.getDatablock().maxDamage)*100)
         %index++;
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Energy: "@FetchPowersEnergyLevel(%arg2)@" / "@FetchMaxEnergy(%arg2)@"");
         %index++;
      }
      messageClient( %client, 'SetLineHud', "", %tag, %index, "---.");
      %index++;
      //messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tRanksSM\t"@%arg2@">Refresh Page</a>");
      //%index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPLS>Back to P.I.L.</a>');
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
      %index++;
      //loopy
      schedule(2000, 0, "SendInfoPage", %arg2, %client, %OldIndex, %tag);
      return %index;
   }
}
