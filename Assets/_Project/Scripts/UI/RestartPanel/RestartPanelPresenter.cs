using System;
using _Project.Scripts.Services.ScoreSystem;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.UI.RestartPanel
{
    public class RestartPanelPresenter : IDisposable, IInitializable
    {
        private RestartPanelUIView _view;
        private readonly RestartPanelModel _model;
        private GameOverController _gameOverController;
        private IInstantiator _instantiator;

        public RestartPanelPresenter(RestartPanelUIView view, RestartPanelModel model, GameOverController gameOverController, IInstantiator instantiator)
        {
            _view = view;
            _model = model;
            _gameOverController = gameOverController;
            _instantiator = instantiator;
        }
        
        public void Initialize()
        {
            _view = _instantiator.InstantiatePrefabForComponent<RestartPanelUIView>(_view);
            
            _gameOverController.GameOverEvent += CreateRestartPanelUIView;
            _view.RestartClicked += OnRestartClicked;
            
            _view.gameObject.SetActive(false);
        }
        
        public void Dispose()
        {
            _gameOverController.GameOverEvent -= CreateRestartPanelUIView;
            _view.RestartClicked -= OnRestartClicked;
        }

        private void CreateRestartPanelUIView()
        {
            _view.gameObject.SetActive(true);
            _view.SetRecord(_model.Record);
            _view.SetScore(_model.Score);
        }

        private void OnRestartClicked()
        {
            RestartAsync().Forget();
        }

        private async UniTask RestartAsync()
        {
            await _model.SceneLoader.LoadLevelScene();
        }
    }
}
