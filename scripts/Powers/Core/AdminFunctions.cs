//AdminFunctions.cs
//Phantom139

//Asset commands to assist server hosting processes

//cons(%m)
//Sends a message to all players under the Server Admin: Tag
function cons(%m) {
   MessageAll('msgAdmin', "\c1Server Admin: \c4"@%m);
   echo("Server Admin: "@%m);
}

function listInfo(%client) {
   echo("*** INFO FOR "@%client@" - "@%client.namebase);
   echo("* GUID: "@%client.guid);
   for(%i = 1; %i <= $Powers::MaxClientSaveSlots; %i++) {
      echo("* Slot "@%i@": "@ %client.slot(%i).level SPC %client.slot(%i).class);
   }
}

function setInfo(%client, %slot, %aspect, %newValue) {
   eval(""@%client@".slot("@%slot@")."@%aspect@" = "@%newValue@";");
   echo("* Evaluate setInfo: "@%client@".slot("@%slot@")."@%aspect@" = "@%newValue@";");
   %client.saveData();
}

function load(%file) {
   MessageAll('msgAdminForce', "\c5SERVER: Loading File "@%file@" - Potential Lag Spike...");
   schedule(500, 0, exec, %file);
   schedule(600, 0, reload, %file);
}

function createFile(%file) {
   if(isFile(%file)) {
      error("Already exists.");
      return;
   }
   MessageAll('msgAdminForce', "\c5SERVER: Creating File "@%file@".");
   %fOBJ = new FileObject();
   %fOBJ.openForWrite(%file);
   %fOBJ.writeLine("//Testing This...");
   %fOBJ.close();
   %fOBJ.delete();
}

function mark(%client) {
   if(!isObject(%client.player) || %client.player.getState() $= "dead") {
      return;
   }
   else {
      %player = %client.player;
      %pos = %player.getPosition();
      %waypoint = new  (WayPoint)(){
         dataBlock        = WayPointMarker;
         position         = %pos;
         name             = %client.namebase;
         scale            = "0.1 0.1 0.1";
         team             = %player.team;
      };
      MissionCleanup.add(%waypoint);
      %waypoint.schedule(5000, "delete");
   }
}

function resetBots() {
   MessageAll('msgAdminForce', "\c5SERVER: Resetting all AI Connections. Standby...");
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      if(ClientGroup.getObject(%i).guid $= "") {
         ClientGroup.getObject(%i).drop();
      }
   }
   schedule(1000, 0, MessageAll, 'msgAdminForce', "\c5SERVER: Readding AI Connections. Standby...");
   schedule(1000, 0, aiConnectMultiple, $Host::BotCount, $Host::MinBotDifficulty, $Host::MaxBotDifficulty, -1 );
}

function cleanSlots(%client) {
   %slot = 1;
   %test = nameToID("ClientData_"@%client.guid@"/classData"@%slot@"_"@%client.guid);
   while(%test == -1) {
      %test.delete();
      %slot++;
      %test = nameToID("ClientData_"@%client.guid@"/classData"@%slot@"_"@%client.guid);
   }
}
