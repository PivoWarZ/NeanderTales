using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character
{
    public class CharacterUpgrade: Upgrade
    {
        private readonly CharacterUpgradesConfig _config;
        private Player _character;

        public CharacterUpgrade(CharacterUpgradesConfig config) : base(config)
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
            var speed = _config.GetHealth(level);
            var stamina = _config.GetStamina(level);
            var power = _config.GetPower(level);
            Debug.Log($"CharacterUpgrade доделать было _character.Upgrade");

        }
    }
}