using System;
namespace Interfaces.PoolsAndFactories
{
    public interface IObjectPool<T> : IObjectPoolBase<T> where T : class, IPoolableObject
    {
        public T Get();
    }
    
    
    public interface IObjectPool<T, in TArgs> : IObjectPoolBase<T> where T : class, IPoolableObject<TArgs>
    {
        public T Get(TArgs args);
    }
    
    
    public interface IObjectPool<T, in TArgs1, in TArgs2> : IObjectPoolBase<T> where T : class, IPoolableObject<TArgs1, TArgs2>
    {
        public T Get(TArgs1 args1, TArgs2 args2);
    }
    
    
    public interface IObjectPool<T, in TArgs1, in TArgs2, in TArgs3> : IObjectPoolBase<T> where T : class, IPoolableObject<TArgs1, TArgs2, TArgs3>
    {
        public T Get(TArgs1 args1, TArgs2 args2, TArgs3 args3);
    }

    public interface IObjectPoolBase<in T> : IDisposable where T : class, IPoolableObject
    {
        public void Release(T element);
        public void Clear();
        
        public int CountAll { get; }
        public int CountActive  { get; }
        public int CountInactive  { get; }
    }
    
}
