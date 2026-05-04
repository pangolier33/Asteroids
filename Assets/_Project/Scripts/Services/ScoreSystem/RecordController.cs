namespace _Project.Scripts.Services.ScoreSystem
{
    public class RecordController
    {
        private readonly ISaveService _saveService;
        private SaveData _saveData;
    
        public int CurrentRecord { get; private set; }
    
        public RecordController(ISaveService saveService)
        {
            _saveService = saveService;

            _saveData = _saveService.LoadGameData() ?? new SaveData();
            CurrentRecord = _saveData.record;
        }
        
    
        public bool TryUpdateRecord(int newScore)
        {
            if (newScore <= CurrentRecord) return false;
        
            CurrentRecord = newScore;
            _saveData.record = newScore;          
            _saveService.SaveGameData(_saveData);
            return true;
        }
    }
}
