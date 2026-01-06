using UnityEngine;

namespace _Project.Scripts.Services
{
    public class BootstrapLoader : MonoBehaviour
    {
        private void Start()
        {
            LoadLevel();
        }

        private async void LoadLevel()
        {
            SceneLoaderService sceneLoaderService = new SceneLoaderService();
            await sceneLoaderService.LoadLevelScene();
        }
    }
}
