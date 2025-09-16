using _Project.Scripts.Creatures.Player;
using _Project.Scripts.Creatures.Health;
using UnityEngine;

namespace _Project.Scripts.Components
{
    public class EnemyDamageComponent : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Health health) && other.gameObject.TryGetComponent(out SpaceShipMovement spaceShipMovement))
            {
                health.TakeDamage(_damage);
            }
        }
    }
}
