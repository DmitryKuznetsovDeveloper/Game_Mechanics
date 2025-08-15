using Features.Player.Configs;
using UnityEngine;

namespace UI.ViewModel
{
    public sealed class PlayerViewModel : IPlayerViewModel
    {
        public PlayerConfig SourceConfig { get; }

        public string Name { get; }
        public int HitPoints { get; }
        public int Damage { get; }
        public float Speed { get; }

        public PlayerViewModel(PlayerConfig playerConfig)
        {
            SourceConfig = playerConfig;
            Name = playerConfig.Name;
            HitPoints = playerConfig.MaxHitPoints;
            Damage = playerConfig.Damage;
            Speed = playerConfig.Speed;
        }
    }
}