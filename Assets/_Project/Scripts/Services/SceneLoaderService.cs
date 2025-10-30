using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Services
{
    public class SceneLoaderService
    {
        public async Task LoadSceneAsync(int sceneIndex)
        {
            GameObject loadingScreenPrefab = Resources.Load<GameObject>("LoadingScreen");
            GameObject loadingScreenInstance = Object.Instantiate(loadingScreenPrefab);

            await SceneManager.LoadSceneAsync(sceneIndex);

            Object.Destroy(loadingScreenInstance);
        }
    }
}
