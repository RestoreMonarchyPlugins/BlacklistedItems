using HarmonyLib;
using RestoreMonarchy.BlacklistedItems;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Linq;

namespace BlacklistedItems.Patches
{
    [HarmonyPatch(typeof(PlayerInventory))]
    class PlayerInventoryPatches
    {
        [HarmonyPatch(nameof(PlayerInventory.ReceiveDragItem))]
        [HarmonyPrefix]
        static bool ReceiveDragItemPrefix(PlayerInventory __instance, byte page_0, byte x_0, byte y_0, byte page_1, byte x_1, byte y_1, byte rot_1)
        {
            byte index = __instance.getIndex(page_0, x_0, y_0);
            ItemJar item = __instance.getItem(page_0, index);

            if (item == null)
            {
                return true;
            }

            BlacklistedItemsPlugin pluginInstance = BlacklistedItemsPlugin.Instance;

            if (!pluginInstance.Configuration.Instance.BlacklistItems.Where(i => !i.CanStore).Any(i => i.ItemId == item.item.id))
            {
                return true;
            }

            UnturnedPlayer untPlayer = UnturnedPlayer.FromPlayer(__instance.player);

            if (pluginInstance.HasBypassPermission(untPlayer))
            {
                return true;
            }

            UnturnedChat.Say(untPlayer, pluginInstance.Translate("CantBeStored", item.GetAsset().itemName), pluginInstance.MessageColor);

            return false;
        }

        [HarmonyPatch(nameof(PlayerInventory.ReceiveSwapItem))]
        [HarmonyPrefix]
        static bool ReceiveSwapItemPrefix(PlayerInventory __instance, byte page_0, byte x_0, byte y_0, byte page_1, byte x_1, byte y_1, byte rot_1)
        {
            if (page_1 != PlayerInventory.STORAGE)
            {
                return true;
            }

            byte index = __instance.getIndex(page_0, x_0, y_0);
            ItemJar item = __instance.getItem(page_0, index);

            if (item == null)
            {
                return true;
            }

            BlacklistedItemsPlugin pluginInstance = BlacklistedItemsPlugin.Instance;

            if (!pluginInstance.Configuration.Instance.BlacklistItems.Where(i => !i.CanStore).Any(i => i.ItemId == item.item.id))
            {
                return true;
            }

            UnturnedPlayer untPlayer = UnturnedPlayer.FromPlayer(__instance.player);

            if (pluginInstance.HasBypassPermission(untPlayer))
            {
                return true;
            }

            UnturnedChat.Say(untPlayer, pluginInstance.Translate("CantBeStored", item.GetAsset().itemName), pluginInstance.MessageColor);

            return false;
        }
    }
}
