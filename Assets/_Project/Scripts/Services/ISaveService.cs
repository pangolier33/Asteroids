namespace _Project.Scripts.Services
{
    public interface ISaveService
    {
        void SaveGameData(SaveData data);
        SaveData LoadGameData();
    }
}