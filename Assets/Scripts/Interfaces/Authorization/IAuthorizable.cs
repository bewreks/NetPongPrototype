using Cysharp.Threading.Tasks;
namespace Interfaces
{
    public interface IAuthorizable
    {
        UniTask<string> Authorize();
    }
}
