using System;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class SessionDataManager : IInitializable
    {
        public event Action GameOver;
        
        private ISaveService _saveService;
        private SaveData _currentSaveData;
        private RestartPanelUI _restartCanvas;
        private IInstantiator _instantiator;
        
        private int _enemyScore = 1;
        
        public int EnemyKilledScore { get; private set; }
        public int UfoKilledScore { get; private set; }
        public int AsterodisKilledScore { get; private set; }
        public bool IsGameOver { get; private set; }
        public int CurrentRecord { get; private set; }

        [Inject]
        public void Construct(ISaveService saveService, SaveData currentSaveData, RestartPanelUI restartCanvas,
            IInstantiator instantiator)
        {
            _saveService = saveService;
            _currentSaveData = currentSaveData;
            _restartCanvas = restartCanvas;
            _instantiator = instantiator;
        }
        public void Initialize()
        {
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
            CreateRestartCanvas();
        }

        private void LoadGameData()
        {
            _currentSaveData = _saveService.LoadGameData();
            CurrentRecord = _currentSaveData.record;
        }

        private void CheckAndUpdateRecord()
        {
            if (EnemyKilledScore > CurrentRecord)
            {
                CurrentRecord = EnemyKilledScore;
                _currentSaveData.record = EnemyKilledScore;
                _saveService.SaveGameData(_currentSaveData);
            }
        }

        private void CreateRestartCanvas()
        {
            var gameObject = new GameObject("RestartCanvas");
            _restartCanvas = _instantiator.InstantiatePrefabForComponent<RestartPanelUI>(_restartCanvas, gameObject.transform);
            _restartCanvas.SetScore(EnemyKilledScore);
            _restartCanvas.SetRecord(CurrentRecord);
        }
    }
}