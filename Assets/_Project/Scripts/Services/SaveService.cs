using UnityEngine;

namespace _Project.Scripts.Services
{
    public class SaveService
    {
        private const string RECORD_KEY = "PlayerRecord";
        
        public void SaveRecord(int record)
        {
            PlayerPrefs.SetInt(RECORD_KEY, record);
            PlayerPrefs.Save();
        }
        
        public int LoadRecord()
        {
            int record = PlayerPrefs.GetInt(RECORD_KEY, 0);
            return record;
        }
    }
}
