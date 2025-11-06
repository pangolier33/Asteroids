using System;
using _Project.Scripts.Creatures.Health;
using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Creatures.Player
{
    public class SpaceShipDied : MonoBehaviour, ICreatureDied
    {
        [SerializeField] private RestartPanelUI _restartCanvas;
        
        private SessionData _sessionData;

        public void Initialize(SessionData sessionData)
        {
            _sessionData = sessionData;
        }

        public void CreatureDied()
        {
            _sessionData.GameOverEvent();
            GameObject restartCanvasGameObject = Instantiate(_restartCanvas.gameObject);
            RestartPanelUI restartCanvas = restartCanvasGameObject.GetComponent<RestartPanelUI>();
            restartCanvas.SetScore(_sessionData.EnemyKilledScore);
            restartCanvas.SetRecord(_sessionData.GetCurrentScore());
            Destroy(gameObject);
        }
    }
}
