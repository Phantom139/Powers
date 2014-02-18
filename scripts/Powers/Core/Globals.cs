//Globals.cs
//Phantom139

//Holds all the mod's Global Variables

//Do not touch these unless you ABSOLUTELY know what you're doing, you may break something...

$PowerSave::Version = "2.2";
$PowerSave::VersionNumeric = 2.2;

$Power::LevelReq["Low"] = 0;
$Power::LevelReq["Mid"] = 15;
$Power::LevelReq["High"] = 27;
$Power::LevelReq["Affinity"] = 36;
$Power::LevelReq["Affinity2"] = 71;

$TribesDamageToNumericDamage = 100.0; //RPG MOD

$Powers::MaxClientSaveSlots = 10;

$host::BotsCannotSpell = 1; // 0 to enable madness

$ScoreHudMaxVisible = 16; //maybe 16 for low end people?

$PowerSave::RanksDirectory = "Server/ClientSave";
// This is where all of the ranks will be saved
// I highly recommend leaving this alone

$AllObjMask = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::ItemObjectType | $TypeMasks::StationObjectType | $TypeMasks::GeneratorObjectType | $TypeMasks::SensorObjectType | $TypeMasks::TurretObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::StaticObjectType | $TypeMasks::MoveableObjectType | $TypeMasks::DamagableItemObjectType | $TypeMasks::StaticShapeObjectType;
$ArtilleryMask = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::StaticObjectType | $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::StationObjectType | $TypeMasks::GeneratorObjectType | $TypeMasks::SensorObjectType | $TypeMasks::TurretObjectType;


$Powers::EXPGainRate = 2.0; // default is 1.

//powers
$Power::PowerCount["Witch"] = 17;
$Power::PowerCount["Demon"] = 16;
$Power::PowerCount["Phantom"] = 16;
$Power::PowerCount["Hunter"] = 16;
//Affinity 1
$Power::PowerCount["Guardian"] = 6;
$Power::PowerCount["StarLighter"] = 6;
$Power::PowerCount["Enforcer"] = 6;
$Power::PowerCount["Devastator"] = 6;
$Power::PowerCount["Cryonium"] = 6;
$Power::PowerCount["Overseer"] = 6;
$Power::PowerCount["NatureWalker"] = 6;
//Affinity 2
$Power::PowerCount["Gladiator"] = 5;
$Power::PowerCount["Star Sighter"] = 6;
$Power::PowerCount["Prospector"] = 5;
$Power::PowerCount["Annihilator"] = 5;
$Power::PowerCount["Deep Freezer"] = 5;
$Power::PowerCount["Phantom Lord"] = 5;
//--------------------------------------------------------------------------
//WITCH
//ArrayPos
$PowerSave::PowerPosition["Witch",0] = "LightStrike";     //C
$PowerSave::PowerPosition["Witch",1] = "5SecCamo";        //C
$PowerSave::PowerPosition["Witch",2] = "Dispell";         //C
$PowerSave::PowerPosition["Witch",3] = "LowHeal";         //C
$PowerSave::PowerPosition["Witch",4] = "BasicShield";     //C
$PowerSave::PowerPosition["Witch",5] = "EnemyRepel";      //C
$PowerSave::PowerPosition["Witch",6] = "Shift";           //C
$PowerSave::PowerPosition["Witch",7] = "BasicExplosion";  //C
$PowerSave::PowerPosition["Witch",8] = "MedHeal";         //C
$PowerSave::PowerPosition["Witch",9] = "10SecCamo";       //C
$PowerSave::PowerPosition["Witch",10] = "ModerateShield"; //C
$PowerSave::PowerPosition["Witch",11] = "Stun";           //C
$PowerSave::PowerPosition["Witch",12] = "VapExplosion";   //C
$PowerSave::PowerPosition["Witch",13] = "SuperCloaker";   //C
$PowerSave::PowerPosition["Witch",14] = "Shielder";       //C
$PowerSave::PowerPosition["Witch",15] = "Healer";         //C
$PowerSave::PowerPosition["Witch",16] = "Vaporize";       //C
$PowerSave::PowerPosition["Witch",17] = "ToTheHeavens";   //C

$Power::PowerMenu["Witch", 1] = "LightStrike\tLight Strike\t1\tLow";
$Power::PowerInfo["LightStrike"] = "A Basic Light Strike, deals light damage\t2\t2\t2\t2";

$Power::PowerMenu["Witch", 2] = "5SecCamo\tLow Level Camo\t3\tLow";
$Power::PowerInfo["5SecCamo"] = "A basic spell that cloaks the caster for 5 seconds\t4\t4\t4\t4";

$Power::PowerMenu["Witch", 3] = "Dispell\tDispell Weapon\t5\tLow";
$Power::PowerInfo["Dispell"] = "A spell that has enough force to blast the weapon a person is holding out of their hands\t3\t3\t3\t3";

$Power::PowerMenu["Witch", 4] = "LowHeal\tLow Level Heal\t5\tLow";
$Power::PowerInfo["LowHeal"] = "A basic spell that lightly heals the caster\t4\t4\t4\t4";

$Power::PowerMenu["Witch", 5] = "BasicShield\tBasic Shielding\t5\tLow";
$Power::PowerInfo["BasicShield"] = "A basic spell that lightly shields the caster\t4\t4\t4\t4";

$Power::PowerMenu["Witch", 6] = "EnemyRepel\tRepel Force\t5\tLow";
$Power::PowerInfo["EnemyRepel"] = "A basic spell that pushes a target away from the caster\t1\t2\t1\t1";

$Power::PowerMenu["Witch", 7] = "Shift\tShift\t7\tLow";
$Power::PowerInfo["Shift"] = "A basic spell that teleports the caster\t4\t4\t4\t4";

$Power::PowerMenu["Witch", 8] = "BasicExplosion\tLow Level Blast\t8\tLow";
$Power::PowerInfo["BasicExplosion"] = "A basic spell that causes a minor explosion\t3\t1\t2\t2";
//MID
$Power::PowerMenu["Witch", 9] = "MedHeal\tMid Level Heal\t10\tMid";
$Power::PowerInfo["MedHeal"] = "A spell that moderately heals the caster\t4\t4\t4\t4";

$Power::PowerMenu["Witch", 10] = "10SecCamo\tMid Level Camo\t15\tMid";
$Power::PowerInfo["10SecCamo"] = "A spell that cloaks the caster for 10 seconds\t4\t4\t4\t4";

$Power::PowerMenu["Witch", 11] = "ModerateShield\tModerate Shielding\t15\tMid";
$Power::PowerInfo["ModerateShield"] = "A spell that moderately shields the caster\t4\t4\t4\t4";

$Power::PowerMenu["Witch", 12] = "Stun\tStun Enemy\t10\tMid";
$Power::PowerInfo["Stun"] = "A spell that temporarly freezes an enemy player\t3\t1\t1\t3";

$Power::PowerMenu["Witch", 13] = "VapExplosion\tVapor Explosion\t15\tMid";
$Power::PowerInfo["VapExplosion"] = "A spell that causes a moderate explosion\t3\t2\t3\t3";
//HIGH
$Power::PowerMenu["Witch", 14] = "SuperCloaker\tHigh Level Camo\t20\tHigh";
$Power::PowerInfo["SuperCloaker"] = "An advanced spell that cloaks the caster and nearby allies for 15 seconds\t4\t4\t4\t4";

$Power::PowerMenu["Witch", 15] = "Shielder\tExtreme Shielding\t20\tHigh";
$Power::PowerInfo["Shielder"] = "An advanced spell that heavily shields the caster and nearby allies. | NOTE: This Power Applies a 25 Second ENERGY BLOCK Status.\t4\t4\t4\t4";

$Power::PowerMenu["Witch", 16] = "Healer\tTeam Healer\t30\tHigh";
$Power::PowerInfo["Healer"] = "An advanced spell that heavily heals the caster and nearby allies | NOTE: This Power Applies a 25 Second ENERGY BLOCK Status.\t4\t4\t4\t4";

$Power::PowerMenu["Witch", 17] = "Vaporize\tVaporize\t30\tHigh";
$Power::PowerInfo["Vaporize"] = "A spell that causes a massive explosion\t3\t3\t3\t3";

$Power::PowerMenu["Witch", 18] = "ToTheHeavens\tTo The Heavens\t25\tHigh";
$Power::PowerInfo["ToTheHeavens"] = "A spell that sends a target to unimaginable heights\t2\t2\t2\t1";
//--------------------------------------------------------------------------
//PHANTOM CLASS
//ArrayPos
$PowerSave::PowerPosition["Phantom",0] = "ShadowStrike";     //C
$PowerSave::PowerPosition["Phantom",1] = "ShadowRush";       //C
$PowerSave::PowerPosition["Phantom",2] = "Flicker";          //C
$PowerSave::PowerPosition["Phantom",3] = "ShadowBomb";       //C
$PowerSave::PowerPosition["Phantom",4] = "FrostBite";        //C
$PowerSave::PowerPosition["Phantom",5] = "ShadowBoost";      //C
$PowerSave::PowerPosition["Phantom",6] = "ShadowBlast";      //C
$PowerSave::PowerPosition["Phantom",7] = "Hail";             //C
$PowerSave::PowerPosition["Phantom",8] = "ShadowPulse";      //C
$PowerSave::PowerPosition["Phantom",9] = "Flasher";          //C
$PowerSave::PowerPosition["Phantom",10] = "ShadowBombDrop";  //C
$PowerSave::PowerPosition["Phantom",11] = "Nightmare";       //C
$PowerSave::PowerPosition["Phantom",12] = "ShadowArtillery"; //C
$PowerSave::PowerPosition["Phantom",13] = "HailStorm";       //C
$PowerSave::PowerPosition["Phantom",14] = "ShadowBrigade";   //C
$PowerSave::PowerPosition["Phantom",15] = "Avalanche";       //C
$PowerSave::PowerPosition["Phantom",16] = "ShadowStorm";     //C

