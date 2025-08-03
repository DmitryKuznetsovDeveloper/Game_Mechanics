namespace Core.Shared.Components
{
    public sealed class TeamComponent
    {
        private bool _isPlayer;
        
        public TeamComponent(bool isPlayer)
        {
            _isPlayer = isPlayer;
        }

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