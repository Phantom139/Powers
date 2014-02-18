datablock AudioProfile(WarpGunTeleportSound) {
   filename    = "fx/powered/vehicle_pad_on.wav";
   description = AudioClose3d;
   preload = true;
};

function Teleport(%obj, %slot) {
   %range = 9999; //Max range someone can warp to.
   %rangeFromFlag = 150; //Meters from enemy Flag must be to work. Prevents Flag Capping with WarpGun
   %radius = 0.5;
   %rot = getWords(%obj.getTransform(), 3, 6);
   %muzzlePos = %obj.getMuzzlePoint(%slot);
   %muzzleVec = %obj.getMuzzleVector(%slot);

   %endPos    = VectorAdd(%muzzlePos, VectorScale(%muzzleVec, %range));

   %damageMasks = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType |
                  $TypeMasks::StationObjectType | $TypeMasks::GeneratorObjectType |
                  $TypeMasks::SensorObjectType | $TypeMasks::TurretObjectType |
                  $TypeMasks::InteriorObjectType;

   %hit = ContainerRayCast(%muzzlePos, %endPos, %damageMasks | $TypeMasks::TerrainObjectType, %obj);
   %x2 = getWord(%hit, 1);
   %y2 = getWord(%hit, 2);
   %z2 = getWord(%hit, 3);
   if ((%hit) && (%z2 > 15)) {
      %count = 0;
      %endpos = getWords(%hit.getWorldBoxCenter(), 0, 2);
      while (%count < 5) {
         %z2 += 2;
         %check1 = %x2 SPC %y2 SPC %z2;
         InitContainerRadiusSearch(%check1, %radius, %damageMasks);
         %checkok = 1;
         %targetObject = containerSearchNext();
         if (%targetObject) {
            %count++;
         }
         else {
            %checkok = 0;
            %count = 5;
         }
      }
      if (%checkok == 0) {
         if (%obj.team == 1) {
            %team1 = 2;
         }
         else {
            %team1 = 1;
         }
         %enemyFlag = $TeamFlag[%team1];
         if (isObject(%enemyFlag)) {
            %dist = VectorDist(posfromTransform(%enemyFlag.getTransform()), %check1);
            %dist2 = VectorDist(posfromTransform(%enemyFlag.getTransform()), %obj.getMuzzlePoint(%slot));
            if ((%dist < %rangeFromFlag) || (%dist2 < %rangeFromFlag) || (%enemyFlag.carrier == %obj)) {
               %checkok = 1;
               %checkok2 = 1;
               messageClient(%obj.client, 'msgBlocked', '\c1Interference from Flag.');
            }
         }
      }
      if (%checkok == 0) {
         %obj.setEnergyLevel(0);
         %newtrans = %check1 SPC %rot;
         %obj.setWhiteout(0.3);
         %obj.setVelocity("0 0 0");
         %obj.setTransform(%newtrans);
         ServerPlay3d(WarpGunTeleportSound, %obj.getPosition());
         schedule(100, 0, "ServerPlay3d", WarpGunTeleportSound, %newtrans);
      }
      else if(%checkok2 != 1) {
         messageClient(%obj.client, 'msgBlocked', '\c1target location is blocked.');
         %obj.setEnergyLevel(100);
      }
   }
   else {
      messageClient(%obj.client, 'msgNothing', '\c1Cant find anything.');
      %obj.setEnergyLevel(100);
   }
}
