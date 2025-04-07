# BlacklistedItems

A simple Unturned plugin that allows server administrators to blacklist specific items from being crafted, picked up, spawned, or stored.

## Features

- Block items from being crafted by players
- Prevent items from spawning or being dropped in the world
- Stop players from picking up specific items
- Disable storing certain items in containers (lockers, etc.)
- Permission system to allow certain players to bypass restrictions

## Configuration

### Basic Settings

- `MessageColor`: Color of plugin messages (default: yellow)
- `EnableBypassPermission`: Enable/disable the permission bypass system (default: true)
- `BypassPermission`: Permission node for bypassing restrictions (default: blacklist.bypass)

### Blacklisting Items

Each blacklisted item has the following options:

- `ItemId`: The numerical ID of the item
- `Name`: Name of the item (optional, for readability)
- `CanCraft`: If false, players cannot craft this item
- `CanSpawn`: If false, this item cannot spawn or be dropped
- `CanTake`: If false, players cannot pick up this item
- `CanStore`: If false, players cannot store this item in containers

### Example Configuration

```xml
<BlacklistedItemsConfiguration>
  <MessageColor>yellow</MessageColor>
  <EnableBypassPermission>true</EnableBypassPermission>
  <BypassPermission>blacklist.bypass</BypassPermission>
  <BlacklistItems>
    <Item ItemId="1441" Name="Shadowstalker Mk. II" CanCraft="false" CanSpawn="false" CanTake="false" CanStore="false" />
    <Item ItemId="1464" Name="Blindfold" CanCraft="false" CanSpawn="true" CanTake="true" CanStore="true" />
  </BlacklistItems>
</BlacklistedItemsConfiguration>
```

## Messages

You can customize the messages players receive when attempting to interact with blacklisted items:

```xml
<Translations>
  <Translation Id="CantBeCrafted" Value="{0} can't be crafted!" />
  <Translation Id="CantBeTaken" Value="{0} can't be picked up!" />
  <Translation Id="CantBeStored" Value="{0} can't be stored!" />
</Translations>
```

## Permissions

- `blacklist.bypass`: Allows players to ignore item restrictions (if enabled in config)