$Power::PowerMenu["Phantom", 1] = "ShadowStrike\tShadow Strike\t1\tLow";
$Power::PowerInfo["ShadowStrike"] = "A basic shadow spell that causes light damage\t2\t2\t2\t2";

$Power::PowerMenu["Phantom", 2] = "ShadowRush\tShadow Rush\t5\tLow";
$Power::PowerInfo["ShadowRush"] = "A basic shadow spell that sends 3 quick shadow missiles out from the staff\t3\t2\t2\t3";

$Power::PowerMenu["Phantom", 3] = "Flicker\tFlicker\t7\tLow";
$Power::PowerInfo["Flicker"] = "A basic shadow spell that unleashes a single shockwave of shadow energy at the hit location\t3\t3\t2\t3";

$Power::PowerMenu["Phantom", 4] = "ShadowBomb\tShadow Bomb\t10\tLow";
$Power::PowerInfo["ShadowBomb"] = "A basic shadow spell that arcs a shadow explosive at a target area\t3\t3\t3\t3";

$Power::PowerMenu["Phantom", 5] = "FrostBite\tFrost Bite\t10\tLow";
$Power::PowerInfo["FrostBite"] = "A basic shadow spell that casts an icy shard that freezes targets\t3\t2\t1\t3";

$Power::PowerMenu["Phantom", 6] = "ShadowBoost\tShadow Boost\t10\tLow";
$Power::PowerInfo["ShadowBoost"] = "A basic shadow spell that pulls the caster and nearby objects into a shadow vortex\t4\t4\t4\t4";
//MID
$Power::PowerMenu["Phantom", 7] = "ShadowBlast\tShadow Blast\t15\tMid";
$Power::PowerInfo["ShadowBlast"] = "A shadow spell that instantly causes a shadow explosion at a target location\t3\t1\t1\t2";

$Power::PowerMenu["Phantom", 8] = "Hail\tHail\t20\tMid";
$Power::PowerInfo["Hail"] = "A shadow spell that opens an icy portal for a short period of time that drops frost bite shards at the target location\t2\t2\t2\t2";

$Power::PowerMenu["Phantom", 9] = "ShadowPulse\tShadow Pulse\t20\tMid";
$Power::PowerInfo["ShadowPulse"] = "A shadow spell that damages an enemy and drains their energy for a period of time\t2\t1\t3\t3";

$Power::PowerMenu["Phantom", 10] = "Flasher\tFlasher\t20\tMid";
$Power::PowerInfo["Flasher"] = "A shadow spell that shockwaves shadow energy and then explodes again as a massive ending shockwave\t3\t3\t2\t3";

$Power::PowerMenu["Phantom", 11] = "ShadowBombDrop\tShadow Bomb Drop\t20\tMid";
$Power::PowerInfo["ShadowBombDrop"] = "A shadow spell that opens a small shadow rift to drop 4 shadow bombs at rapid speed\t3\t2\t2\t2";

$Power::PowerMenu["Phantom", 12] = "Nightmare\tNightmare\t25\tMid";
$Power::PowerInfo["Nightmare"] = "The darkest shadow spell a phantom can learn, it slowly drains the life of a target\t3\t3\t3\t3";

$Power::PowerMenu["Phantom", 13] = "ShadowArtillery\tShadow Artillery\t25\tMid";
$Power::PowerInfo["ShadowArtillery"] = "A shadow spell that opens a larger shadow portal to drop 8 shadow bombs from a higher altitude\t3\t3\t3\t2";

//HIGH
$Power::PowerMenu["Phantom", 14] = "HailStorm\tHail Storm\t30\tHigh";
$Power::PowerInfo["HailStorm"] = "An advanced shadow spell that opens a large ice portal to rain ice shards for a second or two\t3\t3\t2\t2";

$Power::PowerMenu["Phantom", 15] = "ShadowBrigade\tShadow Brigade\t20\tHigh";
$Power::PowerInfo["ShadowBrigade"] = "An advanced shadow spell that opens a large shadow portal and pulls in large shadow meteors to collide with the planet surface\t3\t2\t2\t2";

$Power::PowerMenu["Phantom", 16] = "Avalanche\tAvalanche\t30\tHigh";
$Power::PowerInfo["Avalanche"] = "An advanced shadow spell that opens a large ice portal to unleash massive ice shards that freezes targets in place for a long duration | NOTE: This Power Applies a 15 Second ENERGY BLOCK Status.\t3\t3\t3\t3";

$Power::PowerMenu["Phantom", 17] = "ShadowStorm\tShadow Storm\t30\tHigh";
$Power::PowerInfo["ShadowStorm"] = "One of the hardest shadow spells to master, it opens a massive shadow rift to dump everything explosive the phantom has at the target | NOTE: This Power Applies a 30 Second ENERGY BLOCK Status.\t3\t3\t3\t3";

//--------------------------------------------------------------------------
//DEMONZ
//ArrayPos
$PowerSave::PowerPosition["Demon",0] = "FireBolt";      //C
$PowerSave::PowerPosition["Demon",1] = "FireBall1";     //C
$PowerSave::PowerPosition["Demon",2] = "EnergyBall1";   //C
$PowerSave::PowerPosition["Demon",3] = "BasicShock";    //C
$PowerSave::PowerPosition["Demon",4] = "EnergyDrainer"; //C
$PowerSave::PowerPosition["Demon",5] = "FireBall2";     //C
$PowerSave::PowerPosition["Demon",6] = "Paralyze";      //C
$PowerSave::PowerPosition["Demon",7] = "EnergyBall2";   //C
$PowerSave::PowerPosition["Demon",8] = "EleStrike";     //C
$PowerSave::PowerPosition["Demon",9] = "EnergyVampire"; //C
$PowerSave::PowerPosition["Demon",10] = "FireBall3";    //C
$PowerSave::PowerPosition["Demon",11] = "EnergyBall3";  //C
$PowerSave::PowerPosition["Demon",12] = "Thunder";      //C
$PowerSave::PowerPosition["Demon",13] = "SplitFire";    //C
$PowerSave::PowerPosition["Demon",14] = "EnergyGod";    //C
$PowerSave::PowerPosition["Demon",15] = "FireBall4";    //C
$PowerSave::PowerPosition["Demon",16] = "Lightning";    //C

$Power::PowerMenu["Demon", 1] = "FireBolt\tFire Bolt\t1\tLow";
$Power::PowerInfo["FireBolt"] = "A basic spell that causes radius damage\t2\t2\t2\t2";

$Power::PowerMenu["Demon", 2] = "FireBall1\tFire Ball Lv. 1\t3\tLow";
$Power::PowerInfo["FireBall1"] = "A basic spell that causes a fire based explosion\t2\t1\t1\t1";

$Power::PowerMenu["Demon", 3] = "EnergyBall1\tEnergy Ball Lv. 1\t5\tLow";
$Power::PowerInfo["EnergyBall1"] = "A basic spell that causes an electrical based explosion\t3\t2\t2\t2";

$Power::PowerMenu["Demon", 4] = "BasicShock\tBasic Shock\t7\tLow";
$Power::PowerInfo["BasicShock"] = "A basic spell that zaps a single target with electricity\t2\t3\t1\t2";

$Power::PowerMenu["Demon", 5] = "EnergyDrainer\tBasic Drain\t7\tLow";
$Power::PowerInfo["EnergyDrainer"] = "A basic spell that drains a target's energy for some time \t2\t1\t3\t3";

$Power::PowerMenu["Demon", 6] = "FireBall2\tFire Ball Lv. 2\t10\tLow";
$Power::PowerInfo["FireBall2"] = "The second fire ball a demon learns, causes a larger explosion\t3\t1\t2\t2";
//MID
$Power::PowerMenu["Demon", 7] = "Paralyze\tParalyze Foe\t10\tMid";
$Power::PowerInfo["Paralyze"] = "A spell that freezes a target in fear\t2\t3\t1\t3";

$Power::PowerMenu["Demon", 8] = "EnergyBall2\tEnergy Ball Lv. 2\t10\tMid";
$Power::PowerInfo["EnergyBall2"] = "The second Electric ball a demon learns, deals more damage\t3\t2\t2\t2";

$Power::PowerMenu["Demon", 9] = "EleStrike\tElectric Strike\t15\tMid";
$Power::PowerInfo["EleStrike"] = "A stronger electric shock that zaps a single target with electricity\t2\t3\t2\t2";

$Power::PowerMenu["Demon", 10] = "EnergyVampire\tEnergy Vampire\t15\tMid";
$Power::PowerInfo["EnergyVampire"] = "A spell that drains a target's energy for some time and replenishes the caster's\t2\t2\t3\t3";

