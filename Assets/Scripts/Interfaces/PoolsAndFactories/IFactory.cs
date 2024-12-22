namespace Interfaces.PoolsAndFactories
{
    public interface IFactory<out T> where T : IFactoryObject
    {
        T Create();
    }
    
    public interface IFactory<out T, in TArgs> : IFactory<T> where T : IFactoryObject<TArgs>
    {
        T Create(TArgs tArgs);
    }
    
    public interface IFactory<out T, in TArgs1, in TArgs2> : IFactory<T> where T : IFactoryObject<TArgs1, TArgs2>
    {
        T Create(TArgs1 tArgs1, TArgs2 tArgs2);
    }
    
    public interface IFactory<out T, in TArgs1, in TArgs2, in TArgs3> : IFactory<T> where T : IFactoryObject<TArgs1, TArgs2, TArgs3>
    {
        T Create(TArgs1 tArgs1, TArgs2 tArgs2, TArgs3 tArgs3);
    }

}
