using _Project.Scripts.UI;

namespace _Project.Scripts.Services.ScoreSystem
{
    public interface IRestartUIFactory
    {
        RestartPanelUI Create(int score, int record);
    }
}
