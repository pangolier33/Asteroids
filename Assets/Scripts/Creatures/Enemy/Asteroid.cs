using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float spawnSpeed = Random.Range(1f, 2f);
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
    }
}
