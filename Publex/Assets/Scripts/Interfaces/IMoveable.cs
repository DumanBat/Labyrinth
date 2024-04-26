using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay.Behaviour
{
    public interface IMoveable
    {
        public void Move(Vector3 direction, float moveSpeed);
        public void Stop();
    }
}
