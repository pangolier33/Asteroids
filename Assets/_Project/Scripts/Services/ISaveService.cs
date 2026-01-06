namespace _Project.Scripts.Services
{
    public interface ISaveService
    {
        const string SAVE_DATA_KEY = "GameSaveData";
        void SaveGameData(SaveData data);
        SaveData LoadGameData();
    }
}