$Power::PowerMenu["Demon", 11] = "FireBall3\tFire Ball Lv. 3\t15\tMid";
$Power::PowerInfo["FireBall3"] = "The third fire ball a demon learns, causes an even larger explosion\t3\t2\t3\t3";
//HIGH
$Power::PowerMenu["Demon", 12] = "EnergyBall3\tEnergy Ball Lv. 3\t20\tHigh";
$Power::PowerInfo["EnergyBall3"] = "The third Electric ball a demon learns, deals even more damage\t3\t2\t3\t3";

$Power::PowerMenu["Demon", 13] = "Thunder\tThunder\t20\tHigh";
$Power::PowerInfo["Thunder"] = "the strongest electric shock that zaps a single target with electricity\t3\t3\t3\t3";

$Power::PowerMenu["Demon", 14] = "SplitFire\tSplit Fire Bomb\t20\tHigh";
$Power::PowerInfo["SplitFire"] = "A estranged fireball that splits into multiple smaller ones when it explodes\t3\t3\t3\t3";

$Power::PowerMenu["Demon", 15] = "EnergyGod\tOh-Meg-Ah Energy Drain\t25\tHigh";
$Power::PowerInfo["EnergyGod"] = "The most powerful drain spell, that also replenishes that of the caster\t3\t3\t3\t3";

$Power::PowerMenu["Demon", 16] = "FireBall4\tFire Ball Lv. 4\t30\tHigh";
$Power::PowerInfo["FireBall4"] = "The fourth fire ball a demon learns, causes an even larger explosion\t3\t3\t3\t3";

$Power::PowerMenu["Demon", 17] = "Lightning\tLightning\t30\tHigh";
$Power::PowerInfo["Lightning"] = "A spell that causes a massive electrical disturbance at the area | NOTE: This Power Applies a 15 Second ENERGY BLOCK Status.\t3\t3\t3\t3";

//--------------------------------------------------------------------------
//Hunter
//ArrayPos
$PowerSave::PowerPosition["Hunter",0] = "ManaArrow";      //C
$PowerSave::PowerPosition["Hunter",1] = "Smackback";      //C
$PowerSave::PowerPosition["Hunter",2] = "ConcussionBlow"; //C
$PowerSave::PowerPosition["Hunter",3] = "QuakeTrap";      //C
$PowerSave::PowerPosition["Hunter",4] = "NetTrap";        //C
$PowerSave::PowerPosition["Hunter",5] = "AcidArrow";      //C

$PowerSave::PowerPosition["Hunter",6] = "BlastTrap";      //C
$PowerSave::PowerPosition["Hunter",7] = "ToxicTrap";      //C
$PowerSave::PowerPosition["Hunter",8] = "FreezeTrap";     //C
$PowerSave::PowerPosition["Hunter",9] = "BurnTrap";       //C
$PowerSave::PowerPosition["Hunter",10] = "SpearShower";   //C
$PowerSave::PowerPosition["Hunter",11] = "Ambush";        //C

$PowerSave::PowerPosition["Hunter",12] = "Multishot";     //C
$PowerSave::PowerPosition["Hunter",13] = "ShineShot";     //C
$PowerSave::PowerPosition["Hunter",14] = "Composite";     //C
$PowerSave::PowerPosition["Hunter",15] = "BurstTrap";     //C
$PowerSave::PowerPosition["Hunter",16] = "StrikeDown";    //

$Power::PowerMenu["Hunter", 1] = "ManaArrow\tMana Arrow\t1\tLow";
$Power::PowerInfo["ManaArrow"] = "A basic arrow composed of concentrated energy\t2\t2\t2\t2";

$Power::PowerMenu["Hunter", 2] = "Smackback\tSmackback\t3\tLow";
$Power::PowerInfo["Smackback"] = "Strike a target with the bow temporarily stunning it\t4\t4\t4\t4";

$Power::PowerMenu["Hunter", 3] = "ConcussionBlow\tConcussion Blow\t5\tLow";
$Power::PowerInfo["ConcussionBlow"] = "Fire a concussion bomb to blast targets backward and disarm them\t4\t4\t4\t4";

$Power::PowerMenu["Hunter", 4] = "QuakeTrap\tQuake Trap\t7\tLow";
$Power::PowerInfo["QuakeTrap"] = "A basic trap that impares targets temporarily\t2\t2\t2\t2";

$Power::PowerMenu["Hunter", 5] = "NetTrap\tNet Trap\t7\tLow";
$Power::PowerInfo["NetTrap"] = "A basic trap that ensnares a target, they can still fire\t2\t2\t2\t2";

$Power::PowerMenu["Hunter", 6] = "AcidArrow\tAcid Arrow\t10\tLow";
$Power::PowerInfo["AcidArrow"] = "Fires an arrow that has a chance to poison a target\t2\t2\t2\t2";

//MID
$Power::PowerMenu["Hunter", 7] = "BlastTrap\tBlast Trap\t10\tMid";
$Power::PowerInfo["BlastTrap"] = "An advanced trap that detonates on impact\t2\t3\t3\t3";

$Power::PowerMenu["Hunter", 8] = "ToxicTrap\tToxic Trap\t10\tMid";
$Power::PowerInfo["ToxicTrap"] = "An advanced trap that poisons targets affected by the blast\t3\t2\t2\t2";

$Power::PowerMenu["Hunter", 9] = "FreezeTrap\tFreeze Trap\t15\tMid";
$Power::PowerInfo["FreezeTrap"] = "An advanced trap that releases an icy wind when detonated\t2\t3\t2\t3";

$Power::PowerMenu["Hunter", 10] = "BurnTrap\tBurn Trap\t15\tMid";
$Power::PowerInfo["BurnTrap"] = "An advanced trap that violently explodes in a firey blast\t2\t2\t3\t3";

$Power::PowerMenu["Hunter", 11] = "SpearShower\tSpear Shower\t25\tMid";
$Power::PowerInfo["SpearShower"] = "Launch an array of shard spears, which stun affected targets\t3\t3\t3\t3";

$Power::PowerMenu["Hunter", 12] = "Ambush\tAmbush\t10\tMid";
$Power::PowerInfo["Ambush"] = "Through elemental connections, cloak yourself for 15 seconds\t4\t4\t4\t4";

//HIGH
$Power::PowerMenu["Hunter", 13] = "Multishot\tMultishot\t20\tHigh";
$Power::PowerInfo["Multishot"] = "Launch horizontal arrow shots in three consecutive bursts\t3\t3\t3\t3";

$Power::PowerMenu["Hunter", 14] = "ShineShot\tShine Shot\t20\tHigh";
$Power::PowerInfo["ShineShot"] = "Excellent for tight situations, blast a blinding arrow at a target\t3\t3\t3\t3";

$Power::PowerMenu["Hunter", 15] = "Composite\tComposite\t25\tHigh";
$Power::PowerInfo["Composite"] = "Throw an HE Composite Grenade, causing quite the blast!\t3\t3\t3\t3";

$Power::PowerMenu["Hunter", 16] = "BurstTrap\tBurst Trap\t30\tHigh";
$Power::PowerInfo["BurstTrap"] = "The ultimate hunter trap, launches spliced spears when detonated\t3\t3\t3\t3";

$Power::PowerMenu["Hunter", 17] = "StrikeDown\tStrike Down\t30\tHigh";
$Power::PowerInfo["StrikeDown"] = "Launch a high powered flame bolt that explodes on target impact, hit targets suffer a strong burn effect | NOTE: This Power Applies a 30 Second ENERGY BLOCK Status.\t3\t3\t3\t3";

//--------------------------------------------------------------------------
$PowerSave::PowerPosition["Cryonium",0] = "IceRush";          //C
$PowerSave::PowerPosition["Cryonium",1] = "IceBomb";          //C
$PowerSave::PowerPosition["Cryonium",2] = "FrozenTwister";    //C
$PowerSave::PowerPosition["Cryonium",3] = "CrystalShock";     //C
$PowerSave::PowerPosition["Cryonium",4] = "DeepFreeze";       //C
$PowerSave::PowerPosition["Cryonium",5] = "Blizzard";         //C

$Power::PowerMenu["Cryonium", 1] = "IceRush\tIce Rush\t5\tAffinity";
$Power::PowerInfo["IceRush"] = "A shadow spell that causes moderate damage and freezes targets\t3\t3\t2\t3";

$Power::PowerMenu["Cryonium", 2] = "IceBomb\tIce Bomb\t15\tAffinity";
$Power::PowerInfo["IceBomb"] = "A shadow spell that drops an ice bomb that explodes causing massive damage and freezes targets\t3\t3\t3\t3";

$Power::PowerMenu["Cryonium", 3] = "FrozenTwister\tFrozen Twister\t15\tAffinity";
$Power::PowerInfo["FrozenTwister"] = "A shadow spell that sucks in nearby targets, freezes and damages them\t3\t3\t3\t3";

$Power::PowerMenu["Cryonium", 4] = "CrystalShock\tCrystal Shock\t15\tAffinity";
$Power::PowerInfo["CrystalShock"] = "A shadow spell that shatters ice (unfreezes), deals 3x Damage to frozen targets\t3\t3\t3\t3";

$Power::PowerMenu["Cryonium", 5] = "DeepFreeze\tDeep Freeze\t20\tAffinity";
$Power::PowerInfo["DeepFreeze"] = "A shadow spell that freezes a very large area with no damage\t2\t3\t2\t3";

