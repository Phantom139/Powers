//AccountLoaning.cs
//Phantom139

//Allows players to play on their friend's accounts, EXP gain is disabled for "loaned" accounts
//and a special Green (*) tag is attached to their name if the account is "Loaned"

// 11/4/12 - This is an old file, but it contains some necessary slot adjustment code for when a player loads up.

function DropAccountLoans(%cli) {
   %guid = %cli.guid;
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %TCL = ClientGroup.getObject(%i);
      if(!%TCL.isAiControlled()) {
         %SLOchk = $PowerSave::LoaningSlotNumber[%TCL.guid, %guid];
         if(isSet(%SLOchk)) {
            $PowerSave::LoanAllowed[%TCL.guid, %guid, %SLOchk] = 0;
            $PowerSave::LoanGiven[%TCL.guid, %guid] = 0;
            $PowerSave::LoanOnSlotGiven[%TCL.guid, %SLOchk] = 0;
            $PowerSave::LoaningSlotNumber[%TCL.guid, %guid] = "";
         }
      }
   }
}

function GameConnection::ShowLoanOption(%client, %slot, %index, %tag) {
   %client.SCMPage = "SM";
   messageClient( %client, 'SetScoreHudSubheader', "", "Select a Player to loan Slot "@%slot@" to.");
   for(%i = 0; %i < clientGroup.getCount(); %i++) {
      %tCLI = ClientGroup.getObject(%i);
      if(!%tCLI.isAiControlled()) {
         // Are we currently loaning an account to him?
         if($PowerSave::AccLoan[%tCLI.guid] != %client.guid) {
            //No, however, have we loaned an account to him?
            if(!$PowerSave::LoanGiven[%client.guid, %tCLI.guid]) {
               //nope, looks like we can loan this account, system will pick up others
               messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGrantLoan\t"@%tCLI@"\t"@%slot@">"@%tCLI.namebase@"</a>");
               %index++;
            }
            else {
               //Yes, Display this message
               messageClient( %client, 'SetLineHud', "", %tag, %index, ""@%tCLI.namebase@" - Loan Request Under Way");
               %index++;
            }
         }
         else {
            //Yes, display the message
            messageClient( %client, 'SetLineHud', "", %tag, %index, ""@%tCLI.namebase@" - Currently Loaning Account");
            %index++;
         }
      }
   }
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Exit</a>');
   %index++;
   return %index;
}

function GameConnection::GrantLoan(%client, %target, %accountSlot, %tag, %index) {
   if($PowerSave::LoanGiven[%client.guid, %target.guid] == 1) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Cannot grant two loans to "@%target.namebase@".");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Exit</a>');
      %index++;
      return %index;
   }
   if($PowerSave::LoanOnSlotGiven[%client.guid, %accountSlot] == 1) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "Cannot grant two loans on the same account slot.");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Exit</a>');
      %index++;
      return %index;
   }
   $PowerSave::LoaningSlotNumber[%client.guid, %target.guid] = %accountSlot;
   $PowerSave::LoanAllowed[%client.guid, %target.guid, %accountSlot] = 1;
   $PowerSave::LoanGiven[%client.guid, %target.guid] = 1;
   $PowerSave::LoanOnSlotGiven[%client.guid, %accountSlot] = 1;
   MessageClient(%target, 'msgClient', "\c3Powers: "@%client.namebase@" has granted you access to his account slot #"@%accountSlot@"");
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Access to slot "@%accountSlot@" given to "@%target.namebase@".");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Exit</a>');
   %index++;
   return %index;
}

function GameConnection::ShowLoanSelection(%client, %index, %tag) {
   //
   messageClient( %client, 'SetLineHud', "", %tag, %index, "These are your availiable loans:");
   %index++;
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %tCLI = ClientGroup.getObject(%i);
      if($PowerSave::LoanGiven[%tCLI.guid, %client.guid] == 1) {
         //I have one for you :P
         %slotIndex = $PowerSave::LoaningSlotNumber[%tCLI.guid, %client.guid];
         %detail_class = $PowerSave::Class[%tCLI.guid, %slotIndex];
         %detail_level = $PowerSave::Level[%tCLI.guid, %slotIndex];
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tApplyLoan\t"@%tCLI@"\t"@%slotIndex@">"@%tCLI.namebase@"'s Slot "@%slotIndex@"</a> - LV."@%detail_level@" "@%detail_class@"");
         %index++;
      }
   }
   //
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Exit</a>');
   %index++;
   return %index;
}

