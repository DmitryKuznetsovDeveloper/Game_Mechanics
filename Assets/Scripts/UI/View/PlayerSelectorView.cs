using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UI.Popup;
using UI.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class PlayerSelectorView : BasePopup
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _hitPoints;
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _prevButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _closeButton;
        
        public event Action NextClicked;
        public event Action PrevClicked;
        public event Action SelectClicked;

        private new void Awake()
        {
            base.Awake();
            _nextButton.onClick.AddListener(() => NextClicked?.Invoke());
            _prevButton.onClick.AddListener(() => PrevClicked?.Invoke());
            _selectButton.onClick.AddListener(() => SelectClicked?.Invoke());
            _closeButton.onClick.AddListener(() => HideAsync(default).Forget());
        }

        private void OnDestroy()
        {
            _nextButton.onClick.RemoveAllListeners();
            _prevButton.onClick.RemoveAllListeners();
            _selectButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }

        public void Render(in IPlayerViewModel player)
        {
            _name.text = player.Name;
            _hitPoints.text = $"HEALTH: {player.HitPoints}";
            _damage.text = $"DAMAGE: {player.Damage}";
            _speed.text = $"SPEED: {player.Speed}";
        }
    }
}