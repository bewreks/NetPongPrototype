using Cysharp.Threading.Tasks;

namespace Interfaces
{
    public interface IModelContainer<T>
    {
        UniTask<T> GetModel();
    }
}