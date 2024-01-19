## BlacklistedItems
Blacklist the items from being crafted, spawned or picked up by players.

### Features
- **CanSpawn** - Disable item from spawning or being dropped to the ground 
- **CanCraft** - Disable item from being crafted
- **CanTake** - Disable item from being able to be picked up
- **CanStore** - Disable item from being able to be stored in storage like locker
- Optional blacklist bypass permission

### Configuration
```xml
<?xml version="1.0" encoding="utf-8"?>
<BlacklistedItemsConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MessageColor>yellow</MessageColor>
  <EnableBypassPermission>true</EnableBypassPermission>
  <BypassPermission>blacklist.bypass</BypassPermission>
  <BlacklistItems>
    <Item ItemId="1441" Name="Shadowstalker Mk. II" CanCraft="false" CanSpawn="false" CanTake="false" CanStore="false" />
    <Item ItemId="1442" Name="Shadowstalker Mk. II Scope" CanCraft="false" CanSpawn="false" CanTake="false" CanStore="false" />
    <Item ItemId="1443" Name="Shadowstalker Mk. II Drum" CanCraft="false" CanSpawn="false" CanTake="false" CanStore="false" />
    <Item ItemId="1444" Name="Shadowstalker Mk. II Magazine" CanCraft="false" CanSpawn="false" CanTake="false" CanStore="false" />
    <Item ItemId="1464" Name="Blindfold" CanCraft="false" CanSpawn="true" CanTake="true" CanStore="true" />
  </BlacklistItems>
</BlacklistedItemsConfiguration>
```

### Translations
```xml
<?xml version="1.0" encoding="utf-8"?>
<Translations xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Translation Id="CantBeCrafted" Value="{0} can't be crafted!" />
  <Translation Id="CantBeTaken" Value="{0} can't be picked up!" />
  <Translation Id="CantBeStored" Value="{0} can't be stored!" />
</Translations>
```