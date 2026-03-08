using System;
using _Project.Scripts.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI
{
    public class RestartPanelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _recordText;
        [SerializeField] private Button _button;

        private ISceneLoaderService _sceneLoaderService;
        
        public class Factory : PlaceholderFactory<RestartPanelUI> { }

        [Inject]
        public void Construct(ISceneLoaderService sceneLoaderService)
        {
            _sceneLoaderService = sceneLoaderService;
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnRestartClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnRestartClick);
        }

        public void SetScore(int score)
        {
            _scoreText.text = "Score: " + score;
        }

        public void SetRecord(int record)
        {
            _recordText.text = "Record: " + record;
        }
        
        private async void OnRestartClick()
        {
            await _sceneLoaderService.LoadLevelScene();
        }
    }
}
