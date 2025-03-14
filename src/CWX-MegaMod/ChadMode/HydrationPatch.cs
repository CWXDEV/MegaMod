using System.Reflection;
using EFT.HealthSystem;
using HarmonyLib;
using SPT.Reflection.Patching;

namespace CWX_MegaMod.ChadMode
{
    public class HydrationPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(ActiveHealthController), nameof(ActiveHealthController.ChangeHydration));
        }

        [PatchPrefix]
        public static bool PatchPrefix()
        {
            if (MegaMod.FoodWater.Value)
            {
                return false;
            }

            return true;
        }
    }
}