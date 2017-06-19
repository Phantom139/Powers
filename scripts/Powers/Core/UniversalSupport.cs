//UNIVERSAL RANKS
//CONNECTS TO PGD, UPLOADS/DOWNLOADS/ECT

//Globals
$PGDPHPRankUploadHandler = "/public/Univ/Ranks/upload.php";

$PGDPort = 80; //TCP
$PGDServer = "www.phantomdev.net";
//

//IsPGDConnected
function GameConnection::IsPGDConnected(%client) {
   if($playingOffline) {
      error("*PGD Connect: In Offline Mode, PGD Services are Inactive");
      return 0;
   }
   %guid = %client.guid;
   if(!%client)
      return 0;

   if($PGD::IsPGDConnected[%guid] $= "") {
      %client.CheckPGDConnect();
      return %client.schedule(2000, "IsPGDConnected");
   }
   else {
      return $PGD::IsPGDConnected[%guid];
   }
}
//---------- PGD Saving --------------------------------------------------------

function GameConnection::CheckPGDConnect(%client) {
   if($playingOffline) {
      error("*PGD Connect: In Offline Mode, PGD Services are Inactive");
      return 0;
   }
   %guid = %client.guid;
   %server = ""@$PGDServer@":"@$PGDPort@"";
   if (!isObject(GUIDGrabber))
      %Downloader = new HTTPObject(GUIDGrabber){};
   else %Downloader = GUIDGrabber;
   %filename = "/public/Univ/IsConnected.php?guid="@%guid@""; //File Location
   %Downloader.guid = %guid;
   %Downloader.client = %client;
   %Downloader.get(%server, %filename);
}

function GUIDGrabber::onLine(%this, %line) {
   if (strstr(%line, "yes") != -1) {
      $PGD::IsPGDConnected[%this.guid] = 1;
      %this.disconnect();
      %this.schedule(1000, delete);
   }
   else if (strStr(%line, "no") != -1) {
      $PGD::IsPGDConnected[%this.guid] = 0;
      %this.disconnect();
      %this.schedule(1000, delete);
   }
}

//PGD IS FILE
function PGD_IsFile(%file) {
   if($playingOffline) {
      error("*PGD Connect: In Offline Mode, PGD Services are Inactive");
      return 0;
   }
   
   if($PGD::IsFile[%file] $= "" || $PGD::IsFile[%file] == -1) {
      PGD_IsFileDL(%file);
      return schedule(5000, 0, "PGD_IsFile", %file);
   }
   else
      return $PGD::IsFile[%file];
}

function PGD_IsFileDL(%file) {
   if($playingOffline) {
      error("*PGD Connect: In Offline Mode, PGD Services are Inactive");
      return 0;
   }
   %server = ""@$PGDServer@":"@$PGDPort@"";
   %filename = "/public/Univ/IsFile.php?File="@%file@"";
   if (!isObject(PGDISFile))
      %Downloader = new HTTPObject(PGDISFile){};
   else
      %Downloader = PGDISFile;
   %Downloader.File = %file;
   echo("Getting");
   %Downloader.get(%server, %filename);
}

function PGDISFile::onLine(%this, %line) {
   if(strStr(%line, "Not") != -1) {
      $PGD::IsFile[%this.File] = 0;
      %this.schedule(1000, disconnect);
      %this.schedule(1500, delete);
   }
   else {
      $PGD::IsFile[%this.File] = 1;
      %this.schedule(1000, disconnect);
      %this.schedule(1500, delete);
   }
}

function PGDISFile::onConnectFailed(%this) {
   error("-- Could not connect to PGD Is File, re-attempt, 30 sec.");
   $PGD::IsFile[%this.File] = 0;
   //
   schedule(30000, 0, "PGD_IsFile", %this.File);
   %this.disconnect();
   %this.delete();
}

function PGDISFile::onDisconnect(%this) { }

//END PGD IS FILE


//PGD Support Functions
function getRandomSeparator(%length)
{
   %alphanumeric = "abcdefghijklmnopqrstuvwxyz0123456789";
   for (%i = 0; %i < %length; %i++)
   {
       %index  = getRandom(0,strLen(%alphanumeric));
       %letter = getSubStr(%alphanumeric, %index, 1);
       %UpperC = getRandom(0, 1);
       if (%UpperC == 1)
          %letter = strUpr(%letter);
       else
          %letter = strLwr(%letter);
       %seq = %seq @ %letter;
   }
   return %seq;
}
//

function GetFileLength(%file) {
   new fileobject(LengthReader);
   LengthReader.openforread(%file);
   %bool = 0;
   while(!%bool) {
      %bool = LengthReader.isEOF();
      %Msg = LengthReader.readLine();
      %message = ""@%message@"\n"@%Msg@"";
   }
   %count = strLen(%message);
   %message = "";
   return %count;
}

function getFileContents(%file) {
   new fileobject(filereader);
   filereader.openforread(%file);
   %bool = 0;
   while(!%bool) {
      %bool = filereader.isEOF();
      %Msgget = filereader.readLine();
      %msg = ""@%msg@""NL""@%Msgget@"";
   }
   return %msg;
}


