using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Publex.Gameplay.Behaviour
{
    public interface IMoveableAI : IMoveable
    {
        public NavMeshAgent GetNavMeshAgent();
    }
}
