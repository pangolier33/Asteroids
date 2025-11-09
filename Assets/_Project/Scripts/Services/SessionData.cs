using UnityEngine;

namespace _Project.Scripts.Services
{
    public class SessionData : MonoBehaviour
    {
        private int _enemyScore = 1;
        private SaveService _saveService;
        
        [field: SerializeField] public int EnemyKilledScore { get; private set; }
        public bool IsGameOver { get; private set; }
        public int CurrentRecord { get; private set; }
        
        public void Initialize(SaveService saveService)
        {
            _saveService = saveService;
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

        private void LoadRecord()
        {
            CurrentRecord = _saveService.LoadRecord();
        }

        private void CheckAndUpdateRecord()
        {
            if (EnemyKilledScore > CurrentRecord)
            {
                CurrentRecord = EnemyKilledScore;
                _saveService.SaveRecord(CurrentRecord);
            }
        }
    }
}