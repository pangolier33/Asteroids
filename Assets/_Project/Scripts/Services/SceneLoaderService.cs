using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Services
{
    public class SceneLoaderService : ISceneLoaderService
    {
        private int _bootstrapSceneIndex = 0;
        private int _levelSceneIndex = 1;
        
        public async Task LoadLevelScene()
        {
            await LoadSceneAsync(_levelSceneIndex);
        }
        
        public async Task LoadBootstrapScene()
        {
            await LoadSceneAsync(_bootstrapSceneIndex);
        }
        
        public async Task LoadSceneAsync(int sceneIndex)
        {
            GameObject loadingScreenPrefab = Resources.Load<GameObject>("LoadingScreen");
            GameObject loadingScreenInstance = Object.Instantiate(loadingScreenPrefab);

            await SceneManager.LoadSceneAsync(sceneIndex);

            Object.Destroy(loadingScreenInstance);
        }
    }
}
