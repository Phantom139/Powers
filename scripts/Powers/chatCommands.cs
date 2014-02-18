//chatCommands.cs
//Phantom139

//Allows usage of chat commands via /command arguments

function chatcommands(%sender, %message) {
   %cmd=getWord(%message,0);
   %cmd=stripChars(%cmd,"/");
   %count=getWordCount(%message);
   %args=getwords(%message,1);
   %cmd="cc" @ %cmd;
   call(%cmd,%sender,%args);
}

function plguidtocid(%guid) {
    %count = ClientGroup.getCount(); //counts total clients
    for(%i = 0; %i < %count; %i++) {
       %obj = ClientGroup.getObject(%i);  //gets the clientid based on the ordering hes in on the list
       %guidtest = %obj.guid;  //pointless step but i didnt feel like removing it....
       if(strstr(%guidtest, %guid) != -1)  //is all of name test used in name
           return %obj;  //if so return the clientid and stop the function
    }
    return 0;  //if none fits return 0 and end function
}


function plnametocid(%name)     //this function cut down a lot of work..thnx earthworm.
{
    %count = ClientGroup.getCount(); //counts total clients
    for(%i = 0; %i < %count; %i++)  //loops till all clients are accounted for
        {
        %obj = ClientGroup.getObject(%i);  //gets the clientid based on the ordering hes in on the list
        %nametest=%obj.namebase;  //pointless step but i didnt feel like removing it....
        %nametest=strlwr(%nametest);  //make name lowercase
        %name=strlwr(%name);  //same as above, for the other name
        if(strstr(%nametest,%name) != -1)  //is all of name test used in name
            return %obj;  //if so return the clientid and stop the function
    }
    return 0;  //if none fits return 0 and end function
}

function ccHelp(%sender,%args) {
   messageclient(%sender, 'MsgClient', "\c2Welcome To Powers Mod!");
   messageClient(%sender, 'MsgClient', "\c3/resetScoreHud - Refresh the Score Hud");
}

function ccresetScoreHud(%sender, %args) {
   scoreCmdMainMenu(Game, %sender, $TagToUseForScoreMenu, 1);
}
