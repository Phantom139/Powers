//Core/Leveling.cs
//Phantom139

//Handles the mod's rank/level system
function LoadRanksBase() {
   echo("Loading The Ranking System Base");
   if(!isFile(""@$PowerSave::RanksDirectory@"/Main.Dat")) {
      error("*Base File Not Located, If this is the first run this is expected");
      error("*If not, contact the mod developers.");
      error("*Creating it now*");
      new fileobject(BaseToLoad);
      BaseToLoad.openforWrite(""@$PowerSave::RanksDirectory@"/Main.Dat");
      BaseToLoad.writeLine("$PowerSave::numplayers = 0;");
      BaseToLoad.close();
      BaseToLoad.delete();
      exec(""@$PowerSave::RanksDirectory@"/Main.Dat");
   }
   else {
      exec(""@$PowerSave::RanksDirectory@"/Main.Dat");
   }
}

function CreateClientRankFile(%client) {
   if(%client.donotupdate) {
      echo("Stopped rank file make on "@%client@", server denies access (probably loading rank)");
      return;
   }
   %slot = %client.slotNum;
   if(%client.isAiControlled()) {
      return;
   }
   if(%slot $= "") {
      return;
   }
   if($PowerSave::numplayers $= "")
      $PowerSave::numplayers = 0;
   $PowerSave::numplayers++;
   echo("Updating Base File");
   new fileobject(BaseToLoad);
   BaseToLoad.openforAppend(""@$PowerSave::RanksDirectory@"/Main.Dat");
   BaseToLoad.replaceLine(""@$PowerSave::RanksDirectory@"/Main.Dat", "$PowerSave::numplayers = "@$PowerSave::numplayers@";", 1);
   BaseToLoad.writeline("$PowerSave::AssignedGUID["@$PowerSave::numplayers@"] = "@%client.guid@";");
   BaseToLoad.close();
   BaseToLoad.delete();
   $PowerSave::TopPlPosition[%client.GUID, %slot] = $PowerSave::numplayers;
   $PowerSave::Class[%client.GUID, %slot] = "Undecided";
   $PowerSave::Name[%client.GUID] = ""@%client.namebase@"";
   exec(""@$PowerSave::RanksDirectory@"/Main.Dat");
   //Rank
   new fileobject(RankFile);
   RankFile.openforWrite(""@$PowerSave::RanksDirectory@"/"@%client.guid@"/"@$PowerSave::Name[%client.GUID]@".txt");
   RankFile.WriteLine(""@$PowerSave::Name[%client.GUID]@" - GUID: "@%client.GUID@"");
   RankFile.close();
   RankFile.openforWrite(""@$PowerSave::RanksDirectory@"/"@%client.guid@"/Saved.Dat");
   RankFile.WriteLine("//Data File For GUID "@%client.guid@"");
   RankFile.WriteLine("//Created On "@formattimestring("yy-mm-dd")@", Powers Mod "@$PowerSave::Version@"");
   RankFile.WriteLine("$PowerSave::Name["@%client.guid@"] = \""@$PowerSave::Name[%client.GUID]@"\";");
   RankFile.WriteLine("$PowerSave::Level["@%client.guid@", "@%slot@"] = 1;");
   RankFile.WriteLine("$PowerSave::SpendPoints["@%client.guid@", "@%slot@"] = 1;");
   RankFile.WriteLine("$PowerSave::EXP["@%client.guid@", "@%slot@"] = 0;");
   RankFile.WriteLine("$PowerSave::TopPlPosition["@%client.guid@", "@%slot@"] = "@$PowerSave::TopPlPosition[%client.GUID, %slot]@";");
   RankFile.WriteLine("$PowerSave::Class["@%client.guid@", "@%slot@"] = \""@$PowerSave::Class[%client.GUID, %slot]@"\";");
   RankFile.close();
   RankFile.delete();
   echo("Ranks File For "@%client.namebase@" created");
   exec(""@$PowerSave::RanksDirectory@"/"@%client.guid@"/Saved.Dat");
   MessageAll('WelcomeTheNoob',"\c4Melvin: Welcome To Powers Mod For The First Time "@%client.namebase@".");
   MessageAll('WelcomeTheNoob',"\c4Melvin: You are player Number "@$PowerSave::numplayers@" To Join This Server.");
}

