using _Project.Scripts.EntryPoints;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Addressebles;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SaveService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnalyticsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AddressableReferencesLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoaderService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BootstrapEntryPoint>().AsSingle();
            Container.Bind<SaveData>().AsSingle();
        }
    }
}
