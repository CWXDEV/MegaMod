using System.Threading.Tasks;
using Comfort.Common;
using CWX_MegaMod.AlarmChanger;
using CWX_MegaMod.BotMonitor;
using CWX_MegaMod.BushWhacker;
using CWX_MegaMod.ChadMode;
using CWX_MegaMod.EnvironmentEnjoyer;
using CWX_MegaMod.GrassCutter;
using CWX_MegaMod.MasterKey;
using CWX_MegaMod.WeatherPatcher;
using CWX_MegaMod.WindowWiper;
using EFT;
using EFT.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace CWX_MegaMod
{
    public class MegaModScript : MonoBehaviour
    {
        private GameWorld _gameWorld;
        private Player _player;
        public BushWhackerScript _bushWhackerScript;
        public GrassCutterScript _grassCutterScript;
        public MasterKeyScript _masterKeyScript;
        public EnvironmentEnjoyerScript _environmentEnjoyerScript;
        public WeatherPatcherScript _weatherPatcherScript;
        public AlarmChangerScript _alarmChangerScript;
        public BotMonitorScript _botMonitorScript;
        public GodModeScript _godModeScript;
        public CameraScripts _cameraScripts;
        [FormerlySerializedAs("_windowWiperScript")] public TempFuckaroundScript tempFuckaroundScript;

        private void Awake()
        {
            _gameWorld = Singleton<GameWorld>.Instance;

            if (_gameWorld == null)
            {
                ConsoleScreen.LogError("[CWX-MegaMod] GameWorld not found.");
                Destroy(this);
                return;
            }
            _player = _gameWorld.MainPlayer;

            if (_player == null)
            {
                ConsoleScreen.LogError("[CWX-MegaMod] Player not found.");
                Destroy(this);
                return;
            }

            if (_player.Location.ToLower() == "hideout")
            {
                ConsoleScreen.LogError("[CWX-MegaMod] Hideout not supported.");
                Destroy(this);
                return;
            }

            SetupMegaModScripts();
            SetupMegaModEvents();
            RunFirstTime();
        }

        private void SetupMegaModScripts()
        {
            _bushWhackerScript = _gameWorld.gameObject.AddComponent<BushWhackerScript>();
            _grassCutterScript = _gameWorld.gameObject.AddComponent<GrassCutterScript>();
            _masterKeyScript = _gameWorld.gameObject.AddComponent<MasterKeyScript>();
            _environmentEnjoyerScript = _gameWorld.gameObject.AddComponent<EnvironmentEnjoyerScript>();
            _weatherPatcherScript = _gameWorld.gameObject.AddComponent<WeatherPatcherScript>();
            _alarmChangerScript = _gameWorld.gameObject.AddComponent<AlarmChangerScript>();
            _botMonitorScript = _gameWorld.gameObject.AddComponent<BotMonitorScript>();
            _godModeScript = _gameWorld.gameObject.AddComponent<GodModeScript>();
            _cameraScripts = _gameWorld.gameObject.AddComponent<CameraScripts>();
            tempFuckaroundScript = _gameWorld.gameObject.AddComponent<TempFuckaroundScript>();
        }

        private async Task RunFirstTime()
        {
            _bushWhackerScript.StartTask();
            _grassCutterScript.StartTask();
            _masterKeyScript.StartTask();
            _environmentEnjoyerScript.StartTask();
            _weatherPatcherScript.StartTask();
            _godModeScript.StartTask();
            _cameraScripts.StartTask();
            await tempFuckaroundScript.StartTask();
        }

        private void SetupMegaModEvents()
        {
            MegaMod.BushWhacker.SettingChanged += (a, b) => _bushWhackerScript.StartTask();
            MegaMod.GrassCutter.SettingChanged += (a, b) => _grassCutterScript.StartTask();
            MegaMod.MasterKey.SettingChanged += (a, b) => _masterKeyScript.StartTask();
            MegaMod.MasterKeyToUse.SettingChanged += (a, b) => _masterKeyScript.StartTask();
            MegaMod.EnvironmentEnjoyer.SettingChanged += (a, b) => _environmentEnjoyerScript.StartTask();
            MegaMod.FogRemover.SettingChanged += (a, b) => _weatherPatcherScript.StartTask();
            MegaMod.WeatherDebug.SettingChanged += (a, b) => _weatherPatcherScript.StartTask();
            MegaMod.BotMonitor.SettingChanged += (a, b) => SetBotMonitor();
            // reset the text style so fontsize changes happen
            MegaMod.BotMonitorFontSize.SettingChanged += (a, b) => _botMonitorScript.TextStyle = null;
            MegaMod.GodMode.SettingChanged += (a, b) => _godModeScript.StartTask();
            MegaMod.ThermalMode.SettingChanged += (a, b) => _cameraScripts.StartTask();
            MegaMod.NightVisionMode.SettingChanged += (a, b) => _cameraScripts.StartTask();
            MegaMod.BetterThermalMode.SettingChanged += (a, b) => _cameraScripts.StartTask();
            MegaMod.WindowWiper.SettingChanged += (a, b) => tempFuckaroundScript.StartTask();
        }

        private void SetBotMonitor()
        {
            _botMonitorScript.enabled = !_botMonitorScript.enabled;
        }
    }
}