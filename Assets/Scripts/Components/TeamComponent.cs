using UnityEngine;

namespace Components
{
    public sealed class TeamComponent : MonoBehaviour
    {
        public bool IsPlayer => _isPlayer;
        
        [SerializeField] private bool _isPlayer;
    }
}