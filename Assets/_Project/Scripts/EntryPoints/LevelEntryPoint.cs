using System;
using System.Threading;
using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.Enums;
using _Project.Scripts.Services;
using _Project.Scripts.Services.ScoreSystem;
using _Project.Scripts.Spawners;
using _Project.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Scripts.EntryPoints
{
    public class LevelEntryPoint : IDisposable
    {
        private float _spawnOffset = 0.5f;
        private float _spawnInterval = 5f;
        private int _poolSize = 10;
        
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        
        private Camera _mainCamera;
        private SpaceShipMovement _spaceShip;
        private HUDView _hud;
        private ScoreController _scoreController;
        private GameOverController _gameOverController;
        private IInstantiator _instantiator;
        private AnalyticsController _analyticsController;
        private Enemy _ufoPrefab;
        private Enemy _asteroidPrefab;
        private IAnalyticsService _analyticsService;
        private SpaceShipLaser _spaceShipLaser;
        
        private EnemySpawner _ufoSpawner;
        private EnemySpawner _asteroidSpawner;

        [Inject]
        public void Construct(
            Camera mainCamera,
            SpaceShipMovement spaceShip,
            HUDView hud,
            GameOverController gameOverController,
            ScoreController scoreController,
            IAnalyticsService analyticsService,
            IInstantiator instantiator, 
            [Inject(Id = ZenjectIDs.UfoPrefab)] Enemy ufoPrefab,
            [Inject(Id = ZenjectIDs.AsteroidPrefab)] Enemy asteroidPrefab)
        {
            _mainCamera = mainCamera;
            _spaceShip = spaceShip;
            _hud = hud;
            _gameOverController = gameOverController;
            _scoreController = scoreController;
            _analyticsService = analyticsService;
            _instantiator = instantiator;
            _ufoPrefab = ufoPrefab;
            _asteroidPrefab = asteroidPrefab;
        }

        public void Initialize()
        {
            BindObjects();
            StartWorkSpawniers(_cts.Token).Forget();
        }

        private void BindObjects()
        {
            var spaceShipDied = _spaceShip.GetComponent<SpaceShipDied>();
            var spaceShipGun = _spaceShip.GetComponent<SpaceShipGun>();
            _spaceShipLaser = _spaceShip.GetComponent<SpaceShipLaser>();
                
            spaceShipDied.Initialize(_gameOverController);
                
            _analyticsController = new AnalyticsController(
                _analyticsService,
                _gameOverController,
                _scoreController,
                spaceShipGun,
                _spaceShipLaser);
                
            _analyticsController.Initialize();
                
            BindHud(_spaceShipLaser, _spaceShip.GetComponent<SpaceShipMovement>());

            BindSpawners();
        }

        private async UniTaskVoid StartWorkSpawniers(CancellationToken token)
        {
            await UniTask.WhenAll(
                  _ufoSpawner.SpawnEnemies(token),
                  _asteroidSpawner.SpawnEnemies(token)
            );
        }

        private void BindHud(SpaceShipLaser spaceShipLaser, SpaceShipMovement spaceShip)
        {
            _hud.Initialize(spaceShip, spaceShipLaser);
        }

        private void BindSpawners()
        {
            _ufoSpawner = _instantiator.Instantiate<UfoSpawner>(
                new object[]
                {
                    _ufoPrefab,
                    _spawnOffset,
                    _spawnInterval,
                    _mainCamera,
                    _gameOverController,
                    _scoreController,
                    _poolSize
                });
            _ufoSpawner.SetSpawner();
            
            _asteroidSpawner = _instantiator.Instantiate<AsteroidSpawner>(
                new object[]
                {
                    _asteroidPrefab,
                    _spawnOffset,
                    _spawnInterval,
                    _mainCamera,
                    _gameOverController,
                    _scoreController,
                    _poolSize
                });
            _asteroidSpawner.SetSpawner();
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            
            _analyticsController?.Dispose();
        }
    }
}
