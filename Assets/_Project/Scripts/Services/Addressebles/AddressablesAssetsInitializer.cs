using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.EntryPoints;
using _Project.Scripts.Enums;
using _Project.Scripts.Services.ScoreSystem;
using _Project.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
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

            var spaceShipInstance =
                _instantiator.InstantiatePrefabForComponent<SpaceShipMovement>(spaceShip);

            var hudInstance =
                _instantiator.InstantiatePrefabForComponent<HUDView>(hud);

            _container.BindInstance(spaceShipInstance).AsSingle();
            _container.BindInstance(hudInstance).AsSingle();
            _container.BindInstance(restartPanel).AsSingle();

            _container.BindInstance(ufo).WithId(ZenjectIDs.UfoPrefab);
            _container.BindInstance(asteroid).WithId(ZenjectIDs.AsteroidPrefab);
            _container.BindInstance(bullet).AsSingle();

            var gun = spaceShipInstance.GetComponent<SpaceShipGun>();
            gun.Construct(bullet, _instantiator);
            
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
