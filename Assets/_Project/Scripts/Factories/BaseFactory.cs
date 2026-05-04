using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using _Project.Scripts.Tools;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Factories
{
    public class BaseFactory <T> where T : MonoBehaviour
    {
        private IInstantiator _instantiator;
        private PoolBase<T> _pool;
        private T _prefab;
        private int _poolSize;

        public BaseFactory(T prefab, int poolSize, IInstantiator instantiator)
        {
            _prefab = prefab;
            _poolSize = poolSize;
            _instantiator = instantiator;
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
        
        public T GetPooledObject() => _pool.Get();

        public void GetAction(T obj) => obj.gameObject.SetActive(true);

        public List<T> GetActivePrefabs() => _pool.GetAllActiveItems();

        public Queue<T> GetAllPrefabs() => _pool.GetAllItems();

        public void ReturnAction(T obj) => obj.gameObject.SetActive(false);
        
        private T Preload()
        {
            T prefab = _instantiator.InstantiatePrefabForComponent<T>(_prefab);
            prefab.gameObject.SetActive(false);
            return prefab;
        }
    }
}