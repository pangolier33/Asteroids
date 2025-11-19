using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class AnalyticsManager : MonoBehaviour
    {
        private AnalyticsService _analyticsService = new AnalyticsService();
        private SessionDataManager _sessionDataManager;
        private SpaceShipGun _spaceShipGun;
        private SpaceShipLaser _spaceShipLaser;

        private int _numerOfClickLaser;
        private int _numerOfClickGun;
        private bool _isUsedLaser;

        public void Initialize(SessionDataManager sessionDataManager, SpaceShipGun spaceShipGun, SpaceShipLaser spaceShipLaser)
        {
            _sessionDataManager = sessionDataManager;
            _spaceShipGun = spaceShipGun;
            _spaceShipLaser = spaceShipLaser;
            
            InitializeAnalitics();
            
            _analyticsService.LogGameStart();
            _sessionDataManager.GameOver += LogEvents;
            _spaceShipGun.clickShoot += CalculateGunUsed;
            _spaceShipLaser.clickLaser += CalculateLaser;
        }

        private void LogEvents()
        {
            _analyticsService.LogEnemyKilled("Ufo", _sessionDataManager.UfoKilledScore);
            _analyticsService.LogEnemyKilled("Asteroid", _sessionDataManager.AsterodisKilledScore);
            if (_numerOfClickLaser >= 1) 
                _analyticsService.LogEvent("Is laser used", "Answer", "yes");
            _analyticsService.LogEvent("Laser used", "Count", _numerOfClickLaser.ToString());
            _analyticsService.LogEvent("Gun used", "Count", _numerOfClickGun.ToString());
        }

        private void CalculateLaser()
        {
            _numerOfClickLaser += 1;
        }

        private void CalculateGunUsed()
        {
            _numerOfClickGun += 1;
        }

        private async void InitializeAnalitics()
        {
            await _analyticsService.Initialize();
        }
    }
}