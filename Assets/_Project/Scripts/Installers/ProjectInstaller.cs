using _Project.Scripts.Services;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SaveService>().AsSingle();
        Container.BindInterfacesAndSelfTo<AnalyticsService>().AsSingle();
    }
}
