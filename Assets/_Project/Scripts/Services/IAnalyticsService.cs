using System.Threading.Tasks;

namespace _Project.Scripts.Services
{
    public interface IAnalyticsService
    {
        public Task<bool> Initialize();
        public void LogEvent(string eventName, string parameterName = null, string parameterValue = null);
        public void LogGameStart();
        public void LogEnemyKilled(string enemyType, int totalScore);
        public void LogWeaponUsedCount(string weaponType, int usageCount);
        public void LogIsWeaponUsed(string weaponType);
    }
}