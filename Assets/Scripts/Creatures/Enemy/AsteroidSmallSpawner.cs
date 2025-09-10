using UnityEngine;

public class AsteroidSmallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroid;
    [SerializeField] private int spawnCount = 2;
    [SerializeField] private float spawnForce = 3f;

    public void SpawnSmallerAsteroids()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            Vector2 spawnPos = (Vector2)transform.position + direction * 0.5f;

            GameObject newAsteroid = Instantiate(
                asteroid,
                spawnPos,
                Quaternion.identity
            );

            newAsteroid.transform.localScale = 0.5f * Vector3.one;

            Rigidbody2D rb = newAsteroid.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(direction * spawnForce, ForceMode2D.Impulse);
            }
        }
    }
}
