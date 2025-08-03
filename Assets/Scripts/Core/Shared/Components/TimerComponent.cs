using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Core.Shared.Components
{
    public sealed class TimerComponent
    {
        public async UniTask CountdownAsync(
            int seconds,
            Action<int> onTick = null,
            CancellationToken cancellationToken = default)
        {
            for (int i = seconds; i > 0; i--)
            {
                onTick?.Invoke(i);
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);
            }
            
            onTick?.Invoke(0);
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);
        }
    }
}