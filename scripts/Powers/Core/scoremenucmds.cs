//Core/scoremenucmds.cs
//Phantom139

//Handles the F2 Menu
function DefaultGame::updateScoreHud(%game, %client, %tag){
   if (%client.SCMPage $= "") %client.SCMPage = 1;
   if (%client.SCMPage $= "SM") return;
   $TagToUseForScoreMenu = %tag;
   messageClient( %client, 'ClearHud', "", %tag, 0 );
   messageClient( %client, 'SetScoreHudHeader', "", "" );
   if(%client.SelectingSlot) {
      messageClient( %client, 'SetScoreHudHeader', "", "POWERS MOD: RISE OF THE ELEMENTS" );
      messageClient( %client, 'SetScoreHudSubheader', "", "Slot Selection" );
      %i = 1;
      while(%client.slot(%i) != -1) {
         echo("CLIENT: "@%client.namebase@" - SLOT "@%i@": "@%client.slot(%i)@"");
         if(%client.slot(%i).class $= "Undecided") {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tSlotSel\t"@%i@">SLOT "@%i@"</a> - Open Slot");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tSlotSel\t"@%i@">SLOT "@%i@"</a> - Lv."@%client.slot(%i).level@" "@%client.slot(%i).class@"");
            %index++;
         }
         %i++;
      }
   }
   else {
      messageClient( %client, 'SetScoreHudHeader', "", "<a:gamelink\tGTP\t1>Return To Main Menu</a><rmargin:600><just:right><a:gamelink\tNAC\t1>Close</a>" );
      messageClient( %client, 'SetScoreHudSubheader', "", "Main Menu" );
      scoreCmdMainMenu(%game, %client, %tag, %client.SCMPage);
   }
}

function ResetClick(%client) {
   %client.recentClick = 0;
}

