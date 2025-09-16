using _Project.Scripts.Creatures.Health;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
        }
    }
}
