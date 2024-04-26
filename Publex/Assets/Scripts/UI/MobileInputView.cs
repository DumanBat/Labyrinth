using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay
{
    public class MobileInputView : MonoBehaviour, IInputService
    {
        [SerializeField]
        private Joystick _joystick;

        public Vector3 MoveDirection => new Vector3(_joystick.Direction.x, 0f, _joystick.Direction.y).normalized;
    }
}