function DefaultGame::processGameLink(%game, %client, %arg1, %arg2, %arg3, %arg4, %arg5)  {
   //
   //%client.recentClick = 1;
   //schedule(50, 0, "ResetClick", %client);
   //
   %tag = $TagToUseForScoreMenu;
   messageClient( %client, 'ClearHud', "", %tag, 1 );
   switch$ (%arg1){

        case "ConfirmStartup":
             %client.selectingSlot = 1;
             LoadUniversalRank(%client);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "*Please standby while we prepare your files");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "If you are new, your saved data will be created at this point.");
             %index++;

             %game.schedule(5000, "processGameLink", %client, "SlotSelect");
             CenterPrint(%client, "Visit www.phantomdev.net if you need help! \n\nJoin our forums and get involved in Powers Mod Today!", 3, 3);
             return;
   
        case "GTP":
             scoreCmdMainMenu(%game,%client,$TagToUseForScoreMenu,%arg2);
             %client.SCMPage = %arg2;
             %client.StatView = "";
             return;
             
        case "PGDConnectSave":
             %client.SCMPage = "SM";
             %client.saveData();
             schedule(250, 0, prepareUpload, %client);
             
             messageClient( %client, 'SetLineHud', "", %tag, %index, 'Uploading data to PGD Connect');
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Return To Main Menu</a>');
             %index++;
             return;

        case "PickClass":
             if(%client.recentClick) {
                return;
             }
             %client.recentClick = 1;
             schedule(50, 0, "ResetClick", %client);
             %client.SCMPage = "SM";
             %slot = %client.slotNum;
             %class = %arg2; //What did we select?
             %client.pickingClass = 0;
             %client.slot(%slot).class = %class;

             switch$(%class) {
                case "Witch":
                case "Guardian":
                   %client.slot(%slot).affinity = 1;
                   %client.slot(%slot).level = 36;
                   %client.slot(%slot).exp = 0;
                case "Star Lighter":
                   %client.slot(%slot).affinity = 1;
                   %client.slot(%slot).level = 36;
                   %client.slot(%slot).exp = 0;
                case "Gladiator":
                   %client.slot(%slot).affinity = 2;
                   %client.slot(%slot).level = 71;
                   %client.slot(%slot).exp = 0;
                case "Star Sighter":
                   %client.slot(%slot).affinity = 2;
                   %client.slot(%slot).level = 71;
                   %client.slot(%slot).exp = 0;
                case "Demon":
                case "Enforcer":
                   %client.slot(%slot).affinity = 1;
                   %client.slot(%slot).level = 36;
                   %client.slot(%slot).exp = 0;
                case "Devastator":
                   %client.slot(%slot).affinity = 1;
                   %client.slot(%slot).level = 36;
                   %client.slot(%slot).exp = 0;
                case "Prospector":
                   %client.slot(%slot).affinity = 2;
                   %client.slot(%slot).level = 71;
                   %client.slot(%slot).exp = 0;
                case "Annihilator":
                   %client.slot(%slot).affinity = 2;
                   %client.slot(%slot).level = 71;
                   %client.slot(%slot).exp = 0;
                case "Phantom":
                case "Overseer":
                   %client.slot(%slot).affinity = 1;
                   %client.slot(%slot).level = 36;
                   %client.slot(%slot).exp = 0;
                case "Cryonium":
                   %client.slot(%slot).affinity = 1;
                   %client.slot(%slot).level = 36;
                   %client.slot(%slot).exp = 0;
                case "Phantom Lord":
                   %client.slot(%slot).affinity = 2;
                   %client.slot(%slot).level = 71;
                   %client.slot(%slot).exp = 0;
                case "Deep Freezer":
                   %client.slot(%slot).affinity = 2;
                   %client.slot(%slot).level = 71;
                   %client.slot(%slot).exp = 0;
                case "Hunter":
                case "Nature Walker":
                   %client.slot(%slot).affinity = 1;
                   %client.slot(%slot).level = 36;
                   %client.slot(%slot).exp = 0;
                case "Wispwalker":
                   %client.slot(%slot).affinity = 2;
                   %client.slot(%slot).level = 71;
                   %client.slot(%slot).exp = 0;
             }
             //end
             %client.saveData();
             prepareUpload(%client);
             SetUpClientPowersName(%client);
             if(%client.slot(%slot).affinity >= 1) {
                messageClient(%client, 'MsgClassSlct',"\c5Congratulations, you have successfully promoted to the class of "@%class@"");
             }
             else {
                messageClient(%client, 'MsgClassSlct',"\c5Congratulations, you are now a "@%class@"");
             }
             %game.processGameLink(%client, "GTP");
             return;

             //From the selection screen
        case "PickClassA":
             %client.SCMPage = "SM";
             %class = %arg2; //What did we select?
             %client.pickingClass = 1;
             switch(%class) {
                case 1:
                   ChooseWitch(%client, %tag);
                case 2:
                   ChooseDemon(%client, %tag);
                case 3:
                   ChoosePhantom(%client, %tag);
                //Affinity 1
                case 4:
                   ChooseGuardian(%client, %tag);
                case 5:
                   ChooseStarLighter(%client, %tag);
                case 6:
                   ChooseEnforcer(%client, %tag);
                case 7:
                   ChooseDevastator(%client, %tag);
                case 8:
                   ChooseOverseer(%client, %tag);
                case 9:
                   ChooseCryonium(%client, %tag);
                //Affinity 2
                case 10:
                   ChooseGladiator(%client, %tag);
                case 11:
                   ChooseStarSighter(%client, %tag);
                case 12:
                   ChooseProspector(%client, %tag);
                case 13:
                   ChooseAnnihilator(%client, %tag);
                case 14:
                   ChoosePhantomLord(%client, %tag);
                case 15:
                   ChooseDeepFreezer(%client, %tag);
                //new classes
                case 16:
                   ChooseHunter(%client, %tag);
                   //MessageClient(%client, 'msgAlert', "\c3Powers: This class is not ready for public use yet.");
                   //%game.processGameLink(%client, "GTP");
                case 17:
                   ChooseNatureWalker(%client, %tag);
                case 18:
                   ChooseWispwalker(%client, %tag);
             }
             return;

        case "AffinitySelect":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Affinity Class Selection" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "AFFINITY MODE:");
             
             %slot = %client.SlotNum;
             %class = %client.slot(%slot).class;
             %level = %client.slot(%slot).level;
             if(%level < 35) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>You do not qualify for a class promotion yet. (Return To Menu)</a>");
                %index++;
             }
             else if(%level >= 35 && %level < 70) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Congratulations on reaching level 35");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Are you ready to promote to an Affinity class?");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "");
                %index++;
                switch$(%class) {
                   case "Witch":
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t4>Promote To Guardian?</a>");
                      %index++;
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t5>Promote To Star Lighter?</a>");
                      %index++;
                   case "Demon":
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t6>Promote To Enforcer?</a>");
                      %index++;
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t7>Promote To Devastator?</a>");
                      %index++;
                   case "Phantom":
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t8>Promote To Overseer?</a>");
                      %index++;
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t9>Promote To Cryonium?</a>");
                      %index++;
                   case "Hunter":
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t17>Promote To Nature Walker?</a>");
                      %index++;
                }
                messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>I'm Not Ready Yet</a>");
                %index++;
             }
             else if(%level >= 70) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Congratulations on reaching level 70");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Are you ready to promote to your second Affinity class?");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "");
                %index++;
                switch$(%class) {
                   case "Guardian":
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t10>Guardian -> Gladiator</a>");
                      %index++;
                   case "Star Lighter":
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t11>Star Lighter -> Star Sighter</a>");
                      %index++;
                   case "Enforcer":
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t12>Enforcer -> Prospector</a>");
                      %index++;
                   case "Devastator":
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t13>Devastator -> Annihilator</a>");
                      %index++;
                   case "Overseer":
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t14>Overseer -> Phantom Lord</a>");
                      %index++;
                   case "Cryonium":
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t15>Cryonium -> Deep Freezer</a>");
                      %index++;
                   case "Nature Walker":
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t18>Nature Walker -> Wispwalker</a>");
                      %index++;
                }
             }
             return;

        case "ClassSelect":
             %client.SCMPage = "SM";
             %client.pickingClass = 1;
             messageClient( %client, 'SetScoreHudSubheader', "", "Class Selection" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Welcome To Powers Mod: Rise of The Elements");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "In this mod, you choose a class and use powers with SP");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "First things first, You will pick your class.");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "click a class name from the list below:");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t1>Witch</a>");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t2>Demon</a>");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t3>Phantom</a>");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPickClassA\t16>Hunter</a>"); //
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>I'm Not Ready To Choose Yet</a>");
             %index++;
             return;

        case "PLS":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Player Stats Listings" );
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Select A Player");
             %count=clientgroup.getcount();
             for (%i = 0; %i < %count; %i++){
                %cid = ClientGroup.getObject( %i );
                if(!%cid.isAIControlled()) {
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tRanksSM\t"@%cid@">"@%cid.namebase@"</a>");
                   %index++;
                }
             }
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;

        case "RanksSM":
             %client.StatView = "Yes";
             %index = SendInfoPage(%arg2, %client, %index, %tag);
             return;

        case "PurchasePowers":
             messageClient( %client, 'ClearHud', "", %tag, %index );
             %client.lastStore = "PurchasePowers";
             PowerStore(%client, %tag);
             return;

        case "PurchaseAffinity1Powers":
             messageClient( %client, 'ClearHud', "", %tag, %index );
             %client.lastStore = "PurchaseAffinity1Powers";
             GenerateAffinity1Shop(%client, %tag);
             return;
             
        case "PurchaseAffinity2Powers":
             messageClient( %client, 'ClearHud', "", %tag, %index );
             %client.lastStore = "PurchaseAffinity2Powers";
             GenerateAffinity2Shop(%client, %tag);
             return;

        case "BackToStore":
             schedule(150,0,"PowerStore",%client, %tag);
             return;

             //checks the legality of a purchase
        case "ConfirmPurchase":
             %client.SCMPage = "SM";
             %powerTag = %arg2; //Tag of the power
             %power = %arg3;    //Actual Name
             %cost = %arg4;
             %needLv = %arg5;
             %slot = %client.slotNum;
             //Whoa! Don't but if we already have it
             if(CheckHasPowerByName(%client, %powerTag)) {
                //
                %index = GeneratePowerBuyInfo(%client, %index, %tag, %powerTag);
                //
                messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tBackToStore\t1>Back to Power Store</a>');
                %index++;
                return;
             }
             //whoops we missed it in the store
             if(%client.slot(%slot).spendPoints <= 0) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "You don't have any points to spend.");
                %index++;
                schedule(3000,0,"PowerStore",%client, %tag);
             }
             if(%client.slot(%slot).spendPoints < %cost) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "You don't have enough points to buy this, returning to the store.");
                %index++;
                schedule(3000,0,"PowerStore",%client, %tag);
             }
             else {
                //
                %index = GeneratePowerBuyInfo(%client, %index, %tag, %powerTag);
                //
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Purchase "@%power@" for "@%cost@" points? <a:gamelink\tBuySelected\t"@%powerTag@"\t"@%power@"\t"@%cost@"\t"@%needLv@">Yes</a> - <a:gamelink\tPurchasePowers\t1>No</a>");
                %index++;
             }
             return;

        case "BuySelected":
             if(%client.recentClick) {
                return;
             }
             %client.recentClick = 1;
             schedule(50, 0, "ResetClick", %client);
             %client.SCMPage = "SM";
             %power = %arg2;
             %pwrTag = %arg3;
             %cost = %arg4;
             %needLv = %arg5;
             %slot = %client.slotNum;
             //
             if(%cost > %client.slot(%slot).spendPoints) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Insufficient Points.");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Returning to the store.");
                %index++;
                //
                %game.schedule(1500, "processGameLink", %client, %client.lastStore);
                return;
             }
             //
             %client.slot(%slot).spendPoints -= %cost;
             BuyPower(%client, %power, %needLv);
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Purchased "@%pwrTag@".");
             %index++;
             messageClient( %client, 'SetLineHud', "", %tag, %index, "Returning to the store.");
             %index++;
             %game.schedule(1500, "processGameLink", %client, %client.lastStore);
             return;
             
        case "SlotSelect":
             %index = %client.BuildSlotSelect(%tag, %index);
             return;
             
        case "SlotSel":
             %slot = %arg2;
             //
             %index = %client.SelectAccount(%tag, %index, %slot);
             
             PrepareUpload(%client);
             return;

        case "YourPage":
             %client.SCMPage = "SM";
             %slot = %client.slotNum;
             messageClient( %client, 'SetScoreHudSubheader', "", "My Page" );
             //class selection
             if(%client.slot(%slot).class $= "Undecided") {
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "Welcome to Powers Mod, Ready to pick your class?");
                   %index++;
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tClassSelect\t1>Yes</a>");
                   %index++;
                   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Not Yet</a>');
                   %index++;
                   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tSlotSelect\t1>I clicked the wrong slot</a>');
                   %index++;
             }
             else {
                %level = %client.slot(%slot).level;
                %class = %client.slot(%slot).class;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "SLOT: "@%slot@": Level "@%level@" "@%class@"");
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Class Options:");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tPurchasePowers\t1>Purchase Abilities</a>');
                %index++;
                if(%level >= 35) {
                   if(%client.slot(%slot).affinity == 1) {
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPurchaseAffinity1Powers\t1>Purchase Affinity Abilities (Lv 35 - 70)</a>");
                      %index++;
                      if(%level >= 70) {
                         messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tAffinitySelect\t1>Affinity Mode: Promote To A Third Class.</a>");
                         %index++;
                      }
                      else {
                         messageClient( %client, 'SetLineHud', "", %tag, %index, "Locked: Attain Level 70 to Unlock.");
                         %index++;
                      }
                   }
                   else if(%client.slot(%slot).affinity == 2) {
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPurchaseAffinity1Powers\t1>Purchase Affinity Abilities (Lv 35 - 70)</a>");
                      %index++;
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPurchaseAffinity2Powers\t1>Purchase Affinity Abilities (Lv 70+)</a>");
                      %index++;
                      if(%level < 100) {
                         messageClient( %client, 'SetLineHud', "", %tag, %index, "Locked: Attain Level 100 to Unlock.");
                         %index++;
                      }
                      else {
                         messageClient( %client, 'SetLineHud', "", %tag, %index, "**********");
                         %index++;
                         messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPrestige\t1>Prestige: Level & Experience Reset</a>");
                         %index++;
                         messageClient( %client, 'SetLineHud', "", %tag, %index, "**********");
                         %index++;
                      }
                   }
                   else {
                      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tAffinitySelect\t1>Affinity Mode: Promote To A Second Class.</a>");
                      %index++;
                   }
                }
                else {
                   messageClient( %client, 'SetLineHud', "", %tag, %index, "Locked: Attain Level 35 to Unlock.");
                   %index++;
                }
                messageClient( %client, 'SetLineHud', "", %tag, %index, "");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Slot Options:");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tSlotSelect\t1>Select A Different Slot</a>");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tRanksSM\t"@%client@">Check My Stats Card</a>");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
                %index++;
             }
             return;
             
        case "Prestige":
             %client.SCMPage = "SM";
             %slot = %client.slotNum;
             messageClient( %client, 'SetScoreHudSubheader', "", "My Page" );
             %level = %client.slot(%slot).level;
             %class = %client.slot(%slot).class;
             if(%level < 100) {
                messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Cannot Access This Feature</a>');
                %index++;
                return;
             }
             else {
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Prestige Mode: So, you've reached 100... now what?");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "While I admit, tormenting n0bs is hilarious, it does get boring.");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Why not add insult to injury?\n");
                %index += 2;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Prestige Resets you at Level 1, 0 EXP. However, your skill points earned in leveling are");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "refunded allowing you to expand even further your destructive arsenal of powers.");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "Your title will remain Green to indicate your prestigeness.");
                %index++;
                messageClient( %client, 'SetLineHud', "", %tag, %index, "<Color:990000>This feature is currently in development.");
                %index++;
             }
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             return;
             
        default:
   }
   closeScoreHudFSERV(%client);
}

