using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Scripts.Services.Addressebles
{
    public class AddressableLoader : IAssetLoader
    {
        protected readonly Dictionary<string, Component> CachedComponent = new();
        private readonly Dictionary<string, AsyncOperationHandle<GameObject>> _loadedPrefabs = new();

        public async UniTask<T> LoadAssetComponent<T>(string assetId) where T : Component
        {
            if (CachedComponent.TryGetValue(assetId, out var cachedComponent) && cachedComponent != null)
            {
                if (cachedComponent is T typedComponent)
                    return typedComponent;

                throw new InvalidCastException(
                    $"Cached component of type {cachedComponent.GetType()} cannot be cast to {typeof(T)}");
            }

            var handle = Addressables.LoadAssetAsync<GameObject>(assetId);
            
            _loadedPrefabs[assetId] = handle;
            
            var prefab = await handle.Task;

            if (prefab == null)
            {
                throw new NullReferenceException($"Addressables.LoadAssetAsync returned null for key '{assetId}'");
            }

            if (prefab.TryGetComponent(out T component) == false)
            {
                throw new NullReferenceException(
                    $"Component of type {typeof(T)} not found on prefab '{prefab.name}'"
                );
            }

            CachedComponent[assetId] = component;
            component.gameObject.SetActive(true);
            return component;
        }

        public void ReleaseAsset(string assetId)
        {
            var obj = CachedComponent[assetId].gameObject;
            if (obj == null)
                return;

            obj.SetActive(false);
            Addressables.ReleaseInstance(obj);
            CachedComponent.Remove(assetId);
        }

        public void ReleaseAll()
        {
            Debug.Log($"Releasing all {_loadedPrefabs.Count} loaded prefabs");
        
            foreach (var kvp in _loadedPrefabs)
            {
                if (kvp.Value.IsValid())
                {
                    Addressables.Release(kvp.Value);
                }
            }
        
            _loadedPrefabs.Clear();
        }
    }
}