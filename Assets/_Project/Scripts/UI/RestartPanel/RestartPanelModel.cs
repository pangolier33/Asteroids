using _Project.Scripts.Services;
using _Project.Scripts.Services.ScoreSystem;

namespace _Project.Scripts.UI.RestartPanel
{
    public class RestartPanelModel
    {
        private readonly RecordController _recordController;
        private readonly ScoreController _scoreController;

        public ISceneLoaderService SceneLoader { get; }

        public int Record => _recordController.CurrentRecord;
        public int Score => _scoreController.TotalScore;
    
        public RestartPanelModel(RecordController recordController, ScoreController scoreController, ISceneLoaderService sceneLoader)
        {
            _recordController = recordController;
            _scoreController = scoreController;
            SceneLoader = sceneLoader;
        }
    }
}
