//DamageZone.cs
//Phantom139
//Powers Mod 2.2

//Proper Radius Damage Function for AOE Spells
function AOEDamage(%source, %position, %radius, %damage, %damageType) {
   InitContainerRadiusSearch(%position, %radius, $TypeMasks::PlayerObjectType      |
                                                 $TypeMasks::VehicleObjectType     |
                                                 $TypeMasks::StaticShapeObjectType |
                                                 $TypeMasks::TurretObjectType      |
                                                 $TypeMasks::ItemObjectType);
   //Get all of the targets in the affected zone
   %numTargets = 0;
   while ((%targetObject = containerSearchNext()) != 0) {
      %dist = containerSearchCurrRadDamageDist();
      if(%dist > %radius) {
         //ignore.
         continue;
      }
      else {
         %targets[%numTargets]     = %targetObject;
         %targetDists[%numTargets] = %dist;
         %numTargets++;
      }
   }
   //now apply the damage
   for (%i = 0; %i < %numTargets; %i++) {
      %targetObject = %targets[%i];
      %dist = %targetDists[%i];

      %coverage = calcExplosionCoverage(%position, %targetObject,
                                        ($TypeMasks::InteriorObjectType |
                                         $TypeMasks::TerrainObjectType |
                                         $TypeMasks::ForceFieldObjectType |
                                         $TypeMasks::VehicleObjectType));
      if (%coverage == 0) {
         continue;
      }
      else {
         %data = %targetObject.getDatablock();
         %amount = (1.0 - ((%dist / %radius) * 0.88)) * %coverage * %damage;
         if(%amount > 0) {
            %data.damageObject(%targetObject, %source, %position, %amount, %damageType);
         }
      }
   }
}

function CanAOEHit(%position, %radius, %target) {
   if(vectorDist(%position, %target.getPosition()) > %radius) {
      return false;
   }
   else {
      %coverage = calcExplosionCoverage(%position, %target,
                                        ($TypeMasks::InteriorObjectType |
                                         $TypeMasks::TerrainObjectType |
                                         $TypeMasks::ForceFieldObjectType |
                                         $TypeMasks::VehicleObjectType));
      if (%coverage == 0) {
         return false;
      }
      else {
         return true;
      }
   }
}
