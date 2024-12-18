using Cysharp.Threading.Tasks;
namespace Interfaces
{
    public interface IAuthorization
    {
        UniTask<IAuthorizationModel> GetModel();
        
        UniTask<string> Authorize();
    }
}
