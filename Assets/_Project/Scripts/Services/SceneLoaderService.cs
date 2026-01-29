using _Project.Scripts.Services.Addressebles;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Scripts.Services
{
    public class SceneLoaderService : ISceneLoaderService
    {
        [Inject] private LevelPrefabs _levelPrefabs;
        private int _bootstrapSceneIndex = 0;
        private int _levelSceneIndex = 1;
        
        public async UniTask LoadLevelScene()
        {
            await LoadSceneAsync(_levelSceneIndex);
        }
        
        public async UniTask LoadBootstrapScene()
        {
            await LoadSceneAsync(_bootstrapSceneIndex);
        }
        
        public async UniTask LoadSceneAsync(int sceneIndex)
        {
            var loadingScreenPrefab = _levelPrefabs.loadingScreenPrefab;
            var loadingScreenInstance = Object.Instantiate(loadingScreenPrefab);

            await SceneManager.LoadSceneAsync(sceneIndex);

            Object.Destroy(loadingScreenInstance);
        }
    }
}
