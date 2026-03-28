using _Project.Scripts.Services.Addressebles;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();
            Container.BindInterfacesAndSelfTo<AddressablesAssetsInitializer>().AsSingle();
        }
    }
}