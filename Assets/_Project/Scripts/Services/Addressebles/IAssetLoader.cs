using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Services.Addressebles
{
    public interface IAssetLoader
    {
        UniTask<T> LoadAssetComponent<T>(string assetId) where T : Component;
        void ReleaseAsset(string assetId);
        void ReleaseAll();
    }
}