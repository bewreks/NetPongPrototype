using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Interfaces
{
    public interface ILoadingStep
    {
        int Weight { get; }
        
        UniTask Load(IContainerBuilder builder, Action<int> callback, CancellationToken token);
    }
}