using System.Reflection;
using EFT.HealthSystem;
using HarmonyLib;
using SPT.Reflection.Patching;

namespace CWX_MegaMod.ChadMode
{
    public class EnergyPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(ActiveHealthController), nameof(ActiveHealthController.ChangeEnergy));
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