$Power::PowerMenu["Cryonium", 6] = "Blizzard\tShard Blizzard\t25\tAffinity";
$Power::PowerInfo["Blizzard"] = "A shadow spell that opens a icy rift to dump all forms of icy goodness\t3\t3\t3\t3";

//--------------------------------------------------------------------------
$PowerSave::PowerPosition["Overseer",0] = "ShadowSwipe";     //C
$PowerSave::PowerPosition["Overseer",1] = "StabRush";        //C
$PowerSave::PowerPosition["Overseer",2] = "BarrierField";    //C
$PowerSave::PowerPosition["Overseer",3] = "AvadusLightning"; //C
$PowerSave::PowerPosition["Overseer",4] = "ShadowHeal";      //C
$PowerSave::PowerPosition["Overseer",5] = "AsteroidFall";    //C

$Power::PowerMenu["Overseer", 1] = "ShadowSwipe\tShadow Swipe\t10\tAffinity";
$Power::PowerInfo["ShadowSwipe"] = "A powerful sword move that pulls in targets to a slicing force\t2\t3\t3\t3";

$Power::PowerMenu["Overseer", 2] = "StabRush\tStab Rush\t10\tAffinity";
$Power::PowerInfo["StabRush"] = "A rapidly usable sword move that thrusts the sword at the target\t2\t2\t2\t2";

$Power::PowerMenu["Overseer", 3] = "BarrierField\tBarrier Field\t15\tAffinity";
$Power::PowerInfo["BarrierField"] = "Creates an unpenetrable shadow field that absorbs all incoming projectiles\t4\t4\t4\t4";

$Power::PowerMenu["Overseer", 4] = "AvadusLightning\tMarvolic Lightning\t25\tAffinity";
$Power::PowerInfo["AvadusLightning"] = "Casts an extremely powerful electrical force that instantly kills a single target\t3\t3\t3\t3";

$Power::PowerMenu["Overseer", 5] = "ShadowHeal\tShadow Heal\t10\tAffinity";
$Power::PowerInfo["ShadowHeal"] = "A shadow spell that absorbs nearby shadow energy to heal the caster\t4\t4\t4\t4";

$Power::PowerMenu["Overseer", 6] = "AsteroidFall\tAsteroid Fall\t25\tAffinity";
$Power::PowerInfo["AsteroidFall"] = "A shadow spell that disrupts a small asteroid off course to collide with a target location\t3\t3\t3\t3";

//--------------------------------------------------------------------------
$PowerSave::PowerPosition["Devastator",0] = "BurnForce";    //C
$PowerSave::PowerPosition["Devastator",1] = "FissureBurst"; //C
$PowerSave::PowerPosition["Devastator",2] = "Microwave";    //C
$PowerSave::PowerPosition["Devastator",3] = "Crisp";        //C
$PowerSave::PowerPosition["Devastator",4] = "Desolation";   //C
$PowerSave::PowerPosition["Devastator",5] = "SunRay";       //C

$Power::PowerMenu["Devastator", 1] = "BurnForce\tBurn Force\t5\tAffinity";
$Power::PowerInfo["BurnForce"] = "A basic fire move that inflicts a burn on a single target\t2\t2\t2\t2";

$Power::PowerMenu["Devastator", 2] = "FissureBurst\tFissure Burst\t15\tAffinity";
$Power::PowerInfo["FissureBurst"] = "A straight line attack that bursts explosives to the surface along it's line of attack\t3\t2\t3\t3";

$Power::PowerMenu["Devastator", 3] = "Microwave\tMicrowave\t15\tAffinity";
$Power::PowerInfo["Microwave"] = "Casts a shockwave force that burns all targets that come in it's pulse\t3\t2\t3\t3";

$Power::PowerMenu["Devastator", 4] = "Crisp\tCrisp\t15\tAffinity";
$Power::PowerInfo["Crisp"] = "Casts a fiery explosive to inflict burns on all targets affected by it's power\t3\t3\t3\t3";

$Power::PowerMenu["Devastator", 5] = "Desolation\tDesolation\t20\tAffinity";
$Power::PowerInfo["Desolation"] = "A powerful fire attack that pulls volcanic forces to the surface, causes burns\t3\t3\t3\t3";

$Power::PowerMenu["Devastator", 6] = "SunRay\tSun Ray\t25\tAffinity";
$Power::PowerInfo["SunRay"] = "A powerful fire attack that unleashes a firey burst of energy at the target\t3\t3\t3\t3";

//--------------------------------------------------------------------------

$PowerSave::PowerPosition["Enforcer",0] = "DeepFlameShot";     //C
$PowerSave::PowerPosition["Enforcer",1] = "FireBall5";         //C
$PowerSave::PowerPosition["Enforcer",2] = "FireBall6";         //C
$PowerSave::PowerPosition["Enforcer",3] = "Enrage";            //C
$PowerSave::PowerPosition["Enforcer",4] = "SuperSplitFire";    //C
$PowerSave::PowerPosition["Enforcer",5] = "Hellstorm";         //C

$Power::PowerMenu["Enforcer", 1] = "DeepFlameShot\tDeep Flame Shot\t10\tAffinity";
$Power::PowerInfo["DeepFlameShot"] = "A powerful fire shot that has a chance to temporarily stun a target\t2\t3\t2\t3";

$Power::PowerMenu["Enforcer", 2] = "FireBall5\tFire Ball Lv. 5\t15\tAffinity";
$Power::PowerInfo["FireBall5"] = "A powerful fire ball that pulls targets in before exploding\t3\t3\t2\t3";

$Power::PowerMenu["Enforcer", 3] = "FireBall6\tFire Ball Lv. 6\t20\tAffinity";
$Power::PowerInfo["FireBall6"] = "Expands the sucking and explosion radius of the Fireball\t3\t3\t3\t3";

$Power::PowerMenu["Enforcer", 4] = "Enrage\tEnrage\t10\tAffinity";
$Power::PowerInfo["Enrage"] = "Makes the caster invincible for a short period of time\t4\t4\t4\t4";

$Power::PowerMenu["Enforcer", 5] = "SuperSplitFire\tSuper Split Fire\t20\tAffinity";
$Power::PowerInfo["SuperSplitFire"] = "Casts a stronger version of the split fire bomb\t3\t3\t3\t3";

$Power::PowerMenu["Enforcer", 6] = "Hellstorm\tHellstorm\t20\tAffinity";
$Power::PowerInfo["Hellstorm"] = "Opens a fire rift to rain down fire balls at a rapid pace\t3\t3\t3\t3";

//--------------------------------------------------------------------------

$PowerSave::PowerPosition["StarLighter",0] = "StarShard";            //C
$PowerSave::PowerPosition["StarLighter",1] = "RepelShift";           //C
$PowerSave::PowerPosition["StarLighter",2] = "ShiningStar";          //C
$PowerSave::PowerPosition["StarLighter",3] = "ShardStorm";           //C
$PowerSave::PowerPosition["StarLighter",4] = "EnviousDownpour";      //C
$PowerSave::PowerPosition["StarLighter",5] = "CatalystBurst";        //C

$Power::PowerMenu["Star Lighter", 1] = "StarShard\tStar Shard\t5\tAffinity";
$Power::PowerInfo["StarShard"] = "Casts a radiated tip missile\t2\t2\t2\t2";

$Power::PowerMenu["Star Lighter", 2] = "RepelShift\tRepel Shift\t10\tAffinity";
$Power::PowerInfo["RepelShift"] = "Teleports the user, blasts away targets at the location\t2\t2\t2\t2";

$Power::PowerMenu["Star Lighter", 3] = "ShiningStar\tShining Star\t15\tAffinity";
$Power::PowerInfo["ShiningStar"] = "Creates a miniature star that causes radiation damage\t3\t3\t3\t3";

$Power::PowerMenu["Star Lighter", 4] = "ShardStorm\tShard Storm\t20\tAffinity";
$Power::PowerInfo["ShardStorm"] = "Opens a shard rift at the target location\t3\t3\t3\t3";

$Power::PowerMenu["Star Lighter", 5] = "EnviousDownpour\tEnvious Downpour\t20\tAffinity";
$Power::PowerInfo["EnviousDownpour"] = "Casts a radiated field of energy at the target\t3\t3\t3\t3";

$Power::PowerMenu["Star Lighter", 6] = "CatalystBurst\tCatalyst Burst\t25\tAffinity";
$Power::PowerInfo["CatalystBurst"] = "Hits a target with an energy explosion that chains once to other targets\t3\t3\t3\t3";

//--------------------------------------------------------------------------

$PowerSave::PowerPosition["Guardian",0] = "ForceedPush";      //C
$PowerSave::PowerPosition["Guardian",1] = "EnergyNuke";       //C
$PowerSave::PowerPosition["Guardian",2] = "StasisField";      //C
$PowerSave::PowerPosition["Guardian",3] = "EnergyBoost";      //C
$PowerSave::PowerPosition["Guardian",4] = "DamageReducer";    //C
$PowerSave::PowerPosition["Guardian",5] = "DegaussianStrike"; //C

$Power::PowerMenu["Guardian", 1] = "ForceedPush\tForceed Push\t5\tAffinity";
$Power::PowerInfo["ForceedPush"] = "Creates a shockwave of energy that pushes targets away from it\t1\t1\t1\t1";

$Power::PowerMenu["Guardian", 2] = "EnergyNuke\tEnergy Nuke\t15\tAffinity";
$Power::PowerInfo["EnergyNuke"] = "Creates a massive ionic pulse that wipes all the energy from all players\t2\t1\t3\t3";

