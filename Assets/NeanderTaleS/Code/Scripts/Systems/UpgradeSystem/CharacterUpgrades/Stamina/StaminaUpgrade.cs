using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Stamina
{
    public class StaminaUpgrade: Upgrade
    {
        private readonly StaminaUpgradeConfig _config;
        private Player _character;

        public StaminaUpgrade(StaminaUpgradeConfig config) : base(config)
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
            Debug.Log($"Player Stamina Level: {level} => {_config.GetStamina(level)}");
        }
    }
}