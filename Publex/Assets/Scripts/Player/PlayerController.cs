using Publex.Gameplay.Behaviour;
using Publex.Gameplay.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private float _moveSpeed;

        private StateMachine _stateMachine;
        private IMoveable _move;
        private IInputService _input;

        private PlayerIdleState _idle;

        public float MoveSpeed => _moveSpeed;
        public bool IsMoving => _input.MoveDirection != Vector3.zero;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            _move = GetComponent<IMoveable>();
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        public void Init(IInputService inputService)
        {
            _input = inputService;

            InitStates();
        }

        private void InitStates()
        {
            _idle = new PlayerIdleState(_animator);
            var move = new PlayerMoveState(this, _animator, _move, _input);

            At(_idle, move, () => IsMoving);
            At(move, _idle, () => !IsMoving);

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            _stateMachine.SetState(_idle);
        }
    }
}
