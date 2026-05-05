using _Project.Scripts.Services;
using _Project.Scripts.Services.ScoreSystem;
using Zenject;

namespace _Project.Scripts.UI
{
    public class RestartUIFactory : IRestartUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly RestartPanelUIView _restartPrefab;
        private readonly ISceneLoaderService _sceneLoader;

        public RestartUIFactory(
            IInstantiator instantiator,
            RestartPanelUIView restartPrefab,
            ISceneLoaderService sceneLoader)
        {
            _instantiator = instantiator;
            _restartPrefab = restartPrefab;
            _sceneLoader = sceneLoader;
        }

        public RestartPanelUIView Create(int score, int record)
        {
            var view = _instantiator
                .InstantiatePrefabForComponent<RestartPanelUIView>(_restartPrefab);

            var presenter = new RestartPanelPresenter(
                view,
                _sceneLoader,
                score,
                record
            );
            
            return view;
        }
    }
}
