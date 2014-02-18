$HandInvThrowTimeout = 0.8 * 1000; // 1/2 second between throwing grenades or mines

// z0dd - ZOD, 9/13/02. Added global array for serverside weapon reticles and "visible"
$WeaponsHudData[0, bitmapName] = "gui/hud_blaster";
$WeaponsHudData[0, itemDataName] = "Blaster";
//$WeaponsHudData[0, ammoDataName] = "";
$WeaponsHudData[0, reticle] = "gui/ret_blaster";
$WeaponsHudData[0, visible] = "true";

$WeaponsHudData[1, bitmapName] = "gui/hud_blaster";
$WeaponsHudData[1, itemDataName] = "DemonWeapon";
$WeaponsHudData[1, reticle] = "gui/hud_ret_targlaser";
$WeaponsHudData[1, visible] = "false";

$WeaponsHudData[2, bitmapName] = "gui/hud_blaster";
$WeaponsHudData[2, itemDataName] = "PhantomGun";
$WeaponsHudData[2, reticle] = "gui/hud_ret_targlaser";
$WeaponsHudData[2, visible] = "false";

$WeaponsHudData[3, bitmapName] = "gui/hud_blaster";
$WeaponsHudData[3, itemDataName] = "WitchWeapon";
$WeaponsHudData[3, reticle] = "gui/hud_ret_targlaser";
$WeaponsHudData[3, visible] = "false";

$WeaponsHudData[4, bitmapName]   = "gui/hud_targetlaser";
$WeaponsHudData[4, itemDataName] = "TargetingLaser";
//$WeaponsHudData[9, ammoDataName] = "";
$WeaponsHudData[4, reticle] = "gui/hud_ret_targlaser";
$WeaponsHudData[4, visible] = "false";

$WeaponsHudCount = 5;
$AmmoIncrement[Mine]                = 3;
$AmmoIncrement[Grenade]             = 5;
$AmmoIncrement[FlashGrenade]        = 5;
$AmmoIncrement[FlareGrenade]        = 5;
$AmmoIncrement[ConcussionGrenade]   = 5;
$AmmoIncrement[RepairKit]           = 1;

// -------------------------------------------------------------------
// z0dd - ZOD, 4/17/02. Addition. Ammo pickup fix, these were missing.
$AmmoIncrement[CameraGrenade]       = 2;
$AmmoIncrement[Beacon]              = 1;

//----------------------------------------------------------------------------
// Weapons scripts
//--------------------------------------

// --- Mounting weapons
exec("scripts/weapons/T2Guns/blaster.cs");
exec("scripts/weapons/T2Guns/plasma.cs");
exec("scripts/weapons/T2Guns/chaingun.cs");
exec("scripts/weapons/T2Guns/disc.cs");
exec("scripts/weapons/T2Guns/grenadeLauncher.cs");
exec("scripts/weapons/T2Guns/sniperRifle.cs");
exec("scripts/weapons/T2Guns/ELFGun.cs");
exec("scripts/weapons/T2Guns/mortar.cs");
exec("scripts/weapons/T2Guns/missileLauncher.cs");
exec("scripts/weapons/T2Guns/targetingLaser.cs");
exec("scripts/weapons/T2Guns/shockLance.cs");

// --- Throwing weapons
exec("scripts/weapons/Grenades/mine.cs");
exec("scripts/weapons/Grenades/grenade.cs");
exec("scripts/weapons/Grenades/flashGrenade.cs");
exec("scripts/weapons/Grenades/flareGrenade.cs");
exec("scripts/weapons/Grenades/concussionGrenade.cs");
exec("scripts/weapons/Grenades/cameraGrenade.cs");

//----------------------------------------------------------------------------

function Weapon::onUse(%data, %obj)
{
   if(Game.weaponOnUse(%data, %obj))
      if (%obj.getDataBlock().className $= Armor)
         %obj.mountImage(%data.image, $WeaponSlot);
}

function WeaponImage::onMount(%this,%obj,%slot)
{
   //MES -- is call below useful at all?
   //Parent::onMount(%this, %obj, %slot);
   if(%obj.getClassName() !$= "Player")
      return;

   //messageClient(%obj.client, 'MsgWeaponMount', "", %this, %obj, %slot);
   // Looks arm position
   if (%this.armthread $= "")
   {
      %obj.setArmThread(look);
   }
   else
   {
      %obj.setArmThread(%this.armThread);
   }
   
   // Initial ammo state
   if(%obj.getMountedImage($WeaponSlot).ammo !$= "")
      if (%obj.getInventory(%this.ammo))
         %obj.setImageAmmo(%slot,true);

   %obj.client.setWeaponsHudActive(%this.item);
   if(%obj.getMountedImage($WeaponSlot).ammo !$= "")
      %obj.client.setAmmoHudCount(%obj.getInventory(%this.ammo));
   else
      %obj.client.setAmmoHudCount(-1);
}

function WeaponImage::onUnmount(%this,%obj,%slot)
{
   %obj.client.setWeaponsHudActive(%this.item, 1);
   %obj.client.setAmmoHudCount(-1);
   commandToClient(%obj.client,'removeReticle');
   // try to avoid running around with sniper/missile arm thread and no weapon
   %obj.setArmThread(look);
   Parent::onUnmount(%this, %obj, %slot);
}

