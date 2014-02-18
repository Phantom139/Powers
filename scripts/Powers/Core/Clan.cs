//Core/Clan.cs
//Phantom139

//Clan System
//Allows players to 'Buddy Up' in FFA Games

//How it works (psedocode)
//Player opens up the score menu -> My Page -> Clans
//A Player may only be in one clan at any given time
//If a player is not in a clan, they may make one (leader), or request to join one
//if a player is in a clan, they may leave the clan, or invite people (if the leader)
//the clan leader may remove players from the clan or disband it.

//Clan members have the option to spawn near their leader is he is alive
//this function is used for the clan interface menu
function GetClaLevGUID(%guid, %slot) {
   //Steps: Load the client's Rank file
   //Grab the data stored in the file
   //Retun it.
   if($PowerSave::Class[%guid, %slot] $= "" || $PowerSave::Level[%guid, %slot] $= "") {
      if(!isFile(""@$PowerSave::RanksDirectory@"/"@%guid@"/Saved.Dat")) {
         //Invalid GUID
         error("(Clan.cs)Function GetClaLevGUID: Non-Existing GUID in the files passed");
         return;
      }
      exec(""@$PowerSave::RanksDirectory@"/"@%guid@"/Saved.Dat");
      %class = $PowerSave::Class[%guid, %slot];
      %level = $PowerSave::Level[%guid, %slot];
   }
   else {
      %class = $PowerSave::Class[%guid, %slot];
      %level = $PowerSave::Level[%guid, %slot];
   }
   return %class TAB %level;
}

function GameConnection::LoadClanData(%client) {
   //We need to load two files here
   //1. The Client's Clan File.
   //2. Their Leader's Clan File. (If not Leader)
   //Perform #1 First, we apply leader checks after
   if(!isFile(""@$PowerSave::RanksDirectory@"/"@%client.guid@"/ClanData.Dat")) {
      //not in a clan, end here.
      return;
   }
   exec(""@$PowerSave::RanksDirectory@"/"@%client.guid@"/ClanData.Dat");
   if($PowerSave::Clan["Kicked", %client.guid] == 1) {
      %client.leaveClan("The clan leader has kicked you.");
   }
   //and now, perform check #2
   if(!%client.IsClanLeader()) {
      //the clan is disbanded?
      if(!isFile(""@$PowerSave::RanksDirectory@"/"@$PowerSave::Clan["Leader", %client.guid]@"/ClanData.Dat")) {
         %client.ClanDisbanded();
         return;
      }
      else {
         exec(""@$PowerSave::RanksDirectory@"/"@$PowerSave::Clan["Leader", %client.guid]@"/ClanData.Dat");
      }
   }
}

function GameConnection::InClan(%client) {
   if(%client.isAiControlled()) {  //bots cannot have clans
      return 1;
   }
   if($PowerSave::Clan["Leader", %client.guid] $= "") {
      return 0;
   }
   else {
      return 1;
   }
}

function GameConnection::IsMemberOf(%client, %ownerGuid) {
   if($PowerSave::Clan["Leader", %client.guid] == %ownerGuid) {
      return 1;
   }
   else {
      return 0;
   }
}

function GameConnection::IsClanLeader(%client) {
   if(%client.isAiControlled()) {
      return 0;
   }
   if($PowerSave::Clan["Leader", %client.guid] == %client.guid) {
      return 1;
   }
   else {
      return 0;
   }
}