function GameConnection::LoanAccount(%client, %owner, %accountSlot, %tag, %index) {
   if(!$PowerSave::LoanAllowed[%owner.guid, %client.guid, %accountSlot]) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "You do not have access to this account.");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Exit</a>');
      %index++;
      return %index;
   }
   $PowerSave::AccLoan[%client.guid] = %owner.guid;
   $PowerSave::Loan[%owner.guid, %accountSlot] = %client;
   $PowerSave::Loaner[%client.guid] = %owner TAB %accountSlot;
   //clean this up
   $PowerSave::LoanAllowed[%owner.guid, %client.guid, %accountSlot] = 0;
   $PowerSave::LoanGiven[%owner.guid, %client.guid] = 0;
   $PowerSave::LoanOnSlotGiven[%owner.guid, %accountSlot] = 0;
   $PowerSave::LoaningSlotNumber[%owner.guid, %client.guid] = "";
   //Grab the details from the owner's account's stats
   //and then apply them to the "loanee"
   ApplyAspectsOf(%owner, %client, %accountSlot);
   if(isObject(%client.player)) {
      buyFavorites(%client);
   }
   %client.SelectingSlot = 0;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Loaned Account "@%owner.namebase@"'s Slot "@%accountSlot@" activated.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Exit</a>');
   %index++;
   return %index;
}

//Eviction time :D
function GameConnection::EvictLoan(%client, %slot) {
   %target = $PowerSave::Loan[%client.guid, %slot];
   // kill the slot 11 data, kill their player, and push the selection hud
   $PowerSave::Level[%target.guid, "LoanSlot"] = 1;
   $PowerSave::Class[%target.guid, "LoanSlot"] = "Undecided";
   if(%target.player.isAlive()) {
      %target.player.scriptKill();
   }
   messageClient(%client, 'msgClient', "\c3Powers: Loan on slot "@%slot@"("@%target.namebase@") evicted");
   messageClient(%target, 'msgClient', "\c3Powers: Your loan on this account has been evicted by "@%client.namebase@"");
   //
   //open the slot selection hud, set to slot select mode
   %target.selectingSlot = 1;
   serverCmdShowHud(%target, 'scoreScreen');
   Game.processGameLink(%target, "SelectSlot");
}

function GameConnection::BuildSlotSelect(%client, %tag, %index) {
   %client.SCMPage = "SM";
   //Update Ranks First, We like to keep our stuff
   UpdateClientRank(%client);
   //Then Load the Data to check for additional slot details
   LoadClientRankfile(%client);
   //
   messageClient( %client, 'SetScoreHudSubheader', "", "Choose Your Account To Use");
   for(%i = 1; %i <= $Powers::MaxClientSaveSlots; %i++) {
      if(isSet($PowerSave::Loan[%client.guid, %i])) {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tEvictLoan\t"@%i@">(*)SLOT "@%i@"</a> - Lv."@$PowerSave::Level[%client.guid, %i]@" "@$PowerSave::Class[%client.guid, %i]@" -> Loaned To: "@$PowerSave::Loan[%client.guid, %i].namebase@"");
         %index++;
      }
      else {
         if($PowerSave::Class[%client.guid, %i] $= "") {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tSlotSel\t"@%i@">SLOT "@%i@"</a> - Open Slot");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tSlotSel\t"@%i@">SLOT "@%i@"</a> - Lv."@$PowerSave::Level[%client.guid, %i]@" "@$PowerSave::Class[%client.guid, %i]@" * <a:gamelink\tDoLoan\t"@%i@">Loan This Account</a>");
            %index++;
         }
      }
   }
   messageClient( %client, 'SetLineHud', "", %tag, %index, "");
   %index++;
   if(!%client.SelectingSlot) {     //If we already have selected, we add the exit and loan select buttons
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tLoanSel\t1>Select a Loaned Account</a>");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Exit</a>');
      %index++;
   }
   return %index;
}

