using System;
using UnityEngine;
using _Project.Scripts.Creatures.Enemy;

namespace _Project.Scripts.Creatures.Health
{
    public class EnemyHealth : Health
    {
        private Enemy.Enemy _enemy;

        private void Awake()
        {
            _enemy = GetComponent<Enemy.Enemy>();
        }

        protected override void Die()
        {
            _enemy.DestroyEnemy();
        }
    }
}