using _Project.Scripts.Services;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ISaveService>().To<SaveService>().AsSingle();
        Container.Bind<IAnalyticsService>().To<AnalyticsService>().AsSingle();
    }
}
