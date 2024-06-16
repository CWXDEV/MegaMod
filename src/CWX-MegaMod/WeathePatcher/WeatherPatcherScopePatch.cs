using System.Reflection;
using Aki.Reflection.Patching;
using EFT.CameraControl;
using HarmonyLib;

namespace CWX_MegaMod.WeatherPatcher
{
    public class WeatherPatcherScopePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(OpticComponentUpdater), nameof(OpticComponentUpdater.LateUpdate));
        }

        [PatchPostfix]
        public static void PatchPostfix(ref OpticComponentUpdater __instance)
        {
            if (!WeatherPatcherScript.ScopeRunOnce)
            {
                WeatherPatcherScript.ScopeGlobalFog = __instance.gameObject.GetComponent<CustomGlobalFog>();
                WeatherPatcherScript.ScopeScattering = __instance.gameObject.GetComponent<TOD_Scattering>();
                WeatherPatcherScript.ScopeMboit = __instance.gameObject.GetComponent<MBOIT_Scattering>();

                if (WeatherPatcherScript.ScopeGlobalFog != null &&
                    WeatherPatcherScript.ScopeScattering != null &&
                    WeatherPatcherScript.ScopeMboit != null)
                {
                    WeatherPatcherScript.ScopeRunOnce = true;
                }
            }

            WeatherPatcherScript.ScopeGlobalFog.enabled = !MegaMod.FogRemover.Value;
            WeatherPatcherScript.ScopeScattering.enabled = !MegaMod.FogRemover.Value;
            WeatherPatcherScript.ScopeMboit.enabled = !MegaMod.FogRemover.Value;
        }
    }
}