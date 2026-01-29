using _Project.Scripts.Services;
using _Project.Scripts.Services.Addressebles;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private LevelPrefabs _levelPrefabs;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SaveService>().AsSingle();
        Container.BindInterfacesAndSelfTo<AnalyticsService>().AsSingle();
        Container.BindInterfacesAndSelfTo<AddressableReferencesLoader>().AsSingle();
        Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle().NonLazy();
        Container.Bind<DiContainer>().AsSingle();
        
        Container.Bind<LevelPrefabs>().FromInstance(_levelPrefabs).AsSingle().NonLazy();
    }
}
