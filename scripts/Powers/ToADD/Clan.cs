//Clan.cs
//Second Version
//Phantom139

//Second Clan (Teamming) System for powers mod
function initializeClanStack() {
   new ScriptObject(ClanData) {};
}

function GameConnection::CDL(%client) {
   return ClanData.loadedData[%client.guid];
}

function GameConnection::loadClanData(%client) {
   //
   if(!isObject(ClanData)) {
      initializeClanStack();
   }
   //
   if(!isFile($PowerSave::RanksDirectory@"/"@%client.guid@"/ClanData.Dat")) {
      //not in a clan, end here.
      return;
   }
   exec($PowerSave::RanksDirectory@"/"@%client.guid@"/ClanData.Dat");
   ClanData.loadedData[%client.guid] = 1;
   %leaderGUID = %client.getClanLeader();
   //Leader is present, identify self as "avaliable"
   if(isSet(%leaderGUID)) {
      ClanData.inClan[%client.guid] = 1;
      ClanData.present[%leaderGUID, %client.guid] = 1;
      ClanData.lead[%client.guid] = %leaderGUID;
      %leading_Client = isPresentGUID(%leaderGUID);
      if(%leading_Client != -1) {
         ClanData.leadClient[%client.guid] = %leading_Client;
      }
   }
   else {
      ClanData.inClan[%client.guid] = 0;
   }
}

function GameConnection::getClanLeader(%client) {
   if(!%client.CDL()) {
      %client.loadClanData();
   }
   return $PowerSave::Clan[%client.guid, "leaderGUID"];
}

function GameConnection::addToClan(%client, %leader) {
   //Begin here
   if(!%client.CDL()) {
      messageClient(%client, 'msgClient', "\c3Powers Mod: Whoops! Your clan data wasn't loaded, let's do that now");
      %client.loadClanData();
   }
   if(ClanData.inClan[%client.guid]) {
      messageClient(%client, 'msgClient', "\c3Powers Mod: Whoa there, you can't be in two clans.");
      return;
   }
   //
   if(!%client.hasClanInvite[%leader]) {
      //nope, you do not get in this time buddy :P
      messageClient(%client, 'msgClient', "\c3Powers Mod: Nice Try...");
      return;
   }
   else {
      //Invite Recieved, check if the leader's data is currently loaded
      //this should be true, seeing as the leader just sent the invite.
      if(!%leader.CDL()) {
         messageClient(%client, 'msgClient', "\c3Powers Mod: This is a rare error, the leader's clan data is not loaded, yet you have an invite to his clan.");
         return;
      }
      else {
         //update the ClanData
         ClanData.inClan[%client.guid] = 1;
         ClanData.present[%leader.guid, %client.guid] = 1;
         ClanData.lead[%client.guid] = %leader.guid;
         ClanData.leadClient[%client.guid] = %leader;
         ReOrderClanList(%leader);
         UpdateClanFile(%client);
         UpdateClanFile(%leader);
      }
   }
}

function UpdateClanFile(%client) {
   %FILO = new FileObject();
   %FILO.target = $PowerSave::RanksDirectory@"/"@%client.guid@"/ClanData.Dat";
   //First thing to check, are we the clan leader? if so, DO NOT clear the file
   %leader = ClanData.lead[%client.guid];
   if(%client.guid $= %leader) {
      //ok, our file is already jam packed full-o-data, let's re-tag the time format
      %FILO.openForRead(%FILO.target);
      %i = 1;
      while(!%FILO.isEOF()) {
         %line[%i] = %FILO.readLine();
         //formatted data is like so...
         // comment lines                                                    1,2
         // $PowerSave::ClanData[%client.guid, "Leader"] = leader's guid;      3
         // $PowerSave::ClanData[%client.guid, "LeaderName"] = leader's name;  4
         // alternating lines are as follows below
         // $PowerSave::ClanData[%client.guid, index, "memberGUID"] = guid;    % 2 != 0
         // $PowerSave::ClanData[%client.guid, index, "memberName"] = name;    % 2 == 0
         if(%i % 2 == 0) {

         }
      }
      %i = 3;
      %FILO.close();
      %FILO.openForWrite(%FILO.target);
      //Tag the last edit time
      %FILO.writeLine("//Clan Data File: "@%client.guid@"("@%client.namebase@")");
      %FILO.writeLine("//Updated: "@FormatTimeString("mm/dd/yyyy - hh::nn")@");
      while(isSet(%line[%i])) {
         %FILO.writeLine(%line[%i]);
         %i++;
      }
   }
   //alrigt, not the leader, letsa move on!
   else {
      //clear the file, openForWrite
      %FILO.openForWrite(%FILO.target);
      //Tag the last edit time
      %FILO.writeLine("//Clan Data File: "@%client.guid@"("@%client.namebase@")");
      %FILO.writeLine("//Updated: "@FormatTimeString("mm/dd/yyyy - hh::nn")@");
      //obtain our leader info from the ClanData Object
      %FILO.writeLine("$PowerSave::Clan["@%client.guid@", \"LeaderGUID\"] = "@%leader@";");
   }
}

//Asset Functions
function isPresentGUID(%guid) {
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(%cl.guid == %guid) {
         return %cl;
      }
   }
   return -1;
}
