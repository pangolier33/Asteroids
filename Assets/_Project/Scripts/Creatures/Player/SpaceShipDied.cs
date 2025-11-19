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
        
        private SessionDataManager _sessionDataManager;

        public void Initialize(SessionDataManager sessionDataManager)
        {
            _sessionDataManager = sessionDataManager;
        }

        public void CreatureDied()
        {
            _sessionDataManager.GameOverEvent();
            GameObject restartCanvasGameObject = Instantiate(_restartCanvas.gameObject);
            RestartPanelUI restartCanvas = restartCanvasGameObject.GetComponent<RestartPanelUI>();
            restartCanvas.SetScore(_sessionDataManager.EnemyKilledScore);
            restartCanvas.SetRecord(_sessionDataManager.CurrentRecord);
            Destroy(gameObject);
        }
    }
}
