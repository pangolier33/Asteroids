using UnityEngine;

namespace _Project.Scripts.Creatures.Enemy
{
    public class AsteroidMovement : MonoBehaviour
    {
        [SerializeField] private float _minSpawnSpeed = 1f;
        [SerializeField] private float _maxSpawnSpeed = 2f;
        private void OnEnable()
        {
            Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(Random.value, Random.value).normalized;
            float spawnSpeed = Random.Range(_minSpawnSpeed, _maxSpawnSpeed);
            rigidBody2D.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
        }
    }
}