$Power::PowerMenu["Guardian", 3] = "StasisField\tStasis Field\t15\tAffinity";
$Power::PowerInfo["StasisField"] = "Creates a field that freezes all inside it in place from damage or movement\t2\t2\t2\t2";

$Power::PowerMenu["Guardian", 4] = "EnergyBoost\tEnergy Boost\t15\tAffinity";
$Power::PowerInfo["EnergyBoost"] = "Wipes the caster's energy to fully restore a teammate's\t3\t2\t3\t3";

$Power::PowerMenu["Guardian", 5] = "DamageReducer\tDamage Reducer\t20\tAffinity";
$Power::PowerInfo["DamageReducer"] = "Reduces the damage targets in the caster's area can inflict\t2\t3\t3\t3";

$Power::PowerMenu["Guardian", 6] = "DegaussianStrike\tDegaussian Strike\t25\tAffinity";
$Power::PowerInfo["DegaussianStrike"] = "Drops a powerful gauss tipped strike at the target area\t3\t3\t3\t3";

//--------------------------------------------------------------------------

$PowerSave::PowerPosition["Nature Walker",0] = "FalconStrike";     //
$PowerSave::PowerPosition["Nature Walker",1] = "Fury";             //
$PowerSave::PowerPosition["Nature Walker",2] = "StormWing";        //
$PowerSave::PowerPosition["Nature Walker",3] = "FerociousRoar";    //
$PowerSave::PowerPosition["Nature Walker",4] = "RisingFlight";     //
$PowerSave::PowerPosition["Nature Walker",5] = "TigerLash";        //

$Power::PowerMenu["Nature Walker", 1] = "FalconStrike\tFalcon Strike\t15\tAffinity";
$Power::PowerInfo["FalconStrike"] = "Summon a flaming falcon to carpet bomb a line in front of you\t3\t3\t3\t3";

$Power::PowerMenu["Nature Walker", 2] = "Fury\tFury\t15\tAffinity";
$Power::PowerInfo["Fury"] = "Summon a flaming falcon to very rapidly crash into a small area with force\t3\t3\t3\t3";

$Power::PowerMenu["Nature Walker", 3] = "StormWing\tStorm Wing\t15\tAffinity";
$Power::PowerInfo["StormWing"] = "Summon a lightning eagle to electrify a target zone\t2\t2\t2\t2";

$Power::PowerMenu["Nature Walker", 4] = "FerociousRoar\tFerocious Roar\t15\tAffinity";
$Power::PowerInfo["FerociousRoar"] = "Summon a spiritual wolf to create a shockwave which sends targets flying\t2\t2\t2\t2";

$Power::PowerMenu["Nature Walker", 5] = "RisingFlight\tRising Flight\t20\tAffinity";
$Power::PowerInfo["RisingFlight"] = "Summon a flaming falcon which swoops in and lifts a target high into the air\t3\t3\t3\t3";

$Power::PowerMenu["Nature Walker", 6] = "TigerLash\tTiger Lash\t25\tAffinity";
$Power::PowerInfo["TigerLash"] = "Launch a spiritual tiger forward dealing major damage in a straight line\t3\t3\t3\t3";

$Power::SpecialMenu["Nature Walker", 1] = "NightTrap\tNight Traps\t20\tAffinity";
$Power::PowerInfo["NightTrap"] = "Cloak all traps when they are deployed making them nearly impossible to see\t3\t3\t3\t3";

//Affinity 2 Powers
$PowerSave::PowerPosition["Gladiator",0] = "ChargeLight";      //C
$PowerSave::PowerPosition["Gladiator",1] = "ChainBurst";       //
$PowerSave::PowerPosition["Gladiator",2] = "TeamUnlimEnergy";  //
$PowerSave::PowerPosition["Gladiator",3] = "HealthBoost";      //
$PowerSave::PowerPosition["Gladiator",4] = "ROTChampion";      //

$Power::PowerMenu["Gladiator", 1] = "ChargeLight\tCharge The Light\t5\tAffinity2";
$Power::PowerInfo["ChargeLight"] = "Witch Second Affinity Power, Charges Lightforce Energy needed to use ultimate powers, 100 Lightforce Maximum.\t4\t4\t4\t4";

$Power::PowerMenu["Gladiator", 2] = "ChainBurst\tChain Burst\t20\tAffinity2";
$Power::PowerInfo["ChainBurst"] = "Unleash a wave of degaussian strikes [-40 Lightforce]\t3\t3\t3\t3";

$Power::PowerMenu["Gladiator", 3] = "TeamUnlimEnergy\tTeam Unlimited Energy\t25\tAffinity2";
$Power::PowerInfo["TeamUnlimEnergy"] = "Spawn an energy spike that maximizes the energy of all teammates near it. [-75 Lightforce]\t4\t4\t4\t4";

$Power::PowerMenu["Gladiator", 4] = "HealthBoost\tHealth Boost\t25\tAffinity2";
$Power::PowerInfo["HealthBoost"] = "Cast a 2x Health Boost Buff of all Teammates affected by it, and fully restore their HP [-80 Lightforce]\t4\t4\t4\t4";

$Power::PowerMenu["Gladiator", 5] = "ROTChampion\t[U] Rise of The Champion\t50\tAffinity2";
$Power::PowerInfo["ROTChampion"] = "[Ultimate Power] Transform into the champion, an unstopable force armed with destructive weapons [-100 Lightforce]\t3\t3\t3\t3";

//--------------------------------------------------------------------------

$PowerSave::PowerPosition["StarSighter",0] = "ChargeLight";          //C
$PowerSave::PowerPosition["StarSighter",1] = "CatalystExplosive";    //
$PowerSave::PowerPosition["StarSighter",2] = "ShardRain";            //
$PowerSave::PowerPosition["StarSighter",3] = "SwiftVengeance";       //
$PowerSave::PowerPosition["StarSighter",4] = "StarLight";            //
$PowerSave::PowerPosition["StarSighter",5] = "Rift";                 //

$Power::PowerMenu["Star Sighter", 1] = "ChargeLight\tCharge The Light\t5\tAffinity2";
//$Power::PowerInfo["ChargeLight"] = "Witch Second Affinity Power, Charges Lightforce Energy needed to use ultimate powers, 100 Lightforce Maximum.\t4\t4\t4\t4";
//Phantom139: Can just use the above one ;)

$Power::PowerMenu["Star Sighter", 2] = "CatalystExplosive\tCatalyst Explosive\t20\tAffinity2";
$Power::PowerInfo["CatalystExplosive"] = "Unleash a chain reaction of explosive forces at a target [-40 Lightforce]\t3\t3\t3\t3";

$Power::PowerMenu["Star Sighter", 3] = "ShardRain\tShard Rain\t25\tAffinity2";
$Power::PowerInfo["ShardRain"] = "Open a rift to the stars, bringing down a furious downpour of shard spikes [-55 Lightforce]\t3\t3\t3\t3";

$Power::PowerMenu["Star Sighter", 4] = "SwiftVengeance\tSwift Vengeance\t25\tAffinity2";
$Power::PowerInfo["SwiftVengeance"] = "Transform into a swift force of energy and blast into a target with explosive power [-75 Lightforce]\t3\t3\t3\t3";

$Power::PowerMenu["Star Sighter", 5] = "StarLight\tStar Light\t30\tAffinity2";
$Power::PowerInfo["StarLight"] = "Draw in a huge force wave and summon a small star, which then explodes with violent power [-90 Lightforce]\t3\t3\t3\t3";

$Power::PowerMenu["Star Sighter", 6] = "Rift\t[U] Rift\t50\tAffinity2";
$Power::PowerInfo["Rift"] = "[Ultimate Power] Summon a temporal rift, which sucks enemies into oblivion [-100 Lightforce]\t3\t3\t3\t3";

//--------------------------------------------------------------------------

$PowerSave::PowerPosition["Prospector",0] = "DarkVoid";           //C
$PowerSave::PowerPosition["Prospector",1] = "Flamethrower";       //
$PowerSave::PowerPosition["Prospector",2] = "DarkHeal";           //
$PowerSave::PowerPosition["Prospector",3] = "HellsFury";          //
$PowerSave::PowerPosition["Prospector",4] = "SpiritofPower";      //

$Power::PowerMenu["Prospector", 1] = "DarkVoid\tDark Void\t5\tAffinity2";
$Power::PowerInfo["DarkVoid"] = "Demon Second Affinity Power, Charges Darkforce Energy needed to use ultimate powers, 100 Darkforce Maximum.\t4\t4\t4\t4";

$Power::PowerMenu["Prospector", 2] = "Flamethrower\tFlamethrower\t20\tAffinity2";
$Power::PowerInfo["Flamethrower"] = "Pull a force of flames from below up and then blast them forward dealing massive damage to the front [-40 Darkforce]\t3\t3\t3\t3";

$Power::PowerMenu["Prospector", 3] = "DarkHeal\tDark Heal\t20\tAffinity2";
$Power::PowerInfo["DarkHeal"] = "Through pure darkness, moderately restore the HP of yourself.\t4\t4\t4\t4";

$Power::PowerMenu["Prospector", 4] = "HellsFury\tHell's Fury\t25\tAffinity2";
$Power::PowerInfo["HellsFury"] = "Open a firey rift from above summoning a meteor storm of galactic proportions [-80 Darkforce]\t3\t3\t3\t3";

