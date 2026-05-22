using System;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.Services.ScoreSystem;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services
{
    public class AnalyticsController : IDisposable
    {
        private const string UfoName = "Ufo";
        private const string AsteroidName = "Asteroid";
        private const string GunName = "Gun";
        private const string LaserName = "Laser";

        private readonly IAnalyticsService _analyticsService;
        private readonly ScoreController _scoreController;
        private readonly GameOverController _gameOverController;
        private readonly SpaceShipGun _spaceShipGun;
        private readonly SpaceShipLaser _spaceShipLaser;

        private int _laserUsageCount;
        private int _gunUsageCount;

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
        }

        public void Initialize()
        {
            InitializeAnalyticsAsync().Forget();

            SubscribeToEvents();
        }
        
        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        private async UniTaskVoid InitializeAnalyticsAsync()
        {
            await _analyticsService.Initialize();

            _analyticsService.LogGameStart();
        }

        private void SubscribeToEvents()
        {
            _gameOverController.GameOverEvent += OnGameOver;
            _spaceShipGun.ClickShoot += OnGunShot;
            _spaceShipLaser.ClickLaser += OnLaserUsed;
        }

        private void UnsubscribeFromEvents()
        {
            _gameOverController.GameOverEvent -= OnGameOver;
            _spaceShipGun.ClickShoot -= OnGunShot;
            _spaceShipLaser.ClickLaser -= OnLaserUsed;
        }

        private void OnGameOver()
        {
            LogKilledEnemies();
            LogWeaponsUsage();
        }

        private void LogKilledEnemies()
        {
            _analyticsService.LogEnemyKilled(UfoName, _scoreController.UfoKilledScore);
            _analyticsService.LogEnemyKilled(AsteroidName, _scoreController.AsteroidsKilledScore);
        }

        private void LogWeaponsUsage()
        {
            if (_laserUsageCount > 0)
            {
                _analyticsService.LogIsWeaponUsed(LaserName);
            }

            _analyticsService.LogWeaponUsedCount(LaserName, _laserUsageCount);
            _analyticsService.LogWeaponUsedCount(GunName, _gunUsageCount);
        }

        private void OnLaserUsed()
        {
            _laserUsageCount++;
        }

        private void OnGunShot()
        {
            _gunUsageCount++;
        }
    }
}