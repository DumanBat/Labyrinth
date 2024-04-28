using Publex.Gameplay.Units;
using Publex.Gameplay.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Publex.Gameplay.Behaviour
{
    public class EnemyPatrolState : IState
    {
        private static readonly int IdleHash = Animator.StringToHash("isIdle");
        private static readonly int MoveBlendHash = Animator.StringToHash("MoveBlend");
        private static readonly int IsMovingHash = Animator.StringToHash("isMoving");

        private const int WalkBlendValue = 0;

        public StateMachine.States State => StateMachine.States.Patrol;

        private EnemyUnit _unit;
        private Animator _animator;
        private IMoveableAI _move;
        private UnitPatrolZone _patrolZone;
        private NavMeshAgent _navMeshAgent;

        private float _patrolIdleDuration;
        private float _patrolMoveSpeed;

        private int _currentPatrolIndex = 0;
        private float _waitStartTime = 0f;
        private bool _waitingAtNode = false;

        public EnemyPatrolState(EnemyUnit unit, IMoveableAI move, UnitPatrolZone patrolZone)
        {
            _unit = unit;
            _animator = unit.Animator;
            _move = move;
            _patrolZone = patrolZone;
            _navMeshAgent = _move.GetNavMeshAgent();

            _patrolIdleDuration = unit.Config.PatrolIdleDuration;
            _patrolMoveSpeed = unit.Config.PatrolMoveSpeed;
        }

        public void OnEnter()
        {
            _animator.SetFloat(MoveBlendHash, WalkBlendValue);

            _currentPatrolIndex = 0;
            MoveToNextPatrolNode();
        }

        public void OnExit()
        {
            _animator.SetBool(IdleHash, false);
            _animator.SetBool(IsMovingHash, false);

            _move.Stop();
        }

        public void Tick()
        {
            if (_waitingAtNode)
            {
                if (Time.time - _waitStartTime >= _patrolIdleDuration)
                {
                    _waitingAtNode = false;
                    MoveToNextPatrolNode();
                }
                return;
            }

            if (IsNodeReached())
            {
                _waitingAtNode = true;
                _waitStartTime = Time.time;
                SetMovingAnimation(false);
            }
        }

        private void MoveToNextPatrolNode()
        {
            SetMovingAnimation(true);

            _move.Move(_patrolZone.PatrolNodes[_currentPatrolIndex].position, _patrolMoveSpeed);
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolZone.PatrolNodes.Count;
        }

        private void SetMovingAnimation(bool isMoving)
        {
            _animator.SetBool(IsMovingHash, isMoving);
            _animator.SetBool(IdleHash, !isMoving);
        }

        private bool IsNodeReached() => _navMeshAgent.remainingDistance < 0.1f && !_navMeshAgent.pathPending;

        public void FixedTick() { }
    }
}
