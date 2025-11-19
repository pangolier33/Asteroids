using System;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class SessionDataManager : MonoBehaviour
    {
        public event Action GameOver;
        
        private int _enemyScore = 1;
        private SaveService _saveService;
        private SaveData _currentSaveData;
        
        [field: SerializeField] public int EnemyKilledScore { get; private set; }
        [field: SerializeField] public int UfoKilledScore { get; private set; }
        [field: SerializeField] public int AsterodisKilledScore { get; private set; }
        public bool IsGameOver { get; private set; }
        public int CurrentRecord { get; private set; }
        
        public void Initialize(SaveService saveService)
        {
            _saveService = saveService;
            
            LoadGameData();
        }

        public void AddKillUfoEvent()
        {
            UfoKilledScore += _enemyScore;
        }

        public void AddKillAsteroidEvent()
        {
            AsterodisKilledScore += _enemyScore;
        }

        public void GameOverEvent()
        {
            IsGameOver = true;
            GameOver?.Invoke();
            EnemyKilledScore = AsterodisKilledScore + UfoKilledScore;
            
            CheckAndUpdateRecord();
        }

        private void LoadGameData()
        {
            _currentSaveData = _saveService.LoadGameData();
            CurrentRecord = _currentSaveData.Record;
        }

        private void CheckAndUpdateRecord()
        {
            if (EnemyKilledScore > CurrentRecord)
            {
                CurrentRecord = EnemyKilledScore;
                _currentSaveData.Record = EnemyKilledScore;
                _saveService.SaveGameData(_currentSaveData);
            }
        }
    }
}