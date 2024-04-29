using Publex.Gameplay.Behaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay
{
    [RequireComponent(typeof(SphereCollider))]
    public class EnemyDetector : MonoBehaviour
    {
        private SphereCollider _collider;

        public ITarget DetectedTarget { get; set; }

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
        }

        public void Init(float aggroRange)
        {
            _collider.radius = aggroRange;
            _collider.enabled = true;
        }

        public void OnTriggerEnter(Collider other)
        {
            CheckTargetInLineOfSight(other);
        }

        public void OnTriggerStay(Collider other)
        {
            CheckTargetInLineOfSight(other);
        }

        public void OnTriggerExit(Collider other)
        {
            var target = other.GetComponent<ITarget>();
            if (target != null && DetectedTarget == target)
            {
                DetectedTarget = null;
            }
        }

        private void CheckTargetInLineOfSight(Collider other)
        {
            var target = other.GetComponent<ITarget>();
            if (target == null)
                return;

            DetectedTarget = IsTargetInLineOfSight(target) ? target : null;
        }

        private bool IsTargetInLineOfSight(ITarget target)
        {
            Vector3 direction = target.Position.GetPosition() - transform.position;
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, _collider.radius))
            {
                if (hit.collider.gameObject == target.GetGameObject())
                    return true;
            }
            return false;
        }
    }
}
