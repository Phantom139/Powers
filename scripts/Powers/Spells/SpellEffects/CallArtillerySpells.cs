//Raycast functions
function objectFrRaycast(%transform) {
   %obj = getWord(%transform, 0);
   if(isObject(%obj)) {
      return %obj;
   }
}

function GetRandomPosition(%mult,%nz) {
   %x = getRandom()*%mult;
   %y = getRandom()*%mult;
   %z = getRandom()*%mult;

   %rndx = getrandom(0,1);
   %rndy = getrandom(0,1);
   %rndz = getrandom(0,1);

   if(%nz)
      %z = 5;

   if (%rndx == 1){
      %negx = -1;
   }
   if (%rndx == 0){
      %negx = 1;
   }
   if (%rndy == 1){
      %negy = -1;
   }
   if (%rndy == 0){
      %negy = 1;
   }
   if (%rndz == 1){
      %negz = -1;
   }
   if (%rndz == 0){
      %negz = 1;
   }

   %rand = %negx * %x SPC %negy * %y SPC %Negz * %z;
   return %rand;
}

function posFrRaycast(%transform)
{
   %position = getWord(%transform, 1) @ " " @ getWord(%transform, 2) @ " " @ getWord(%transform, 3);
   return %position;
}

function normalFrRaycast(%transform)
{
   %norm = getWord(%transform, 4) @ " " @ getWord(%transform, 5) @ " " @ getWord(%transform, 6);
   return %norm;
}
//

datablock StaticShapeData(ArtilleryRet) : StaticShapeDamageProfile {
	className = "decoration";
	shapeFile = "reticle_bomber.dts";

	maxDamage      = 0.5;
	destroyedLevel = 0.5;
	disabledLevel  = 0.3;

    invincible = true;

	explosion      = HandGrenadeExplosion;
	expDmgRadius = 1.0;
	expDamage = 0.05;
	expImpulse = 200;

	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSensorIcon;
	cmdMiniIconName = "commander/MiniIcons/com_deploymotionsensor";
	targetNameTag = 'Orders';
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
	needsPower = true;

    collidable = false;
};

function PowersEyeCast(%source) {
   %pos        = getWords(%source.getEyeTransform(), 0, 2);
   %vec        = %source.getEyeVector();
   %targetpos  = vectoradd(%pos,vectorscale(%vec,99999));
   %targetPos  = getWords(containerraycast(%pos,%targetpos,$ArtilleryMask, %source), 1, 3);
   return %targetPos;
}

function CallArtillerySpell(%source, %spell, %slot, %radius, %time) {
   if(!%source.isAlive()) {
      return; //must be alive!
   }
   //Declare The Raycast
   %targetPos  = PowersEyeCast(%source);
   //And lastly, use it.
   // Place the target bib (Marker)
   %reticle = new StaticShape() {
      datablock = "ArtilleryRet";
      position = %targetPos;
      team = %source.team;
      scale = ""@%radius@" "@%radius@" 1";
   };
   MissionCleanup.add(%reticle);
   %reticle.schedule(%time * 1000, "delete");
   //Cast
   switch$(%spell) {
      case "IceBomb":
         DoIceBomb(%source, %targetPos);
      case "Lightning":
         DoLightning(%source, %targetPos);
      case "Hail":
         DoHailArtillery(%source, %targetPos);
      case "Hailstorm":
         DoHailstormArtillery(%source, %targetPos);
      case "ShadowBombDrop":
         DoShadowBombDrop(%source, %targetPos);
      case "ShadowArtillery":
         DoShadowArtillery(%source, %targetPos);
      case "ShadowBrigade":
         DoShadowBrigade(%source, %targetPos);
      case "ShadowStorm":
         DoShadowStorm(%source, %targetPos);
      case "Avalanche":
         DoAvalanche(%source, %targetPos);
      case "DeepFreeze":
         DoDeepFreeze(%source, %targetPos);
      case "AsteroidFall":
         DoAsteroidFall(%source, %targetPos);
      case "Desolation":
         DropDesolation(%source, %targetPos);
      case "SunRay":
         DoSunRay(%source, %targetPos);
      case "DegaussianStrike":
         DoDegaussianStrike(%source, %targetPos);
      case "ShardStorm":
         DoShardStorm(%source, %targetPos);
      case "EnviousDownpour":
         DoEnviousDownpour(%source, %targetPos);
      case "SpearShower":
         DoSpearShower(%source, %targetPos);
      case "SunStorm":
         DoSunStorm(%source, %targetPos);
   }
}
