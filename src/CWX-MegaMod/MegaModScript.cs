using Comfort.Common;
using CWX_MegaMod.BushWhacker;
using CWX_MegaMod.GrassCutter;
using CWX_MegaMod.MasterKey;
using EFT;
using EFT.UI;
using EFT.Weather;
using UnityEngine;

namespace CWX_MegaMod
{
    public class MegaModScript : MonoBehaviour
    {
        private GameWorld _gameWorld;
        private Player _player;
        
        private BushWhackerScript _bushWhackerScript;
        private GrassCutterScript _grassCutterScript;
        private MasterKeyScript _masterKeyScript;
        
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
            ConsoleScreen.LogError("[CWX-MegaMod] BushWhackerScript");
            _bushWhackerScript = new BushWhackerScript();
            ConsoleScreen.LogError("[CWX-MegaMod] GrassCutterScript");
            _grassCutterScript = new GrassCutterScript();
            ConsoleScreen.LogError("[CWX-MegaMod] MasterKeyScript");
            _masterKeyScript = new MasterKeyScript();
        }

        private void RunFirstTime()
        {
            _bushWhackerScript.StartTask();
            _grassCutterScript.StartTask();
            _masterKeyScript.StartTask();
        }

        private void SetupMegaModEvents()
        {
            MegaMod.BushWhacker.SettingChanged += (a, b) => _bushWhackerScript.StartTask();
            MegaMod.GrassCutter.SettingChanged += (a, b) => _grassCutterScript.StartTask();
            MegaMod.MasterKey.SettingChanged += (a, b) => _masterKeyScript.StartTask();
            MegaMod.MasterKeyToUse.SettingChanged += (a, b) => _masterKeyScript.StartTask();
        }
    }
}