using UnityEngine;

namespace _Project.Scripts.Services
{
    public class SessionData : MonoBehaviour
    {
        private const string RECORD_KEY = "PlayerRecord";
        private int _enemyScore = 1;
        private SaveData _saveData;
        
        [field: SerializeField] public int EnemyKilledScore { get; private set; }
        public bool IsGameOver { get; private set; }
        public int CurrentRecord { get; private set; }
        
        private void Start()
        {
            LoadRecord();
        }

        public void AddKillEvent()
        {
            EnemyKilledScore += _enemyScore;
        }

        public void GameOverEvent()
        {
            IsGameOver = true;
            
            CheckAndUpdateRecord();
        }

        public int GetCurrentScore()
        {
            return CurrentRecord;
        }

        private void LoadRecord()
        {
            CurrentRecord = PlayerPrefs.GetInt(RECORD_KEY, 0);
            
            if (_saveData != null)
            {
                _saveData.Record = CurrentRecord;
            }
        }

        private void SaveRecord(int newRecord)
        {
            CurrentRecord = newRecord;
            PlayerPrefs.SetInt(RECORD_KEY, CurrentRecord);
            PlayerPrefs.Save();
            
            if (_saveData != null)
            {
                _saveData.Record = CurrentRecord;
            }
        }

        private void CheckAndUpdateRecord()
        {
            if (EnemyKilledScore > CurrentRecord)
            {
                SaveRecord(EnemyKilledScore);
            }
        }
    }
}