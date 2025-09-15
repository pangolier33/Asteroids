using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Tools
{
    public class PoolBase<T>
    {
        private readonly Func<T> _preloadFunc;
        private readonly Action<T> _getAction;
        private readonly Action<T> _returnAction;

        private Queue<T> _pool = new();
        private List<T> _activeItems = new();

        public PoolBase(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
        {
            _preloadFunc = preloadFunc;
            _getAction = getAction;
            _returnAction = returnAction;

            if (preloadFunc == null)
            {
                Debug.LogError("Preload function is null");
                return;
            }

            for (int i = 0; i < preloadCount; i++)
                Return(preloadFunc());
        }

        public T Get()
        {
            T item = _pool.Count > 0 ? _pool.Dequeue() : _preloadFunc();

            _getAction(item);

            _activeItems.Add(item);

            return item;
        }

        public void Return(T item)
        {
            _returnAction(item);

            _pool.Enqueue(item);
            _activeItems.Remove(item);
        }

        public void ReturnAll()
        {
            List<T> newList = new List<T>(_activeItems);

            foreach (var activeItem in newList)
                Return(activeItem);
        }
    }
}
