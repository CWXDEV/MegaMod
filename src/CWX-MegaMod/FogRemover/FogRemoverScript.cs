using EFT.UI;
using EFT.Weather;
using HarmonyLib;
using UnityEngine;

namespace CWX_MegaMod.FogRemover
{
    public class FogRemoverScript : MonoBehaviour
    {
        private WeatherController weatherController;
        private float debugFogFloat = 0f;

        private void Awake()
        {
            weatherController = WeatherController.Instance;

            if (weatherController != null)
            {
                debugFogFloat = weatherController.WeatherDebug.Fog;
            }
        }

        public void StartTask()
        {
            if (weatherController == null)
            {
                ConsoleScreen.LogError("[CWX-MegaMod] WeatherController not found.");
                return;
            }

            // var customGlobalFog = (CustomGlobalFog) AccessTools.Field(weatherController.GetType(), "customGlobalFog_0").GetValue(weatherController);
            // customGlobalFog.enabled = !MegaMod.FogRemover.Value;
            // weatherController.WeatherDebug.Fog = MegaMod.FogRemover.Value ? 0f : debugFogFloat;
            // weatherController.WeatherDebug.Enabled = MegaMod.SunMode.Value;
        }
    }
}