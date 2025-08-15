using System;
using System.Threading;
using Core.Utilities;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UI.Popup
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public class BasePopup : MonoBehaviour
    {
        public RectTransform Root => _root;
        
        [Header("Refs")] 
        [SerializeField] protected RectTransform _root;
        [SerializeField] protected CanvasGroup _canvasGroup;

        [Header("Animation")] 
        [SerializeField] protected float _showDuration = 0.25f;
        [SerializeField] protected float _hideDuration = 0.20f;
        [SerializeField] protected Ease _showEase = Ease.OutBack;
        [SerializeField] protected Ease _hideEase = Ease.InBack;
        [SerializeField] protected float _scaleFrom = 0.9f;
        [SerializeField] protected float _scaleTo = 1.0f;

        private bool _isVisible;
        private Sequence _seq;

        public bool IsVisible => _isVisible;

        protected virtual void Reset()
        {
            _root = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        protected virtual void Awake()
        {
            if (_root == null) 
                _root = GetComponent<RectTransform>();
            
            if (_canvasGroup == null) 
                _canvasGroup = GetComponent<CanvasGroup>();

            _canvasGroup.alpha = 0f;
            _root.localScale = Vector3.one * _scaleFrom;
            gameObject.SetActive(false);
        }

        public virtual async UniTask ShowAsync(CancellationToken token)
        {
            if (_isVisible) 
                return;
            
            KillTweens();

            gameObject.SetActive(true);
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;

            _canvasGroup.alpha = 0f;
            _root.localScale = Vector3.one * _scaleFrom;

            _seq = DOTween.Sequence()
                .Join(_canvasGroup.DOFade(1f, _showDuration))
                .Join(_root.DOScale(_scaleTo, _showDuration).SetEase(_showEase));

            var wait = _seq.AsyncWaitForCompletion().AsUniTask();
            try
            {
                await (token.CanBeCanceled ? wait.AttachExternalCancellation(token) : wait);
                _isVisible = true;
            }
            catch (OperationCanceledException)
            {
                InstantHide();
                DebugUtil.Log("Отмена анимации попапа");
                throw;
            }
            _isVisible = true;
        }

        public virtual async UniTask HideAsync(CancellationToken token)
        {
            if (!_isVisible && !gameObject.activeSelf) 
                return;

            KillTweens();

            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;

            _seq = DOTween.Sequence()
                .Join(_canvasGroup.DOFade(0f, _hideDuration))
                .Join(_root.DOScale(_scaleFrom, _hideDuration).SetEase(_hideEase));

            var wait = _seq.AsyncWaitForCompletion().AsUniTask();
            try
            {
                await (token.CanBeCanceled ? wait.AttachExternalCancellation(token) : wait);
                _isVisible = true;
            }
            catch (OperationCanceledException)
            {
                InstantHide();
                DebugUtil.Log("Отмена анимации попапа");
                throw;
            }

            _isVisible = false;
            gameObject.SetActive(false);
        }
        
        public void InstantHide()
        {
            KillTweens();
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _root.localScale = Vector3.one * _scaleFrom;
            _isVisible = false;
            gameObject.SetActive(false);
        }

        private void KillTweens()
        {
            DOTween.Kill(_canvasGroup, complete: false);
            DOTween.Kill(_root, complete: false);
            DOTween.Kill(this, complete: false);

            _seq?.Kill(false);
            _seq = null;
        }
    }
}