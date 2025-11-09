using UnityEngine;

namespace _Project.Scripts.Services
{
    public class SaveService
    {
        private const string SAVE_DATA_KEY = "GameSaveData";
        
        public void SaveGameData(SaveData data)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(SAVE_DATA_KEY, json);
        }
        
        public SaveData LoadGameData()
        {
            if (PlayerPrefs.HasKey(SAVE_DATA_KEY))
            {
                string json = PlayerPrefs.GetString(SAVE_DATA_KEY);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                return data;
            }
            else
            {
                return new SaveData();
            }
        }
    }
}
