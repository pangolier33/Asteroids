using System;
using _Project.Scripts.Services;

namespace _Project.Scripts.UI
{
    public class RestartPanelPresenter : IDisposable
    {
        private readonly RestartPanelUIView _view;
        private readonly ISceneLoaderService _sceneLoader;

        public RestartPanelPresenter(
            RestartPanelUIView view,
            ISceneLoaderService sceneLoader,
            int score,
            int record)
        {
            _view = view;
            _sceneLoader = sceneLoader;

            _view.SetScore(score);
            _view.SetRecord(record);

            _view.RestartClicked += OnRestartClicked;
        }

        private async void OnRestartClicked()
        {
            await _sceneLoader.LoadLevelScene();
        }

        public void Dispose()
        {
            _view.RestartClicked -= OnRestartClicked;
        }
    }
}
