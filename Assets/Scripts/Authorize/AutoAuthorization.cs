using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interfaces;
using Unity.Services.Authentication;
using Unity.Services.Core;
using VContainer.Unity;

namespace Authorize
{
    public class AutoAuthorization : IAuthorization, IInitializable, IDisposable
    {
        private AuthorizationModel _authorizationModel;
        
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public async UniTask<IAuthorizationModel> GetModel()
        {
            await UniTask.WaitUntil(() => _authorizationModel != null, cancellationToken: _cancellationTokenSource.Token);
            return _authorizationModel;
        }

        public async UniTask<string> Authorize()
        {
            await UnityServices.InitializeAsync();
            if (_cancellationTokenSource.IsCancellationRequested) return null; 
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            return _cancellationTokenSource.IsCancellationRequested ? null : AuthenticationService.Instance.PlayerId;
        }

        public async void Initialize()
        {
            _authorizationModel = new AuthorizationModel
            {
                PlayerId = await Authorize() 
            };
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }
}
