//Classes/ClassSelection.cs
//Phantom139

//Builds the F2 Menu Dialogs for Class Bio/Selection


//Class selection
function GameConnection::BuildSlotSelect(%client, %tag, %index) {
   %client.SCMPage = "SM";
   //save the client file, if we can
   %client.saveData();
   //build the menu
   messageClient( %client, 'SetScoreHudSubheader', "", "Choose Your Account To Use");
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
   messageClient( %client, 'SetLineHud', "", %tag, %index, "");
   %index++;
   if(!%client.SelectingSlot) {
      messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Exit</a>');
      %index++;
   }
   return %index;
}

function GameConnection::SelectAccount(%client, %tag, %index, %slot) {
   %client.SlotNum = %slot;
   if(isObject(%client.player)) {
      %client.player.clearInventory();
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

//Class BIOS:
function ChooseWitch(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Witch Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:FFE303> = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Commonly Best At Levels:<color:FFE303> Affinity Classes (35+)");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Witches are good defenders. They have healing powers along with a");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "few powers that allow them to destroy attackers. They do however lack");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Strong Offensive powers, except for high level explosion powers, if you're");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "looking for a challenging way to the top, this class is perfect for you.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Vaporize, To The Heavens, Team Healer, Extreme Shielding.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Affinity Classes: Guardian, Star Lighter.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "This Class Is Good For Players Of The <color:FFE303>INTERMEDIATE LEVEL");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Do You Want To Be A Witch? <a:gamelink\tPickClass\tWitch>Yes</a> - <a:gamelink\tClassSelect\t1>No</a>");
   %index++;
}

function ChooseDemon(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Demon Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:CD0000> = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Commonly Best At Levels:<color:003300> 1 - 35");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Demons are soldiers. They have a load of offensive powers that can plow");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "through enemy lines. Demons are great at offense, but suck at defensive");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "tactics, they gain explosive powers at the higher levels, and can do a");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "variety of evil things to enemy stats (Energy, Movement, Ect.).");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Lightning, Fire Ball, Energy Ball, Energy God.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Affinity Classes: Enforcer, Devastator.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "This Class Is Good For Players Of The <color:003300>BEGINNER LEVEL");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Do You Want To Be A Demon? <a:gamelink\tPickClass\tDemon>Yes</a> - <a:gamelink\tClassSelect\t1>No</a>");
   %index++;
}

function ChoosePhantom(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Phantom Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:FFE303> = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Commonly Best At Levels:<color:CD0000> 1 - 10, Second Affinity Class(70+)");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Phantoms are a mysterious group that rely on the shadows for their power.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Phantoms possess incredible bombardment and stunning powers but lack proper");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "defense powers, Their explosive abilities combined with bombardment makes for");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "excellent long range bombardment and detonation of enemies you run in to.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Their powers may be extremely strong, but the energy consumption is insane.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Shadow Storm, Avalanche, Shadow Brigade, Nightmare.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Affinity Classes: Overseer, Cryonic Embassador.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "This Class Is Good For Players Of The <color:CD0000>EXPERT LEVEL");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Do You Want To Be A Phantom? <a:gamelink\tPickClass\tPhantom>Yes</a> - <a:gamelink\tClassSelect\t1>No</a>");
   %index++;
}


//Phantom139: Added New Class
function ChooseHunter(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Hunter Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Commonly Best At Levels:<color:CD0000> First Affinity and Above (35+)");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Hunters live among nature, a mysterious cult that has only recently");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "come into this conflict. They seek to grow their connection with nature");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "and the spirits of nature through their mysterious summoning powers.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Spear Shower, Elemental Traps, Strike Down.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Affinity Class: Nature Walker.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "This Class Is Good For Players Of The <color:CD0000>EXPERT LEVEL");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Do You Want To Be A Hunter? <a:gamelink\tPickClass\tHunter>Yes</a> - <a:gamelink\tClassSelect\t1>No</a>");
   %index++;
}


// Affinity UNO

