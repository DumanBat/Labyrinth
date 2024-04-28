using Publex.Gameplay.Units;
using Publex.Gameplay.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Publex.Gameplay.Behaviour
{
    public class EnemyAggroState : IState
    {
        private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
        private static readonly int MoveBlendHash = Animator.StringToHash("MoveBlend");

        private const int ChaseBlendValue = 1;

        public StateMachine.States State => StateMachine.States.Aggro;

        private EnemyUnit _unit;
        private IMoveableAI _moveable;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private ITarget _target;

        private float _defaultNavMeshStoppingDistance;

        private float _chaseMoveSpeed;
        private float _attackReach;

        public EnemyAggroState(EnemyUnit unit, IMoveableAI moveable)
        {
            _unit = unit;
            _moveable = moveable;
            _animator = unit.Animator;
            _navMeshAgent = moveable.GetNavMeshAgent();
            _defaultNavMeshStoppingDistance = _navMeshAgent.stoppingDistance;

            _chaseMoveSpeed = unit.Config.ChaseMoveSpeed;
            _attackReach = unit.Config.AttackReach;
        }
        public void Tick()
        {
            if (!_target.Health.IsAlive())
            {
                _unit.CurrentAttackTarget = null;
                return;
            }

            var targetPosition = _target.Position.GetPosition();
            var targetDistance = Vector3.Distance(_unit.transform.position, targetPosition);

            if (targetDistance < _attackReach)
                _unit.HasTargetInRange = true;
            else
                _moveable.Move(targetPosition, _chaseMoveSpeed);
        }

        public void OnEnter()
        {
            _target = _unit.CurrentAttackTarget;
            _navMeshAgent.stoppingDistance = _attackReach;
            _animator.SetBool(IsMovingHash, true);
            _animator.SetFloat(MoveBlendHash, ChaseBlendValue);
        }

        public void OnExit()
        {
            _moveable.Stop();
            _target = null;
            _navMeshAgent.stoppingDistance = _defaultNavMeshStoppingDistance;
            _animator.SetBool(IsMovingHash, false);
        }

        public void FixedTick() { }
    }
}
