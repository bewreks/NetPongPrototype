using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interfaces;
using VContainer;

namespace Preloading
{
    public class Preloading : IDisposable
    {
        public event Action<float> OnProgressChanged = _ => {};
        
        private float _progress = 0f;
        private int _totalWeight = 0;
        private int _currentWeight = 0;
        
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private List<ILoadingStep>[] _steps = new List<ILoadingStep>[5];

        public Preloading()
        {
            OnProgressChanged(_progress);
            for (var i = 0; i < _steps.Length; i++)
            {
                _steps[i] = new List<ILoadingStep>();
            }
        }

        public async UniTask StartLoading(IContainerBuilder builder)
        {
            foreach (var steps in _steps)
            {
                await UniTask.WhenAll(steps.Select(step => step.Load(builder, OnProgressChangedInternal, _cancellationTokenSource.Token)))
                             .AttachExternalCancellation(_cancellationTokenSource.Token);
            }
        }

        public void AddStep(ILoadingStep step, int stepId)
        {
            _totalWeight += step.Weight;
            _steps[stepId].Add(step);
        }
        
        private void OnProgressChangedInternal(int weight)
        {
            _currentWeight += weight;
            _progress = _currentWeight / (float) _totalWeight;
            OnProgressChanged(_progress);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }
}