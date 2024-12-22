using System;
using Interfaces.PoolsAndFactories;
namespace PoolsAndFactories
{
    public class ObjectPool<T> : 
        AObjectPool<T>, IObjectPool<T> 
        where T : class, IDisposable, IPoolableObject, new()
    {
        public ObjectPool(int capacity, int maxPoolSize) : base(capacity, maxPoolSize)
        {
        }

        public T Get()
        {
            var obj = _pool.Get();
            obj.Reinitialize();
            return obj;
        }
    }
    
    public class ObjectPool<T, TArgs> : 
        AObjectPool<T>, IObjectPool<T, TArgs> 
        where T : class, IDisposable, IPoolableObject<TArgs>, new() 
    {
        public ObjectPool(int capacity, int maxPoolSize) : base(capacity, maxPoolSize)
        {
        }
        
        public T Get(TArgs args)
        {
            var obj = _pool.Get();
            obj.Reinitialize(args);
            return obj;
        }
    }
    
    public class ObjectPool<T, TArgs1, TArgs2> : 
        AObjectPool<T>, IObjectPool<T, TArgs1, TArgs2> 
        where T : class, IDisposable, IPoolableObject<TArgs1, TArgs2>, new() 
    {
        public ObjectPool(int capacity, int maxPoolSize) : base(capacity, maxPoolSize)
        {
        }
        
        public T Get(TArgs1 args1, TArgs2 args2)
        {
            var obj = _pool.Get();
            obj.Reinitialize(args1, args2);
            return obj;
        }
    }
    
    public class ObjectPool<T, TArgs1, TArgs2, TArgs3> : 
        AObjectPool<T>, IObjectPool<T, TArgs1, TArgs2, TArgs3> 
        where T : class, IDisposable, IPoolableObject<TArgs1, TArgs2, TArgs3>, new() 
    {
        public ObjectPool(int capacity, int maxPoolSize) : base(capacity, maxPoolSize)
        {
        }
        
        public T Get(TArgs1 args1, TArgs2 args2, TArgs3 args3)
        {
            var obj = _pool.Get();
            obj.Reinitialize(args1, args2, args3);
            return obj;
        }
    } 
}
