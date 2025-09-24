using UnityEngine;

namespace _Project.Scripts.Creatures.Enemy
{
    public class AsteroidSmallSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _asteroid;
        [SerializeField] private int _spawnCount = 2;
        [SerializeField] private float _spawnForce = 3f;

        public void SpawnSmallerAsteroids()
        {
            for (int i = 0; i < _spawnCount; i++)
            {
                Vector2 direction = Random.insideUnitCircle.normalized;
                Vector2 spawnPos = (Vector2)transform.position + direction * 0.5f;

                GameObject newAsteroid = Instantiate(
                    _asteroid,
                    spawnPos,
                    Quaternion.identity
                );

                newAsteroid.transform.localScale = 0.5f * Vector3.one;

                Rigidbody2D rigidbody2D = newAsteroid.GetComponent<Rigidbody2D>();
                if (rigidbody2D != null)
                {
                    rigidbody2D.AddForce(direction * _spawnForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
