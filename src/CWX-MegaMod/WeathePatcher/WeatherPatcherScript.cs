using EFT.Weather;
using UnityEngine;

namespace CWX_MegaMod.WeatherPatcher
{
    public class WeatherPatcherScript : MonoBehaviour
    {
        private CameraClass camera;
        private WeatherController weatherController;
        private float debugFogFloat = 0f;

        private void Awake()
        {
            camera = CameraClass.Instance;
            weatherController = WeatherController.Instance;

            if (weatherController != null)
            {
                debugFogFloat = weatherController.WeatherDebug.Fog;
            }
        }

        public void StartTask()
        {
            // fog related
            camera.Camera.gameObject.GetComponentInChildren<CustomGlobalFog>().enabled = !MegaMod.FogRemover.Value;
            camera.Camera.gameObject.GetComponentInChildren<TOD_Scattering>().enabled = !MegaMod.FogRemover.Value;

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
    }
}