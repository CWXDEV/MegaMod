#if !DEBUG
using System.Reflection;
using SPT.Reflection.Patching;
using HarmonyLib;

namespace CWX_MegaMod.PainkillerDesat
{
    public class PainkillerDesatScript2 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(EffectsController.Class644), nameof(EffectsController.Class644.method_1));
        }

        [PatchPrefix] // removes the sharpen/desat effect from some painkillers
        public static bool PatchPrefix(ref CC_Sharpen ___cc_Sharpen_0)
        {
            if (!MegaMod.PainkillerDesat.Value)
            {
                return true;
            }

            if (___cc_Sharpen_0 != null)
            {
                ___cc_Sharpen_0.MaskDesaturate = 0f;
                ___cc_Sharpen_0.Radius = 1f;
                ___cc_Sharpen_0.RadiusFalloff = 0.425f;

                if (___cc_Sharpen_0.DesaturateEffectSettingsProvider != null)
                {
                    ___cc_Sharpen_0.DesaturateEffectSettingsProvider.MaskDesaturate = 0f;
                    ___cc_Sharpen_0.DesaturateEffectSettingsProvider.Radius = 1f;
                    ___cc_Sharpen_0.DesaturateEffectSettingsProvider.RadiusFalloff = 0.425f;
                }
            }

            return false; // dont do method
        }
    }
}
#endif