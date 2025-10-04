using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Creatures.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public SpaceShipDeath _spaceShipDeath { get; private set; }

        public void Initialize(SpaceShipDeath spaceShipDeath)
        {
            _spaceShipDeath = spaceShipDeath;
        }
        
        public void DestroyEnemy()
        {
            DieEnemy();
            Destroy(gameObject);
        }
        
        private void DieEnemy()
        {
            if (_spaceShipDeath != null)
            {
                _spaceShipDeath.IncrementScore();
            }
        }
    }
}
