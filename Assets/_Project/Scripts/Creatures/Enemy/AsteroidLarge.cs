using UnityEngine;

namespace _Project.Scripts.Creatures.Enemy
{
    [RequireComponent(typeof(AsteroidSmallSpawner))]
    public class AsteroidLarge : Enemy
    {
        private AsteroidSmallSpawner _asteroidSmallSpawner;

        protected override void OnEnable()
        {
            _asteroidSmallSpawner = GetComponent<AsteroidSmallSpawner>();
            base.OnEnable();
        }
        
        protected override void DestroyEnemy()
        {
            _asteroidSmallSpawner.SpawnSmallerAsteroids();
            base.DestroyEnemy();
        }
    }
}