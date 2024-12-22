using System;
namespace Interfaces.PoolsAndFactories
{
    public interface IPoolableObject : 
        IFactoryObject, IDisposable
    {
        void Reinitialize();
        void OnReturnToPool();
    }

    public interface IPoolableObject<in TArgs> : 
        IPoolableObject, IFactoryObject<TArgs> 
    {
        void Reinitialize(TArgs tArgs);
    }

    public interface IPoolableObject<in TArgs1, in TArgs2> : 
        IPoolableObject, IFactoryObject<TArgs1, TArgs2> 
    {
        void Reinitialize(TArgs1 tArgs1, TArgs2 tArgs2);
    }

    public interface IPoolableObject<in TArgs1, in TArgs2, in TArgs3> : 
        IPoolableObject, IFactoryObject<TArgs1, TArgs2, TArgs3> 
    {
        void Reinitialize(TArgs1 tArgs1, TArgs2 tArgs2, TArgs3 tArgs3);
    }
}
