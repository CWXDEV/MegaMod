using System.Reflection;
using Aki.Reflection.Patching;
using HarmonyLib;

namespace CWX_MegaMod.PainkillerDesat
{
    public class PainkillerDesatScript4 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(EffectsController.Class568), nameof(EffectsController.Class568.Toggle));
        }

        [PatchPrefix] // removes the double vision effect from some painkillers
        public static bool PatchPrefix(ref CC_DoubleVision ___cc_DoubleVision_0)
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