//End

//Universal Rank Load/Save
function LoadUniversalRank(%client) {
   //A Little PGD Connect Ad.
   if($playingOffline) {
      error("*PGD Connect: In Offline Mode, PGD Services are Inactive");
      %client.checkClientData();
      return 0;
   }
   
   %client.donotupdate = 1;
   if(!%client.IsPGDConnected()) {
      %client.donotupdate = 0;
      messageClient(%client, 'msgPGDRequired', "\c5PGD: PGD Connect account required to load universal ranks.");
      messageClient(%client, 'msgPGDRequired', "\c5PGD: Sign up for PGD Connect today, It's Fast, Easy, and FREE!");
      messageClient(%client, 'msgPGDRequired', "\c5See: www.public.phantomdev.net/SMF/ in the PGD Section");
      messageClient(%client, 'msgPGDRequired', "\c5For more details. \c3[Forums Account Required]\c5.");
      %client.checkClientData();
      return 1;
   }
   //IS FILE
   if(!PGD_IsFile("Data/"@%client.guid@"/Ranks/Powers/Saved.Dat")) {
      %client.donotupdate = 0;
      messageClient(%client, 'msgPGDRequired', "\c5PGD: PGD Connect confirms you do not have a universal rank.");
      %client.checkClientData();
      return 1;
   }
   //Passed, we have a universal rank, time to loady :)
   %server = $PGDServer@":"@$PGDPort;
   if (!isObject(RankGrabber)) {
      %Downloader = new HTTPObject(RankGrabber);
   }
   else {
      %Downloader = RankGrabber;
   }
   //If the server crashes here, let everyone know why
   MessageClient(%client, 'msgAccess', "\c5PGD: Your Universal Rank file will now be downloaded.");
   echo("Client:" SPC %client.namebase SPC "needs universal rank load. It will be stored locally.");
   //Cache Create
   %file = "/public/Univ/Data/"@%client.guid@"/Ranks/Powers/Saved.Dat";

   //Downloader
   $Buffer[%client] = -1;
   %Downloader.client = %client;
   %Downloader.get(%server, %file);
   %Downloader.schedule(15000, "disconnect");
}

function RankGrabber::onLine(%this, %line) {
   if (!StrStr(%line, "404")) {
      messageClient(%this.client, 'MsgSuppressed', "\c5PGD: Your rank does not appear to exist, please contact a developer to fix the issue.");
      %this.cancel = true;
      return;
   }
   else if(getsubstr(%line, 0, 1) $= "<" && !strStr(%line, "</") && !%this.cancel) {
      error("PGD: Supressed HTML tag, check your internet proxy to ensure PGD is not blocked.");
      echo(%line);
      //messageClient(%this.client, 'MsgSuppressed', "\c5PGD: Supressed HTML Tag "@%line@", please contact the host about this.");
      %line = "";
      %this.cancel = true;
      return;
   }
   $Buffer[%this.client, $Buffer[%this.client]++] = %line;
}

function RankGrabber::onConnectFailed(%this) {
   //oh shit :D
   error("-- Could not connect to PGD.");
   MessageClient(%this.client, 'MsgClient', "\c5PGD: Your Rank could not be loaded, the server may be offline or going through maintenance.");
   MessageClient(%this.client, 'MsgClient', "\c5PGD: Your Original rank will be loaded in the meantime.");
   MessageClient(%this.client, 'MsgClient', "\c5PGD: Re-try in 15 seconds.");
   error("Univ Rank Load: fail (connection)");
   for (%i = 1; %i < $Buffer[%this.client]; %i++)
       $Buffer[%this.client, %i] = "";
   $Buffer[%this.client] = -1;

   %this.client.checkClientData();

   %this.client.donotupdate = 0;
   schedule(15000, 0, "LoadUniversalRank", %this.client);
}

function RankGrabber::onDisconnect(%this) {
   %this.client.donotupdate = 0;
   if (%this.cancel) {
      error("Univ Rank load: fail (http_error)");
      MessageClient(%this.client, 'MsgClient', "\c5PGD: Your Universal rank transfer canceled");
      MessageClient(%this.client, 'MsgClient', "\c5There could of been html tags in the request, or it did not exist.");
   }
   else {
       //save the lines in the buffer..
       %fileO = new FileObject();
       %fileO.openForWrite($PowerSave::RanksDirectory@"/"@%this.client.guid@"/Saved.Dat");
       for (%i = 0; %i <= $Buffer[%this.client]; %i++)
       {
           %fileO.writeLine($Buffer[%this.client, %i]);
           $Buffer[%this.client, %i] = "";
       }
       $Buffer[%this.client] = 0;
       %fileO.close();
       %fileO.delete();
       compile($PowerSave::RanksDirectory@"/"@%this.client.guid@"/Saved.Dat");
       %this.client.checkClientData();
       MessageClient(%this.client, 'MsgClient', "\c5PGD: Your Universal rank has been loaded!");
       echo("Univ Rank Load: OK");
   }
   %this.delete();
}

