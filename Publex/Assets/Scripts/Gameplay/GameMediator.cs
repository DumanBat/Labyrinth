using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay
{
    public class GameMediator : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _playerPrefab;
        [SerializeField]
        private MobileInputView _mobileInputView;
        [SerializeField]
        private CameraFollow _cameraFollow;

        private PlayerController _player;

        private void Start()
        {
            _player = Instantiate(_playerPrefab);
            _player.Init(_mobileInputView);

            _cameraFollow.Init(_player.transform, _player.BasicCameraOffset);
        }
    }
}
