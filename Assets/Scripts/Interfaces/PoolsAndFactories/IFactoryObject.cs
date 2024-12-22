namespace Interfaces.PoolsAndFactories
{
    public interface IFactoryObject
    {
    }
    
    public interface IFactoryObject<in T> : IFactoryObject
    {
        public void Initialize(T t);
    }
    
    public interface IFactoryObject<in T1, in T2> : IFactoryObject
    {
        public void Initialize(T1 t1, T2 t2);
    }
    
    public interface IFactoryObject<in T1, in T2, in T3> : IFactoryObject
    {
        public void Initialize(T1 t1, T2 t2, T3 t3);
    }
    
}
