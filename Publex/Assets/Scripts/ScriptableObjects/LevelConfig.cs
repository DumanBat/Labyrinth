using Publex.Gameplay;
using Publex.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.General
{
    [CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Configs/Level Config")]
    public class LevelConfig : ScriptableObject
    {
        [Tooltip("Level configs must be created using context menu. Configs -> Level Config.\n" +
        "Duplicating existing configs using Ctrl + D will duplicate ID as well.")]
        [ScriptableObjectId]
        public string levelId;
        [SerializeField]
        private float _timerDuration;
        [SerializeField]
        private Level _levelPrefab;

        public string LevelId => levelId;
        public float TimerDuration => _timerDuration;
        public Level LevelPrefab => _levelPrefab;
    }
}
