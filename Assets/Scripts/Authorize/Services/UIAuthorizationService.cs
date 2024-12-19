using System;
using Cysharp.Threading.Tasks;
using Interfaces;
namespace Authorize
{
    public class UIAuthorizationService : IAuthorizationService
    {

        public UniTask<IAuthorizationModel> GetModel()
        {
            throw new System.NotImplementedException();
        }

        public UniTask<string> Authorize()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