//Witch ->
function ChooseGuardian(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Guardian Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:CD0000> = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Guardians expand additional defensive powers and opens up some new.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "offensive capabilities. Their weapon of choice is the Pike Of Faith.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Guardians are usually in high demand in team battles due to their brand new");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Set of team stat boosting powers.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Energy Boost, Degaussian Strike, Damage Reducer");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promotes to Gladiator at Level 70.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promote to Guardian? <a:gamelink\tPickClass\tGuardian>Yes</a> - <a:gamelink\tAffinitySelect\t1>No</a>");
   %index++;
}

function ChooseStarLighter(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Star Lighter Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:CD0000> = ");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "When witches get tired of being overpowered, they choose the");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Star Lighter Affinity class. This class unleashes devastating offensive");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "capabilities that leave foes begging for mercy, they are armed with the");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Harbinger of War weapon that boasts explosive, bombardment, and offense");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Shard Storm, Catalyst Burst, Envious Downpour.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promotes to Star Sighter at Level 70.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promote to Star Lighter? <a:gamelink\tPickClass\tStar Lighter>Yes</a> - <a:gamelink\tAffinitySelect\t1>No</a>");
   %index++;
}

//Demon ->
function ChooseEnforcer(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Enforcer Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:CD0000> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Enforcers boast extreme firepower with spells that swipe off little energy.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "They acomplish this feat with a Staff Of Embrithia weapon that they use");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "The Enforcer class learns unique abilities to expand their offensive and now");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "their bombardment attacks.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Hellstorm, Deep Flame Shot, Fireball Lv 5-6.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promotes to Prospector at Level 70.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promote to Enforcer? <a:gamelink\tPickClass\tEnforcer>Yes</a> - <a:gamelink\tAffinitySelect\t1>No</a>");
   %index++;
}

function ChooseDevastator(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Devastator Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:CD0000> = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:CD0000> = ");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Devastators expand the explosive capabilities of the demon class.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Armed with a Asthenie Sword, the Devastator class can apply the");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "burn effect to enemies to slowly kill them as well as adding deadly");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "explosion powers, this class is not to be messed with.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Crisp, Desolation, Sun Ray.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promotes to Annihilator at Level 70.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promote to Devastator? <a:gamelink\tPickClass\tDevastator>Yes</a> - <a:gamelink\tAffinitySelect\t1>No</a>");
   %index++;
}

//Phantom ->
function ChooseOverseer(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Overseer Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:CD0000> = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Overseers apply the knowledge of light arts and swordsmanship for advantages.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Overseers learn to combine quick and swift attacks with a deadly sword weapon");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "They also gain a set of defensive powers for advanced protection and posseses");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "an insanely powerful bombardment attack.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Marvolic Lightning, Asteroid Fall, Shadow Heal.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promotes to Phantom Lord at Level 70.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promote to Overseer? <a:gamelink\tPickClass\tOverseer>Yes</a> - <a:gamelink\tAffinitySelect\t1>No</a>");
   %index++;
}

function ChooseCryonium(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Cryonium Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:CD0000> = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Cryonium's expand their knowledge of freezing spells at a quick pace.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Using a frozen spear, they learn a whole new set of evil and deadly powers");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "some of these powers actual deal more damage to frozen enemies making this");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "class an insane threat to it's enemies.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Blizzard, Deep Freeze, Crystal Shock.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promotes to Deep Freezer at Level 70.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promote to Cryonium? <a:gamelink\tPickClass\tCryonium>Yes</a> - <a:gamelink\tAffinitySelect\t1>No</a>");
   %index++;
}

//New Classes
function ChooseNatureWalker(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Nature Walker Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:CD0000> = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "With a full connection to the spiritual world, the nature walker can");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "unleash a hellstorm of destructive powers by summoning elemental and");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "spiritual beings to the battlefield. This class is feared by those who");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "do not understand it's true power.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Falcon Strike, Rising Flight, Tiger Lash");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promotes to Wispwalker at Level 70.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promote to Nature Walker? <a:gamelink\tPickClass\tNature Walker>Yes</a> - <a:gamelink\tAffinitySelect\t1>No</a>");
   %index++;
}










// Affinity Dos

//Witch ->
function ChooseGladiator(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Gladiator Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Gladiators expand the defensive capabilities of the guardians");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "They also gain some new explosive powers to rain death on foes");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Gladiators are always in team battle demand lists.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Chain Burst, Team Unlimited Energy, Health Boost");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Ultimate Power: <color:ff0000>Rise of The Champion");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promote to Gladiator? <a:gamelink\tPickClass\tGladiator>Yes</a> - <a:gamelink\tAffinitySelect\t1>No</a>");
   %index++;
}

