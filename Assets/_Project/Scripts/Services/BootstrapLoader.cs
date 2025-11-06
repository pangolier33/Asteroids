using UnityEngine;

namespace _Project.Scripts.Services
{
    public class BootstrapLoader : MonoBehaviour
    {
        [SerializeField] private int _levelSceneIndex = 1;
        private async void Start()
        {
            SceneLoaderService sceneLoaderService = new SceneLoaderService();
            await sceneLoaderService.LoadSceneAsync(_levelSceneIndex);
        }
    }
}
