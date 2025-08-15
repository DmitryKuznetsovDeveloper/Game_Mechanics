using System.Threading;
using Cysharp.Threading.Tasks;

namespace UI.Popup
{
    public interface IPopupService
    {
        UniTask<T> Get<T>(bool createIfMissing = true) where T : BasePopup;
        UniTask Show<T>(CancellationToken token = default) where T : BasePopup;
        UniTask Show(string key, CancellationToken token = default);
        UniTask Hide<T>(CancellationToken token = default) where T : BasePopup;
        UniTask Hide(string key, CancellationToken token = default);
        UniTask HideAll(CancellationToken token = default);
        bool IsVisible<T>() where T : BasePopup;
        bool IsVisible(string key);
    }
}