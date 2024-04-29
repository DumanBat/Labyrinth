using Publex.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.General
{
    [CreateAssetMenu(fileName = "NewGameConfig", menuName = "Configs/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField]
        private List<LevelConfig> _levelConfigs = new List<LevelConfig>();
        [SerializeField]
        private PlayerController _playerPrefab;

        [SerializeField]
        private float _playerMoveSpeed;
        [SerializeField]
        private float _playerHealth;

        public List<LevelConfig> LevelConfigs => _levelConfigs;
        public PlayerController PlayerPrefab => _playerPrefab;
        public float PlayerMoveSpeed => _playerMoveSpeed;
        public float PlayerHealth => _playerHealth;
    }
}
