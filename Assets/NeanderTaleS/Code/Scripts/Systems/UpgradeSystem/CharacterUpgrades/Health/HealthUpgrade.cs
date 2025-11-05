using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Health
{
    public class HealthUpgrade: Upgrade, ICharacterStatUpgrade
    {
        private readonly HealthUpgradeConfig _config;
        private IHitPointsComponent _hitPoints;

        public HealthUpgrade(HealthUpgradeConfig config) : base(config)
        {
            _config = config;
        }
        
        public void Construct(GameObject player)
        {
            _hitPoints = player.GetComponent<IHitPointsComponent>();
            OnUpgrade();
        }

        protected override void OnUpgrade()
        {
            int level = Level.CurrentValue;
            var hpAdded = (float) _config.GetHealth(level);
            _hitPoints.AddHitPoints(hpAdded, hpAdded);
        }
    }
}