datablock ForceFieldBareData(IceField) : StaticShapeDamageProfile {
  fadeMS = 1000;
  baseTranslucency = 0.9;
  powerOffTranslucency = 0.0;
  dynamicType = $TypeMasks::DamagableItemObjectType;

  teamPermiable = false;
  otherPermiable = false;
  color = "0.6 0.7 0.8";
  powerOffColor = "0.6 0.7 0.8";

  cmdIcon = CMDSwitchIcon;
  cmdCategory = "DSupport";
  cmdMiniIconName = "commander/MiniIcons/com_switch_grey";

  texture[0] = "liquidtiles/bluewater";

  framesPerSec = 1;
  numFrames = 1;
  scrollSpeed = 0.1;
  umapping = 0.1;
  vmapping = 0.1;

};

function Thaw(%obj) {
   %obj.isFrozen = false;
}

function FreezeObject(%obj, %freezeTimeMS, %StripGuns) {
  if(%obj.isFrozen) {
    // Don't spawn another forcefile if one is already there
  }

  if(isPlayer(%obj)) {
     if(%obj.antiFreeze) {
        return; // I block yer freeze :D
     }
  }

  if(!%obj.isFrozen) {
     %Icefield = new ForceFieldBare() {
        rotation = "0 0 0 1";
        scale = "7 7 6";
        position =  vectorAdd(%obj.getWorldBoxCenter(), "-5 -5 -6");
        dataBlock = "IceField";
        team = 99;
     };
     MissionCleanup.add(%Icefield);
     %IceField.schedule(1000, "delete");
 	 if (isObject(%IceField.pzone)) {
        %IceField.pzone.delete();
     }
     %obj.isFrozen = true;
	 if( %obj.getType() & $TypeMasks::PlayerObjectType) {
        messageClient(%obj.client, 'MsgFrost', "You have been frozen");
        schedule(%freezeTimeMS, 0, "MessageClient", %obj.client, 'msgThaw', "The Freeze Effect has lifted");
        %pos = %obj.getWorldBoxCenter();
        %obj.setMoveState(true);
   	    %obj.schedule(%freezeTimeMS, "setMoveState", false);
        if(%StripGuns) {
           %obj.clearInventory();
           schedule(%freezeTimeMS, 0, "buyFavorites", %obj.client);
        }
     }
     schedule(%freezeTimeMS, %obj, "Thaw", %obj);
  }
}
