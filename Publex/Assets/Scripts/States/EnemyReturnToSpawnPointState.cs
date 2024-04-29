using Publex.Gameplay.Units;
using Publex.Gameplay.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Publex.Gameplay.Behaviour
{
    public class EnemyReturnToSpawnPointState : IState
    {
        private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
        public StateMachine.States State => StateMachine.States.ReturnToSpawnPoint;

        private EnemyUnit _unit;
        private Animator _animator;
        private IMoveableAI _moveable;
        private Transform _spawnPoint;
        private NavMeshAgent _navMeshAgent;

        public EnemyReturnToSpawnPointState(EnemyUnit unit, IMoveableAI moveable, Transform spawnPoint)
        {
            _unit = unit;
            _animator = unit.Animator;
            _moveable = moveable;
            _spawnPoint = spawnPoint;
            _navMeshAgent = _moveable.GetNavMeshAgent();
        }

        public void OnEnter()
        {
            _animator.SetBool(IsMovingHash, true);
            _moveable.Move(_spawnPoint.position, _unit.Config.PatrolMoveSpeed);
        }

        public void OnExit()
        {
            _moveable.Stop();
            _animator.SetBool(IsMovingHash, false);
        }

        public void Tick()
        {
            if (IsPointReached())
                _unit.ReturnedToSpawnPoint = true;
        }
        private bool IsPointReached() => _navMeshAgent.remainingDistance < 0.1f && !_navMeshAgent.pathPending;

        public void FixedTick() { }
    }
}
