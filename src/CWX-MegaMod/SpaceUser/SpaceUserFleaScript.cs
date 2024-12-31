#if !DEBUG
using System.Linq;
using EFT.UI;
using UnityEngine;

namespace CWX_MegaMod.SpaceUser
{
    public class SpaceUserFleaScript : MonoBehaviour
    {
        private DefaultUIButton _acceptButton = null;

        public SpaceUserFleaScript()
        {
            _acceptButton = GameObject.Find("Menu UI")
                .GetComponentsInChildren<Transform>(true)
                .First(x => x.name == "YesButton")
                .GetComponentInChildren<DefaultUIButton>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space) && _acceptButton != null && MegaMod.SpaceUser.Value == true)
            {
                _acceptButton.OnClick.Invoke();
            }
        }
    }
}
#endif