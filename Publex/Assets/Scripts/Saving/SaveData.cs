using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.General
{
    [System.Serializable]
    public class SaveData
    {
        public string levelId;
        public Vector3 playerPosition;
        public float timeLeft;
        public float health;
    }
}
