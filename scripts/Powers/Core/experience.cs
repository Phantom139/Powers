function doEXPAward(%client, %killed) {
   //
   %levelUp = 0;
   %ks = %client.slotnum;
   //
   if(%killed $= "") {
      //echo("PLUC");
      //post level up request, kind of redundant to the below code, but it works
      %myLevel = %client.slot(%ks).level;
      if(%myLevel <= 35) {
         %nextLevel = $PowerSave::MinEXP[%client.slot(%ks).level];
         if(%nextLevel > 0) {
            //validation-affinity
            if(%client.slot(%ks).exp >= %nextLevel) {
               //level up!
               if(isSet($PowerSave::LevelMessage[%myLevel])) {
                  messageClient(%client, 'msgNote', "\c5POWERS: "@$PowerSave::LevelMessage[%myLevel]);
               }
               %client.slot(%ks).level++;
               %client.slot(%ks).spendPoints += $PowerSave::PointGain[%client.slot(%ks).level - 1];
               messageAll('msgClient',"\c2"@getTaggedString(%client.name)@" Has Leveled Up To Level "@%myLevel+1@" with "@%client.slot(%ks).exp@"EXP Points!");
               bottomPrint(%client, "Congratulations "@getTaggedString(%client.name)@" you have Leveled up to Level "@%myLevel+1@"!", 5, 2 );
               messageClient(%client, 'msgSnd', "\c3Melvin: You have earned "@$PowerSave::PointGain[%client.slot(%ks).level - 1]@" points and now have "@%client.slot(%ks).spendPoints@"~wfx/misc/MA3.wav");

               SetUpClientPowersName(%client);
               %levelUp = 1;
               doEXPAward(%client, "");
            }
         }
      }
      else if(%myLevel > 35 && %myLevel <= 70) {
         %nextLevel = $PowerSave::Affinity1MinEXP[%myLevel - 35];
         if(%nextLevel > 0) {
            //validation-affinity
            if(%client.slot(%ks).exp >= %nextLevel) {
               //level up!
               if(isSet($PowerSave::Affinity1LevelMessage[%myLevel - 35])) {
                  messageClient(%client, 'msgNote', "\c5POWERS: "@$PowerSave::Affinity1LevelMessage[%myLevel - 35]);
               }
               %client.slot(%ks).level++;
               %client.slot(%ks).spendPoints += $PowerSave::Affinity1PointGain[%myLevel - 36];
               messageAll('msgClient',"\c2"@getTaggedString(%client.name)@" Has Leveled Up To Level "@%myLevel+1@" with "@%client.slot(%ks).exp@"EXP Points!");
               bottomPrint(%client, "Congratulations "@getTaggedString(%client.name)@" you have Leveled up to Level "@%myLevel+1@"!", 5, 2 );
               messageClient(%client, 'msgSnd', "\c3Melvin: You have earned "@$PowerSave::Affinity1PointGain[%client.slot(%ks).level - 36]@" points and now have "@%client.slot(%ks).spendPoints@"~wfx/misc/MA3.wav");

               SetUpClientPowersName(%client);
               %levelUp = 1;
               doEXPAward(%client, "");
            }
         }
      }
      else {
         %nextLevel = $PowerSave::Affinity2MinEXP[%myLevel - 70];
         if(%nextLevel > 0) {
            //validation-affinity
            if(%client.slot(%ks).exp >= %nextLevel) {
               //level up!
               if(isSet($PowerSave::Affinity2LevelMessage[%myLevel - 70])) {
                  messageClient(%client, 'msgNote', "\c5POWERS: "@$PowerSave::Affinity2LevelMessage[%myLevel - 70]);
               }
               %client.slot(%ks).level++;
               %client.slot(%ks).spendPoints += $PowerSave::Affinity2PointGain[%client.slot(%ks).level - 71];
               messageAll('msgClient',"\c2"@getTaggedString(%client.name)@" Has Leveled Up To Level "@%myLevel+1@" with "@%client.slot(%ks).exp@"EXP Points!");
               bottomPrint(%client, "Congratulations "@getTaggedString(%client.name)@" you have Leveled up to Level "@%myLevel+1@"!", 5, 2 );
               messageClient(%client, 'msgSnd', "\c3Melvin: You have earned "@$PowerSave::Affinity2PointGain[%client.slot(%ks).level - 71]@" points and now have "@%client.slot(%ks).spendPoints@"~wfx/misc/MA3.wav");

               SetUpClientPowersName(%client);
               %levelUp = 1;
               doEXPAward(%client, "");
            }
         }
      }
      return;
   }
   //the NEW XP algorithm
   //Adding XP = Floor((1/7)Killed Level)
   //Affinity?
   %vs = %killed.slotnum;

   if(%killed.isAiControlled()) {
      %info = $AIInfo[%killed];
      %level = getWord(%info, 1);
      
      //echo("AI DEATH: "@%info);
   }
   else {
      %level = %killed.slot(%vs).level;
   }
   if(%level <= 0) {
      %level = 1;
   }

   if(!%client.slot(%ks).affinity) {
      //echo("no affny");
      %toAdd = MCeil((1/7) * %level) * $Powers::EXPGainRate;
      %client.slot(%ks).exp += %toAdd;
      messageClient(%client, 'msgClient', "\c5POWERS: Enemy Player Killed: \c3+"@%toAdd@"EXP.");
   }
   else {
      %toAdd = MCeil((1/7) * %level) * $Powers::EXPGainRate;
      %toAdd2 = MCeil(%toAdd/(%client.slot(%ks).affinity + 1));
      %client.slot(%ks).exp += %toAdd2;
      messageClient(%client, 'msgClient', "\c5POWERS: Enemy Player Killed: \c3+"@%toAdd2@"EXP.");
   }

   //test for level up
   %myLevel = %client.slot(%ks).level;
   if(%myLevel <= 35) {
      %nextLevel = $PowerSave::MinEXP[%client.slot(%ks).level];
      if(%nextLevel > 0) {
         //validation-affinity
         if(%client.slot(%ks).exp >= %nextLevel) {
            //level up!
            if(isSet($PowerSave::LevelMessage[%myLevel])) {
               messageClient(%client, 'msgNote', "\c5POWERS: "@$PowerSave::LevelMessage[%myLevel]);
            }
            %client.slot(%ks).level++;
            %client.slot(%ks).spendPoints += $PowerSave::PointGain[%client.slot(%ks).level - 1];
            messageAll('msgClient',"\c2"@getTaggedString(%client.name)@" Has Leveled Up To Level "@%myLevel+1@" with "@%client.slot(%ks).exp@"EXP Points!");
            bottomPrint(%client, "Congratulations "@getTaggedString(%client.name)@" you have Leveled up to Level "@%myLevel+1@"!", 5, 2 );
            messageClient(%client, 'msgSnd', "\c3Melvin: You have earned "@$PowerSave::PointGain[%client.slot(%ks).level - 1]@" points and now have "@%client.slot(%ks).spendPoints@"~wfx/misc/MA3.wav");
            
            SetUpClientPowersName(%client);
            %levelUp = 1;
            doEXPAward(%client, "");
         }
      }
   }
   else if(%myLevel > 35 && %myLevel <= 70) {
      %nextLevel = $PowerSave::Affinity1MinEXP[%myLevel - 35];
      if(%nextLevel > 0) {
         //validation-affinity
         if(%client.slot(%ks).exp >= %nextLevel) {
            //level up!
            if(isSet($PowerSave::Affinity1LevelMessage[%myLevel - 35])) {
               messageClient(%client, 'msgNote', "\c5POWERS: "@$PowerSave::Affinity1LevelMessage[%myLevel - 35]);
            }
            %client.slot(%ks).level++;
            %client.slot(%ks).spendPoints += $PowerSave::Affinity1PointGain[%client.slot(%ks).level - 36];
            messageAll('msgClient',"\c2"@getTaggedString(%client.name)@" Has Leveled Up To Level "@%myLevel+1@" with "@%client.slot(%ks).exp@"EXP Points!");
            bottomPrint(%client, "Congratulations "@getTaggedString(%client.name)@" you have Leveled up to Level "@%myLevel+1@"!", 5, 2 );
            messageClient(%client, 'msgSnd', "\c3Melvin: You have earned "@$PowerSave::Affinity1PointGain[%client.slot(%ks).level - 36]@" points and now have "@%client.slot(%ks).spendPoints@"~wfx/misc/MA3.wav");

            SetUpClientPowersName(%client);
            %levelUp = 1;
            doEXPAward(%client, "");
         }
      }
   }
   else {
      %nextLevel = $PowerSave::Affinity2MinEXP[%myLevel - 70];
      if(%nextLevel > 0) {
         //validation-affinity
         if(%client.slot(%ks).exp >= %nextLevel) {
            //level up!
            if(isSet($PowerSave::Affinity2LevelMessage[%myLevel - 70])) {
               messageClient(%client, 'msgNote', "\c5POWERS: "@$PowerSave::Affinity2LevelMessage[%myLevel - 70]);
            }
            %client.slot(%ks).level++;
            %client.slot(%ks).spendPoints += $PowerSave::Affinity2PointGain[%client.slot(%ks).level - 71];
            messageAll('msgClient',"\c2"@getTaggedString(%client.name)@" Has Leveled Up To Level "@%myLevel+1@" with "@%client.slot(%ks).exp@"EXP Points!");
            bottomPrint(%client, "Congratulations "@getTaggedString(%client.name)@" you have Leveled up to Level "@%myLevel+1@"!", 5, 2 );
            messageClient(%client, 'msgSnd', "\c3Melvin: You have earned "@$PowerSave::Affinity2PointGain[%client.slot(%ks).level - 71]@" points and now have "@%client.slot(%ks).spendPoints@"~wfx/misc/MA3.wav");

            
            SetUpClientPowersName(%client);
            %levelUp = 1;
            doEXPAward(%client, "");
         }
      }
   }

   //save
   %client.saveData();
   if(%levelUp) {
      prepareUpload(%client);
   }
}
