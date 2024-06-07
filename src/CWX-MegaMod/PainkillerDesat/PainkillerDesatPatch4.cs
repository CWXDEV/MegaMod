using System.Reflection;
using Aki.Reflection.Patching;
using HarmonyLib;

namespace CWX_MegaMod.PainkillerDesat
{
    public class PainkillerDesatScript4 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(EffectsController.Class578), nameof(EffectsController.Class578.Toggle));
        }

        [PatchPrefix] // Removes Double vision from taking some painkillers
        public static bool PatchPrefix(CC_DoubleVision ___cc_DoubleVision_0)
        {
            if (!MegaMod.PainkillerDesat.Value)
            {
                return true;
            }

            ___cc_DoubleVision_0.enabled = false;

            return false; // dont do method
        }
    }
}