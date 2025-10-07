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
            Object.Instantiate(loadingScreenPrefab);

            AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneIndex);
            loadAsync.allowSceneActivation = false;
        
            while (!loadAsync.isDone)
            {
                if (loadAsync.progress >= 0.9f)
                {
                    await Task.Delay(500);
                    loadAsync.allowSceneActivation = true;
                }
                await Task.Yield();
            }

            Object.Destroy(loadingScreenPrefab);
        }
    }
}
