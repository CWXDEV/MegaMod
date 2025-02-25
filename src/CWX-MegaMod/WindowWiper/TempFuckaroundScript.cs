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
        
        private static CameraClass _cameraClass;
        private static Camera _camera;
        private static Shader customAAShader;
        private static Material customAAMaterial;
        private static RenderTexture customPreviousFrame;
        
        private void Awake()
        {
            _gameWorld = Singleton<GameWorld>.Instance;
            _allObjects = FindObjectsOfType<GameObject>(true).ToList();
            _windowGameObjects = _allObjects.Where(x => x.name.ToLower().Contains("window")).ToList();
            _windowsGameObjects = _allObjects.Where(x => x.name.ToLower() == "windows").ToList();
            _glassGameObjects = _allObjects.Where(x => x.name.ToLower().Contains("glass")).ToList();
            _doorGameObjects = _allObjects.Where(x => x.name.ToLower().Contains("door")).ToList();
            _decalGameObjects = _allObjects.Where(x => x.name.ToLower().Contains("decal")).ToList();
            _cameraClass = Singleton<CameraClass>.Instance;
            _camera = _cameraClass.Camera;
            
            customAAShader = Shader.Find("Custom/LightweightAA");

            if (customAAShader == null)
            {
                MegaMod.Logger.LogError("Custom AA Shader Not Found!");
            }
            
            customAAMaterial = new Material(customAAShader);
            
            customPreviousFrame = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.DefaultHDR);
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
        
        private class CustomAACallback : MonoBehaviour
        {
            private void OnRenderImage(RenderTexture source, RenderTexture destination)
            {
                if (customAAMaterial == null || customPreviousFrame == null)
                {
                    Graphics.Blit(source, destination);
                    return;
                }

                // Update shader parameters
                customAAMaterial.SetMatrix("unity_ObjectToWorld", transform.localToWorldMatrix);
                customAAMaterial.SetMatrix("unity_MatrixVP", Camera.main.projectionMatrix * Camera.main.worldToCameraMatrix);

                // Apply AA effect
                Graphics.Blit(source, destination, customAAMaterial);

                // Store current frame for next frame's temporal AA
                Graphics.Blit(destination, customPreviousFrame);
            }

            private void OnDestroy()
            {
                if (customPreviousFrame != null)
                {
                    customPreviousFrame.Release();
                    customPreviousFrame = null;
                }
            }
        }
    }
}