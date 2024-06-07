using System.Reflection;
using Aki.Reflection.Patching;
using HarmonyLib;

namespace CWX_MegaMod.PainkillerDesat
{
    public class PainkillerDesatScript1 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(EffectsController.Class572), nameof(EffectsController.Class572.method_2));
        }

        [PatchPrefix] // removes the sharpen/desat effect from some painkillers
        public static bool PatchPrefix(bool desaturateProvider, CC_Sharpen ___cc_Sharpen_0)
        {
            if (!MegaMod.PainkillerDesat.Value)
            {
                return true;
            }

            if (desaturateProvider)
            {
                ___cc_Sharpen_0.DesaturateEffectSettingsProvider.Radius = 0f;
            }
            else
            {
                ___cc_Sharpen_0.Radius = 0f;
            }

            return false; // dont do method
        }
    }
}