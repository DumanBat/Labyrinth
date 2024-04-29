using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Publex.General
{
    public class SaveManager
    {
        private static SaveManager instance;
        private static string SaveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");

        public static SaveManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SaveManager();

                return instance;
            }
        }

        public void SaveData(SaveData data)
        {
            string jsonData = JsonUtility.ToJson(data);
            File.WriteAllText(SaveFilePath, jsonData);
        }

        public SaveData LoadData()
        {
            if (File.Exists(SaveFilePath))
            {
                string jsonData = File.ReadAllText(SaveFilePath);
                return JsonUtility.FromJson<SaveData>(jsonData);
            }
            else
                return null;
        }
    }
}
