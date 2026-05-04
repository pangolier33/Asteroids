using System;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.Services.ScoreSystem;

namespace _Project.Scripts.Services
{
    public class AnalyticsController : IDisposable
    {
        private const string UFO_NAME = "Ufo";
        private const string ASTEROID_NAME = "Asteroid";
        private const string GUN_NAME = "Gun";
        private const string LASER_NAME = "Laser";
        
        private readonly IAnalyticsService _analyticsService;
        private readonly ScoreController _scoreController;
        private readonly GameOverController _gameOverController;
        private readonly SpaceShipGun _spaceShipGun;
        private readonly SpaceShipLaser _spaceShipLaser;
        
        private int _numberOfClicksLaser;
        private int _numberOfClicksGun;
        
        public AnalyticsController(
            IAnalyticsService analyticsService,
            GameOverController gameOverController,
            ScoreController scoreController,
            SpaceShipGun spaceShipGun,
            SpaceShipLaser spaceShipLaser)
        {
            _analyticsService = analyticsService;
            _gameOverController = gameOverController;
            _scoreController = scoreController;
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
            _gameOverController.GameOverEvent += LogEvents;
            
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
            _gameOverController.GameOverEvent -= LogEvents;
            
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
            _analyticsService.LogEnemyKilled(UFO_NAME, _scoreController.UfoKilledScore);
            _analyticsService.LogEnemyKilled(ASTEROID_NAME, _scoreController.AsteroidsKilledScore);
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