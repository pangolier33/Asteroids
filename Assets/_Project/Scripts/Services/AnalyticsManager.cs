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
        
        private int _numberOfClicksLaser;
        private int _numberOfClicksGun;
        private bool _isUsedLaser;

        public void Initialize(SessionDataManager sessionDataManager, SpaceShipGun spaceShipGun, SpaceShipLaser spaceShipLaser)
        {
            _sessionDataManager = sessionDataManager;
            _spaceShipGun = spaceShipGun;
            _spaceShipLaser = spaceShipLaser;
            
            InitializeAnalytics();
            
            _analyticsService.LogGameStart();
            _sessionDataManager.GameOver += LogEvents;
            _spaceShipGun.clickShoot += CalculateGunUsed;
            _spaceShipLaser.clickLaser += CalculateLaser;
        }

        private void LogEvents()
        {
            _analyticsService.LogEnemyKilled("Ufo", _sessionDataManager.UfoKilledScore);
            _analyticsService.LogEnemyKilled("Asteroid", _sessionDataManager.AsterodisKilledScore);
            if (_numberOfClicksLaser >= 1) 
                _analyticsService.LogEvent("Is laser used", "Answer", "yes");
            _analyticsService.LogEvent("Laser used", "Count", _numberOfClicksLaser.ToString());
            _analyticsService.LogEvent("Gun used", "Count", _numberOfClicksGun.ToString());
        }

        private void CalculateLaser()
        {
            _numberOfClicksLaser += 1;
        }

        private void CalculateGunUsed()
        {
            _numberOfClicksGun += 1;
        }

        private async void InitializeAnalytics()
        {
            await _analyticsService.Initialize();
        }
    }
}