function closeScoreHudFSERV(%client) {
   serverCmdHideHud(%client, 'scoreScreen');
   commandToClient(%client, 'setHudMode', 'Standard', "", 0);
   %client.SCMPage = 1;
   %client.StatView = "";
   if(%client.SelectingSlot) {
      schedule(100,0,"serverCmdShowHud", %client, 'scoreScreen');
      Game.schedule(150, "processGameLink", %client, "SlotSelect");
      return;
   }
}

function scoreCmdMainMenu(%game,%client,%tag,%page) {
messageClient( %client, 'ClearHud', "", %tag, 1 );
if (!isobject(cmdobject)) generateCMDObj();
   messageClient( %client, 'SetScoreHudSubheader', "", "Main Menu Page " @ %page);
if (%page > 1) {
   %pgToGo = %page - 1;
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t%1>Previous Page</a>',%pgToGo);
   %index++;
   }
%cmdsToDisp = 15 * %page;
%start = (%page - 1) * 15;
for (%i=%start; %i < %cmdsToDisp;%i++) {
    %line = CmdObject.cmd[%i];
    if (%line !$= "") {
       messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\t%1>%2</a>',getword(%line,0),getwords(%line,1));
       %index++;
    }
}
if (%cmdsToDisp < (CmdObject.commands + 1)) {
   %pgToGo = %page + 1;
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t%1>Next Page</a>',%pgToGo);
   %index++;
   }
if (%page > 1) {
   messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>First Page</a>");
   %index++;
   }
messageClient( %client, 'ClearHud', "", %tag, %index );
}


//format
//CMD indentifier displayname
//CMDHELP identifier help message for cmd gonna implement it
//after noobs get their hands on the base script first

function GenerateCMDObj() {
new fileobject("fIn");
fIn.openforread("scripts/Powers/Core/cmddisplaylist.txt");
if (isobject(cmdobject)) cmdobject.delete();
   new scriptObject("CmdObject") {commands=0;};
while (!fIn.iseof()) {
      %line = fIn.readline();
      if (getword(%line,0) $= "CMD") {
         CmdObject.cmd[CmdObject.commands] = getwords(%line,1);
         CmdObject.commands++;
      }
}

fIn.close();
fIn.delete();
}
