using Publex.Gameplay.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay.Behaviour
{
    public class PlayerMoveState : IState
    {
        private static readonly int MoveHash = Animator.StringToHash("isMoving");

        public StateMachine.States State => StateMachine.States.Move;

        private PlayerController _player;
        private Animator _animator;
        private IMoveable _moveable;
        private IInputService _input;

        private float _moveSpeed;

        public PlayerMoveState(PlayerController player, Animator animator, IMoveable moveable, IInputService input)
        {
            _player = player;
            _animator = animator;
            _moveable = moveable;
            _input = input;

            _moveSpeed = player.Config.PlayerMoveSpeed;
        }

        public void OnEnter()
        {
            _animator.SetBool(MoveHash, true);
        }

        public void Tick()
        {
            if (_input.MoveDirection == Vector3.zero)
            {
                return;
            }
            var moveDirection = _input.MoveDirection;
            var lookRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, lookRotation, 720f * Time.deltaTime);
            _moveable.Move(moveDirection, _moveSpeed);
        }

        public void OnExit()
        {
            _moveable.Stop();
            _animator.SetBool(MoveHash, false);
        }

        public void FixedTick() { }
    }
}
