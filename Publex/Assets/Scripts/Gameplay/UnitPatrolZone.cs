using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay
{
    public class UnitPatrolZone : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> _patrolNodes;

        public List<Transform> PatrolNodes => _patrolNodes;
    }
}
