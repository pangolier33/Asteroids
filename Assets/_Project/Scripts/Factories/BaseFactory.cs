using _Project.Scripts.Tools;
using UnityEngine;

namespace _Project.Scripts.Creatures.Factories
{
    public class BaseFactory <T> where T : MonoBehaviour
    {
        public PoolBase<T> pool;
        
        private T Prefab;
        private int _poolSize;

        public BaseFactory(T prefab, int poolSize)
        {
            Prefab = prefab;
            _poolSize = poolSize;
        }

        public void PoolInitialize()
        {
            pool = new PoolBase<T>(
                    () => Preload(),
                    GetAction,
                    ReturnAction,
                    _poolSize
                );
        }

        private T Preload()
        {
            T prefab = GameObject.Instantiate(Prefab);
            prefab.gameObject.SetActive(false);
            return prefab;
        }

        public void GetAction(T obj) => obj.gameObject.SetActive(true);

        private void ReturnAction(T obj) => obj.gameObject.SetActive(false);
    }
}