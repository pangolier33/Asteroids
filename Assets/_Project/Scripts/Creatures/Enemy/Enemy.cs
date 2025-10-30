using System;
using _Project.Scripts.Creatures.Health;
using UnityEngine;

namespace _Project.Scripts.Creatures.Enemy
{
    public class Enemy : MonoBehaviour, ICreatureDied
    {
        public Action<Enemy> OnDied;

        public void CreatureDied()
        {
            if (OnDied != null)
            {
                OnDied(this);
            }
        }
    }
}
