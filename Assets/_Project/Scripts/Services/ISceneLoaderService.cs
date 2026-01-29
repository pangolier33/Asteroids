using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services
{
    public interface ISceneLoaderService
    {
        UniTask LoadLevelScene();
        UniTask LoadBootstrapScene();
        UniTask LoadSceneAsync(int sceneIndex);
    }
}