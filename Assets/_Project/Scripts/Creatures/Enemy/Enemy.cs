using System;
using _Project.Scripts.Creatures.Health;
using UnityEngine;

namespace _Project.Scripts.Creatures.Enemy
{
    public class Enemy : MonoBehaviour, ICreatureDeath
    {
        public Action OnDisabled;

        public void CreatureDeath()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            if (OnDisabled != null)
                OnDisabled();
        }
    }
}
