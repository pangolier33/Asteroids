using _Project.Scripts.Creatures.Enemy;

namespace _Project.Scripts.Creatures.Health
{
    public class AsteroidLargeHealth : Health
    {
        private AsteroidSmallSpawner _asteroidSmallSpawner;
        private Enemy.Enemy _enemy;

        private void Awake()
        {
            _asteroidSmallSpawner = GetComponent<AsteroidSmallSpawner>();
            _enemy = GetComponent<Enemy.Enemy>();
        }
        protected override void Die()
        {
            _asteroidSmallSpawner.SpawnSmallerAsteroids();
            _enemy.DestroyEnemy();
        }
    }
}