using System;
using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character
{
    public class CharacterUpgrade: Upgrade
    {
        public event Action OnCharacterUpgradeConstructed;
        private readonly CharacterUpgradesConfig _config;
        private IUpgradePlayer _player;

        public CharacterUpgrade(CharacterUpgradesConfig config) : base(config)
        {
            _config = config;
        }
        
        public void Construct(GameObject player)
        {
            player.TryGetComponent<IUpgradePlayer>(out IUpgradePlayer upgradePlayer);

            _player = upgradePlayer ?? throw new InvalidOperationException($"Component IUpgradePlayer missing: {player.name}.");
            
            OnUpgrade();
            
            OnCharacterUpgradeConstructed?.Invoke();
        }

        protected override void OnUpgrade()
        {
      
            int level = Level.CurrentValue;
            var health = _config.GetHealth(level);
            var stamina = _config.GetStamina(level);
            var power = _config.GetPower(level);
            
            Stats stats = new()
            {
                Level = level,
                Health = health,
                Stamina = stamina,
                Power = power,
            };
            
            _player.Upgrade(stats);
        }
    }
}