#if !DEBUG
using System;
using EFT.Weather;
using UnityEngine;

namespace CWX_MegaMod.WeatherPatcher
{
    public class WeatherPatcherScript : MonoBehaviour
    {
        private CameraClass camera;
        private WeatherController weatherController;
        private float debugFogFloat = 0f;
        private TOD_Scattering scattering = null;
        private MBOIT_Scattering mboit = null;
        public static bool ScopeRunOnce = false;
        public static TOD_Scattering ScopeScattering = null;
        public static MBOIT_Scattering ScopeMboit = null;

        private void Awake()
        {
            try
            {
                camera = CameraClass.Instance;
                weatherController = WeatherController.Instance;

                if (weatherController != null)
                {
                    debugFogFloat = weatherController.WeatherDebug.Fog; // TODO: look for a way to disable this error as Jetbrains still have not fixed it
                }

                if (camera.Camera != null)
                {
                    scattering = camera.Camera.gameObject.GetComponentInChildren<TOD_Scattering>();
                    mboit = camera.Camera.gameObject.GetComponentInChildren<MBOIT_Scattering>();
                }
            }
            catch (Exception e)
            {
                MegaMod.LogToScreen("Exception on weatherpatchscript awake", EMessageType.Error);
                MegaMod.LogToScreen(e.Message, EMessageType.Error);
            }
        }

        public void StartTask()
        {
            try
            {
                if (scattering != null)
                {
                    scattering.enabled = !MegaMod.FogRemover.Value;
                }

                if (mboit != null)
                {
                    mboit.enabled = !MegaMod.FogRemover.Value;
                }

                if (weatherController != null)
                {
                    if (MegaMod.FogRemover.Value)
                    {
                        weatherController.WeatherDebug.Fog = 0f;
                    }
                    else
                    {
                        weatherController.WeatherDebug.Fog = debugFogFloat;
                    }

                    // debug mode related
                    if (MegaMod.WeatherDebug.Value)
                    {
                        weatherController.WeatherDebug.Enabled = true;
                    }
                    else
                    {
                        weatherController.WeatherDebug.Enabled = false;
                    }
                }
            }
            catch (Exception e)
            {
                MegaMod.LogToScreen("Exception on weatherpatchscript start task", EMessageType.Error);
                MegaMod.LogToScreen(e.Message, EMessageType.Error);
            }
        }

        public void OnDestroy()
        {
            ScopeRunOnce = false;
            ScopeScattering = null;
            ScopeMboit = null;
        }
    }
}
#endif