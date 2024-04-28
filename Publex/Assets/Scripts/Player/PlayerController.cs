using Publex.Gameplay.Behaviour;
using Publex.Gameplay.Utils;
using Publex.General;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay
{
    public class PlayerController : MonoBehaviour, ITarget
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private Transform _basicCameraOffset;

        private GameConfig _config;
        private IMoveable _move;
        private IHealth _health;
        private IPosition _position;

        private StateMachine _stateMachine;
        private IInputService _input;

        private PlayerIdleState _idle;

        public Transform BasicCameraOffset => _basicCameraOffset;
        public bool IsMoving => _input.MoveDirection != Vector3.zero;

        public IHealth Health => _health;
        public IPosition Position => _position;
        public GameConfig Config => _config;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            _move = GetComponent<IMoveable>();
            _health = GetComponent<IHealth>();
            _position = GetComponent<IPosition>();
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        public void Init(GameConfig config, IInputService inputService)
        {
            _config = config;
            _input = inputService;

            _health.Init(Config.PlayerHealth);
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
