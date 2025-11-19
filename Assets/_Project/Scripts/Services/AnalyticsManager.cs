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

<<<<<<< HEAD
        private int _numberOfClicksLaser;
        private int _numberOfClicksGun;
=======
        private int _numerOfClickLaser;
        private int _numerOfClickGun;
>>>>>>> origin/master
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
<<<<<<< HEAD
            if (_numberOfClicksLaser >= 1) 
                _analyticsService.LogEvent("Is laser used", "Answer", "yes");
            _analyticsService.LogEvent("Laser used", "Count", _numberOfClicksLaser.ToString());
            _analyticsService.LogEvent("Gun used", "Count", _numberOfClicksGun.ToString());
=======
            if (_numerOfClickLaser >= 1) 
                _analyticsService.LogEvent("Is laser used", "Answer", "yes");
            _analyticsService.LogEvent("Laser used", "Count", _numerOfClickLaser.ToString());
            _analyticsService.LogEvent("Gun used", "Count", _numerOfClickGun.ToString());
>>>>>>> origin/master
        }

        private void CalculateLaser()
        {
<<<<<<< HEAD
            _numberOfClicksLaser += 1;
=======
            _numerOfClickLaser += 1;
>>>>>>> origin/master
        }

        private void CalculateGunUsed()
        {
<<<<<<< HEAD
            _numberOfClicksGun += 1;
=======
            _numerOfClickGun += 1;
>>>>>>> origin/master
        }

        private async void InitializeAnalitics()
        {
            await _analyticsService.Initialize();
        }
    }
}