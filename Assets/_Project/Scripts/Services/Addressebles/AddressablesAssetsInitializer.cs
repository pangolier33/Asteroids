using _Project.Scripts.Creatures.Player.SpaceShipWeapon;
using _Project.Scripts.EntryPoints;
using _Project.Scripts.Enums;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Services.Addressebles
{
    public class AddressablesAssetsInitializer : IInitializable
    {
        private IAddressableReferencesLoader _loader;
        private DiContainer _container;
        private IInstantiator _instantiator;
        private LevelEntryPoint _levelEntryPoint;

        public AddressablesAssetsInitializer(IAddressableReferencesLoader loader, DiContainer container)
        {
            _loader = loader;
            _container = container;
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
            
            _container.BindInstance(spaceShip).AsSingle();
            _container.BindInstance(hud).AsSingle();
            _container.BindInstance(restartPanel).AsSingle();

            _container.BindInstance(ufo).WithId(ZenjectIDs.UfoPrefab);
            _container.BindInstance(asteroid).WithId(ZenjectIDs.AsteroidPrefab);
            _container.BindInstance(bullet).AsSingle();

            var spaceShipGun = spaceShip.GetComponent<SpaceShipGun>();
            spaceShipGun.Construct(bullet);

            _container.BindInterfacesAndSelfTo<SessionDataManager>().AsSingle();
            var sessionDataManager = _container.Resolve<SessionDataManager>();
            sessionDataManager.Initialize();
            
            _container.BindInterfacesAndSelfTo<LevelEntryPoint>().AsSingle();
            var levelEntryPoint = _container.Resolve<LevelEntryPoint>();
            levelEntryPoint.Initialize();
        }
    }
}
