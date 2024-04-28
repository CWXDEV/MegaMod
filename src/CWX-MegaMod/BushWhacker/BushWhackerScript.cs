using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EFT.Interactive;
using EFT.UI;
using UnityEngine;

namespace CWX_MegaMod.BushWhacker
{
    public class BushWhackerScript : MonoBehaviour
    {
        private List<ObstacleCollider> Bushes;
        private List<BoxCollider> Swamps;

        private void Awake()
        {
            Bushes = FindObjectsOfType<ObstacleCollider>().ToList();
            Swamps = FindObjectsOfType<BoxCollider>().ToList();
        }

        public void StartTask()
        {
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

                    if (filbert == true || fibert == true)
                    {
                        if (MegaMod.BushWhacker.Value == true)
                        {
                            bushesItem.enabled = false;
                        }
                        else
                        {
                            bushesItem.enabled = true;
                        }
                    }
                }
            }
        }
    }
}