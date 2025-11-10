using System;
using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character
{
    public class CharacterUpgrade: Upgrade, IUpgradeSystemConstruct
    {
        private readonly CharacterUpgradesConfig _config;
        private IUpgradePlayer _upgradePlayer;

        public CharacterUpgrade(CharacterUpgradesConfig config) : base(config)
        {
            _config = config;
        }
        
        public void Construct(GameObject player)
        {
            player.TryGetComponent<IUpgradePlayer>(out IUpgradePlayer upgradePlayer);
            
            if (upgradePlayer == null)
            {
                throw new InvalidOperationException($"Component IUpgradePlayer missing: {player.name}.");
            }
            
            _upgradePlayer = upgradePlayer;

            OnUpgrade();
        }

        protected override void OnUpgrade()
        {
            int level = Level.CurrentValue;
            var health = _config.GetHealth(level);
            var stamina = _config.GetStamina(level);
            var power = _config.GetPower(level);
            
            _upgradePlayer.Upgrade(level, health, stamina, power);
        }
    }

    public interface IUpgradeSystemConstruct
    {
        void Construct(GameObject player);
    }
}