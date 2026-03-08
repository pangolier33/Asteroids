using _Project.Scripts.Services;
using _Project.Scripts.Services.Addressebles;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SaveService>().AsSingle();
        Container.Bind<SaveData>().AsSingle();
        Container.BindInterfacesAndSelfTo<AnalyticsService>().AsSingle();
        Container.BindInterfacesAndSelfTo<AddressableReferencesLoader>().AsSingle();
        Container.BindInterfacesAndSelfTo<SceneLoaderService>().AsSingle();
        Container.Bind<LevelPrefabs>().AsSingle();
    }
}
