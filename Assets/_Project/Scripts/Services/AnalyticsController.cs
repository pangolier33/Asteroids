using System;
using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class AnalyticsController : MonoBehaviour
    {
        private const string UFO_NAME = "Ufo";
        private const string ASTEROID_NAME = "Asteroid";
        private const string GUN_NAME = "Gun";
        private const string LASER_NAME = "Laser";
        
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

        private void OnDisable()
        {
            _sessionDataManager.GameOver -= LogEvents;
            _spaceShipGun.clickShoot -= CalculateGunUsed;
            _spaceShipLaser.clickLaser -= CalculateLaser;
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