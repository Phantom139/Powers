function PowersRandChance(%pct_chance) {
   //between 1 and 100
   if(!isObject(randomChance)) {
      new scriptObject(randomChance) {};
   }
   //generate %pct_chance non-similar values
   for(%i = 0; %i < %pct_chance; %i++) {
      %numberToTest = getRandom(1, 100);
      randomChance.value[%i] = %numberToTest;
   }
   //First time using a three layer loop :X -Phantom139
   for(%x = 0; %x < %pct_chance; %x++) {
      for(%y = 0; %y < %pct_chance; %y++) {
         for(%z = 0; %z < %pct_chance; %z++) {
            if(randomChance.value[%y] == randomChance.value[%z]) {
               if(%y != %z) {
                  // houston, we have a match, generate a second value
                  randomChance.value[%z] = getRandom(1, 100);
               }
            }
         }
      }
   }
   //we should now have %pct_chance different values
   //one final time through the loop :P
   %test = getRandom(1, 100);
   for(%f = 0; %f < %pct_chance; %f++) {
      if(randomChance.value[%f] == %test) {
         //according to tom petty: You Got Lucky Babe!!!
         return 1;
      }
   }
   return 0;
}