function UpdateRankFile(%client) {
   if(%client.donotupdate) {
      echo("Stopped rank file make on "@%client@", server denies access (probably loading rank)");
      return;
   }
   if($PowerSave::AccLoan[%client.guid]) {
      return;
   }
   %slot = %client.slotNum;
   if(%slot $= "") {
      return;
   }
   if(%client.isAiControlled()) {
      return;
   }
   echo("Updating "@%client.namebase@"'s Rank File");
   %file = ""@$PowerSave::RanksDirectory@"/"@%client.guid@"/Saved.Dat";
   new fileobject(ClientRank);
   //
   if(ClientRank.findInFile(%file, "$PowerSave::Level["@%client.guid@", "@%slot@"]") != 0) {
      %ln = ClientRank.findInFile(%file, "$PowerSave::Level["@%client.guid@", "@%slot@"]");
      echo(%ln);
   }
   //
   ClientRank.replaceLine(%file, "$PowerSave::Level["@%client.guid@", "@%slot@"] = "@$PowerSave::Level[%client.GUID, %slot]@";", %ln);
   ClientRank.replaceLine(%file, "$PowerSave::SpendPoints["@%client.guid@", "@%slot@"] = "@$PowerSave::SpendPoints[%client.GUID, %slot]@";", %ln+1);
   ClientRank.replaceLine(%file, "$PowerSave::EXP["@%client.guid@", "@%slot@"] = "@$PowerSave::EXP[%client.GUID, %slot]@";", %ln+2);
   ClientRank.replaceLine(%file, "$PowerSave::TopPlPosition["@%client.guid@", "@%slot@"] = "@$PowerSave::TopPlPosition[%client.GUID, %slot]@";", %ln+3);
   ClientRank.replaceLine(%file, "$PowerSave::Class["@%client.guid@", "@%slot@"] = \""@$PowerSave::Class[%client.GUID, %slot]@"\";", %ln+4);
   ClientRank.close();
   ClientRank.delete();
   echo("Update complete, attempting to save to univ server");
   //Univ_ServerConnect(%client, "Server/ClientSave/"@%client.guid@"/Saved.Dat", "Save");
   exec(%file);
}

function LoadClientRankfile(%client) {
   %client.donotupdate = 0;
   echo("Attempting To Load "@%client.namebase@"'s Ranks File");
   %file = ""@$PowerSave::RanksDirectory@"/"@%client.guid@"/Saved.Dat";
   if(!isFile(%file)) {
      echo(""@%client.namebase@" does not have a save file, creating one.");
      CreateClientRankFile(%client);
      schedule(5000,0,"UpdateRankFile", %client);
   }
   else {
      echo("File Located, Attempting To Execute...");
      exec(%file);
      schedule(5000,0,"UpdateRankFile", %client);
      echo("File Load Complete");
      %client.loadClanData();
   }
   loadRPGData(%client);
}