$Power::PowerMenu["Prospector", 5] = "SpiritofPower\t[U] Spirit of Power\t50\tAffinity2";
$Power::PowerInfo["SpiritofPower"] = "[Ultimate Power] Transform into a dark entity that can blast targets away with destructive weapons [-100 Darkforce]\t3\t3\t3\t3";

//--------------------------------------------------------------------------

$PowerSave::PowerPosition["Annihilator",0] = "DarkVoid";            //C
$PowerSave::PowerPosition["Annihilator",1] = "EnergyBall4";         //
$PowerSave::PowerPosition["Annihilator",2] = "UnbearableFirestorm"; //
$PowerSave::PowerPosition["Annihilator",3] = "SunStorm";            //
$PowerSave::PowerPosition["Annihilator",4] = "DragonsRevenge";      //

$Power::PowerMenu["Annihilator", 1] = "DarkVoid\tDark Void\t5\tAffinity2";
//$Power::PowerInfo["DarkVoid"] = "Demon Second Affinity Power, Charges Darkforce Energy needed to use ultimate powers, 100 Darkforce Maximum.\t4\t4\t4\t4";

$Power::PowerMenu["Annihilator", 2] = "EnergyBall4\tEnergy Ball Lv. 4\t20\tAffinity2";
$Power::PowerInfo["EnergyBall4"] = "Fires a large energy pulse forward draining the energy of everyone near it before detonating violently [-40 Darkforce]\t3\t3\t3\t3";

$Power::PowerMenu["Annihilator", 3] = "UnbearableFirestorm\tUnbearable Firestorm\t25\tAffinity2";
$Power::PowerInfo["UnbearableFirestorm"] = "Summon a darkened wave that unleashes a pure force of fire below wiping out everything [-80 Darkforce]\t3\t3\t3\t3";

$Power::PowerMenu["Annihilator", 4] = "SunStorm\tSun Storm\t25\tAffinity2";
$Power::PowerInfo["SunStorm"] = "Bring down a mega pillar of flames that expand causing areas of crisp to violently erupt [-80 Darkforce]\t3\t3\t3\t3";

$Power::PowerMenu["Annihilator", 5] = "DragonsRevenge\t[U] Dragon's Revenge\t50\tAffinity2";
$Power::PowerInfo["DragonsRevenge"] = "[Ultimate Power] Summon the Dragon Spirit that unleashes a wall of fire instantly killing everyone that it hits [-100 Darkforce]\t3\t3\t3\t3";

//--------------------------------------------------------------------------

$PowerSave::PowerPosition["Phantom Lord",0] = "ShadowRift";          //C
$PowerSave::PowerPosition["Phantom Lord",1] = "Dematerialize";       //C
$PowerSave::PowerPosition["Phantom Lord",2] = "ChainLightning";      //
$PowerSave::PowerPosition["Phantom Lord",3] = "TornadicUprising";    //
$PowerSave::PowerPosition["Phantom Lord",4] = "PhantomStorm";        //

$Power::PowerMenu["Phantom Lord", 1] = "ShadowRift\tShadow Rift\t5\tAffinity2";
$Power::PowerInfo["ShadowRift"] = "Phantom Second Affinity Power, Charges Shadow Energy needed to use ultimate powers, 100 Shadow Energy Maximum.\t4\t4\t4\t4";

$Power::PowerMenu["Phantom Lord", 2] = "Dematerialize\tDematerialize\t20\tAffinity2";
$Power::PowerInfo["Dematerialize"] = "Shove a wave of force that vaporizes and blows them out of existance [-40 Shadow Energy]\t3\t3\t3\t3";

$Power::PowerMenu["Phantom Lord", 3] = "ChainLightning\tChain Lightning\t25\tAffinity2";
$Power::PowerInfo["ChainLightning"] = "Fires a burst of lightning energy that chains to nearby targets. Each successive hit increases the damage [-65 Shadow Energy]\t3\t3\t3\t3";

$Power::PowerMenu["Phantom Lord", 4] = "TornadicUprising\tTornadic Uprising\t25\tAffinity2";
$Power::PowerInfo["TornadicUprising"] = "Spin up a furious Force Five tornado that sucks in and kills enemies [-80 Shadow Energy]\t3\t3\t3\t3";

$Power::PowerMenu["Phantom Lord", 5] = "PhantomStorm\t[U] Phantom Storm\t50\tAffinity2";
$Power::PowerInfo["PhantomStorm"] = "[Ultimate Power] Plunge the world into darkness and then let off a massive shadow pulse wave, obliterating all it hits instantly [-100 Shadow Energy]\t3\t3\t3\t3";

//--------------------------------------------------------------------------

$PowerSave::PowerPosition["Deep Freezer",0] = "ShadowRift";          //C
$PowerSave::PowerPosition["Deep Freezer",1] = "SeekingFreeze";       //
$PowerSave::PowerPosition["Deep Freezer",2] = "CrystalBlast";        //
$PowerSave::PowerPosition["Deep Freezer",3] = "Whiteout";            //
$PowerSave::PowerPosition["Deep Freezer",4] = "Snowstorm";           //

$Power::PowerMenu["Deep Freezer", 1] = "ShadowRift\tShadow Rift\t5\tAffinity2";

$Power::PowerMenu["Deep Freezer", 2] = "SeekingFreeze\tSeeking Freeze\t20\tAffinity2";
$Power::PowerInfo["SeekingFreeze"] = "Summon a photon pulse that seeks out enemies and freezes them dealing massive damage [-40 Shadow Energy]\t3\t3\t3\t3";

$Power::PowerMenu["Deep Freezer", 3] = "CrystalBlast\tCrystal Blast\t25\tAffinity2";
$Power::PowerInfo["CrystalBlast"] = "A violent shattering pulse that shatters frozen targets into icy spears which can impact nearby targets [-65 Shadow Energy]\t3\t3\t3\t3";

$Power::PowerMenu["Deep Freezer", 4] = "Whiteout\tWhiteout\t25\tAffinity2";
$Power::PowerInfo["Whiteout"] = "Summons a blinding blizzard which freezes anyone caught in the zone of impact for a punishing 15 seconds [-80 Shadow Energy]\t3\t3\t3\t3";

$Power::PowerMenu["Deep Freezer", 5] = "Snowstorm\t[U] Snowstorm\t50\tAffinity2";
$Power::PowerInfo["Snowstorm"] = "[Ultimate Power] Blind the world in a violent snowstorm freezing and killing anyone caught outside in it's wrath [-100 Shadow Energy]\t3\t3\t3\t3";

//--------------------------------------------------------------------------
//Extra Powers
$Power::PowerInfo["EnergyBoost1"] = "Increases your max energy by 50\t4\t4\t4\t4";
$Power::PowerInfo["EnergyBoost2"] = "Increases your max energy by 50\t4\t4\t4\t4";
$Power::PowerInfo["EnergyBoost3"] = "Increases your max energy by 50\t4\t4\t4\t4";
$Power::PowerInfo["EnergyBoost4"] = "Increases your max energy by 75\t4\t4\t4\t4";
$Power::PowerInfo["EnergyBoost5"] = "Increases your max energy by 75\t4\t4\t4\t4";
$Power::PowerInfo["EnergyBoost6"] = "Increases your max energy by 100\t4\t4\t4\t4";
$Power::PowerInfo["EnergyBoost7"] = "Increases your max energy by 150\t4\t4\t4\t4";

$Power::PowerInfo["HPBoost1"] = "Increases your max health by 50\t4\t4\t4\t4";
$Power::PowerInfo["HPBoost2"] = "Increases your max health by 50\t4\t4\t4\t4";
$Power::PowerInfo["HPBoost3"] = "Increases your max health by 50\t4\t4\t4\t4";
$Power::PowerInfo["HPBoost4"] = "Increases your max health by 75\t4\t4\t4\t4";
$Power::PowerInfo["HPBoost5"] = "Increases your max health by 75\t4\t4\t4\t4";
$Power::PowerInfo["HPBoost6"] = "Increases your max health by 100\t4\t4\t4\t4";

