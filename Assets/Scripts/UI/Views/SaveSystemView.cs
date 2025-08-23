using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public interface ISaveSystemView
    {
        event System.Action OnSaveClicked;
        event System.Action OnLoadClicked;

        void SetSaveButtonInteractable(bool interactable);
        void SetLoadButtonInteractable(bool interactable);
        void ShowMessage(string message);
    }
    
    public class SaveSystemView : MonoBehaviour, ISaveSystemView
    {
        [Header("UI Buttons")]
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;

        public event Action OnSaveClicked;
        public event Action OnLoadClicked;

        private void OnEnable()
        {
            _saveButton.onClick.AddListener(() => OnSaveClicked?.Invoke());
            _loadButton.onClick.AddListener(() => OnLoadClicked?.Invoke());
        }

        private void OnDisable()
        {
            _saveButton.onClick.RemoveAllListeners();
            _loadButton.onClick.RemoveAllListeners();
        }

        public void SetSaveButtonInteractable(bool interactable)
        {
            _saveButton.interactable = interactable;
        }

        public void SetLoadButtonInteractable(bool interactable)
        {
            _loadButton.interactable = interactable;
        }

        public void ShowMessage(string message)
        {
            Debug.Log(message);
        }
    }
}