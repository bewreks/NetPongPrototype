using Interfaces.PoolsAndFactories;
using UnityEngine.Pool;

namespace PoolsAndFactories
{
    public class Factory<T> : IFactory<T> where T : IFactoryObject, new()
    {
        public T Create()
        {
            var obj = new T();
            return obj;
        }
    }
    
    public class Factory<T, TArgs> : Factory<T>, IFactory<T, TArgs> where T : IFactoryObject<TArgs>, new()
    {
        public T Create(TArgs tArgs)
        {
            var obj = new T();
            obj.Initialize(tArgs);
            return obj;
        }
    }
    
    public class Factory<T, TArgs1, TArgs2> : Factory<T>, IFactory<T, TArgs1, TArgs2> where T : IFactoryObject<TArgs1, TArgs2>, new()
    {
        public T Create(TArgs1 args1, TArgs2 args2)
        {
            var obj = new T();
            obj.Initialize(args1, args2);
            return obj;
        }
    }
    
    public class Factory<T, TArgs1, TArgs2, TArgs3> : Factory<T>, IFactory<T, TArgs1, TArgs2, TArgs3> where T : IFactoryObject<TArgs1, TArgs2, TArgs3>, new()
    {
        public T Create(TArgs1 args1, TArgs2 args2, TArgs3 args3)
        {
            var obj = new T();
            obj.Initialize(args1, args2, args3);
            return obj;
        }
    }

}
