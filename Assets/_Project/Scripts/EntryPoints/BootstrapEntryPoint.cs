using _Project.Scripts.Services;
using _Project.Scripts.Services.Addressebles;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.EntryPoints
{
    public class BootstrapEntryPoint : IInitializable
    {
        [Inject] private LevelPrefabs _levelPrefabs;
        [Inject] private ISceneLoaderService _sceneLoaderService;
        [Inject] private IAddressableReferencesLoader _addressableReferencesLoader;

        public async void Initialize()
        {
            await LoadAssets();
            await LoadLevelScene();
        }

        private async UniTask LoadAssets()
        {
            var (hud, spaceShip, ufo, asteroid, loadingScreen, restartPanelUI, bullet) = await UniTask.WhenAll(_addressableReferencesLoader.CreateHUD(),
                _addressableReferencesLoader.CreateSpaceShip(), _addressableReferencesLoader.CreateUFO(),
                _addressableReferencesLoader.CreateAsteroid(), _addressableReferencesLoader.CreateLoadingScreen(), _addressableReferencesLoader.CreateRestartPanelUI(), _addressableReferencesLoader.CreateBullet());

            _levelPrefabs.hudPrefab = hud;
            _levelPrefabs.spaceShipPrefab = spaceShip;
            _levelPrefabs.asteroidPrefab = asteroid;
            _levelPrefabs.ufoPrefab = ufo;
            _levelPrefabs.loadingScreenPrefab = loadingScreen;
            _levelPrefabs.restartPanelUIPrefab = restartPanelUI;
            _levelPrefabs.bulletPrefab = bullet;
            
            _addressableReferencesLoader.ReleaseAllAssets();
        }

        private async UniTask LoadLevelScene()
        {
            await _sceneLoaderService.LoadLevelScene();
        }
    }
}