function UpdateClientRank(%client) {
    if(%client.donotupdate) {
       echo("Stopped rank up check on "@%client@", server denies access (probably loading univ rank)");
       return;
    }
   if($PowerSave::AccLoan[%client.guid]) {
      return;
   }
   %slot = %client.slotNum;
   if(%slot $= "") {
      return;
   }
   if(%client.isAiControlled()) {
      return;
   }
    if($PowerSave::Level[%client.guid, %slot] $= "") {
       $PowerSave::Level[%client.guid, %slot] = 1;
    }
    %name = %client.namebase;
    $PowerSave::EXP[%client.guid, %slot] = $PowerSave::EXP[%client.guid, %slot] + (%client.EXP - %client.lastEXP);
	%stat = $PowerSave::EXP[%client.guid, %slot];
	%client.lastEXP = %client.EXP;
    if($PowerSave::Level[%client.guid, %slot] >= 35 && $PowerSave::Level[%client.guid, %slot] <= 70 && $PowerSave::Affinity[%client.guid, %slot] == 1) {
       //Affinity mode stuff
	   for(%lv = 0; %lv < $PowerSave::Affinity1RankCount; %lv++){  //for(%lv = $PowerSave::Affinity1RankCount; %lv > 0; %lv++) {       //check all ranks
	      if($PowerSave::EXP[%client.guid, %slot] >= $PowerSave::Affinity1MinEXP[%lv+1]){
		     if($PowerSave::Level[%client.guid, %slot] < $PowerSave::Affinity1Nextlevel[%lv+1]){ //less than to prevent unlimited loop of doom
                $PowerSave::Level[%client.guid, %slot]++;
                $PowerSave::SpendPoints[%client.guid, %slot] = $PowerSave::SpendPoints[%client.guid, %slot] + $PowerSave::Affinity1PointGain[%lv+1];
                messageAll('msgClient',"\c2"@getTaggedString(%client.name)@" Has Leveled Up To Level "@$PowerSave::Affinity1Nextlevel[%lv+1]@" with "@$PowerSave::EXP[%client.guid, %slot]@"EXP Points!");
                messageClient(%client, 'msgSnd', "~wfx/misc/MA3.wav");
                bottomPrint(%client, "Congradulations "@getTaggedString(%client.name)@" you have Leveled up to Level "@$PowerSave::Affinity1Nextlevel[%lv+1]@"!", 5, 2 );
                echo(""@getTaggedString(%client.name)@" to level "@$PowerSave::Affinity1Nextlevel[%lv+1]@", EXP: "@$PowerSave::EXP[%client.guid, %slot]@".");
                if($PowerSave::Affinity1LevelMessage[%lv+1] !$= "") {
                   messageClient(%client, 'MsgLevel', "\c5"@$PowerSave::Affinity1LevelMessage[%lv+1]@"");
                }
                UpdateRankFile(%client);
                PrepareUpload(%client);
                //Change their name
                SetUpClientPowersName(%client);
		     }
		     //%lv = $PowerSave::Affinity1RankCount; //we start the loop again until we are there.
          }
       }
    }
    else if($PowerSave::Level[%client.guid, %slot] > 70 && $PowerSave::Affinity[%client.guid, %slot] == 2) {
       //Affinity mode stuff
	   for(%lv = 0; %lv < $PowerSave::Affinity2RankCount; %lv++){        //check all ranks
	      if($PowerSave::EXP[%client.guid, %slot] >= $PowerSave::Affinity2MinEXP[%lv+1]){
		     if($PowerSave::Level[%client.guid, %slot] < $PowerSave::Affinity2Nextlevel[%lv+1]){
                $PowerSave::Level[%client.guid, %slot]++;
                $PowerSave::SpendPoints[%client.guid, %slot] = $PowerSave::SpendPoints[%client.guid, %slot] + $PowerSave::Affinity2PointGain[%lv+1];
                messageAll('msgClient',"\c2"@getTaggedString(%client.name)@" Has Leveled Up To Level "@$PowerSave::Affinity2Nextlevel[%lv+1]@" with "@$PowerSave::EXP[%client.guid, %slot]@"EXP Points!");
                bottomPrint(%client, "Congradulations "@getTaggedString(%client.name)@" you have Leveled up to Level "@$PowerSave::Affinity2Nextlevel[%lv+1]@"!", 5, 2 );
                messageClient(%client, 'msgSnd', "~wfx/misc/MA3.wav");
                echo(""@getTaggedString(%client.name)@" to level "@$PowerSave::Affinity2Nextlevel[%lv+1]@", EXP: "@$PowerSave::EXP[%client.guid, %slot]@".");
                if($PowerSave::Affinity2LevelMessage[%lv+1] !$= "") {
                   messageClient(%client, 'MsgLevel', "\c5"@$PowerSave::Affinity2LevelMessage[%lv+1]@"");
                }
                UpdateRankFile(%client);
                PrepareUpload(%client);
                //Change their name
                SetUpClientPowersName(%client);
		     }
		     //%lv = 1;
          }
       }
    }
    else {
	   for(%lv = 0; %lv < $PowerSave::RankCount; %lv++){        //check all ranks
	      if($PowerSave::EXP[%client.guid, %slot] >= $PowerSave::MinEXP[%lv+1]){
		     if($PowerSave::Level[%client.guid, %slot] < $PowerSave::Nextlevel[%lv+1]){
                $PowerSave::Level[%client.guid, %slot]++;
                $PowerSave::SpendPoints[%client.guid, %slot] = $PowerSave::SpendPoints[%client.guid, %slot] + $PowerSave::PointGain[%lv+1];
                messageAll('msgClient',"\c2"@getTaggedString(%client.name)@" Has Leveled Up To Level "@$PowerSave::Nextlevel[%lv+1]@" with "@$PowerSave::EXP[%client.guid, %slot]@"EXP Points!");
                bottomPrint(%client, "Congradulations "@getTaggedString(%client.name)@" you have Leveled up to Level "@$PowerSave::Nextlevel[%lv+1]@"!", 5, 2 );
                messageClient(%client, 'msgSnd', "~wfx/misc/MA3.wav");
                echo(""@getTaggedString(%client.name)@" to level "@$PowerSave::Nextlevel[%lv+1]@", EXP: "@$PowerSave::EXP[%client.guid, %slot]@".");
                if($PowerSave::LevelMessage[%lv+1] !$= "") {
                   messageClient(%client, 'MsgLevel', "\c5"@$PowerSave::LevelMessage[%lv+1]@"");
                }
                UpdateRankFile(%client);
                PrepareUpload(%client);
                //Change their name
                SetUpClientPowersName(%client);
		     }
		     //%lv = 1;
          }
	   }
	}
}