function ChooseStarSighter(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Star Sighter Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:CD0000> = ");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Learning the bombardment abilities from phantoms, the Star Sighter");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "class boasts destructive explosive powers and bombardment spells that");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "can even match the powerful phantom lords.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Catalyst Explosive, Shard Rain, Swift Vengeance.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Ultimate Power: <color:ff0000>Rift");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promote to Star Sighter? <a:gamelink\tPickClass\tStar Sighter>Yes</a> - <a:gamelink\tAffinitySelect\t1>No</a>");
   %index++;
}

//Demon ->
function ChooseProspector(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Prospector Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:CD0000> = ");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Prospectors can utter the sigh of defensive relief, they learn.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "powers to heal themselves when injured, while not losing out on their");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "powerful explosive powers earned from the enforcer class.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Hell's Fury, Flamethrower, Dark Heal.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Ultimate Power: <color:ff0000>Spirit Of Power");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promote to Prospector? <a:gamelink\tPickClass\tProspector>Yes</a> - <a:gamelink\tAffinitySelect\t1>No</a>");
   %index++;
}

function ChooseAnnihilator(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Annihilator Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:CD0000> = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:CD0000> = ");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "When it MUST be destroyed, the team sends in the Annihilator.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "their furious burn and destruction spells makes them the ultimate");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "war machine that is almost unstoppable.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Sun Storm, Energy Ball Lv. 4-5, Unbearable Firestorm.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Ultimate Power: <color:ff0000>Dragon's Revenge");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promote to Annihilator? <a:gamelink\tPickClass\tAnnihilator>Yes</a> - <a:gamelink\tAffinitySelect\t1>No</a>");
   %index++;
}

//Phantom ->
function ChoosePhantomLord(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Phantom Lord Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "The reason for the existance of withches and demons, the Phantom Lord");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "has earned the full mastery of the shadow arts and can perform spells");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "that many would call, 'Unnatural'. The Phantom Lord has the most powerful");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "powers known to man, and has no problem using them on others.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Chain Lightning, Tornadic Uprising, Dematerialize.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Ultimate Power: <color:ff0000>Phantom Storm");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promote to Phantom Lord? <a:gamelink\tPickClass\tPhantom Lord>Yes</a> - <a:gamelink\tAffinitySelect\t1>No</a>");
   %index++;
}

function ChooseDeepFreezer(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Deep Freezer Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:CD0000> = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Believed to be the cause of earth's ice ages, the deep freezer can");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "unleash the most dark of icy powers, that they can even white out the map in");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "a snap of their fingers, all who challenge a deep freezer will either freeze");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "to death, or die trying to escape a unrelenting snowstorm.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Whiteout, Seeking Freeze, Crystal Blast.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Ultimate Power: <color:ff0000>Snowstorm");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promote to Deep Freezer? <a:gamelink\tPickClass\tDeep Freezer>Yes</a> - <a:gamelink\tAffinitySelect\t1>No</a>");
   %index++;
}

//New New... and uh.... new!
function ChooseWispwalker(%client, %tag) {
   %client.SCMPage = "SM";
   serverCmdShowHud(%client, 'scoreScreen');
   messageClient( %client, 'SetScoreHudSubheader', "", "The Wispwalker Class" );
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stats for this Class:");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Defense:<color:CD0000> =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Offense:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Bombardment:<color:CD0000> = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Stun:<color:CD0000> = ");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Explosion:<color:003300> = = = = =");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "A blissful and frightening figure, is the wispwalker. Learning the traces");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "of the world, and the world beyond is the motive of this class, with a");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "spiritual connection like none other. The wispwalker can summon creatures");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "capable of nothing but pure annihilation");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Main Powers: Kiss of the Dragon, Tidal Surge, Saber Wind.");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Ultimate Power: <color:ff0000>Wispstorm");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "Promote to Wispwalker? <a:gamelink\tPickClass\tWispwalker>Yes</a> - <a:gamelink\tAffinitySelect\t1>No</a>");
   %index++;
}
