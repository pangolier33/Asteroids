using System;
using _Project.Scripts.UI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Services
{
    public class SessionDataManager : IInitializable
    {
        public event Action GameOver;
        
        [Inject] private ISaveService _saveService;
        [Inject] private SaveData _currentSaveData;
        [Inject] private RestartPanelUI _restartCanvas;
        [Inject] private IInstantiator _instantiator;
        
        private int _enemyScore = 1;
        
        public int EnemyKilledScore { get; private set; }
        public int UfoKilledScore { get; private set; }
        public int AsterodisKilledScore { get; private set; }
        public bool IsGameOver { get; private set; }
        public int CurrentRecord { get; private set; }

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

            CreateRestartCanvas();
            CheckAndUpdateRecord();
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