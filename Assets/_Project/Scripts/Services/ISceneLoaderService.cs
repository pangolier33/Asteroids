using System.Threading.Tasks;

namespace _Project.Scripts.Services
{
    public interface ISceneLoaderService
    {
        Task LoadLevelScene();
        Task LoadBootstrapScene();
        Task LoadSceneAsync(int sceneIndex);
    }
}