function PrepareUpload(%client) {
   if($playingOffline) {
      error("*PGD Connect: In Offline Mode, PGD Services are Inactive");
      return 0;
   }

   if(!%client.IsPGDConnected()) {
      messageClient(%client, 'msgPGDRequired', "\c5PGD: PGD Connect account required to perform this action.");
   }
   else {
       %connection = Univ_RanksClient;
       if (isObject(%connection)) {
          %connection.disconnect();
          %connection.delete();
       }
       new TCPObject(%connection);
       %connection.client = %client;
       %connection.guid = %client.guid;
       UniversalRankUpload(%client, %connection);
   }
}

function UniversalRankUpload(%client, %connection) {
   //ensure they are permitted connection one final time
   if(!%client.IsPGDConnected()) {
      messageClient(%client, 'msgPGDRequired', "\c5PGD: PGD Connect account required to perform this action.");
      return;
   }
   //passed test, upload the file
   else {
      %file = $PowerSave::RanksDirectory@"/"@%client.guid@"/Saved.Dat";
      %connection.orgfile = %file;
      %connection.file = %file;   //what are we sending?
      %connection.filebase = FileBase(%file) @ ".Dat";
      %connection.client = %client;
      %connection.connect(""@$PGDServer@":"@$PGDPort@"");
   }
}

function Univ_RanksClient::onConnected(%this) {
   %this.schedule(30000, "disconnect");
   %sep = getRandomSeparator(16);
   %filecont = getFileContents(%this.orgfile);
   %loc = $PGDPHPRankUploadHandler;
   %header1 = "POST" SPC %loc SPC "HTTP/1.1\r\n";
   %host = "Host: "@$PGDServer@"\r\n";
   %header2 = "Connection: close\r\nUser-Agent: Tribes 2\r\n";
   %contType = "Content-Type: multipart/form-data; boundary="@%sep@"\r\n";

   %guidReq = "--"@%sep@"\r\nContent-Disposition: form-data; name=\"guid\"\r\n\r\n"@%this.guid;
   %verReq = "--"@%sep@"\r\nContent-Disposition: form-data; name=\"version\"\r\n\r\n2.0";
   %modReq = "--"@%sep@"\r\nContent-Disposition: form-data; name=\"mod\"\r\n\r\nPowers";
   %fileReq = "--"@%sep@"\r\nContent-Disposition: form-data; name=\"uploadedfile\"; filename=\""@%this.filebase@"\"\r\nContent-Type: application/octet-stream";

   %payload = ""@%guidReq@"\r\n"@%modReq@"\r\n"@%verReq@"\r\n"@%filereq@"\r\n"@%filecont@"--"@%sep@"--";

   %qlen = strLen(%payload);
   %contentLeng = "Content-Length: "@%qlen@"\r\n\r\n";
   %query = %header1@
            %host@
            %header2@
            %contType@
            %contentLeng@
            %payload;
   echo("Connected to Phantom Games Server, Sending File Data...");
   %this.send(%query);
}

function Univ_RanksClient::get(%connection, %server, %query) {
   %connection.server = %server;
   %connection.query = %query;
   %connection.connect(%server);
}

//Handle Errors
function Univ_RanksClient::onLine(%this, %line) {
   %ok = false;
   if(strStr(%line, "pgd_ban") != -1) {
      messageClient(%this.client, 'msgPGDRequired', "\c2PGD: You are banned from PGD Connect. \c3"@%line@".");
   }
   if(strStr(%line, "pgd_debug") != -1) {
      if($PGDDebugMode == 1) {
         MessageDevs("\c5PGD DEBUG:" SPC %line);
      }
   }
   switch$ (%line) {
      case "file_upload_ok":
           %ok = true;
           messageClient(%this.client, 'msgdone', "\c5PGD: Your Rank was saved successfully.");
      case "not_registered":
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: PGD Connect account required to perform this action.");
      case "incompatible_version":
           error("PGD: This version is no longer supported by PGD Ranks");
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: This version of Powers is no longer supported.");
      case "invalid_guid":
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: Your GUID had invalid characters in it,"NL
                                                         "try again, when it hasn't been tampered with >_>");
      case "no_guid_input":
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: No GUID was sent to the server. Try again!");
      case "bad_version_input":
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: The version field was not filled in.");
      case "sql_error":
           messageClient(%this.client, 'msgPGDRequired', "\c5PGD: There is a problem with the SQL database on the server!");
      //case "pgd_debug":
      //   if($PGDDebugMode == 1) {
      //      MessageDevs("\c5PGD DEBUG:" SPC %line);
      //   }
      default:
           return;
   }
   if ( %ok )
      echo( "Universal Rank Save: OK" );
   else
      error( "Universal Rank Save: fail (" @ %line @ ")" );
}

function Univ_RanksClient::onDisconnect(%this) {
    %this.delete();
}

function Univ_RanksClient::onConnectFailed(%this) {
   error("-- Could not connect to PGD.");
   messageClient(%this.client, 'MsgClient', "\c5PGD: Your Ranks could not be saved, the server may be offline or going through maintenance.");
   error("Universal Rank Save: fail (connection)");
   %this.delete();
   return;
}
