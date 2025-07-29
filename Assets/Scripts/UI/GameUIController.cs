using System.Collections;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameUIController : MonoBehaviour, IGameFinishListener
    {
        [Header("UI Elements")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private TMP_Text _countdownText;

        [Header("Timer")]
        [SerializeField] private float _countdownTime = 3f;

        private GameManager _gameManager;
        private Coroutine _countdownRoutine;

        private void Awake()
        {
            _gameManager = ServiceLocator.Resolve<GameManager>();
            ServiceLocator.Resolve<GameCycle>().AddListener(this);

            _startButton.onClick.AddListener(HandleStartClicked);
            _pauseButton.onClick.AddListener(HandlePauseClicked);
            _resumeButton.onClick.AddListener(HandleResumeClicked);

            ShowOnly(_startButton);
            _countdownText.gameObject.SetActive(false);
        }

        public void OnFinishGame()
        {
            _startButton.onClick.RemoveListener(HandleStartClicked);
            _pauseButton.onClick.RemoveListener(HandlePauseClicked);
            _resumeButton.onClick.RemoveListener(HandleResumeClicked);

            StopCountdown();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void HandleStartClicked()
        {
            _startButton.gameObject.SetActive(false);
            StopCountdown();
            _countdownRoutine = StartCoroutine(CountdownThenStart());
        }

        private void HandlePauseClicked()
        {
            _gameManager.PauseGame();
            ShowOnly(_resumeButton);
        }

        private void HandleResumeClicked()
        {
            _gameManager.ResumeGame();
            ShowOnly(_pauseButton);
        }

        private IEnumerator CountdownThenStart()
        {
            _countdownText.gameObject.SetActive(true);

            float timer = _countdownTime;
            while (timer > 0f)
            {
                _countdownText.text = Mathf.CeilToInt(timer).ToString();
                yield return null;
                timer -= Time.deltaTime;
            }

            _countdownText.gameObject.SetActive(false);
            _gameManager.StartGame();
            ShowOnly(_pauseButton);

            _countdownRoutine = null;
        }
        
        private void StopCountdown()
        {
            if (_countdownRoutine != null)
            {
                StopCoroutine(_countdownRoutine);
                _countdownRoutine = null;
                _countdownText.gameObject.SetActive(false);
            }
        }

        private void ShowOnly(Button buttonToShow)
        {
            _startButton.gameObject.SetActive(buttonToShow == _startButton);
            _pauseButton.gameObject.SetActive(buttonToShow == _pauseButton);
            _resumeButton.gameObject.SetActive(buttonToShow == _resumeButton);
        }
    }
}