function GameConnection::SelectAccount(%client, %tag, %index, %slot) {
   if($PowerSave::Level[%client.guid, %slot] $= "") {
      SaveClientDataInfo(%client, "Level["@%client.guid@", "@%slot@"]", "1", "NX");
      SaveClientDataInfo(%client, "SpendPoints["@%client.guid@", "@%slot@"]", "1", "NX");
      SaveClientDataInfo(%client, "EXP["@%client.guid@", "@%slot@"]", "0", "NX");
      SaveClientDataInfo(%client, "TopPlPosition["@%client.guid@", "@%slot@"]", "1", "NX");
   }
   //
   if($PowerSave::AccLoan[%client.guid] != 0) {
      %owner = getField($PowerSave::Loaner[%client.guid], 0);
      %slot = getField($PowerSave::Loaner[%client.guid], 1);
      $PowerSave::Loan[%owner.guid, %slot] = "";
      $PowerSave::Loaner[%client.guid] = "";
      $PowerSave::AccLoan[%client.guid] = 0;
   }
   //
   %client.SlotNum = %slot;
   LoadClientRankfile(%client);
   if(isObject(%client.player)) {
      buyFavorites(%client);
   }
   SetUpClientPowersName(%client);
   %client.SelectingSlot = 0;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Slot "@%slot@" activated, Setting Your Stats and Activating Account.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Exit</a>');
   %index++;
   return %index;
}

function ApplyAspectsOf(%owner, %recieve, %slot) {
   //obtain the level/class information
   %lv = $PowerSave::Level[%owner.guid, %slot];
   %class = $PowerSave::Class[%owner.guid, %slot];
   //temporarilly induct a slot Max + 1 on the revieving client
   //and apply these new stats to it.
   $PowerSave::Level[%recieve.guid, "LoanSlot"] = %lv;
   $PowerSave::Class[%recieve.guid, "LoanSlot"] = %class;
   %recieve.SlotNum = "LoanSlot";
   //set their name accordingly
   if(%lv $= "") {
      %lv = 1;
   }
   if(%class $= "") {
      %classTag = "Und";
   }
   else if(%class $= "Undecided") {
      %classTag = "Und";
   }
   else if(%class $= "Witch") {
      %classTag = "Wit";
   }
   else if(%class $= "Demon") {
      %classTag = "Dem";
   }
   else if(%class $= "Phantom") {
      %classTag = "Pha";
   }
   //
   else if(%class $= "Guardian") {
      %classTag = "Grd";
   }
   else if(%class $= "Star Lighter") {
      %classTag = "Str";
   }
   else if(%class $= "Enforcer") {
      %classTag = "Enf";
   }
   else if(%class $= "Devastator") {
      %classTag = "Dev";
   }
   else if(%class $= "Overseer") {
      %classTag = "Osr";
   }
   else if(%class $= "Cryonic Embassador") {
      %classTag = "Cye";
   }
   //
   else if(%class $= "Gladiator") {
      %classTag = "Gld";
   }
   else if(%class $= "Star Sighter") {
      %classTag = "Sgt";
   }
   else if(%class $= "Prospector") {
      %classTag = "Pct";
   }
   else if(%class $= "Annihilator") {
      %classTag = "Ahi";
   }
   else if(%class $= "Phantom Lord") {
      %classTag = "PLd";
   }
   else if(%class $= "Deep Freezer") {
      %classTag = "Frz";
   }
   //
   %tag = "["@%classTag@":LV"@%lv@"]";
   %name = "\cp\c9(*)\c7" @ %tag @ "\c6" @ %recieve.namebase @ "\co";
   MessageAll( 'MsgClientNameChanged', "", %recieve.name, %name, %recieve );
   removeTaggedString(%recieve.name);
   %recieve.name = addTaggedString(%name);
   setTargetName(%recieve.target, %recieve.name);
}
