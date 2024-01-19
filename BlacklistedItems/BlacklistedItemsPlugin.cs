using BlacklistedItems.Models;
using HarmonyLib;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace RestoreMonarchy.BlacklistedItems
{
    public class BlacklistedItemsPlugin : RocketPlugin<BlacklistedItemsConfiguration>
    {
        public static BlacklistedItemsPlugin Instance { get; private set; }

        private const string HarmonyId = "com.restoremonarchy.blacklisteditems";
        private Harmony harmony;

        public Color MessageColor { get; private set; }

        protected override void Load()
        {
            Instance = this;

            harmony = new(HarmonyId);
            harmony.PatchAll();

            MessageColor = UnturnedChat.GetColorFromName(Configuration.Instance.MessageColor, Color.green);
            ItemManager.onServerSpawningItemDrop += OnServerSpawningItemDrop;
            ItemManager.onTakeItemRequested += OnTakeItemRequested;
            PlayerCrafting.onCraftBlueprintRequested += OnCraftBlueprintRequested;

            Logger.Log($"{Name} {Assembly.GetName().Version} has been loadded!", ConsoleColor.Yellow);
        }

        protected override void Unload()
        {
            ItemManager.onServerSpawningItemDrop -= OnServerSpawningItemDrop;
            ItemManager.onTakeItemRequested -= OnTakeItemRequested;
            PlayerCrafting.onCraftBlueprintRequested -= OnCraftBlueprintRequested;

            Logger.Log($"{Name} has been unloaded!", ConsoleColor.Yellow);
        }

        internal bool HasBypassPermission(UnturnedPlayer player)
        {
            if (!Configuration.Instance.EnableBypassPermission)
            {
                return false;
            }

            return player.HasPermission(Configuration.Instance.BypassPermission);
        }

        private void OnTakeItemRequested(Player player, byte x, byte y, uint instanceID, byte to_x, byte to_y, byte to_rot, byte to_page, ItemData itemData, ref bool shouldAllow)
        {
            UnturnedPlayer untPlayer = UnturnedPlayer.FromPlayer(player);

            if (Configuration.Instance.BlacklistItems.Where(i => !i.CanTake).Any(i => i.ItemId == itemData.item.id))
            {
                if (HasBypassPermission(untPlayer))
                {
                    return;
                }

                shouldAllow = false;
                ItemAsset asset = Assets.find(EAssetType.ITEM, itemData.item.id) as ItemAsset;
                if (asset != null)
                {
                    UnturnedChat.Say(untPlayer, Translate("CantBeTaken", asset.itemName), MessageColor);
                }
            }   
        }

        private void OnServerSpawningItemDrop(Item item, ref Vector3 location, ref bool shouldAllow)
        {
            if (Configuration.Instance.BlacklistItems.Where(x => !x.CanSpawn).Any(x => x.ItemId == item.id))
            {
                shouldAllow = false;
            }                
        }

        private void OnCraftBlueprintRequested(PlayerCrafting crafting, ref ushort itemID, ref byte blueprintIndex, ref bool shouldAllow)
        {
            UnturnedPlayer untPlayer = UnturnedPlayer.FromPlayer(crafting.player);

            if (HasBypassPermission(untPlayer))
            {
                return;
            }

            ItemAsset asset = Assets.find(EAssetType.ITEM, itemID) as ItemAsset;
            BlueprintOutput[] ouputs = asset.blueprints[blueprintIndex].outputs;

            IEnumerable<BlacklistItem> blacklistItems = Configuration.Instance.BlacklistItems.Where(x => !x.CanCraft);

            foreach (BlueprintOutput output in ouputs)
            {
                if (blacklistItems.Any(x => x.ItemId == output.id))
                {
                    shouldAllow = false;

                    ItemAsset asset2 = Assets.find(EAssetType.ITEM, itemID) as ItemAsset;
                    if (asset2 != null)
                    {
                        UnturnedChat.Say(untPlayer, Translate("CantBeCrafted", asset2.itemName), MessageColor);
                    }

                    return;
                }
            }
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "CantBeCrafted", "{0} can't be crafted!" },
            { "CantBeTaken", "{0} can't be picked up!" },
            { "CantBeStored", "{0} can't be stored!" }
        };
    }
}
