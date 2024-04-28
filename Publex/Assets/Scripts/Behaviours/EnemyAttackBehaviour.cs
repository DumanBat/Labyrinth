using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay.Behaviour
{
    public class EnemyAttackBehaviour : MonoBehaviour, IAttack
    {
        public void Attack(ITarget target, float damage)
        {
            target.Health.TakeDamage(damage);
        }
    }
}