function Ammo::onInventory(%this,%obj,%amount)
{
   // Loop through and make sure the images using this ammo have
   // their ammo states set.
   for (%i = 0; %i < 8; %i++) {
      %image = %obj.getMountedImage(%i);
      if (%image > 0)
      {
         if (isObject(%image.ammo) && %image.ammo.getId() == %this.getId())
            %obj.setImageAmmo(%i,%amount != 0);
      }
   }
   ItemData::onInventory(%this,%obj,%amount);
   // Uh, don't update the hud ammo counters if this is a corpse...that's bad.
   if ( %obj.getClassname() $= "Player" && %obj.getState() !$= "Dead" )
   {
      %obj.client.setWeaponsHudAmmo(%this.getName(), %amount);
      if(%obj.getMountedImage($WeaponSlot).ammo $= %this.getName())
         %obj.client.setAmmoHudCount(%amount);
   }
}

function Weapon::onInventory(%this,%obj,%amount)
{
   if(Game.weaponOnInventory(%this, %obj, %amount))
   {
      // Do not update the hud if this object is a corpse:
      if ( %obj.getState() !$= "Dead" )
         %obj.client.setWeaponsHudItem(%this.getName(), 0, 1);   
      ItemData::onInventory(%this,%obj,%amount);
      // if a player threw a weapon (which means that player isn't currently
      // holding a weapon), set armthread to "no weapon"
		// MES - taken out to avoid v-menu animation problems (bug #4749)
      //if((%amount == 0) && (%obj.getClassName() $= "Player"))
      //   %obj.setArmThread(looknw);
   }
}

function Weapon::onPickup(%this, %obj, %shape, %amount)
{
   // If player doesn't have a weapon in hand, use this one...
   if ( %shape.getClassName() $= "Player" 
     && %shape.getMountedImage( $WeaponSlot ) == 0 )
      %shape.use( %this.getName() );
}

function HandInventory::onInventory(%this,%obj,%amount)
{
   // prevent console errors when throwing ammo pack
   if(%obj.getClassName() $= "Player")
      %obj.client.setInventoryHudAmount(%this.getName(), %amount);
   ItemData::onInventory(%this,%obj,%amount);
}

function HandInventory::onUse(%data, %obj)
{
   // %obj = player  %data = datablock of what's being thrown
   if(Game.handInvOnUse(%data, %obj))
   {
      //AI HOOK - If you change the %throwStren, tell Tinman!!!
      //Or edit aiInventory.cs and search for: use(%grenadeType);

      %tossTimeout = getSimTime() - %obj.lastThrowTime[%data];
      if(%tossTimeout < $HandInvThrowTimeout)
         return;

      %throwStren = %obj.throwStrength;

      %obj.decInventory(%data, 1);
      %thrownItem = new Item()
      {
         dataBlock = %data.thrownItem;
         sourceObject = %obj;
      };
      MissionCleanup.add(%thrownItem);
      
      // throw it
      %eye = %obj.getEyeVector();
      %vec = vectorScale(%eye, (%throwStren * 20.0));
      
      // add a vertical component to give it a better arc
      %dot = vectorDot("0 0 1", %eye);
      if(%dot < 0)
         %dot = -%dot;
      %vec = vectorAdd(%vec, vectorScale("0 0 4", 1 - %dot));
      
      // add player's velocity
      %vec = vectorAdd(%vec, vectorScale(%obj.getVelocity(), 0.4));
      %pos = getBoxCenter(%obj.getWorldBox());
      

      %thrownItem.sourceObject = %obj;
      %thrownItem.team = %obj.team;
      %thrownItem.setTransform(%pos);
      
      %thrownItem.applyImpulse(%pos, %vec);
      %thrownItem.setCollisionTimeout(%obj);
      serverPlay3D(GrenadeThrowSound, %pos);
      %obj.lastThrowTime[%data] = getSimTime();

      %thrownItem.getDataBlock().onThrow(%thrownItem, %obj);
      %obj.throwStrength = 0;
   }
}

function HandInventoryImage::onMount(%this,%obj,%slot)
{
   messageClient(%col.client, 'MsgHandInventoryMount', "", %this, %obj, %slot);
   // Looks arm position
   if (%this.armthread $= "")
      %obj.setArmThread(look);
   else
      %obj.setArmThread(%this.armThread);
   
   // Initial ammo state
   if (%obj.getInventory(%this.ammo))
      %obj.setImageAmmo(%slot,true);

   %obj.client.setWeaponsHudActive(%this.item);
}

function Weapon::incCatagory(%data, %obj)
{
   // Don't count the targeting laser as a weapon slot:
   if ( %data.getName() !$= "TargetingLaser" )
      %obj.weaponCount++;   
}

function Weapon::decCatagory(%data, %obj)
{
   // Don't count the targeting laser as a weapon slot:
   if ( %data.getName() !$= "TargetingLaser" )
      %obj.weaponCount--;   
}

function SimObject::damageObject(%data)
{
   //function was added to reduce console err msg spam
}

