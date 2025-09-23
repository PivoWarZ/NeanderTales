using NeanderTaleS.Code.Scripts.Core.PlayerComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character
{
    public class CharacterUpgrade: Upgrade
    {
        private readonly CharacterUpgradesConfig _config;
        private ICharacterUpgrade _character;

        public CharacterUpgrade(CharacterUpgradesConfig config) : base(config)
        {
            _config = config;
        }
        
        public void Construct(ICharacterUpgrade character)
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
            Debug.Log($"CharacterUpgrade:");
            Debug.Log($"Speed: {speed}");
            Debug.Log($"Stamina: {stamina}");
            Debug.Log($"Power: {power}");
            
            _character.Upgrade(level, speed, stamina, power);

        }
    }
}