//--------------------------------------------------------------------------
//RANK SYSTEM LEVELS
$PowerSave::RankCount = 34;
$PowerSave::MinEXP[0] = 0;
$PowerSave::Nextlevel[0] = 1;
$PowerSave::PointGain[0] = 0;
$PowerSave::MinEXP[1] = 25;
$PowerSave::Nextlevel[1] = 2;
$PowerSave::PointGain[1] = 1; //1
$PowerSave::MinEXP[2] = 50;
$PowerSave::Nextlevel[2] = 3;
$PowerSave::PointGain[2] = 1; //2
$PowerSave::MinEXP[3] = 75;
$PowerSave::Nextlevel[3] = 4;
$PowerSave::PointGain[3] = 2; //4
$PowerSave::MinEXP[4] = 100;
$PowerSave::Nextlevel[4] = 5;
$PowerSave::PointGain[4] = 2; //6
$PowerSave::MinEXP[5] = 125;
$PowerSave::Nextlevel[5] = 6;
$PowerSave::PointGain[5] = 2; //8
$PowerSave::MinEXP[6] = 175;
$PowerSave::Nextlevel[6] = 7;
$PowerSave::PointGain[6] = 2; //10
$PowerSave::MinEXP[7] = 225;
$PowerSave::Nextlevel[7] = 8;
$PowerSave::PointGain[7] = 3; //13
$PowerSave::MinEXP[8] = 375;
$PowerSave::Nextlevel[8] = 9;
$PowerSave::PointGain[8] = 3; //16
$PowerSave::MinEXP[9] = 425;
$PowerSave::Nextlevel[9] = 10;
$PowerSave::PointGain[9] = 3; //19
$PowerSave::LevelMessage[9] = "You show a promissing potential by reaching level 10, as a reward, your Energy has recieved a significant boost.";
$PowerSave::MinEXP[10] = 500;
$PowerSave::Nextlevel[10] = 11;
$PowerSave::PointGain[10] = 3; //22
$PowerSave::MinEXP[11] = 575;
$PowerSave::Nextlevel[11] = 12;
$PowerSave::PointGain[11] = 2; //25
$PowerSave::MinEXP[12] = 650;
$PowerSave::Nextlevel[12] = 13;
$PowerSave::PointGain[12] = 3; //28
$PowerSave::MinEXP[13] = 700;
$PowerSave::Nextlevel[13] = 14;
$PowerSave::PointGain[13] = 4; //32
$PowerSave::MinEXP[14] = 800;
$PowerSave::Nextlevel[14] = 15;
$PowerSave::PointGain[14] = 4; //36
$PowerSave::LevelMessage[14] = "Melvin has new powers avaliable to purchase! You should check it out!";
$PowerSave::MinEXP[15] = 900;
$PowerSave::Nextlevel[15] = 16;
$PowerSave::PointGain[15] = 4; //40
$PowerSave::MinEXP[16] = 1000;
$PowerSave::Nextlevel[16] = 17;
$PowerSave::PointGain[16] = 5; //45
$PowerSave::MinEXP[17] = 1250;
$PowerSave::Nextlevel[17] = 18;
$PowerSave::PointGain[17] = 5; //50
$PowerSave::MinEXP[18] = 1500;
$PowerSave::Nextlevel[18] = 19;
$PowerSave::PointGain[18] = 5; //55
$PowerSave::MinEXP[19] = 1750;
$PowerSave::Nextlevel[19] = 20;
$PowerSave::PointGain[19] = 5; //60
$PowerSave::LevelMessage[19] = "As you proceed in your power, your focus becomes deeper allowing a further boost in energy.";
$PowerSave::MinEXP[20] = 2000;
$PowerSave::Nextlevel[20] = 21;
$PowerSave::PointGain[20] = 5; //65
$PowerSave::MinEXP[21] = 2350;
$PowerSave::Nextlevel[21] = 22;
$PowerSave::PointGain[21] = 5; //70
$PowerSave::MinEXP[22] = 2700;
$PowerSave::Nextlevel[22] = 23;
$PowerSave::PointGain[22] = 6; //76
$PowerSave::MinEXP[23] = 3050;
$PowerSave::Nextlevel[23] = 24;
$PowerSave::PointGain[23] = 6; //82
$PowerSave::MinEXP[24] = 3500;
$PowerSave::Nextlevel[24] = 25;
$PowerSave::PointGain[24] = 6; //88
$PowerSave::MinEXP[25] = 4000;
$PowerSave::Nextlevel[25] = 26;
$PowerSave::PointGain[25] = 6; //94
$PowerSave::MinEXP[26] = 4500;
$PowerSave::Nextlevel[26] = 27;
$PowerSave::PointGain[26] = 6; //100
$PowerSave::LevelMessage[26] = "Melvin has new powers avaliable to purchase! You should check it out!";
$PowerSave::MinEXP[27] = 5000;
$PowerSave::Nextlevel[27] = 28;
$PowerSave::PointGain[27] = 7; //107
$PowerSave::MinEXP[28] = 6000;
$PowerSave::Nextlevel[28] = 29;
$PowerSave::PointGain[28] = 7; //114
$PowerSave::MinEXP[29] = 7000;
$PowerSave::Nextlevel[29] = 30;
$PowerSave::PointGain[29] = 7; //121
$PowerSave::MinEXP[30] = 8000;
$PowerSave::Nextlevel[30] = 31;
$PowerSave::PointGain[30] = 9; //130
$PowerSave::MinEXP[31] = 9000;
$PowerSave::Nextlevel[31] = 32;
$PowerSave::PointGain[31] = 10; //140
$PowerSave::MinEXP[32] = 10000;
$PowerSave::Nextlevel[32] = 33;
$PowerSave::PointGain[32] = 15; //155
$PowerSave::MinEXP[33] = 12000;
$PowerSave::Nextlevel[33] = 34;
$PowerSave::PointGain[33] = 15; //170
$PowerSave::MinEXP[34] = 15000;
$PowerSave::Nextlevel[34] = 35;
$PowerSave::PointGain[34] = 30; //200
$PowerSave::LevelMessage[34] = "Excellent, you have reached the end of the base class leveling, now hit the affinity classes! * Press [F2] -> My Page to promote to your affinity class!";

//  Affinity 1
$PowerSave::Affinity1RankCount = 34;
$PowerSave::Affinity1MinEXP[0] = 0;
$PowerSave::Affinity1Nextlevel[0] = 36;
$PowerSave::Affinity1PointGain[0] = 0;
$PowerSave::Affinity1LevelMessage[0] = "Welcome fully to the affinity levels, as you journey to level 70, you will expand your powerbase even further.. good luck and have fun!";
$PowerSave::Affinity1MinEXP[1] = 50;
$PowerSave::Affinity1Nextlevel[1] = 37;
$PowerSave::Affinity1PointGain[1] = 1; //1
$PowerSave::Affinity1MinEXP[2] = 75;
$PowerSave::Affinity1Nextlevel[2] = 38;
$PowerSave::Affinity1PointGain[2] = 1; //2
$PowerSave::Affinity1MinEXP[3] = 100;
$PowerSave::Affinity1Nextlevel[3] = 39;
$PowerSave::Affinity1PointGain[3] = 1; //3
$PowerSave::Affinity1MinEXP[4] = 125;
$PowerSave::Affinity1Nextlevel[4] = 40;
$PowerSave::Affinity1PointGain[4] = 1; //4
$PowerSave::Affinity1MinEXP[5] = 150;
$PowerSave::Affinity1Nextlevel[5] = 41;
$PowerSave::Affinity1PointGain[5] = 2; //6
$PowerSave::Affinity1MinEXP[6] = 175;
$PowerSave::Affinity1Nextlevel[6] = 42;
$PowerSave::Affinity1PointGain[6] = 2; //8
$PowerSave::Affinity1MinEXP[7] = 200;
$PowerSave::Affinity1Nextlevel[7] = 43;
$PowerSave::Affinity1PointGain[7] = 2; //10
$PowerSave::Affinity1MinEXP[8] = 250;
$PowerSave::Affinity1Nextlevel[8] = 44;
$PowerSave::Affinity1PointGain[8] = 2; //12
$PowerSave::Affinity1MinEXP[9] = 300;
$PowerSave::Affinity1Nextlevel[9] = 45;
$PowerSave::Affinity1PointGain[9] = 2; //14
$PowerSave::Affinity1LevelMessage[9] = "As you rise further in affinity power, you gain more energy to prosper upon, welcome to level 45.";
$PowerSave::Affinity1MinEXP[10] = 350;
$PowerSave::Affinity1Nextlevel[10] = 46;
$PowerSave::Affinity1PointGain[10] = 2; //16
$PowerSave::Affinity1MinEXP[11] = 425;
$PowerSave::Affinity1Nextlevel[11] = 47;
$PowerSave::Affinity1PointGain[11] = 2; //18
$PowerSave::Affinity1MinEXP[12] = 475;
$PowerSave::Affinity1Nextlevel[12] = 48;
$PowerSave::Affinity1PointGain[12] = 2; //20
$PowerSave::Affinity1MinEXP[13] = 525;
$PowerSave::Affinity1Nextlevel[13] = 49;
$PowerSave::Affinity1PointGain[13] = 2; //22
$PowerSave::Affinity1MinEXP[14] = 600;
$PowerSave::Affinity1Nextlevel[14] = 50;
$PowerSave::Affinity1PointGain[14] = 3; //25
$PowerSave::Affinity1MinEXP[15] = 700;
$PowerSave::Affinity1Nextlevel[15] = 51;
$PowerSave::Affinity1PointGain[15] = 3; //28
$PowerSave::Affinity1MinEXP[16] = 800;
$PowerSave::Affinity1Nextlevel[16] = 52;
$PowerSave::Affinity1PointGain[16] = 3; //31
$PowerSave::Affinity1MinEXP[17] = 1000;
$PowerSave::Affinity1Nextlevel[17] = 53;
$PowerSave::Affinity1PointGain[17] = 3; //34
$PowerSave::Affinity1MinEXP[18] = 1250;
$PowerSave::Affinity1Nextlevel[18] = 54;
$PowerSave::Affinity1PointGain[18] = 3; //37
$PowerSave::Affinity1MinEXP[19] = 1500;
$PowerSave::Affinity1Nextlevel[19] = 55;
$PowerSave::Affinity1PointGain[19] = 3; //40
$PowerSave::Affinity1LevelMessage[19] = "As your affinity flow of power continously grows, so does your powers, and your energy. You are now level 55.";
$PowerSave::Affinity1MinEXP[20] = 1700;
$PowerSave::Affinity1Nextlevel[20] = 56;
$PowerSave::Affinity1PointGain[20] = 3; //43
$PowerSave::Affinity1MinEXP[21] = 2000;
$PowerSave::Affinity1Nextlevel[21] = 57;
$PowerSave::Affinity1PointGain[21] = 3; //46
$PowerSave::Affinity1MinEXP[22] = 2500;
$PowerSave::Affinity1Nextlevel[22] = 58;
$PowerSave::Affinity1PointGain[22] = 3; //49
$PowerSave::Affinity1MinEXP[23] = 3000;
$PowerSave::Affinity1Nextlevel[23] = 59;
$PowerSave::Affinity1PointGain[23] = 3; //52
$PowerSave::Affinity1MinEXP[24] = 3500;
$PowerSave::Affinity1Nextlevel[24] = 60;
$PowerSave::Affinity1PointGain[24] = 4; //56
$PowerSave::Affinity1MinEXP[25] = 4000;
$PowerSave::Affinity1Nextlevel[25] = 61;
$PowerSave::Affinity1PointGain[25] = 4; //60
$PowerSave::Affinity1MinEXP[26] = 4750;
$PowerSave::Affinity1Nextlevel[26] = 62;
$PowerSave::Affinity1PointGain[26] = 4; //64
$PowerSave::Affinity1MinEXP[27] = 5500;
$PowerSave::Affinity1Nextlevel[27] = 63;
$PowerSave::Affinity1PointGain[27] = 4; //68
$PowerSave::Affinity1MinEXP[28] = 6250;
$PowerSave::Affinity1Nextlevel[28] = 64;
$PowerSave::Affinity1PointGain[28] = 5; //73
$PowerSave::Affinity1MinEXP[29] = 7000;
$PowerSave::Affinity1Nextlevel[29] = 65;
$PowerSave::Affinity1PointGain[29] = 5; //78
$PowerSave::Affinity1MinEXP[30] = 8000;
$PowerSave::Affinity1Nextlevel[30] = 66;
$PowerSave::Affinity1PointGain[30] = 5; //83
$PowerSave::Affinity1MinEXP[31] = 9000;
$PowerSave::Affinity1Nextlevel[31] = 67;
$PowerSave::Affinity1PointGain[31] = 5; //88
$PowerSave::Affinity1MinEXP[32] = 10000;
$PowerSave::Affinity1Nextlevel[32] = 68;
$PowerSave::Affinity1PointGain[32] = 5; //93
$PowerSave::Affinity1MinEXP[33] = 12500;
$PowerSave::Affinity1Nextlevel[33] = 69;
$PowerSave::Affinity1PointGain[33] = 5; //98
$PowerSave::Affinity1MinEXP[34] = 15000;
$PowerSave::Affinity1Nextlevel[34] = 70;
$PowerSave::Affinity1PointGain[34] = 5; //103
$PowerSave::Affinity1LevelMessage[34] = "Don't utter the sigh of relief just yet! now enter your second Affinity class!";

