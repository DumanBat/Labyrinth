using Publex.Gameplay.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.Gameplay
{
    public class Level : MonoBehaviour
    {
        [System.Serializable]
        public class PatrolData
        {
            public EnemyUnitConfig unitConfig;
            public Transform spawnPoint;
            public UnitPatrolZone patrolZone;
        }

        [SerializeField]
        private List<PatrolData> _patrolDatas;

        public void InitLevel()
        {
            foreach (var patrolData in _patrolDatas)
            {
                var config = patrolData.unitConfig;

                var unit = Instantiate(config.UnitPrefab, this.transform);
                unit.transform.position = patrolData.spawnPoint.position;
                unit.Init(config, patrolData.patrolZone);
            }
        }
    }
}
