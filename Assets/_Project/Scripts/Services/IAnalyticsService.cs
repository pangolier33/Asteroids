using System.Threading.Tasks;

namespace _Project.Scripts.Services
{
    public interface IAnalyticsService
    {
        Task<bool> Initialize();
        
        void LogEvent(string eventName, string parameterName = null, string parameterValue = null);
        
        void LogGameStart();
        void LogEnemyKilled(string enemyType, int totalScore);
        void LogWeaponUsedCount(string weaponType, int usageCount);
        void LogIsWeaponUsed(string weaponType);
    }
}