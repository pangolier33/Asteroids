using System;
using _Project.Scripts.Creatures.Health;
using UnityEngine;

namespace _Project.Scripts.Creatures.Enemy
{
    public class Enemy : MonoBehaviour, ICreatureDied
    {
        public event Action<Enemy> OnDied;

        public void CreatureDied()
        {
            OnDied?.Invoke(this);
        }
    }
}
