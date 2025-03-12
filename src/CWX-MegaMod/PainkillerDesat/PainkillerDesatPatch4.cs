using System;
using System.Linq;
using System.Reflection;
using SPT.Reflection.Patching;
using HarmonyLib;

namespace CWX_MegaMod.PainkillerDesat
{
    public class PainkillerDesatScript4 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(GetTargetType(), "Toggle");
        }

        private Type GetTargetType()
        {
            // get effectsController
            var effectsController = AccessTools.TypeByName(nameof(EffectsController));

            // get nested types
            var nestedTypes = effectsController.GetNestedTypes();

            // get type that has a "toggle" method and a field named "cc_DoubleVision_0"
            var result = nestedTypes.FirstOrDefault(x =>
                x.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                    .Any(f => f.Name.ToLower() == "cc_doublevision_0") &&
                x.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                    .Any(m => m.Name.ToLower() == "toggle"));

            // return resulting type
            return result;
        }

        [PatchPrefix] // removes the double vision effect from some painkillers
        public static bool PatchPrefix(ref CC_DoubleVision ___cc_DoubleVision_0)
        {
            if (!MegaMod.PainkillerDesat.Value)
            {
                return true;
            }

            if (___cc_DoubleVision_0 != null)
            {
                ___cc_DoubleVision_0.enabled = false;
            }

            return false; // dont do method
        }
    }
}