using _Project.Scripts.Creatures.Enemy;
using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.Spawners
{
    public class UfoSpawner : EnemySpawner
    {
        public UfoSpawner(Enemy enemyPrefab, float spawnOffset, WaitForSeconds spawnInterval, Camera mainCamera, SessionDataManager sessionDataManager, int poolSize) : base(enemyPrefab, spawnOffset, spawnInterval, mainCamera, sessionDataManager, poolSize)
        {
        }
        
        public override void HandleEnemyDied(Enemy enemy)
        {
            SessionDataManager.AddKillUfoEvent();
            _enemyFactory.ReturnAction(enemy);
        }
    }
}