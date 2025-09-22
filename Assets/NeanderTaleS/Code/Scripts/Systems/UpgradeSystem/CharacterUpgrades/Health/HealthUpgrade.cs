using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Health
{
    public class HealthUpgrade: Upgrade
    {
        private readonly HealthUpgradeConfig _config;
        private Player _character;

        public HealthUpgrade(HealthUpgradeConfig config) : base(config)
        {
            _config = config;
        }
        
        public void Construct(Player character)
        {
            _character = character;
            OnUpgrade();
        }

        protected override void OnUpgrade()
        {
            int level = Level.CurrentValue;
            Debug.Log($"Player Health Level: {level} => {_config.GetHealth(level)}");
        }
    }
}