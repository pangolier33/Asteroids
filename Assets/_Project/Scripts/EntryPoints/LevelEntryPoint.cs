using System;
using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.Enums;
using _Project.Scripts.Services;
using _Project.Scripts.Spawners;
using _Project.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Scripts.EntryPoints
{
    public class LevelEntryPoint : IInitializable, IDisposable
    {
        private float _spawnOffset = 0.5f;
        private float _spawnInterval = 5f;
        private int _poolSize = 10;
        
        private ISaveService _saveService;
        private Camera _mainCamera;
        private SpaceShipMovement _spaceShip;
        private HUD _hud;
        private SessionDataManager _sessionDataManager;
        private AnalyticsController _analyticsController;
        private Enemy _ufoPrefab;
        private Enemy _asteroidPrefab;
        private IAnalyticsService _analyticsService;
        private SpaceShipLaser _spaceShipLaser;
        
        private EnemySpawner _ufoSpawner;
        private EnemySpawner _asteroidSpawner;

        [Inject]
        public void Construct(
            ISaveService saveService,
            Camera mainCamera,
            SpaceShipMovement spaceShip,
            HUD hud,
            SessionDataManager sessionDataManager,
            IAnalyticsService analyticsService,
            [Inject(Id = ZenjectIDs.UfoPrefab)] Enemy ufoPrefab,
            [Inject(Id = ZenjectIDs.AsteroidPrefab)] Enemy asteroidPrefab)
        {
            _saveService = saveService;
            _mainCamera = mainCamera;
            _spaceShip = spaceShip;
            _hud = hud;
            _sessionDataManager = sessionDataManager;
            _analyticsService = analyticsService;
            _ufoPrefab = ufoPrefab;
            _asteroidPrefab = asteroidPrefab;
        }

        public void Initialize()
        {
            InstantiatePrefabs();
            BindObjects();
            StartWorkSpawniers().Forget();
        }

        private void BindObjects()
        {
            var spaceShipDied = _spaceShip.GetComponent<SpaceShipDied>();
            var spaceShipGun = _spaceShip.GetComponent<SpaceShipGun>();
            _spaceShipLaser = _spaceShip.GetComponent<SpaceShipLaser>();
                
            spaceShipDied.Initialize(_sessionDataManager);
                
            _analyticsController = new AnalyticsController(
                _analyticsService,
                _sessionDataManager,
                spaceShipGun,
                _spaceShipLaser);
                
            _analyticsController.Initialize();
                
            BindHud(_spaceShipLaser, _spaceShip.GetComponent<SpaceShipMovement>());

            BindSpawners();
        }

        private async UniTaskVoid StartWorkSpawniers()
        {
            await UniTask.WhenAll(
                _ufoSpawner.SpawnEnemies(),
                _asteroidSpawner.SpawnEnemies()
            );
        }

        private void InstantiatePrefabs()
        {
            _spaceShip = Object.Instantiate(_spaceShip);
            _hud = Object.Instantiate(_hud);
        }

        private void BindHud(SpaceShipLaser spaceShipLaser, SpaceShipMovement spaceShip)
        {
            _hud.Initialize(spaceShip, spaceShipLaser);
            _hud.GetComponent<Canvas>().worldCamera = Camera.main;
        }

        private void BindSpawners()
        {
            _ufoSpawner = new EnemySpawner(
                _ufoPrefab, 
                _spawnOffset, 
                _spawnInterval, 
                _mainCamera, 
                _sessionDataManager, 
                _poolSize);
            _ufoSpawner.SetSpawner();
            
            _asteroidSpawner = new AsteroidSpawner(
                _asteroidPrefab, 
                _spawnOffset, 
                _spawnInterval, 
                _mainCamera, 
                _sessionDataManager, 
                _poolSize);
            _asteroidSpawner.SetSpawner();
        }

        public void Dispose()
        {
            _analyticsController?.Dispose();
        }
    }
}
