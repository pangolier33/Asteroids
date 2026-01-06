using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Services
{
    public class SceneLoaderService
    {
        private int _bootstrapSceneIndex = 0;
        private int _levelSceneIndex = 1;
        
        private async Task LoadSceneAsync(int sceneIndex)
        {
            GameObject loadingScreenPrefab = Resources.Load<GameObject>("LoadingScreen");
            GameObject loadingScreenInstance = Object.Instantiate(loadingScreenPrefab);

            await SceneManager.LoadSceneAsync(sceneIndex);

            Object.Destroy(loadingScreenInstance);
        }

        public async Task LoadLevelScene()
        {
            await LoadSceneAsync(_levelSceneIndex);
        }
        
        public async Task LoadBootstraplScene()
        {
            await LoadSceneAsync(_bootstrapSceneIndex);
        }
    }
}
