//LoadScreen.cs
//Phantom139

//Builds and sends the debrief load screen

package loadmodinfo {
   function sendLoadInfoToClient( %client ) {
      Parent::sendLoadInfoToClient(%client);
	  schedule(1000, 0, "sendLoadscreen", %client);
   }

   function sendLoadscreen(%client){
	  messageClient( %client, 'MsgGameOver', "");
      messageClient( %client, 'MsgClearDebrief', "" );

      messageClient(%client, 'MsgDebriefResult', "", "<Font:Arial Bold:18><Just:CENTER>"@$Host::GameName);
      messageClient(%client, 'MsgDebriefResult', "", "<Font:Arial Bold:14><Just:CENTER>Powers Mod: Rise of the Elements");

      %Credits = "\n<Font:Arial:16>Developer: Phantom139" @
                 "\nCo-Developers: None yet, Apply Today" @
                 "\nMod Version: "@$PowerSave::Version@"";

      // this callback adds content to the bulk of the gui
      messageClient(%client, 'MsgDebriefAddLine', "", %Credits);

      %Intro = "\n<Font:Arial:14>What is Powers Mod?" @
               "\n<Font:Arial:12>What are you? A Witch, A Demon, A Phantom?"@
               "\nYou decide! Select a class and battle using many powers"@
               "\nAdvance in levels to unlock even more powers for alot of fun!";

      messageClient(%client, 'MsgDebriefAddLine', "", %Intro);
      
      %Intro2 = "\n<Font:Arial:14>Powers Mod: Rise of the Elements" @
               "\nThe New Powers Mod Major Release is Here!" @
               "\n<Font:Arial:12>Led astray from their natural home"@
               "\nThe great hunter rises to reclaim what is rightfully his."@
               "\nCommand this new dangerous class now."@
               "\nAnd can you control the ultimate powers of the dangerous"@
               "\nSecond affinity classes?";

      messageClient(%client, 'MsgDebriefAddLine', "", %Intro2);
    
      %TC2 =   "\n" @
               "\nWE SUPPORT TRIBES NEXT! Thanks to Thyth/Krash123" @
               "\nFor dedicating your time to bring our favorite game back to life!"@
               "\nhttp://www.tribesnext.com";
      messageClient(%client, 'MsgDebriefAddLine', "", %TC2);

      %PGD =   "\n" @
               "\nNeed help understanding a part of this mod?" @
               "\nJoin the Phantom Games Development Community at:"@
               "\nhttp://www.phantomdev.net";
      messageClient(%client, 'MsgDebriefAddLine', "", %PGD);

      %BasicGame =   "\n" @
                     "\nPowers mod uses the F2 Menu to function" @
                     "\nYou can also open the F2 menu with your"@
                     "\ninventory hud key.";
      messageClient(%client, 'MsgDebriefAddLine', "", %BasicGame);
   }
};
activatepackage(loadmodinfo);
