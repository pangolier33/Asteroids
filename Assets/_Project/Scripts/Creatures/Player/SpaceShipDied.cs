using _Project.Scripts.Creatures.Health;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Creatures.Player
{
    public class SpaceShipDied : MonoBehaviour, ICreatureDied
    {
        [SerializeField] private RestartPanelUI _restartCanvas;
        
        private SessionData _sessionData;

        public void Inizialize(SessionData sessionData)
        {
            _sessionData = sessionData;
        }
        
        public void CreatureDied()
        {
            _sessionData.GameOverEvent();
            Instantiate(_restartCanvas.gameObject);
            _restartCanvas.SetScore(_sessionData.EnemyKilledScore);
            Destroy(gameObject);
        }
    }
}
