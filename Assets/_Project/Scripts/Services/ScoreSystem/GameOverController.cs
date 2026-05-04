using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.ScoreSystem
{
    public class GameOverController
    {
        public event Action GameOverEvent;
    
        private readonly ScoreController _scoreController;
        private readonly RecordController _recordController;
        private readonly IRestartUIFactory _restartUIFactory;
        
        public bool IsGameOver { get; private set; }
    
        public GameOverController(ScoreController scoreManager, RecordController recordManager, IRestartUIFactory restartUIFactory)
        {
            _scoreController = scoreManager;
            _recordController = recordManager;
            _restartUIFactory = restartUIFactory;
        }
    
        public void TriggerGameOver()
        {
            IsGameOver = true;
            _recordController.TryUpdateRecord(_scoreController.TotalScore);
            _restartUIFactory.Create(_scoreController.TotalScore, _recordController.CurrentRecord);
            GameOverEvent?.Invoke();
        }
    }
}