//we assume both players must be present.
function GameConnection::AddClientToClan(%client, %leader) {
   //first, lets get the clan count
   for(%i = 0; $PowerSave::ClanMember[%leader.guid, %i] !$= ""; %i++) {
      %count++;
   }
   //now that we have the count, lets specify it
   echo("CLAN: "@%client.namebase@"("@%client@"|"@%client.guid@") joined clan "@%leader.namebase@"("@%leader@"|"@%leader.guid@")");
   $PowerSave::ClanMember[%leader.guid, %count] = %client.guid;
   $PowerSave::ClanMemberName[%leader.guid, %count] = %client.namebase;
   $PowerSave::Clan["MemberCt", %client.guid] = %count;
   $PowerSave::Clan["Leader", %client.guid] = %leader.guid;
   $PowerSave::Clan["LeaderName", %client.guid] = %leader.namebase;
   //and now, we write this to the joining client's file.
   new fileobject(ClanFile);
   ClanFile.openforWrite(""@$PowerSave::RanksDirectory@"/"@%client.guid@"/ClanData.Dat");
   ClanFile.WriteLine("//Clan File For GUID "@%client.guid@"");
   ClanFile.WriteLine("//Created On "@formattimestring("yy-mm-dd")@", Powers Mod "@$PowerSave::Version@"");
   ClanFile.WriteLine("$PowerSave::ClanMember["@%leader.guid@", "@%count@"] = "@%client.guid@";");
   ClanFile.WriteLine("$PowerSave::ClanMemberName["@%leader.guid@", "@%count@"] = \""@%client.namebase@"\";");
   ClanFile.WriteLine("$PowerSave::Clan[\"MemberCt\", "@%client.guid@"] = "@%count@";");
   ClanFile.WriteLine("$PowerSave::Clan[\"Leader\", "@%client.guid@"] = "@%leader.guid@";");
   ClanFile.WriteLine("$PowerSave::Clan[\"LeaderName\", "@%client.guid@"] = \""@%leader.namebase@"\";");
   ClanFile.close();
   //As well as the leader's File
   ClanFile.openforAppend(""@$PowerSave::RanksDirectory@"/"@%leader.guid@"/ClanData.Dat");
   ClanFile.WriteLine("$PowerSave::ClanMember["@%leader.guid@", "@%count@"] = "@%client.guid@";");
   ClanFile.WriteLine("$PowerSave::ClanMemberName["@%leader.guid@", "@%count@"] = \""@%client.namebase@"\";");
   ClanFile.close();
   ClanFile.delete();
}

function GameConnection::DisplayClanMenu(%client, %tag) {
   %client.SCMPage = "SM";
   messageClient( %client, 'ClearHud', "", %tag);
   messageClient( %client, 'SetScoreHudSubheader', "", "Clans Main Menu" );
   if(%client.inClan()) {
      if(%client.IsClanLeader()) {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Clan Leader Interface");
         %index++;
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tDisband\t1>Disband Clan (Delete Clan)</a>");
         %index++;
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tViewInvites\t1>View Invites</a>");
         %index++;
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Clan Members:");
         %index++;
         //I am the leader, so my GUID goes here
         for(%i = 0; $PowerSave::ClanMember[%client.guid, %i] !$= ""; %i++) {
            %count++;
         }
         //now we output this
         for(%i = 0; %i < %count; %i++) {
            if($PowerSave::ClanMember[%client.guid, %i] == %client.guid) {
               //no kicking yourself :)
               messageClient( %client, 'SetLineHud', "", %tag, %index, ""@$PowerSave::ClanMemberName[%client.guid, %i]@" - LEADER");
               %index++;
            }
            else {
               messageClient( %client, 'SetLineHud', "", %tag, %index, ""@$PowerSave::ClanMemberName[%client.guid, %i]@" - <a:gamelink\tKickFromClan\t"@$PowerSave::ClanMember[%client.guid, %i]@"\t1>[Kick]</a>");
               %index++;
            }
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, ""@$PowerSave::Clan["LeaderName", %client.guid]@"'s Clan");
         %index++;
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Options:");
         %index++;
         messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tLeaveClan\t1>*Leave Clan</a>");
         %index++;
         messageClient( %client, 'SetLineHud', "", %tag, %index, "*Spawn Near Leader (If Alive)");
         %index++;
         messageClient( %client, 'SetLineHud', "", %tag, %index, "Clan Members:");
         %index++;
         //
         //I am NOT the leader, so my leader's GUID goes here
         for(%i = 0; $PowerSave::ClanMember[$PowerSave::Clan["Leader", %client.guid], %i] !$= ""; %i++) {
            %count++;
         }
         //now we output this
         for(%i = 0; %i < %count; %i++) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, ""@$PowerSave::ClanMemberName[%client.guid, %count]@"");
            %index++;
         }
         //
      }
   }
   else {
      messageClient( %client, 'SetLineHud', "", %tag, %index, "You are not in a clan");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tFormClan\t1>Form A Clan</a>");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tListClans\t1>Join A Clan</a>");
      %index++;
   }
   return %index;
}

