using System.Collections.Generic;
using System.Reflection;
using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;

namespace CWX_MegaMod.LootLoss
{
    public class LootLossPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(LocalGame), nameof(LocalGame.smethod_6));
        }

        [PatchPrefix]
        public static void PatchPrefix(ref LocalRaidSettings raidSettings)
        {
            if (MegaMod.LootLoss.Value)
            {
                raidSettings.selectedLocation.containers = new Dictionary<string, LocationSettingsClass.Location.GClass1350>();
                raidSettings.selectedLocation.Loot = new GClass1333();
            }
        }
    }
}