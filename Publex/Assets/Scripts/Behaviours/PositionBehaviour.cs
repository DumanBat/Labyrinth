using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay.Behaviour
{
    public class PositionBehaviour : MonoBehaviour, IPosition
    {
        public Vector3 GetPosition() => transform.position;
    }
}