function GameConnection::FormClan(%client, %tag) {
   %client.SCMPage = "SM";
   messageClient( %client, 'ClearHud', "", %tag);
   messageClient( %client, 'SetScoreHudSubheader', "", "Clan Formation" );
   if(%client.inClan() || %client.IsClanLeader()) {
      //ooo hacky hacky
      messageClient( %client, 'SetLineHud', "", %tag, %index, "You are already in a clan.");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Return to main menu</a>");
      %index++;
      return;
   }
   //make the clan, create the file
   echo("CLAN: "@%client.namebase@"("@%client@"|"@%client.guid@") created clan.");
   $PowerSave::ClanMember[%client.guid, 0] = %client.guid;
   $PowerSave::ClanMemberName[%client.guid, 0] = %client.namebase;
   $PowerSave::Clan["MemberCt", %client.guid] = 0;
   $PowerSave::Clan["Leader", %client.guid] = %client.guid;
   $PowerSave::Clan["LeaderName", %client.guid] = %client.namebase;
   //and now, we write this to the joining client's file.
   new fileobject(ClanFile);
   ClanFile.openforWrite(""@$PowerSave::RanksDirectory@"/"@%client.guid@"/ClanData.Dat");
   ClanFile.WriteLine("//Clan File For GUID "@%client.guid@"");
   ClanFile.WriteLine("//Created On "@formattimestring("yy-mm-dd")@", Powers Mod "@$PowerSave::Version@"");
   ClanFile.WriteLine("$PowerSave::ClanMember["@%client.guid@", 0] = "@%client.guid@";");
   ClanFile.WriteLine("$PowerSave::ClanMemberName["@%client.guid@", 0] = \""@%client.namebase@"\";");
   ClanFile.WriteLine("$PowerSave::Clan[\"MemberCt\", "@%client.guid@"] = 0;");
   ClanFile.WriteLine("$PowerSave::Clan[\"Leader\", "@%client.guid@"] = "@%client.guid@";");
   ClanFile.WriteLine("$PowerSave::Clan[\"LeaderName\", "@%client.guid@"] = \""@%client.namebase@"\";");
   ClanFile.close();
   exec(""@$PowerSave::RanksDirectory@"/"@%client.guid@"/ClanData.Dat");
   //
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Clan successfuly formed");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Return to main menu</a>");
   %index++;
}

function GameConnection::DisplayCurrentClans(%client, %tag) {
   %client.SCMPage = "SM";
   messageClient( %client, 'ClearHud', "", %tag);
   messageClient( %client, 'SetScoreHudSubheader', "", "Clans Available To Join" );
   if(%client.inClan() || %client.IsClanLeader()) {
      //ooo hacky hacky
      messageClient( %client, 'SetLineHud', "", %tag, %index, "You are already in a clan.");
      %index++;
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Return to main menu</a>");
      %index++;
      return;
   }
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(%cl.IsClanLeader()) {
         messageClient( %client, 'SetLineHud', "", %tag, %index, ""@%cl.namebase@" - <a:gamelink\tSendRTJ\t"@%cl@"\t1>[Send Request]</a>");
         %index++;
      }
   }
   messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Return to main menu</a>");
   %index++;
}

//Clan Leader things
function GameConnection::DisbandClan(%client) {
   %client.leaveClan("You have disbanded your clan, all current members will be removed.");
}

function GameConnection::ClanDisbanded(%client) {
   %client.leaveClan("The Clan Has Been Disbanded By Your Leader.");
}

function GameConnection::KickMember(%client, %target) {
   %target.leaveClan("The Clan Leader has kicked you from the clan.");
}

function GameConnection::InviteManage(%client, %target, %option) {
   switch$ (%option){
      case "AcceptInvite":
         messageClient(%target, 'MsgTrg', "\c5Clan: "@%client.namebase@" has accepted you into the clan.");
         %target.AddClientToClan(%client);
         %target.inviteSent[%client] = 0;
      case "DenyInvite":
         messageClient(%target, 'MsgTrg', "\c5Clan: "@%client.namebase@" has denied your request to join.");
         %target.inviteSent[%client] = 0;
   }
   closeScoreHudFSERV(%client);
}

