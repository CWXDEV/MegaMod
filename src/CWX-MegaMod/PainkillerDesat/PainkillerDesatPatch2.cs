using System;
using System.Linq;
using System.Reflection;
using SPT.Reflection.Patching;
using HarmonyLib;

namespace CWX_MegaMod.PainkillerDesat
{
    public class PainkillerDesatScript2 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(GetTargetType(), "method_1");
        }

        private Type GetTargetType()
        {
            var effectsController = AccessTools.TypeByName(nameof(EffectsController));

            // get nested types
            var nestedTypes = effectsController.GetNestedTypes();

            // get type that contains a field named "cc_sharpen_0" and a "method_2" - there should only be one
            var result = nestedTypes.FirstOrDefault(x =>
                x.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                    .Any(f => f.Name.ToLower() == "cc_sharpen_0") &&
                x.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                    .Any(m => m.Name.ToLower() == "method_2"));

            // return resulting type
            return result;
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