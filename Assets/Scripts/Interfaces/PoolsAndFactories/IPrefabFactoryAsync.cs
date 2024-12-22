using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Interfaces.PoolsAndFactories
{
    public interface IPrefabFactoryAsync<T> where T : MonoBehaviour, IFactoryObject
    {
        UniTask<T> CreateAsync(string key);
    }
    
    public interface IPrefabFactoryAsync<T, in TArgs> where T : MonoBehaviour, IFactoryObject<TArgs>
    {
        UniTask<T> CreateAsync(string key, TArgs args);
    }
    
    public interface IPrefabFactoryAsync<T, in TArgs1, in TArgs2> where T : MonoBehaviour, IFactoryObject<TArgs1, TArgs2>
    {
        UniTask<T> CreateAsync(string key, TArgs1 args1, TArgs2 args2);
    }
    
    public interface IPrefabFactoryAsync<T, in TArgs1, in TArgs2, in TArgs3> where T : MonoBehaviour, IFactoryObject<TArgs1, TArgs2, TArgs3>
    {
        UniTask<T> CreateAsync(string key, TArgs1 args1, TArgs2 args2, TArgs3 args3);
    }
}
