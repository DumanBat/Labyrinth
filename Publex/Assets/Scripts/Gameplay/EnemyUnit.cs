using Publex.Gameplay.Behaviour;
using Publex.Gameplay.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private UnitPatrolZone _patrolZone;

        public Animator Animator => _animator;
        public ITarget CurrentAttackTarget
        {
            get => _enemyDetector.DetectedTarget;
            set => _enemyDetector.DetectedTarget = value;
        }
        public bool HasCurrentAttackTarget => _enemyDetector.DetectedTarget != null;
        public bool HasTargetInRange { get; set; }
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

        public void Init(EnemyUnitConfig config, UnitPatrolZone unitPatrolZone)
        {
            _config = config;
            _patrolZone = unitPatrolZone;

            _enemyDetector.Init(_config.AggroRadius);
            InitStates();
        }

        private void InitStates()
        {
            var idle = new EnemyIdleState(_animator);
            var patrol = new EnemyPatrolState(this, _move, _patrolZone);
            var aggro = new EnemyAggroState(this, _move);
            var attack = new EnemyAttackState(this, _attack, _enemyDetector);

            At(patrol, aggro, () => HasCurrentAttackTarget);
            At(aggro, attack, () => HasCurrentAttackTarget && HasTargetInRange);
            At(aggro, patrol, () => !HasCurrentAttackTarget);

            At(attack, aggro, () => HasCurrentAttackTarget && !HasTargetInRange);
            At(attack, patrol, () => !HasCurrentAttackTarget);

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            _stateMachine.SetState(patrol);
        }
    }
}
