using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interfaces;
using MessagePipe;
using VContainer;

namespace Messages
{
    public class StartMessagesLoadingStep : ILoadingStep
    {

        public int Weight => 1;
        
        public UniTask Load(IContainerBuilder builder, Action<int> callback, CancellationToken token)
        {
            var options = builder.RegisterMessagePipe();
            return UniTask.CompletedTask;
        }
    }
}