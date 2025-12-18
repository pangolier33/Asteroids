using System;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;

namespace _Project.Scripts.Services
{
    public class AnalyticsController : IDisposable
    {
        private const string UFO_NAME = "Ufo";
        private const string ASTEROID_NAME = "Asteroid";
        private const string GUN_NAME = "Gun";
        private const string LASER_NAME = "Laser";
        
        private readonly AnalyticsService _analyticsService;
        private readonly SessionDataManager _sessionDataManager;
        private readonly SpaceShipGun _spaceShipGun;
        private readonly SpaceShipLaser _spaceShipLaser;
        
        private int _numberOfClicksLaser;
        private int _numberOfClicksGun;
        
        public AnalyticsController(
            AnalyticsService analyticsService,
            SessionDataManager sessionDataManager,
            SpaceShipGun spaceShipGun,
            SpaceShipLaser spaceShipLaser)
        {
            _analyticsService = analyticsService;
            _sessionDataManager = sessionDataManager;
            _spaceShipGun = spaceShipGun;
            _spaceShipLaser = spaceShipLaser;
            
            SubscribeToEvents();
        }
        
        public async void Initialize()
        {
            await _analyticsService.Initialize();
            _analyticsService.LogGameStart();
        }

        private void SubscribeToEvents()
        {
            _sessionDataManager.GameOver += LogEvents;
            
            if (_spaceShipGun != null)
            {
                _spaceShipGun.clickShoot += CalculateGunUsed;
            }
            
            if (_spaceShipLaser != null)
            {
                _spaceShipLaser.clickLaser += CalculateLaser;
            }
        }

        private void UnsubscribeFromEvents()
        {
            _sessionDataManager.GameOver -= LogEvents;
            
            if (_spaceShipGun != null)
            {
                _spaceShipGun.clickShoot -= CalculateGunUsed;
            }
            
            if (_spaceShipLaser != null)
            {
                _spaceShipLaser.clickLaser -= CalculateLaser;
            }
        }

        private void LogEvents()
        {
            _analyticsService.LogEnemyKilled(UFO_NAME, _sessionDataManager.UfoKilledScore);
            _analyticsService.LogEnemyKilled(ASTEROID_NAME, _sessionDataManager.AsterodisKilledScore);
            if (_numberOfClicksLaser >= 1) 
                _analyticsService.LogIsWeaponUsed(LASER_NAME);
            _analyticsService.LogWeaponUsedCount(LASER_NAME, _numberOfClicksLaser);
            _analyticsService.LogWeaponUsedCount(GUN_NAME, _numberOfClicksGun);
        }

        private void CalculateLaser()
        {
            _numberOfClicksLaser++;
        }

        private void CalculateGunUsed()
        {
            _numberOfClicksGun++;
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}