using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GPUInstancer;
using UnityEngine;

namespace CWX_MegaMod.GrassCutter
{
    public class GrassCutterScript : MonoBehaviour
    {
        private List<GPUInstancerDetailManager> Grass;
        private bool ReadyToEdit = false;

        private void Awake()
        {
            StartCoroutine(GetObjects());
        }

        public void StartTask()
        {
            StartCoroutine(ChangeObjects());
        }

        private IEnumerator GetObjects()
        {
            Grass = FindObjectsOfType<GPUInstancerDetailManager>().ToList();
            ReadyToEdit = true;
            yield return null;
        }

        private IEnumerator ChangeObjects()
        {
            yield return new WaitUntil(() => ReadyToEdit);
            if (Grass != null)
            {
                foreach (var grass in Grass)
                {
                    if (MegaMod.GrassCutter.Value == true)
                    {
                        grass.enabled = false;
                    }
                    else
                    {
                        grass.enabled = true;
                    }
                }
            }

            yield return null;
        }
    }
}