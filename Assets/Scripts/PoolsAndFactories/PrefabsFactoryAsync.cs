using Cysharp.Threading.Tasks;
using Interfaces.PoolsAndFactories;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace PoolsAndFactories
{
    public class PrefabsFactoryAsync<T> : IPrefabFactoryAsync<T> where T : MonoBehaviour, IFactoryObject
    {
        [Inject] private IObjectResolver _resolver;

        public async UniTask<T> CreateAsync(string key)
        {
            var prefab = await Addressables.LoadAssetAsync<GameObject>(key);
            var instance = _resolver.Instantiate(prefab);
            var obj = instance.GetComponent<T>();
            return obj;
        }
    }

    public class PrefabsFactory<T, TArgs> : IPrefabFactoryAsync<T, TArgs> where T : MonoBehaviour, IFactoryObject<TArgs>
    {
        [Inject] private IObjectResolver _resolver;

        public async UniTask<T> CreateAsync(string key, TArgs args)
        {
            var prefab = await Addressables.LoadAssetAsync<GameObject>(key);
            var instance = _resolver.Instantiate(prefab);
            var obj = instance.GetComponent<T>();
            obj.Initialize(args);
            return obj;
        }
    }

    public class PrefabsFactory<T, TArgs1, TArgs2> : IPrefabFactoryAsync<T, TArgs1, TArgs2> where T : MonoBehaviour, IFactoryObject<TArgs1, TArgs2>
    {
        [Inject] private IObjectResolver _resolver;

        public async UniTask<T> CreateAsync(string key, TArgs1 args1, TArgs2 args2)
        {
            var prefab = await Addressables.LoadAssetAsync<GameObject>(key);
            var instance = _resolver.Instantiate(prefab);
            var obj = instance.GetComponent<T>();
            obj.Initialize(args1, args2);
            return obj;
        }
    }
    
    public class PrefabsFactory<T, TArgs1, TArgs2, TArgs3> : IPrefabFactoryAsync<T, TArgs1, TArgs2, TArgs3> where T : MonoBehaviour, IFactoryObject<TArgs1, TArgs2, TArgs3>
    {
        [Inject] private IObjectResolver _resolver;

        public async UniTask<T> CreateAsync(string key, TArgs1 args1, TArgs2 args2, TArgs3 args3)
        {
            var prefab = await Addressables.LoadAssetAsync<GameObject>(key);
            var instance = _resolver.Instantiate(prefab);
            var obj = instance.GetComponent<T>();
            obj.Initialize(args1, args2, args3);
            return obj;
        }
    }
}
