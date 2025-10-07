using _Project.Scripts.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts.Tools
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private int _scene = 0;
        
        private SceneLoaderService _sceneLoader = new SceneLoaderService();
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnRestartClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnRestartClick);
        }

        private async void OnRestartClick()
        {
            await _sceneLoader.LoadSceneAsync(_scene);
        }
    }
}
