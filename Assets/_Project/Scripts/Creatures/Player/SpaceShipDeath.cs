using _Project.Scripts.Creatures.Health;
using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Creatures.Player
{
    [RequireComponent(typeof(ScoreService))]
    public class SpaceShipDeath : MonoBehaviour, ICreatureDeath
    {
        [SerializeField] private RestartPanelUI _restartCanvas;
        
        private SessionData _sessionData;

        public void Inizialize(SessionData sessionData)
        {
            _sessionData = sessionData;
        }
        
        public void CreatureDeath()
        {
            Instantiate(_restartCanvas.gameObject);
            _restartCanvas.SetScore(_sessionData._enemyKilledScore);
            Destroy(gameObject);
        }
    }
}
