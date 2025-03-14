using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Comfort.Common;
using System.Linq;
using EFT;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace CWX_MegaMod.WindowWiper
{
    public class TempFuckaroundScript : MonoBehaviour
    {
        private GameWorld _gameWorld;
        private List<GameObject> _allObjects;
        private List<GameObject> _windowGameObjects;
        private List<GameObject> _windowsGameObjects;
        private List<GameObject> _glassGameObjects;
        private List<GameObject> _doorGameObjects;
        private List<GameObject> _decalGameObjects;
        private IBotGame _botGame;

        private static CameraClass _cameraClass;
        private static Camera _camera;

        private void Awake()
        {
            _gameWorld = Singleton<GameWorld>.Instance;
            _allObjects = FindObjectsOfType<GameObject>(true).ToList();
            _windowGameObjects = _allObjects.Where(x => x.name.ToLower().Contains("window")).ToList();
            _windowsGameObjects = _allObjects.Where(x => x.name.ToLower() == "windows").ToList();
            _glassGameObjects = _allObjects.Where(x => x.name.ToLower().Contains("glass")).ToList();
            _doorGameObjects = _allObjects.Where(x => x.name.ToLower().Contains("door")).ToList();
            _decalGameObjects = _allObjects.Where(x => x.name.ToLower().Contains("decal")).ToList();
            _cameraClass = CameraClass.Instance;
            _camera = _cameraClass.Camera;
            _botGame = Singleton<IBotGame>.Instance;
        }

        public async Task StartTask()
        {
            // do nothing for now
        }

        public async Task RemovePostProcessLayer()
        {
            var postProcessLayer = _cameraClass.Camera.gameObject.GetComponent<PostProcessLayer>();
            postProcessLayer.StopAllCoroutines();
            postProcessLayer.enabled = false;
        }

        public async Task DisableHDR()
        {
            _cameraClass.Camera.allowHDR = false;
        }

        public async Task DisableBotController()
        {
            _botGame.BotsController.Disable();
        }
    }
}