using _Project.Scripts.Services;
using UnityEngine;

public class BootstrapLoader : MonoBehaviour
{
    [SerializeField] private int _levelSceneIndex = 1;
    private async void Start()
    {
        SceneLoaderService sceneLoaderService = new SceneLoaderService();
        await sceneLoaderService.LoadSceneAsync(_levelSceneIndex);
    }
}
