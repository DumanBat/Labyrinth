using Publex.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay
{
    public class GameMediator : MonoBehaviour
    {
        [Header("General")]
        [SerializeField]
        private GameConfig _config;

        [Space]
        [Header("References")]
        [SerializeField]
        private MobileInputView _mobileInputView;
        [SerializeField]
        private CameraFollow _cameraFollow;

        private PlayerController _player;
        private Level _currentLevel;

        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            LoadLevel();
            LoadPlayer();

            _cameraFollow.Init(_player.transform, _player.BasicCameraOffset);
        }

        private void LoadLevel()
        {
            _currentLevel = Instantiate(GetRandomLevel());
            _currentLevel.InitLevel();
        }

        private void LoadPlayer()
        {
            _player = Instantiate(_config.PlayerPrefab);
            _player.Init(_config, _mobileInputView);
        }

        private Level GetRandomLevel()
        {
            var randomIndex = Random.Range(0, _config.Levels.Count);
            return _config.Levels[randomIndex];
        }
    }
}
