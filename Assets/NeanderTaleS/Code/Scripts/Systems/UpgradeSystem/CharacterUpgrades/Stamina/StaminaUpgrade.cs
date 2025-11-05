using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Stamina
{
    public class StaminaUpgrade: Upgrade, ICharacterStatUpgrade
    {
        private readonly StaminaUpgradeConfig _config;
        private IStaminaComponent _staminaComponent;

        public StaminaUpgrade(StaminaUpgradeConfig config) : base(config)
        {
            _config = config;
        }
        
        public void Construct(GameObject player)
        {
            _staminaComponent = player.GetComponent<IStaminaComponent>();
            OnUpgrade();
        }

        protected override void OnUpgrade()
        {
            int level = Level.CurrentValue;
            var addStamina = _config.GetStamina(level);
            _staminaComponent.SetStamina(addStamina, addStamina);
        }
    }
}