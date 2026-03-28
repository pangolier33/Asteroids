using _Project.Scripts.Services;
using _Project.Scripts.Services.Addressebles;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.EntryPoints
{
    public class BootstrapEntryPoint : IInitializable
    {
        private ISceneLoaderService _sceneLoaderService;
        
        [Inject]
        public void Construct(ISceneLoaderService sceneLoaderService)
        {
            _sceneLoaderService = sceneLoaderService;
        }

        public async void Initialize()
        {
            await LoadLevelScene();
        }
        private async UniTask LoadLevelScene()
        {
            await _sceneLoaderService.LoadLevelScene();
        }
    }
}
