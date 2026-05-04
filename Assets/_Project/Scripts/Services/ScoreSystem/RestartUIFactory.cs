using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.ScoreSystem
{
    public class RestartUIFactory : IRestartUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly RestartPanelUI _restartPrefab;
    
        public RestartUIFactory(IInstantiator instantiator, RestartPanelUI restartPrefab)
        {
            _instantiator = instantiator;
            _restartPrefab = restartPrefab;
        }
    
        public RestartPanelUI Create(int score, int record)
        {
            var restartPanel = _instantiator.InstantiatePrefabForComponent<RestartPanelUI>(_restartPrefab);
            restartPanel.SetScore(score);
            restartPanel.SetRecord(record);
        
            return restartPanel;
        }
    }
}
