using Comfort.Common;
using CWX_MegaMod.BushWhacker;
using CWX_MegaMod.EnvironmentEnjoyer;
using CWX_MegaMod.GrassCutter;
using CWX_MegaMod.MasterKey;
using EFT;
using EFT.UI;
using UnityEngine;

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
        }

        private void RunFirstTime()
        {
            _bushWhackerScript.StartTask();
            _grassCutterScript.StartTask();
            _masterKeyScript.StartTask();
            _environmentEnjoyerScript.StartTask();
        }

        private void SetupMegaModEvents()
        {
            MegaMod.BushWhacker.SettingChanged += (a, b) => _bushWhackerScript.StartTask();
            MegaMod.GrassCutter.SettingChanged += (a, b) => _grassCutterScript.StartTask();
            MegaMod.MasterKey.SettingChanged += (a, b) => _masterKeyScript.StartTask();
            MegaMod.MasterKeyToUse.SettingChanged += (a, b) => _masterKeyScript.StartTask();
            MegaMod.EnvironmentEnjoyer.SettingChanged += (a, b) => _environmentEnjoyerScript.StartTask();
        }
    }
}