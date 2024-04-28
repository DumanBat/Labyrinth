using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay.Behaviour
{
    public interface IAttack
    {
        public void Attack(ITarget target, float damage);
    }
}