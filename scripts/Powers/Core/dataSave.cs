//dataSave.cs
//Phantom139
//this script file manages the client save data for powers mod

//GameConnection::checkClientData(%client) - This function has two purposes, first it can load client data, and secondly it can prepare creation of new data
function GameConnection::checkClientData(%client) {
   if(%client.isAIControlled() || %client.guid $= "") {
      return;
   }
   %file = $PowerSave::RanksDirectory@"/"@%client.guid@"/Saved.Dat";
   if(!isFile(%file)) {
      echo("*Creating client data file: "@%client.namebase@" ("@$client.guid@")");
      %client.dataContainer = new SimSet("ClientData_"@%client.guid);
      //file does not exist on the server, skip process and move to create
      for(%i = 1; %i <= $Powers::MaxClientSaveSlots; %i++) {
         %client.data[%i] = new ScriptObject("classData"@%i@"_"@%client.guid);
         //write the default data
         %client.data[%i].level = 1;
         %client.data[%i].spendPoints = 1;
         %client.data[%i].exp = 0;
         %client.data[%i].class = "Undecided";
         %client.data[%i].affinity = 0;
         //add the container and scriptObject to the core
         %client.dataContainer.add(%client.data[%i]);
      }
      //save the file
      %client.saveData();
   }
   else {
      if(isObject(%client.dataContainer)) {
         %client.dataContainer.delete();
      }
      //load client data
      echo("Loading client data "@%client.guid);
      exec(%file);
      //load the data container
      if(nameToID("ClientData_"@%client.guid) == -1) {
         checkForPowersFileUpdate21(%client, %file);
      }
      else {
         %client.dataContainer = nameToID("ClientData_"@%client.guid);
      }
      
      //validate the loaded data
      // * Validation steps.
      // ** 1. Additional slot adding
      %maxSlots = $Powers::MaxClientSaveSlots;
      %i = 1;
      while(%client.slot(%i) != -1) {
         %lastOpen = %i;
         %i++;
      }
      if(%lastOpen < %maxSlots) {
         //server has added more slots, add it to the data files of the client
         echo("New slots avaliable for "@%client@", last one is "@%lastOpen@", needs "@%maxSlots);
         for(%i = %lastOpen+1; %i <= %maxSlots; %i++) {
            %client.data[%i] = new ScriptObject("classData"@%i@"_"@%client.guid);
            //write the default data
            %client.data[%i].level = 1;
            %client.data[%i].spendPoints = 1;
            %client.data[%i].exp = 0;
            %client.data[%i].class = "Undecided";
            %client.data[%i].affinity = 0;
            %client.dataContainer.add(%client.data[%i]);
         }
      }
      else {
         //server slots are fine or less... if this is the case do not modify the data
         //we do not want to remove any player's data.
      }
   }
}

//GameConnection::Slot(%client, %slot) - Returns the slot object, used in a vast array of functions to either add data or to test it
function GameConnection::slot(%client, %slot) {
   if(!isObject(%client)) {
      return;
   }
   if(%client.isAIControlled() || %client.guid $= "") { //|| %client.guid $= "") {
      return;
   }
   if(%slot $= "") {
      return;
   }
   if(!isObject(%client.data[%slot])) {
      %test = nameToID("ClientData_"@%client.guid@"/classData"@%slot@"_"@%client.guid);
      if(%test == -1) {
         error("*SERVER: call to slot:: "@%client TAB %slot@" is invalid at final test.");
         return -1;
      }
      
      %client.data[%slot] = %test;
   }
   return %client.data[%slot];
}

//GameConnection::saveData(%client) - This function is self explanitory, save the client's data
function GameConnection::saveData(%client) {
   if(%client.isAIControlled() || %client.guid $= "") {// || %client.guid $= "") {
      return;
   }
   echo("Saving data for "@%client.namebase);
   %file = $PowerSave::RanksDirectory@"/"@%client.guid@"/Saved.Dat";

   if(!isObject(%client.dataContainer)) {
      %client.dataContainer = nameToID("ClientData_"@%client.guid);
      if(!isObject(%client.dataContainer)) {
         error("* SaveData Error: No such data container on "@%client SPC %client.namebase);
         repairContainter(%client);
         return;
      }
   }
   
   %slot = %client.slotNum;
   //pull the slot from the container and re-add the updated one
   %test = nameToID("ClientData_"@%client.guid@"/classData"@%slot@"_"@%client.guid);
   if(%test == -1) {
      echo("* SaveData - WARNING: Unable to find slot "@%slot@" on client "@%client SPC %client.namebase);
      %client.dataContainer.save(%file);
      return;
   }
   else {
      %client.dataContainer.remove(%test);
      %client.dataContainer.add(%client.data[%slot]);
      //save the new container.
      %client.dataContainer.save(%file);
   }
}

//Auto-Recovery... Evil Script Majik at work here...
//hunt down the direct data objects, and place them in a new simSet, then force save the new simSet.
function repairContainter(%client) {
   echo("Repairing Container for "@%client);
   %client.dataContainer = new SimSet("ClientData_"@%client.guid);
   %slot = 1;
   %test = %client.data[%slot];
   while(isObject(%test)) {
      echo("* Adding Slot Object: "@%test);
      %client.dataContainer.add(%test);
      %slot++;
      %test = %client.data[%slot];
   }
   //save
   echo("Repair Complete: saving..");
   %client.saveData();
}

function checkForPowersFileUpdate21(%client, %file) {
   exec(%file);
   %testCoreCont = nameToID("coreCont_"@%client.guid);
   if(%testCoreCont != -1) {
      //old system
      echo("Old File Detected On "@%client@" - Converting Now.");
      //
      echo("Creating Main Container");
      %client.dataContainer = new SimSet("ClientData_"@%client.guid);
      %i = 1;
      %test = nameToID("coreCont_"@%client.guid@"/dataContainer_"@%client.guid@"/classData"@%i@"_"@%client.guid);
      while(%test != -1) {
         //create a new script object with the new format
         echo("Adding "@%test@" to "@%data);
         %client.dataContainer.add(%test);
         //
         %i++;
         %test = nameToID("coreCont_"@%client.guid@"/dataContainer_"@%client.guid@"/classData"@%i@"_"@%client.guid);
      }
   }
   %testCoreCont.delete();
   //done.
   %client.saveData();
   schedule(250, 0, exec, %file);
}