//  Affinity 2
$PowerSave::Affinity2RankCount = 29;
$PowerSave::Affinity2MinEXP[0] = 0;
$PowerSave::Affinity2Nextlevel[0] = 71;
$PowerSave::Affinity2PointGain[0] = 0;
$PowerSave::Affinity2LevelMessage[0] = "Welcome to the second affinity levels, your final destination, level 100, have fun, and go pwn those noobs!";
$PowerSave::Affinity2MinEXP[1] = 50;
$PowerSave::Affinity2Nextlevel[1] = 72;
$PowerSave::Affinity2PointGain[1] = 1; //1
$PowerSave::Affinity2MinEXP[2] = 75;
$PowerSave::Affinity2Nextlevel[2] = 73;
$PowerSave::Affinity2PointGain[2] = 1; //2
$PowerSave::Affinity2MinEXP[3] = 100;
$PowerSave::Affinity2Nextlevel[3] = 74;
$PowerSave::Affinity2PointGain[3] = 1; //3
$PowerSave::Affinity2MinEXP[4] = 125;
$PowerSave::Affinity2Nextlevel[4] = 75;
$PowerSave::Affinity2PointGain[4] = 1; //4
$PowerSave::Affinity2MinEXP[5] = 150;
$PowerSave::Affinity2Nextlevel[5] = 76;
$PowerSave::Affinity2PointGain[5] = 2; //6
$PowerSave::Affinity2MinEXP[6] = 200;
$PowerSave::Affinity2Nextlevel[6] = 77;
$PowerSave::Affinity2PointGain[6] = 2; //8
$PowerSave::Affinity2MinEXP[7] = 250;
$PowerSave::Affinity2Nextlevel[7] = 78;
$PowerSave::Affinity2PointGain[7] = 2; //10
$PowerSave::Affinity2MinEXP[8] = 300;
$PowerSave::Affinity2Nextlevel[8] = 79;
$PowerSave::Affinity2PointGain[8] = 2; //12
$PowerSave::Affinity2MinEXP[9] = 400;
$PowerSave::Affinity2Nextlevel[9] = 80;
$PowerSave::Affinity2PointGain[9] = 2; //14
$PowerSave::Affinity2LevelMessage[9] = "As you rise further in affinity power, you gain more energy to prosper upon, welcome to level 80.";
$PowerSave::Affinity2MinEXP[10] = 500;
$PowerSave::Affinity2Nextlevel[10] = 81;
$PowerSave::Affinity2PointGain[10] = 2; //16
$PowerSave::Affinity2MinEXP[11] = 700;
$PowerSave::Affinity2Nextlevel[11] = 82;
$PowerSave::Affinity2PointGain[11] = 2; //18
$PowerSave::Affinity2MinEXP[12] = 1000;
$PowerSave::Affinity2Nextlevel[12] = 83;
$PowerSave::Affinity2PointGain[12] = 2; //20
$PowerSave::Affinity2MinEXP[13] = 1500;
$PowerSave::Affinity2Nextlevel[13] = 84;
$PowerSave::Affinity2PointGain[13] = 2; //22
$PowerSave::Affinity2MinEXP[14] = 2000;
$PowerSave::Affinity2Nextlevel[14] = 85;
$PowerSave::Affinity2PointGain[14] = 3; //25
$PowerSave::Affinity2MinEXP[15] = 3000;
$PowerSave::Affinity2Nextlevel[15] = 86;
$PowerSave::Affinity2PointGain[15] = 3; //28
$PowerSave::Affinity2MinEXP[16] = 4000;
$PowerSave::Affinity2Nextlevel[16] = 87;
$PowerSave::Affinity2PointGain[16] = 3; //31
$PowerSave::Affinity2MinEXP[17] = 6000;
$PowerSave::Affinity2Nextlevel[17] = 88;
$PowerSave::Affinity2PointGain[17] = 3; //34
$PowerSave::Affinity2MinEXP[18] = 8000;
$PowerSave::Affinity2Nextlevel[18] = 89;
$PowerSave::Affinity2PointGain[18] = 3; //37
$PowerSave::Affinity2MinEXP[19] = 10000;
$PowerSave::Affinity2Nextlevel[19] = 90;
$PowerSave::Affinity2PointGain[19] = 3; //40
$PowerSave::Affinity2LevelMessage[19] = "As your affinity flow of power continously grows, so does your powers, and your energy. You are now level 90, 10 to go!!!";
$PowerSave::Affinity2MinEXP[20] = 13000;
$PowerSave::Affinity2Nextlevel[20] = 91;
$PowerSave::Affinity2PointGain[20] = 3; //43
$PowerSave::Affinity2MinEXP[21] = 17000;
$PowerSave::Affinity2Nextlevel[21] = 92;
$PowerSave::Affinity2PointGain[21] = 3; //46
$PowerSave::Affinity2MinEXP[22] = 22000;
$PowerSave::Affinity2Nextlevel[22] = 93;
$PowerSave::Affinity2PointGain[22] = 3; //49
$PowerSave::Affinity2MinEXP[23] = 30000;
$PowerSave::Affinity2Nextlevel[23] = 94;
$PowerSave::Affinity2PointGain[23] = 3; //52
$PowerSave::Affinity2MinEXP[24] = 40000;
$PowerSave::Affinity2Nextlevel[24] = 95;
$PowerSave::Affinity2PointGain[24] = 4; //56
$PowerSave::Affinity2MinEXP[25] = 50000;
$PowerSave::Affinity2Nextlevel[25] = 96;
$PowerSave::Affinity2PointGain[25] = 4; //60
$PowerSave::Affinity2MinEXP[26] = 65000;
$PowerSave::Affinity2Nextlevel[26] = 97;
$PowerSave::Affinity2PointGain[26] = 4; //64
$PowerSave::Affinity2MinEXP[27] = 80000;
$PowerSave::Affinity2Nextlevel[27] = 98;
$PowerSave::Affinity2PointGain[27] = 4; //68
$PowerSave::Affinity2MinEXP[28] = 100000;
$PowerSave::Affinity2Nextlevel[28] = 99;
$PowerSave::Affinity2PointGain[28] = 5; //73
$PowerSave::Affinity2MinEXP[29] = 150000;
$PowerSave::Affinity2Nextlevel[29] = 100;
$PowerSave::Affinity2PointGain[29] = 5; //78
$PowerSave::Affinity2LevelMessage[29] = "Your final honor has been reached, sound your cheer! you have reached the almighty 100!!!";
