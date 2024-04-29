using Publex.Gameplay.Units;
using System;
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
            public bool isIdle;
            public EnemyUnitConfig unitConfig;
            public Transform spawnPoint;
            public UnitPatrolZone patrolZone;
        }

        [SerializeField]
        private LevelFinal _levelFinal;
        [SerializeField]
        private List<PatrolData> _patrolDatas;

        private GameMediator _gameMediator;
        private List<EnemyUnit> _units;

        public Action<bool> LevelFinished;

        public void InitLevel(GameMediator gameMediator)
        {
            _gameMediator = gameMediator;
            _units = new List<EnemyUnit>(_patrolDatas.Count);

            foreach (var patrolData in _patrolDatas)
            {
                var config = patrolData.unitConfig;

                var unit = Instantiate(config.UnitPrefab, patrolData.spawnPoint.position, Quaternion.identity, this.transform);
                unit.Init(config, patrolData);
                _units.Add(unit);
            }

            LevelFinished += FinishLevel;
            _levelFinal.Init(this);
        }

        public void StopLevel()
        {
            foreach (var unit in _units)
                unit.Disable();
        }

        private void FinishLevel(bool isWin)
        {
            _gameMediator.GameFinished?.Invoke(isWin);
        }

        public void Unload()
        {
            Destroy(gameObject);
        }
    }
}
