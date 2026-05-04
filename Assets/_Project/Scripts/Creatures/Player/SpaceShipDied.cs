using _Project.Scripts.Creatures.Health;
using _Project.Scripts.Services;
using _Project.Scripts.Services.ScoreSystem;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Creatures.Player
{
    public class SpaceShipDied : MonoBehaviour, ICreatureDied
    {
        private GameOverController _gameOverController;
        
        public void Initialize(GameOverController gameOverController)
        {
            _gameOverController = gameOverController;
        }

        public void CreatureDied()
        {
            _gameOverController.TriggerGameOver();
        }
    }
}
