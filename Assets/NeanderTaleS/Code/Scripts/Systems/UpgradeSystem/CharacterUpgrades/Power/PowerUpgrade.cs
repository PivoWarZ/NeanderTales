using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Power
{
    public class PowerUpgrade: Upgrade
    {
        private readonly PowerUpgradeConfig _config;
        private Player _character;

        public PowerUpgrade(PowerUpgradeConfig config) : base(config)
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
            Debug.Log($"Player Power Level: {level} => {_config.GetPower(level)}");
        }
    }
}