using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private GameCycle _gameCycle;

        private void Awake()
        {
            _gameCycle = ServiceLocator.Resolve<GameCycle>();
        }

        public void StartGame()
        {
            Debug.Log("Game started");
            _gameCycle.StartGame();
        }
        
        public void PauseGame()
        {
            Debug.Log("Game pause");
            _gameCycle.PauseGame();
        }
        
        public void ResumeGame()
        {
            Debug.Log("Game resumed");
            _gameCycle.ResumeGame();
        }

        public void FinishGame()
        {
            Debug.Log("Game finished");
            _gameCycle.FinishGame();
            ServiceLocator.Clear();
        }

        public void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartGame();
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
            {
                FinishGame();
            }
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
            {
                PauseGame();
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha4))
            {
                ResumeGame();
            }
        }
    }
}