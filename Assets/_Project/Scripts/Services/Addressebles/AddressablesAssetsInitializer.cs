using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.EntryPoints;
using _Project.Scripts.Enums;
using _Project.Scripts.Services.ScoreSystem;
using _Project.Scripts.Spawners;
using _Project.Scripts.Tools;
using _Project.Scripts.UI;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Services.Addressebles
{
    public class AddressablesAssetsInitializer : IInitializable
    {
        private IAddressableReferencesLoader _loader;
        private DiContainer _container;
        private LevelEntryPoint _levelEntryPoint;
        private IInstantiator _instantiator;

        public AddressablesAssetsInitializer(IAddressableReferencesLoader loader, DiContainer container, IInstantiator instantiator)
        {
            _loader = loader;
            _container = container;
            _instantiator = instantiator;
        }
    
        public void Initialize()
        {
            InitializeAsync().Forget();
        }

        private async UniTaskVoid InitializeAsync()
        {
            var (hud, spaceShip, ufo, asteroid, restartPanel, bullet) = await UniTask.WhenAll(
                _loader.CreateHUD(),
                _loader.CreateSpaceShip(),
                _loader.CreateUFO(),
                _loader.CreateAsteroid(),
                _loader.CreateRestartPanelUI(),
                _loader.CreateBullet()
            );
            
            _container.BindInstance(new EnemySpawnerSettings { SpawnOffset = 0.5f, SpawnInterval = 5f }).AsSingle();
            
            _container.BindMemoryPool<Bullet, Bullet.Pool>().WithInitialSize(20).FromComponentInNewPrefab(bullet).UnderTransformGroup("Bullets");
            
            _container.BindMemoryPool<UFO, UFO.Pool>().WithInitialSize(10).FromComponentInNewPrefab(ufo).UnderTransformGroup("Ufos");
            
            _container.BindMemoryPool<Asteroid, Asteroid.Pool>().WithInitialSize(10).FromComponentInNewPrefab(asteroid).UnderTransformGroup("Asteroids");
            
            _container.Bind<UfoSpawner>().AsSingle();
            _container.Bind<AsteroidSpawner>().AsSingle();
            
            var spaceShipInstance = _instantiator.InstantiatePrefabForComponent<SpaceShipMovement>(spaceShip);

            var hudInstance = _instantiator.InstantiatePrefabForComponent<HUDView>(hud);

            _container.BindInstance(spaceShipInstance).AsSingle();
            _container.BindInstance(hudInstance).AsSingle();
            _container.BindInstance(restartPanel).AsSingle();

            _container.BindInstance(ufo).WithId(ZenjectIDs.UfoPrefab);
            _container.BindInstance(asteroid).WithId(ZenjectIDs.AsteroidPrefab);
            _container.BindInstance(bullet).AsSingle();
            
            _container.Bind<ScoreController>().AsSingle();
            _container.BindInterfacesAndSelfTo<RecordController>().AsSingle();
            _container.Bind<IRestartUIFactory>().To<RestartUIFactory>().AsSingle();
            _container.Bind<GameOverController>().AsSingle();
            
            _container.BindInterfacesAndSelfTo<LevelEntryPoint>().AsSingle();
            var levelEntryPoint = _container.Resolve<LevelEntryPoint>();
            levelEntryPoint.Initialize();
        }
    }
}
