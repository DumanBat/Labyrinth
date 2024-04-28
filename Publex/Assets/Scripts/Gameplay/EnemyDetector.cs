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

        public void OnTriggerEnter(Collider collision)
        {
            var target = collision.GetComponent<ITarget>();
            if (target == null)
                return;

            DetectedTarget = target;
        }

        public void OnTriggerExit(Collider collision)
        {
            var target = collision.GetComponent<ITarget>();
            if (target == null)
                return;

            DetectedTarget = null;
        }
    }
}
