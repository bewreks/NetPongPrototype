using Interfaces.PoolsAndFactories;
using VContainer;
namespace PoolsAndFactories
{
    public abstract class AObjectPool<T> : IObjectPoolBase<T> where T : class, IPoolableObject, new()
    {
        [Inject] protected Factory<T> _factory;
        
        protected UnityEngine.Pool.ObjectPool<T> _pool;

        protected AObjectPool(int capacity, int maxPoolSize)
        {
            _pool = new UnityEngine.Pool.ObjectPool<T>(OnCreate, OnGet, OnRelease, OnDestroy, true, capacity, maxPoolSize);
        }

        private void OnDestroy(T obj)
        {
            obj.Dispose();
        }

        private void OnRelease(T obj)
        {
            obj.OnReturnToPool();
        }

        private void OnGet(T obj)
        {
            
        }

        private T OnCreate()
        {
            return _factory.Create();
        }
        
        public void Dispose()
        {
            _pool.Dispose();
        }

        public void Release(T element)
        {
            _pool.Release(element);
        }

        public void Clear()
        {
            _pool.Clear();
        }

        public int CountAll => _pool.CountAll;

        public int CountActive => _pool.CountActive;
        public int CountInactive => _pool.CountInactive;
    }
}
