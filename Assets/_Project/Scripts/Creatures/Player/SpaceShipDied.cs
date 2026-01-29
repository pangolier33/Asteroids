using _Project.Scripts.Creatures.Health;
using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Creatures.Player
{
    public class SpaceShipDied : MonoBehaviour, ICreatureDied
    {
        public RestartPanelUI _restartCanvas;
        
        private SessionDataManager _sessionDataManager;

        public void Construct(RestartPanelUI restartCanvas)
        {
            _restartCanvas = restartCanvas;
        }
        
        public void Initialize(SessionDataManager sessionDataManager)
        {
            _sessionDataManager = sessionDataManager;
        }

        public void CreatureDied()
        {
            _sessionDataManager.GameOverEvent();
            var restartCanvas = Instantiate(_restartCanvas);
            restartCanvas.SetScore(_sessionDataManager.EnemyKilledScore);
            restartCanvas.SetRecord(_sessionDataManager.CurrentRecord);
        }
    }
}