function GameConnection::ViewInvites(%client, %tag) {
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(%cl.inviteSent[%client] == 1) {
         messageClient( %client, 'SetLineHud', "", %tag, %index, ""@%cl.namebase@": <a:gamelink\tInviteMang\t"@%cl@"\tAcceptInvite\t1>[Accept]</a> - <a:gamelink\tInviteMang\t"@%cl@"\tDenyInvite\t1>[Deny]</a>");
         %index++;
      }
   }
   messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Return to main menu</a>");
   %index++;
}

//Clan Member Things
function GameConnection::leaveClan(%client, %reason) {
   %tag = $TagToUseForScoreMenu;
   %leader = $PowerSave::Clan["Leader", %client.guid];
   %file = ""@$PowerSave::RanksDirectory@"/"@%leader@"/ClanData.Dat";
   //perform operations
   echo("CLAN: "@%client.namebase@"("@%client@"|"@%client.guid@") left clan: "@%reason@"");
   %count = $PowerSave::Clan["MemberCt", %client.guid];
   $PowerSave::ClanMember[$PowerSave::Clan["Leader", %client.guid], %count] = "";
   $PowerSave::ClanMemberName[$PowerSave::Clan["Leader", %client.guid], %count] = "";
   $PowerSave::Clan["MemberCt", %client.guid] = "";
   $PowerSave::Clan["Leader", %client.guid] = "";
   $PowerSave::Clan["LeaderName", %client.guid] = "";
   deleteFile(""@$PowerSave::RanksDirectory@"/"@%client.guid@"/ClanData.Dat");
   //And now the tricky part, updating the leader's file
   if(%leader != %client.guid) {
      new fileobject(ClanFile);
      if(ClanFile.findInFile(%file, "$PowerSave::ClanMember["@%leader@", "@%count@"]", "", "") != 0) {
         %ln = ClanFile.findInFile(%file, "$PowerSave::ClanMember["@%leader@", "@%count@"]", "", "");
         ClanFile.replaceLine(%file, "", %ln);
      }
      if(ClanFile.findInFile(%file, "$PowerSave::ClanMemberName["@%leader@", "@%count@"]", "", "") != 0) {
         %ln = ClanFile.findInFile(%file, "$PowerSave::ClanMemberName["@%leader@", "@%count@"]", "", "");
         ClanFile.replaceLine(%file, "", %ln);
      }
      ClanFile.close();
      ClanFile.delete();
      exec(%file);
   }
   MessageClient(%client, 'msgAlert', "\c5CLAN: "@%reason@"");
}

function GameConnection::SendRequestToJoin(%client, %leader) {
   %client.inviteSent[%leader] = 1;
   messageClient(%client, 'msgClient', "\c5Clan: Request to join sent to "@%leader.namebase@".");
   messageClient(%leader, 'msgClient', "\c5Clan: "@%client.namebase@" would like to join your clan, you may accept deny from your page.");
   closeScoreHudFSERV(%client);
}

//Assets
function GameConnection::GetProperTeam(%client) {
   if(!%client.InClan() || %client.isAIControlled()) {
      return "None";
   }
   //1. Check if our leader is here, he always has the correct team.
   // SUB CON 1. If Leader, find one of our clanmates, and join him
   //2. If not, check for similar clan members.
   //3. Place into the smallest team.
   %leader = $PowerSave::Clan["Leader", %client.guid];
   //Condition 1
   if(isPresentGUID(%leader)) {
      if(%leader != %client.guid) {
         %cl = plguidtocid(%leader);
         if(%cl.team != 0) {
            return %cl.team; //stop it here!
         }
      }
      else {
         for(%i = 0; %i < ClientGroup.getCount(); %i++) {
            %cl = ClientGroup.getObject(%i);
            if(%cl != %client) {
               if(%cl.IsMemberOf(%leader)) {
                  if(%team != 0) {
                     %team = %cl.team;
                     return %team;
                  }
                  else {
                     //keep going, lets see if we find a second match
                  }
               }
            }
         }
      }
   }
   //Conditions 2 and 3
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(%cl != %client) {
         if(%cl.IsMemberOf(%leader)) {
            if(%team != 0) {
               %team = %cl.team;
               return %team;
            }
            else {
               //keep going, lets see if we find a second match
            }
         }
      }
   }
   // No matches, proceed
   return "None";   //use default scheme
}

function isPresentGUID(%guid) {
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(%cl.guid == %guid) {
         return true;
      }
   }
   return false;
}
