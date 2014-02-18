//Core/KeyStrokes.cs
//Phantom139

//Handles keystrokes.

function MineKeyStroke(%this, %data) {
   if(%this.usingWitchWeapon == 1) {
      CycleWitchModes(%this, %data); //Primary Modes
   }
   if(%this.usingDemonWeapon == 1) {
      CycleDemonModes(%this, %data); //Primary Modes
   }
   if(%this.usingPhantomStaff == 1) {
      CyclePhantomModes(%this, %data); //Primary Modes
   }
   if(%this.usingHunterBow == 1) {
      CycleHunterModes(%this, %data); //Primary Modes
   }
   //
   if(%this.usingPikeOfFaith == 1) {
      CycleGuardianModes(%this, %data); //Primary Modes
   }
   if(%this.usingStarLighterStaff == 1) {
      CycleStarLighterModes(%this, %data); //Primary Modes
   }
   if(%this.usingAsthenieSword == 1) {
      CycleDevastatorModes(%this, %data); //Primary Modes
   }
   if(%this.usingStaffofEmbirthia == 1) {
      CycleEnforcerModes(%this, %data); //Primary Modes
   }
   if(%this.usingOverseerSword == 1) {
      CycleOverseerModes(%this, %data); //Primary Modes
   }
   if(%this.usingCryonicSpear == 1) {
      CycleCryoniumModes(%this, %data); //Primary Modes
   }
   //
   if(%this.usingHolyCannon == 1) {
      CycleGladiatorModes(%this, %data);
   }
   if(%this.usingStarforceCannon == 1) {
      CycleStarSighterModes(%this, %data);
   }
   if(%this.usingCrystalLance == 1) {
      CycleProspectorModes(%this, %data);
   }
   if(%this.usingVolcanicCannon == 1) {
      CycleAnnihilatorModes(%this, %data);
   }
   if(%this.usingArcticCannon == 1) {
      CycleDeepFreezerModes(%this, %data);
   }
   if(%this.usingShadowLance == 1) {
      CyclePhantomLordModes(%this, %data);
   }
}
