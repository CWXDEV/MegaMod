using EFT.Weather;
using UnityEngine;

namespace CWX_MegaMod.WeatherPatcher
{
    public class WeatherPatcherScript : MonoBehaviour
    {
        private CameraClass camera;
        private WeatherController weatherController;
        private float debugFogFloat = 0f;
        private CustomGlobalFog globalFog = null;
        private TOD_Scattering scattering = null;
        private MBOIT_Scattering mboit = null;
        public static bool ScopeRunOnce = false;
        public static CustomGlobalFog ScopeGlobalFog = null;
        public static TOD_Scattering ScopeScattering = null;
        public static MBOIT_Scattering ScopeMboit = null;

        private void Awake()
        {
            camera = CameraClass.Instance;
            weatherController = WeatherController.Instance;

            if (weatherController != null)
            {
                debugFogFloat = weatherController.WeatherDebug.Fog;
            }

            if (camera.Camera != null)
            {
                globalFog = camera.Camera.gameObject.GetComponentInChildren<CustomGlobalFog>();
                scattering = camera.Camera.gameObject.GetComponentInChildren<TOD_Scattering>();
                mboit = camera.Camera.gameObject.GetComponentInChildren<MBOIT_Scattering>();
            }
        }

        public void StartTask()
        {
            // fog related


            if (globalFog != null)
            {
                globalFog.enabled = !MegaMod.FogRemover.Value;
            }

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

        public void OnDestroy()
        {
            ScopeRunOnce = false;
            ScopeGlobalFog = null;
            ScopeScattering = null;
            ScopeMboit = null;
        }
    }
}