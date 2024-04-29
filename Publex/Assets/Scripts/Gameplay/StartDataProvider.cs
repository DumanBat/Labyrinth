using Publex.General;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Publex.Gameplay
{
    public class StartDataProvider
    {
        private DataHolder _dataHolder;
        private GameConfig _config;

        public StartDataProvider(DataHolder dataHolder, GameConfig config)
        {
            _dataHolder = dataHolder;
            _config = config;
        }

        public GameStartParameters GetStartData(bool loadSaved)
        {
            var savedData = _dataHolder.saveData;
            var startData = new GameStartParameters()
            {
                levelConfig = loadSaved ? _config.LevelConfigs.FirstOrDefault(x => x.LevelId == savedData.levelId) : GetRandomLevelConfig(),
                health = loadSaved ? savedData.health : _config.PlayerHealth,
                playerPosition = loadSaved ? savedData.playerPosition : Vector3.zero
            };

            startData.timeLeft = loadSaved ? savedData.timeLeft : startData.levelConfig.TimerDuration;

            return startData;
        }

        public GameStartParameters GetStartData(SaveData savedData)
        {
            var startData = new GameStartParameters()
            {
                levelConfig = _config.LevelConfigs.FirstOrDefault(x => x.LevelId == savedData.levelId),
                health = savedData.health,
                playerPosition = savedData.playerPosition,
                timeLeft = savedData.timeLeft
            };

            return startData;
        }

        public GameStartParameters GetStartData(LevelConfig levelConfig)
        {
            var startData = new GameStartParameters()
            {
                levelConfig = levelConfig,
                health = _config.PlayerHealth,
                playerPosition = Vector3.zero,
                timeLeft = levelConfig.TimerDuration
            };

            return startData;
        }

        private LevelConfig GetRandomLevelConfig()
        {
            var randomIndex = UnityEngine.Random.Range(0, _config.LevelConfigs.Count);
            return _config.LevelConfigs[randomIndex];
        }
    }
}
