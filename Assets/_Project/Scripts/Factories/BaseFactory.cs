using System.Collections.Generic;
using _Project.Scripts.Tools;
using UnityEngine;

namespace _Project.Scripts.Creatures.Factories
{
    public class BaseFactory <T> where T : MonoBehaviour
    {
        private PoolBase<T> _pool;
        private T _prefab;
        private int _poolSize;

        public BaseFactory(T prefab, int poolSize)
        {
            _prefab = prefab;
            _poolSize = poolSize;
        }

        public void PoolInitialize()
        {
            _pool = new PoolBase<T>(
                    () => Preload(),
                    GetAction,
                    ReturnAction,
                    _poolSize
                );
        }

        private T Preload()
        {
            T prefab = GameObject.Instantiate(_prefab);
            prefab.gameObject.SetActive(false);
            return prefab;
        }
        
        public T GetPrefab() => _pool.Get();

        public void GetAction(T obj) => obj.gameObject.SetActive(true);

        public List<T> GetActivePrefabs() => _pool.GetAllActiveItems();

        public Queue<T> GetAllPrefabs() => _pool.GetAllItems();

        public void ReturnAction(T obj) => obj.gameObject.SetActive(false);
    }
}