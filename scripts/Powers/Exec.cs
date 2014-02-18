//Exec.cs
//Phantom139

//Loads the mod's scripts
setPerfCounterEnable(0);

exec("scripts/Powers/Core/Globals.cs");
exec("scripts/Powers/Core/scoremenucmds.cs");
exec("scripts/Powers/Core/Functions.cs");
exec("scripts/Powers/Core/experience.cs");
exec("scripts/Powers/Core/KeyStrokes.cs");
exec("scripts/Powers/Core/UniversalSupport.cs");
exec("scripts/Powers/Core/dataSave.cs");
exec("scripts/Powers/Core/AdminFunctions.cs");

exec("scripts/Powers/Classes/ClassSelection.cs");
exec("scripts/Powers/Classes/PowerStore.cs");
exec("scripts/Powers/Classes/CoreStats.cs");

exec("scripts/Powers/chatCommands.cs");
exec("scripts/Powers/LoadScreen.cs");
exec("scripts/Powers/Functions.cs");

//Weapons
LoadSpellEffects(); //load these first
exec("scripts/weapons/DemonGun.cs");
exec("scripts/weapons/PhantomGun.cs");
exec("scripts/weapons/WitchGun.cs");
exec("scripts/weapons/HunterBow.cs");

exec("scripts/weapons/PikeOfFaith.cs");
exec("scripts/weapons/HarbingerofWar.cs");
exec("scripts/weapons/AsthenieSword.cs");
exec("scripts/weapons/CryonicSpear.cs");
exec("scripts/weapons/OverseerSword.cs");
exec("scripts/weapons/StaffOfEmbrithia.cs");

exec("scripts/weapons/HolyCannon.cs");
exec("scripts/weapons/StarforceCannon.cs");
exec("scripts/weapons/CrystalLance.cs");
exec("scripts/weapons/VolcanicCannon.cs");
exec("scripts/weapons/ArcticCannon.cs");
exec("scripts/weapons/ShadowLance.cs");

// -- LoadRanksBase(); //load the base

//Load for good fortitude.
LoadWitchPowers();
LoadDemonPowers();
LoadPhantomPowers();
LoadHunterPowers();
//----------
LoadGuardianPowers();
LoadStarLighterPowers();
LoadDevastatorPowers();
LoadEnforcerPowers();
LoadCryoniumPowers();
LoadOverseerPowers();
//----------
LoadGladiatorPowers();
LoadStarSighterPowers();
LoadProspectorPowers();
LoadAnnihilatorPowers();
LoadDeepFreezerPowers();
LoadPhantomLordPowers();
