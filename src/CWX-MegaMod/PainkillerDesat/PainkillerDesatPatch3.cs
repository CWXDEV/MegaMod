using System;
using System.Linq;
using System.Reflection;
using SPT.Reflection.Patching;
using HarmonyLib;

namespace CWX_MegaMod.PainkillerDesat
{
    public class PainkillerDesatScript3 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(GetTargetType(), "Toggle");
        }

        private Type GetTargetType()
        {
            var effectsController = AccessTools.TypeByName(nameof(EffectsController));

            // get nested types
            var nestedTypes = effectsController.GetNestedTypes();

            // get type that has a "toggle" method and a field named "cc_Wiggle_0"
            var result = nestedTypes.FirstOrDefault(x =>
                x.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                    .Any(f => f.Name.ToLower() == "cc_wiggle_0") &&
                x.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                    .Any(m => m.Name.ToLower() == "toggle"));

            // return resulting type
            return result;
        }

        [PatchPrefix] // removes the wiggle effect from some painkillers
        public static bool PatchPrefix(ref CC_Wiggle ___cc_Wiggle_0)
        {
            if (!MegaMod.PainkillerDesat.Value)
            {
                return true;
            }

            if (___cc_Wiggle_0 != null)
            {
                ___cc_Wiggle_0.enabled = false;
            }

            return false; // dont do method
        }
    }
}