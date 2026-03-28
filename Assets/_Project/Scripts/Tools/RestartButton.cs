using _Project.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Tools
{
    [RequireComponent(typeof(Button))]
    public class RestartButton : MonoBehaviour
    {
        private ISceneLoaderService _sceneLoader;
        private Button _button;

        [Inject] 
        public void Construct(ISceneLoaderService sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

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
            await _sceneLoader.LoadLevelScene();
        }
    }
}
