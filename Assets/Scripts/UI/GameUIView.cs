using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class GameUIView : MonoBehaviour
    {
        const float STEP_DURATION = 1f;

        [Header("Buttons")] [SerializeField] private Button _startButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;

        [Header("Countdown")] [SerializeField] private TMP_Text _countdownText;
        [SerializeField] private CanvasGroup _countdownCanvas; // assign in inspector or get in Awake

        [Header("Tween Settings")] [SerializeField]
        private float _buttonTweenDuration = 0.4f;

        [SerializeField] private float _countdownFadeDuration = 0.5f;

        public event Action OnStartClicked;
        public event Action OnPauseClicked;
        public event Action OnResumeClicked;

        void Awake()
        {
            // Ensure CanvasGroup reference
            if (_countdownCanvas == null)
                _countdownCanvas = _countdownText.GetComponent<CanvasGroup>();

            // Subscribe button clicks
            _startButton.onClick.AddListener(() => OnStartClicked?.Invoke());
            _pauseButton.onClick.AddListener(() => OnPauseClicked?.Invoke());
            _resumeButton.onClick.AddListener(() => OnResumeClicked?.Invoke());

            // Initialize countdown visuals
            _countdownCanvas.alpha = 0f;
            _countdownText.gameObject.SetActive(false);

            // Initial button states
            InstantHide(_pauseButton.gameObject);
            InstantHide(_resumeButton.gameObject);
            InstantShow(_startButton.gameObject);
        }

        private void InstantShow(GameObject go)
        {
            go.SetActive(true);
            go.transform.DOKill(); // kill any running tweens
            go.transform.localScale = Vector3.one;
        }

        private void InstantHide(GameObject go)
        {
            go.transform.DOKill();
            go.transform.localScale = Vector3.zero;
            go.SetActive(false);
        }

        public void ShowStartButton()
        {
            AnimateHide(_pauseButton.gameObject);
            AnimateHide(_resumeButton.gameObject);
            AnimateShow(_startButton.gameObject);
        }

        public void ShowPauseButton()
        {
            AnimateHide(_startButton.gameObject);
            AnimateHide(_resumeButton.gameObject);
            AnimateShow(_pauseButton.gameObject);
        }

        public void ShowResumeButton()
        {
            AnimateHide(_startButton.gameObject);
            AnimateHide(_pauseButton.gameObject);
            AnimateShow(_resumeButton.gameObject);
        }

        public void ShowCountdown()
        {
            // Hide buttons immediately
            InstantHide(_startButton.gameObject);
            InstantHide(_pauseButton.gameObject);
            InstantHide(_resumeButton.gameObject);

            // Prepare countdown text
            _countdownText.gameObject.SetActive(true);
            _countdownCanvas.DOKill();
            _countdownCanvas.alpha = 0f;
            _countdownCanvas.DOFade(1f, _countdownFadeDuration);
        }

        public void HideCountdown()
        {
            _countdownCanvas.DOKill();
            _countdownCanvas.DOFade(0f, _countdownFadeDuration)
                .OnComplete(() => _countdownText.gameObject.SetActive(false));
        }

        public void SetCountdownText(string text)
        {
            _countdownText.text = text;

            var half = STEP_DURATION * 0.5f;
            _countdownCanvas.DOKill();
            _countdownText.transform.DOKill();
            _countdownCanvas.alpha = 0f;
            _countdownText.transform.localScale = Vector3.zero;
            
            DOTween.Sequence()
                .Append(_countdownCanvas.DOFade(1f, half))
                .Join(_countdownText.transform
                    .DOScale(Vector3.one, half)
                    .SetEase(Ease.OutBack))
                // fade out + scale out
                .Append(_countdownCanvas.DOFade(0f, half))
                .Join(_countdownText.transform
                    .DOScale(Vector3.zero, half)
                    .SetEase(Ease.InBack));
        }

        private void AnimateShow(GameObject go)
        {
            go.SetActive(true);
            go.transform.DOKill();
            go.transform.localScale = Vector3.zero;
            go.transform.DOScale(Vector3.one, _buttonTweenDuration)
                .SetEase(Ease.OutBack);
        }

        private void AnimateHide(GameObject go)
        {
            if (!go.activeSelf) return;
            go.transform.DOKill();
            go.transform.DOScale(Vector3.zero, _buttonTweenDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() => go.SetActive(false));
        }
    }
}