using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Publex.General
{
    [CreateAssetMenu(fileName = "NewDataHolder", menuName = "DataHolder")]
    public class DataHolder : ScriptableObject
    {
        [HideInInspector]
        public bool loadSaved;
        [HideInInspector]
        public SaveData saveData = null;
    }
}
