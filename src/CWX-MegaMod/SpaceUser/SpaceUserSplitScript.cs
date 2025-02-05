using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CWX_MegaMod.SpaceUser
{
    public class SpaceUserSplitScript : MonoBehaviour
    {
        private Button _acceptButton = null;

        public SpaceUserSplitScript()
        {
            _acceptButton = GameObject.Find("Split Dialog(Clone)")
                .GetComponentsInChildren<Button>()
                .First(x => x.name.ToLower().Contains("accept button"));
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space) && _acceptButton != null && MegaMod.SpaceUser.Value == true)
            {
                _acceptButton.onClick.Invoke();
            }
        }
    }
}