function PromoteAffinity(%client) {
   if($PowerSave::AccLoan[%client.guid]) {
      return;
   }
   %slot = %client.SlotNum;
   %file = ""@$PowerSave::RanksDirectory@"/"@%client.guid@"/Saved.Dat";
   new fileobject(ClientRank);
   //
   if(ClientRank.findInFile(%file, "$PowerSave::Level["@%client.guid@", "@%slot@"]") != 0) {
      %ln = ClientRank.findInFile(%file, "$PowerSave::Level["@%client.guid@", "@%slot@"]");
      echo(%ln);
   }
   //
   ClientRank.replaceLine(%file, "$PowerSave::Level["@%client.guid@", "@%slot@"] = 36;", %ln);  //going up!
   ClientRank.replaceLine(%file, "$PowerSave::EXP["@%client.guid@", "@%slot@"] = 0;", %ln+2); //back at 0
   ClientRank.close();
   ClientRank.delete();
   
   exec(%file);
   PrepareUpload(%client);
   
   SetUpClientPowersName(%client);
}

function PromoteAffinity2(%client) {
   if($PowerSave::AccLoan[%client.guid]) {
      return;
   }
   %slot = %client.SlotNum;
   %file = ""@$PowerSave::RanksDirectory@"/"@%client.guid@"/Saved.Dat";
   new fileobject(ClientRank);
   //
   if(ClientRank.findInFile(%file, "$PowerSave::Level["@%client.guid@", "@%slot@"]") != 0) {
      %ln = ClientRank.findInFile(%file, "$PowerSave::Level["@%client.guid@", "@%slot@"]");
      echo(%ln);
   }
   //
   ClientRank.replaceLine(%file, "$PowerSave::Level["@%client.guid@", "@%slot@"] = 71;", %ln);  //going up!
   ClientRank.replaceLine(%file, "$PowerSave::EXP["@%client.guid@", "@%slot@"] = 0;", %ln+2); //back at 0
   ClientRank.close();
   ClientRank.delete();

   exec(%file);
   PrepareUpload(%client);

   SetUpClientPowersName(%client);
}

function doEXPAward(%clKiller, %clVictim) {
   if($PowerSave::AccLoan[%clKiller.guid]) {
      //loaned accounts cannot gain EXP
      return;
   }
   //the NEW XP algorithm
   //Adding XP = Floor((1/7)Killed Level)
   //Affinity?
   if(!$PowerSave::Affinity[%clKiller.guid, %clKiller.slotnum]) {
      if(%clVictim.isAiControlled()) {
         %info = $AIInfo[%clVictim];
         %level = getWord(%info, 1);
      }
      else {
         %level = $PowerSave::Level[%clVictim.guid, %clVictim.slotNum];
      }
      if(%level <= 0) {
         %level = 1;
      }
      %toAdd = MCeil((1/7) * %level) * $Powers::EXPGainRate;
      %clKiller.EXP += %toAdd;
      messageClient(%clKiller, 'msgClient', "\c5POWERS: Enemy Player Killed: \c3+"@%toAdd@"EXP.");
      UpdateClientRank(%clKiller);
   }
   else {
      if(%clVictim.isAiControlled()) {
         %info = $AIInfo[%clVictim];
         %level = getWord(%info, 1);
      }
      else {
         %level = $PowerSave::Level[%clVictim.guid, %clVictim.slotNum];
      }
      if(%level <= 0) {
         %level = 1;
      }
      %toAdd = MCeil((1/7) * %level) * $Powers::EXPGainRate;
      %toAdd2 = MCeil(%toAdd/($PowerSave::Affinity[%clKiller.guid, %clKiller.slotnum] + 1));
      messageClient(%clKiller, 'msgClient', "\c5POWERS: Enemy Player Killed: \c3+"@%toAdd2@"EXP.");
      %clKiller.EXP += %toAdd2;
      UpdateClientRank(%clKiller);
   }
}
