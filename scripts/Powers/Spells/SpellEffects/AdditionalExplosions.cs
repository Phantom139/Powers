//use this when creating a "n-th" explosion on a projectile
function nthExplosion(%source, %position, %datablock) {
   if(!isObject(Game)) {
      return; //redundancy check, may not be needed, but it's better to play it safely :P
   }
   %cnS = %datablock.className;
   %type = strReplace(%cnS, "Data", "");
   %projectileType = %type;
   //
   %explosion = new (%projectileType)() {
      datablock = %datablock;
      initialPosition = vectorAdd(%position, "0 0 .01"); //add inf-small value
      initialDirection = "0 0 -10";
   };
   %explosion.sourceObject = %source;
   MissionCleanup.add(%explosion);
}
