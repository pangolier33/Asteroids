using System;
using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.Services;
using _Project.Scripts.Spawners;
using _Project.Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [Header("Objects to instantiate")]
        [SerializeField] private Canvas _hud;
        [SerializeField] private SpaceShipMovement _spaceShip;
        [SerializeField] private SessionDataManager _sessionDataManager;
        [SerializeField] private AnalyticsManager _analyticsManager;
        
        [Header("Objects to bind enemy spawner")]
        [SerializeField] private Enemy _ufoPrefab;
        [SerializeField] private Enemy _asteroidPrefab;
        [SerializeField] private float _spawnOffset;
        [SerializeField] private float _spawnIntervalValue = 5f;
        [SerializeField] private int _poolSize = 10;
        
        private EnemySpawner _ufoSpawner;
        private EnemySpawner _asteroidSpawner;
        private Camera _mainCamera;
        private WaitForSeconds _spawnInterval;
        private SaveService _saveService = new SaveService();

        private void Start()
        {
            BindObjects();
        }

        private void BindObjects()
        {
            _mainCamera = Camera.main;
            _spaceShip = Instantiate(_spaceShip);
            _hud = Instantiate(_hud);
            _sessionDataManager = Instantiate(_sessionDataManager);
            _analyticsManager = Instantiate(_analyticsManager);
            
            _sessionDataManager.Initialize(_saveService);
            _spaceShip.GetComponent<SpaceShipDied>().Initialize(_sessionDataManager);
            _analyticsManager.Initialize(_sessionDataManager, _spaceShip.GetComponent<SpaceShipGun>(), _spaceShip.GetComponent<SpaceShipLaser>());
            BindSpawners();
            BindHud();
            StartCoroutine(_ufoSpawner.SpawnEnemies());
            StartCoroutine(_asteroidSpawner.SpawnEnemies());
        }

        private void BindHud()
        {
            HUD hudComponent = _hud.GetComponent<HUD>();
            SpaceShipLaser spaceShipLaser = _spaceShip.GetComponent<SpaceShipLaser>();
            hudComponent.Initialize(_spaceShip, spaceShipLaser);
            _hud.worldCamera = _mainCamera;
        }

        private void BindSpawners()
        {
            _spawnInterval = new WaitForSeconds(_spawnIntervalValue);
            _ufoSpawner = new EnemySpawner(_ufoPrefab, _spawnOffset, _spawnInterval, _mainCamera, _sessionDataManager, _poolSize);
            _ufoSpawner.SetSpawner();
            
            _asteroidSpawner = new AsteroidSpawner(_asteroidPrefab, _spawnOffset, _spawnInterval, _mainCamera, _sessionDataManager, _poolSize);
            _asteroidSpawner.SetSpawner();
        }
    }
}
