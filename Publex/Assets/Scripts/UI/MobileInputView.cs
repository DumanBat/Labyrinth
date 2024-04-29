using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Publex.Gameplay
{
    public class MobileInputView : MonoBehaviour, IInputService
    {
        [SerializeField]
        private GameObject _root;
        [SerializeField]
        private Joystick _joystick;
        [SerializeField]
        private Button _pause;

        public Button Pause => _pause;
        public Vector3 MoveDirection => new Vector3(_joystick.Direction.x, 0f, _joystick.Direction.y).normalized;

        public void SetActiveInputView(bool isActive)
        {
            _root.SetActive(isActive);

            if (!isActive)
                _joystick.ResetHandle();
        }
    }
}
