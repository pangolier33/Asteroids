using UnityEngine;

namespace _Project.Scripts.Creatures.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private void OnDisable()
        {
            EnemyEvents.EnemyDestroyed();
        }

        public void DestroyEnemy()
        {
            Destroy(gameObject);
        }
    }
}
