using System;
using System.Threading.Tasks;
using Comfort.Common;
using EFT;
using UnityEngine;

namespace CWX_MegaMod.ChadMode
{
    public class InstantSearchScript : MonoBehaviour
    {
        private GameWorld _gameWorld;
        private Player _player;
        private SkillManager _skillManager;
        private bool _oldEliteContainer = false;
        private float _oldEliteSearch = 0f;

        private void Awake()
        {
            _gameWorld = Singleton<GameWorld>.Instance;
            _player = _gameWorld.MainPlayer;
            _skillManager = _player.Skills;
            _oldEliteContainer = _skillManager.IntellectEliteContainerScope.Value;
            _oldEliteSearch = _skillManager.AttentionEliteLuckySearch.Value;
        }

        public async Task StartTask()
        {
            if (MegaMod.InstantSearch.Value)
            {
                _skillManager.IntellectEliteContainerScope.Value = true;
                _skillManager.AttentionEliteLuckySearch.Value = 100f;
            }
            else
            {
                _skillManager.IntellectEliteContainerScope.Value = _oldEliteContainer;
                _skillManager.AttentionEliteLuckySearch.Value = _oldEliteSearch;
            }
        }
    }
}