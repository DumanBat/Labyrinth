using Publex.Gameplay.Behaviour;
using Publex.Gameplay.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Publex.Gameplay.Level;

namespace Publex.Gameplay.Units
{
    public class EnemyUnit : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private EnemyDetector _enemyDetector;

        private EnemyUnitConfig _config;
        private StateMachine _stateMachine;
        private IMoveableAI _move;
        private IAttack _attack;

        private PatrolData _patrolData;
        private Transform _spawnPoint;
        private EnemyIdleState _idle;

        public Animator Animator => _animator;
        public ITarget CurrentAttackTarget
        {
            get => _enemyDetector.DetectedTarget;
            set => _enemyDetector.DetectedTarget = value;
        }
        public bool HasCurrentAttackTarget => _enemyDetector.DetectedTarget != null;
        public bool HasTargetInRange { get; set; }
        public bool ReturnedToSpawnPoint { get; set; }
        public bool IsIdle { get; set; }
        public bool IsActive { get; set; }
        public EnemyUnitConfig Config => _config;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            _move = GetComponent<IMoveableAI>();
            _attack = GetComponent<IAttack>();
        }

        private void Update()
        {    
            _stateMachine.Tick();
        }

        public void Init(EnemyUnitConfig config, PatrolData patrolData)
        {
            _config = config;
            _patrolData = patrolData;
            IsIdle = patrolData.isIdle;

            _enemyDetector.Init(_config.AggroRadius);
            InitStates();

            IsActive = true;
        }

        public void Disable()
        {
            IsActive = false;
            _stateMachine.SetState(_idle);
        }

        private void InitStates()
        {
            _idle = new EnemyIdleState(_animator);
            var returnToSpawnPoint = new EnemyReturnToSpawnPointState(this, _move, _patrolData.spawnPoint);
            var patrol = new EnemyPatrolState(this, _move, _patrolData.patrolZone);
            var aggro = new EnemyAggroState(this, _move);
            var attack = new EnemyAttackState(this, _attack, _enemyDetector);

            At(_idle, aggro, () => HasCurrentAttackTarget && IsIdle && IsActive);
            At(returnToSpawnPoint, _idle, () => ReturnedToSpawnPoint && IsIdle);

            At(patrol, aggro, () => HasCurrentAttackTarget && !IsIdle);
            At(aggro, attack, () => HasCurrentAttackTarget && HasTargetInRange);
            At(aggro, patrol, () => !HasCurrentAttackTarget && !IsIdle);
            At(aggro, returnToSpawnPoint, () => !HasCurrentAttackTarget && IsIdle);

            At(attack, aggro, () => HasCurrentAttackTarget && !HasTargetInRange);
            At(attack, patrol, () => !HasCurrentAttackTarget && !IsIdle);
            At(attack, returnToSpawnPoint, () => !HasCurrentAttackTarget && IsIdle);

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            _stateMachine.SetState(IsIdle ? _idle : patrol);
        }
    }
}
