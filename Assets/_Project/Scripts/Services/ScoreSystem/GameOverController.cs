using System;

namespace _Project.Scripts.Services.ScoreSystem
{
    public class GameOverController
    {
        public event Action GameOverEvent;

        private RecordController _recordController;
        private ScoreController _scoreController;
        public bool IsGameOver { get; private set; }

        public GameOverController(RecordController recordController, ScoreController scoreController)
        {
            _recordController = recordController;
            _scoreController = scoreController;
        }
    
        public void TriggerGameOver()
        {
            _recordController.TryUpdateRecord(_scoreController.TotalScore);
            IsGameOver = true;
            GameOverEvent?.Invoke();
        }
    }
}
