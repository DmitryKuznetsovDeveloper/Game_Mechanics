using System;
using TMPro;
using UI.View;
using UI.ViewModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Installer
{
    public class MetaUIView : MonoBehaviour , IMetaUIView
    {
        [SerializeField] private Button _selectCharacterButton;
        [SerializeField] private TMP_Text _selectedLabel;

        public event Action OnSelectCharacterClicked;

        private void Awake()
        {
            if (_selectCharacterButton != null)
                _selectCharacterButton.onClick.AddListener(HandleClick);
        }

        private void OnDestroy()
        {
            if (_selectCharacterButton != null)
                _selectCharacterButton.onClick.RemoveListener(HandleClick);
        }
        
        public void SetSelectedText(string text)
        {
            if (_selectedLabel != null)
                _selectedLabel.text = text ?? string.Empty;
        }

        private void HandleClick() => OnSelectCharacterClicked?.Invoke();
    }
}