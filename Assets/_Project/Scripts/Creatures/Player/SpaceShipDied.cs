using _Project.Scripts.Creatures.Health;
using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Creatures.Player
{
    public class SpaceShipDied : MonoBehaviour, ICreatureDied
    {
        private SessionDataManager _sessionDataManager;
        
        public void Initialize(SessionDataManager sessionDataManager)
        {
            _sessionDataManager = sessionDataManager;
        }

        public void CreatureDied()
        {
            _sessionDataManager.GameOverEvent();
        }
    }
}
