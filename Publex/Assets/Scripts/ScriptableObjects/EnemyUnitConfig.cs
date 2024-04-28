using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay.Units
{
    [CreateAssetMenu(fileName = "NewEnemyUnitConfig", menuName = "Configs/Enemy Unit Config")]
    public class EnemyUnitConfig : ScriptableObject
    {
        [SerializeField]
        private EnemyUnit _unitPrefab;

        [SerializeField]
        private float _patrolMoveSpeed;
        [SerializeField]
        private float _patrolIdleDuration;
        [SerializeField]
        private float _chaseMoveSpeed;
        [SerializeField]
        private float _attackReach;
        [SerializeField]
        private float _attackCooldown;
        [SerializeField]
        private float _aggroRadius;
        [SerializeField]
        private float _damage;

        public EnemyUnit UnitPrefab => _unitPrefab;
        public float PatrolMoveSpeed => _patrolMoveSpeed;
        public float PatrolIdleDuration => _patrolIdleDuration;
        public float ChaseMoveSpeed => _chaseMoveSpeed;
        public float AttackReach => _attackReach;
        public float AttackCooldown => _attackCooldown;
        public float AggroRadius => _aggroRadius;
        public float Damage => _damage;
    }
}
