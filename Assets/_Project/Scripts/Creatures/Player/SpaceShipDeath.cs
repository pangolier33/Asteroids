using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Creatures.Player
{
    [RequireComponent(typeof(ScoreService))]
    public class SpaceShipDeath : MonoBehaviour
    {
        [SerializeField] private RestartPanelUI _restartCanvas;
    
        private ScoreService _scoreService;
        private Health.Health _health;

        private void Awake()
        {
            _scoreService = GetComponent<ScoreService>();
            _health = GetComponent<Health.Health>();
            _health.OnDie += Death;
        }

        private void OnDestroy()
        {
            _health.OnDie -= Death;
        }

        private void Death()
        {
            Instantiate(_restartCanvas.gameObject);
            _restartCanvas.SetScore(_scoreService.ShowCurrentScore());
            Destroy(gameObject);
        }
    }
}
