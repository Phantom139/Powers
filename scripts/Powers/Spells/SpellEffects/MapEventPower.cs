//MapEventPower.cs
//Phantom139
//Special Powers that occupy the entire map

datablock StaticShapeData(Hoverpad) : StaticShapeDamageProfile {
	className = "spine";
	shapeFile = "Pmiscf.dts";

	maxDamage      = 10000.5;
	destroyedLevel = 10000.5;
	disabledLevel  = 10000.3;

	isShielded = true;
	energyPerDamagePoint = 240;
	maxEnergy = 50;
	rechargeRate = 0.25;

	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;

	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
	needsPower = true;
};

function HoverPlayer(%player) {
   %player.hoverPad = new StaticShape() {
      dataBlock = Hoverpad;
      scale = ".55 .55 1";
      position = "0 0 0";
   };
   %player.hoverPad.setCloaked(true);
   
   %player.setMoveState(true);
   
   %trans = %player.getTransform();
   %Vec = vectoradd(%trans, "0 0 -2");
   %player.hoverPad.settransform(%Vec);
   
   schedule(1000, 0, KillHoverpad, %player, false);
}

function KillHoverpad(%player, %trueKill) {
   if(!%trueKill) {
      if(!isObject(%player.hoverPad)) {
         %player.setMoveState(false);
         return;
      }
      //player alive loop
      if(!isObject(%player) || %player.getState() $= "Dead") {
         if(isObject(%player.hoverPad)) {
            %player.hoverPad.delete();
         }
         return;
      }
      schedule(1000, 0, KillHoverpad, %player, false);
   }
   else {
      %player.setMoveState(false);
      if(!isObject(%player.hoverPad)) {
         return;
      }
      %player.hoverPad.delete();
   }
}

if(!isObject($MapManager)) {
   $MapManager = new ScriptObject() {
      class = "MapEventManager";
   };
}

function activateMapEventPower(%obj, %power) {
   %this = $MapManager;
   if(%this.powerState) {
      //trip off the "someone else if being ultra powerful" message
      return false;
   }
   else {
      %this.setPowerState(%obj, %power, true);
      //special call function
      %call = %this @".useMapPower_"@%power@"("@%obj@");";
      eval(%call);
      return true;
   }
}

function MapEventManager::setPowerState(%this, %user, %power, %state) {
   %this.powerState = %state;
   %this.activePower = %power;
   %this.powerUser = %user;
}

//all of the map event powers call this at some point.
function MapEventManager::beginHolderCheckup(%this, %player, %duration) {
   if(%duration <= 0) {
      %this.setPowerState("", "", false);
      return;
   }
   //
   if(!isObject(%player) || %player.getState() $= "dead") {
      %power = %this.activePower;
   
      %this.setPowerState("", "", false); //reset!
   
      %call = %this @".handleDeath_"@%power@"("@%player@");";
      eval(%call);
      return;
   }
   //
   %this.schedule(1000, beginHolderCheckup, %player, %duration - 1000);
}
