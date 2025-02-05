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
        public static void PatchPostfix(ref OpticComponentUpdater __instance, MBOIT_Scattering ___mboit_Scattering_1, TOD_Scattering ___tod_Scattering_1)
        {
            try
            {
                if (___mboit_Scattering_1.sky == null || ___tod_Scattering_1.sky == null)
                {
                    return;
                }
                
                if (___tod_Scattering_1 != null)
                {
                    ___tod_Scattering_1.enabled = !MegaMod.FogRemover.Value;
                }

                if (___mboit_Scattering_1 != null)
                {
                    ___mboit_Scattering_1.enabled = !MegaMod.FogRemover.Value;
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