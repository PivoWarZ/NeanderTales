using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Health
{
    public class HealthUpgrade: Upgrade, ICharacterStatUpgrade
    {
        private readonly HealthUpgradeConfig _config;
        private ITakeDamageable _hitPoints;

        public HealthUpgrade(HealthUpgradeConfig config) : base(config)
        {
            _config = config;
        }
        
        public void Construct(PlayerService service)
        {
            _hitPoints = service.GetPlayer().GetComponent<ITakeDamageable>();
            OnUpgrade();
        }

        protected override void OnUpgrade()
        {
            int level = Level.CurrentValue;
            float zero = 0;
            var hpAdded = (float) _config.GetHealth(level);
            _hitPoints.AddedtHitPoints(zero, hpAdded);
            Debug.Log($"Health upgrade {level} HP added {hpAdded}");
        }
    }
}