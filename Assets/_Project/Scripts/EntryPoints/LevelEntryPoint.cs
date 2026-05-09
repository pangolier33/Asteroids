using System;
using System.Threading;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.Services;
using _Project.Scripts.Services.ScoreSystem;
using _Project.Scripts.Spawners;
using _Project.Scripts.UI;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.EntryPoints
{
    public class LevelEntryPoint : IInitializable, IDisposable
    {
        private readonly SpaceShipMovement _spaceShip;
        private readonly HUDView _hud;

        private readonly UfoSpawner _ufoSpawner;
        private readonly AsteroidSpawner _asteroidSpawner;

        private readonly GameOverController _gameOverController;
        private readonly ScoreController _scoreController;
        private readonly IAnalyticsService _analyticsService;

        private AnalyticsController _analyticsController;
        private SpaceShipLaser _spaceShipLaser;

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        [Inject]
        public LevelEntryPoint(
            SpaceShipMovement spaceShip,
            HUDView hud,
            UfoSpawner ufoSpawner,
            AsteroidSpawner asteroidSpawner,
            GameOverController gameOverController,
            ScoreController scoreController,
            IAnalyticsService analyticsService)
        {
            _spaceShip = spaceShip;
            _hud = hud;

            _ufoSpawner = ufoSpawner;
            _asteroidSpawner = asteroidSpawner;

            _gameOverController = gameOverController;
            _scoreController = scoreController;
            _analyticsService = analyticsService;
        }

        public void Initialize()
        {
            BindGameplayObjects();

            RunSpawners().Forget();
        }

        private void BindGameplayObjects()
        {
            SpaceShipDied spaceShipDied =
                _spaceShip.GetComponent<SpaceShipDied>();

            SpaceShipGun spaceShipGun =
                _spaceShip.GetComponent<SpaceShipGun>();

            _spaceShipLaser =
                _spaceShip.GetComponent<SpaceShipLaser>();

            spaceShipDied.Initialize(_gameOverController);

            _analyticsController = new AnalyticsController(
                _analyticsService,
                _gameOverController,
                _scoreController,
                spaceShipGun,
                _spaceShipLaser);

            _analyticsController.Initialize();

            InitializeHud();
        }

        private void InitializeHud()
        {
            _hud.Initialize(_spaceShip, _spaceShipLaser);
        }

        private async UniTaskVoid RunSpawners()
        {
            await UniTask.WhenAll(
                _ufoSpawner.SpawnEnemies(_cts.Token),
                _asteroidSpawner.SpawnEnemies(_cts.Token)
            );
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();

            _analyticsController?.Dispose();
        }
    }
}
