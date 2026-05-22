using UnityEngine;
using Zenject;

namespace _Project.Scripts.Factories
{
    public class MonoBehFactory<T> where T : MonoBehaviour
    {
        private readonly T _monoBeh;
        private readonly IInstantiator _instantiator;
        
        public MonoBehFactory(T monoBeh, IInstantiator instantiator)
        {
            _monoBeh = monoBeh;
            _instantiator = instantiator;
        }

        public T Create()
        {
            var monoBeh = _instantiator.InstantiatePrefabForComponent<T>(_monoBeh);
            
            return monoBeh;
        }
    }
}