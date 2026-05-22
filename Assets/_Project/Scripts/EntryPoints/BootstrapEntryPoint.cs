using _Project.Scripts.Services;
using _Project.Scripts.Services.Addressebles;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.EntryPoints
{
    public class BootstrapEntryPoint : IInitializable
    {
        private LevelAssetsStorage _storage;
        private ISceneLoaderService _sceneLoaderService;
        private IAddressableReferencesLoader _addressableReferencesLoader;

        public BootstrapEntryPoint(LevelAssetsStorage storage, ISceneLoaderService sceneLoaderService,
            IAddressableReferencesLoader addressableReferencesLoader)
        {
            _storage = storage;
            _sceneLoaderService = sceneLoaderService;
            _addressableReferencesLoader = addressableReferencesLoader;
        }

        public void Initialize()
        {
            PrepareAndLoadScene().Forget();
        }
        
        private async UniTaskVoid PrepareAndLoadScene()
        {
            await LoadAssets();
            await LoadLevelScene();
        }

        private async UniTask LoadAssets()
        {
            var (hud, spaceShip, ufo, asteroid,  restartPanelUI, bullet) = await UniTask.WhenAll(_addressableReferencesLoader.CreateHUD(),
                _addressableReferencesLoader.CreateSpaceShip(), _addressableReferencesLoader.CreateUFO(),
                _addressableReferencesLoader.CreateAsteroid(),  _addressableReferencesLoader.CreateRestartPanelUI(), _addressableReferencesLoader.CreateBullet());

            _storage.HudPrefab = hud;
            _storage.SpaceShipPrefab = spaceShip;
            _storage.AsteroidPrefab = asteroid;
            _storage.UfoPrefab = ufo;
            _storage.RestartPanelPrefab = restartPanelUI;
            _storage.BulletPrefab = bullet;
            
            _addressableReferencesLoader.ReleaseAllAssets();
        }

        private async UniTask LoadLevelScene()
        {
            await _sceneLoaderService.LoadLevelScene();
        }
    }
}
