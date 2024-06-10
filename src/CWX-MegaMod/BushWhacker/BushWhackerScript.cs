using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EFT.Interactive;
using UnityEngine;

namespace CWX_MegaMod.BushWhacker
{
    public class BushWhackerScript : MonoBehaviour
    {
        private List<ObstacleCollider> Bushes;
        private List<BoxCollider> Swamps;
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
            Bushes = FindObjectsOfType<ObstacleCollider>().ToList();
            Swamps = FindObjectsOfType<BoxCollider>().ToList();
            ReadyToEdit = true;
            yield return null;
        }

        public IEnumerator ChangeObjects()
        {
            yield return new WaitUntil(() => ReadyToEdit);
            if (Swamps != null)
            {
                foreach (var swamp in Swamps)
                {
                    if (swamp.name == "Swamp_collider")
                    {
                        if (MegaMod.BushWhacker.Value == true)
                        {
                            swamp.enabled = false;
                        }
                        else
                        {
                            swamp.enabled = true;
                        }
                    }
                }
            }

            if (Bushes != null)
            {
                foreach (var bushesItem in Bushes)
                {
                    var filbert = bushesItem?.transform?.parent?.gameObject?.name?.Contains("filbert");
                    var fibert = bushesItem?.transform?.parent?.gameObject?.name?.Contains("fibert");
                    var fibert2 = bushesItem?.transform?.name?.Contains("fibert");
                    var filbert2 = bushesItem?.transform?.name?.Contains("filbert");
                    var swamp = bushesItem?.transform?.name.Contains("Swamp_collider");

                    if (filbert == true || fibert == true)
                    {
                        if (MegaMod.BushWhacker.Value)
                        {
                            bushesItem.enabled = false;
                        }
                        else
                        {
                            bushesItem.enabled = true;
                        }
                    }

                    if (filbert2 == true || fibert2 == true || swamp == true)
                    {
                        if (MegaMod.BushWhacker.Value)
                        {
                            bushesItem.gameObject.SmartDisable();
                        }
                        else
                        {
                            bushesItem.gameObject.SmartEnable();
                        }
                    }
                }
            }

            yield return null;
        }
    }
}