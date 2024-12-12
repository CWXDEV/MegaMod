using System.Reflection;
using HarmonyLib;
using SPT.Reflection.Patching;

namespace CWX_MegaMod.ChadMode
{
    public class CameraShakePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(EffectsController), nameof(EffectsController.method_7));
        }

        [PatchPrefix]
        public static bool PatchPrefix()
        {
            if (MegaMod.CameraShake.Value)
            {
                // Skip method
                return false;
            }

            return true;
        }
    }
}