//Vortex comes in two forms

//Unbounded -> No Time restriction (used by powers like frozen twister [cont. cast])
//Bounded -> a set time limit (FireBall 5, FireBall 6)

function DoUnboundedVortex(%obj, %position, %SuckRadius, %externRadius, %force) {
   %TargetSearchMask = $TypeMasks::PlayerObjectType;
   InitContainerRadiusSearch(%position, %SuckRadius, %TargetSearchMask);

   while ((%potentialTarget = ContainerSearchNext()) != 0) {
      if(CanAOEHit(%position, %suckRadius, %potentialTarget)) {
         %dist = containerSearchCurrRadDamageDist();
         %tgtPos = %potentialTarget.getWorldBoxCenter();
         %distance2 =VectorDist(%tgtPos,%position);
         %distance = mfloor(%distance2);
         %vec = VectorNormalize(VectorSub(%position,%tgtpos));
         %nForce = (-1)*(%force);                              //come here =-D

         %forceVector = vectorScale(%vec, %force);
         if(%potentialTarget.team != %obj.team) {
            if (%potentialTarget.getPosition() != %position) {
               if(VectorDist(%potentialTarget.getPosition(), %position) > %externRadius) {
                  %potentialTarget.applyImpulse(%potentialTarget.getPosition(), %forceVector);
               }
            }
         }
      }
   }
}

function DoBoundedVortex(%obj, %position, %SuckRadius, %externRadius, %force, %duration, %counter) {
   if(%counter >= %duration) {
      return;
   }
   %counter++;
   DoUnboundedVortex(%obj, %position, %SuckRadius, %externRadius, %force);
   schedule(100, 0, "DoBoundedVortex", %obj, %position, %SuckRadius, %externRadius, %force, %duration, %counter);
}
