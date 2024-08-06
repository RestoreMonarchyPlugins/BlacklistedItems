using HarmonyLib;
using RestoreMonarchy.BlacklistedItems.Models;
using SDG.Unturned;

namespace RestoreMonarchy.BlacklistedItems.Patches
{
    [HarmonyPatch(typeof(LevelVehicles))]
    class LevelVehiclesPatches
    {
        [HarmonyPatch(nameof(LevelVehicles.GetRandomAssetForSpawnpoint))]
        [HarmonyPostfix]
        static void GetRandomAssetForSpawnpointPostfix(ref Asset __result)
        {
            BlacklistVehicle foundBlacklistVehicle = null;

            foreach (BlacklistVehicle blacklistVehicle in BlacklistedItemsPlugin.Instance.Configuration.Instance.BlacklistVehicles)
            {
                if (blacklistVehicle.VehicleId != 0 && blacklistVehicle.VehicleId == __result.id)
                {
                    foundBlacklistVehicle = blacklistVehicle;
                    break;
                }
                if (blacklistVehicle.GUID.HasValue && blacklistVehicle.GUID.Value == __result.GUID)
                {
                    foundBlacklistVehicle = blacklistVehicle;
                    break;
                }
            }

            if (foundBlacklistVehicle != null)
            {
                if (!foundBlacklistVehicle.CanSpawn)
                {
                    __result = null;
                }
            }
        }
    }
}
