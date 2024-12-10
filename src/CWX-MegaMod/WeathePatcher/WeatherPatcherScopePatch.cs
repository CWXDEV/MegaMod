using System;
using System.Reflection;
using SPT.Reflection.Patching;
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
            try
            {
                if (!WeatherPatcherScript.ScopeRunOnce)
                {
                    WeatherPatcherScript.ScopeScattering = __instance.gameObject.GetComponent<TOD_Scattering>();
                    WeatherPatcherScript.ScopeMboit = __instance.gameObject.GetComponent<MBOIT_Scattering>();

                    WeatherPatcherScript.ScopeRunOnce = true;
                }

                if (WeatherPatcherScript.ScopeScattering != null)
                {
                    WeatherPatcherScript.ScopeScattering.enabled = !MegaMod.FogRemover.Value;
                }

                if (WeatherPatcherScript.ScopeMboit != null)
                {
                    WeatherPatcherScript.ScopeMboit.enabled = !MegaMod.FogRemover.Value;
                }
            }
            catch (Exception e)
            {
                MegaMod.LogToScreen("Exception in weatherscope patchpostfix", EMessageType.Error);
                MegaMod.LogToScreen(e.Message, EMessageType.Error);
            }
        }
    }
}