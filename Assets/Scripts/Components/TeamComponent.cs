using UnityEngine;

namespace Components
{
    public sealed class TeamComponent : MonoBehaviour
    {
        [SerializeField] private bool _isPlayer;
        public bool IsPlayer
        {
            get => _isPlayer;
            set
            {
                if (_isPlayer.Equals(value)) 
                    return;
                
                _isPlayer = value;
            }
        }
    }
}