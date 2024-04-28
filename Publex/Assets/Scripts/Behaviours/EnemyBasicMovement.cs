using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Publex.Gameplay.Behaviour
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyBasicMovement : MonoBehaviour, IMoveableAI
    {
        private NavMeshAgent _navMeshAgent;
        private Vector3 _targetPosition;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public NavMeshAgent GetNavMeshAgent() => _navMeshAgent;

        public void Move(Vector3 pos, float moveSpeed)
        {
            if (_targetPosition == pos)
                return;

            _targetPosition = pos;
            _navMeshAgent.speed = moveSpeed;
            _navMeshAgent.SetDestination(pos);
        }

        public void Stop()
        {
            _navMeshAgent.ResetPath();
            _targetPosition = Vector3.zero;
        }
    }
}
