using UnityEngine;

namespace _Project.Scripts.Creatures.Enemy
{
    public class AsteroidMovement : MonoBehaviour
    {
        private void OnEnable()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(Random.value, Random.value).normalized;
            float spawnSpeed = Random.Range(1f, 2f);
            rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
        }
    }
}
