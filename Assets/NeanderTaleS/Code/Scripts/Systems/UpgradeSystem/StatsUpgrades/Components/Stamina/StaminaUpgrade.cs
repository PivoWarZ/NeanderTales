using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Components.Stamina
{
    public sealed class StaminaUpgrade: Upgrade
    {
        private readonly StaminaUpgradeConfig _config;
        private IStaminaComponent _staminaComponent;

        public StaminaUpgrade(StaminaUpgradeConfig config) : base(config)
        {
            _config = config;
        }

        public override void Construct(GameObject player)
        {
            _staminaComponent = player.GetComponent<IStaminaComponent>();
            OnUpgrade();
        }

        protected override void OnUpgrade()
        {
            int level = Level.CurrentValue;
            var stamina = _config.GetStamina(level);
            _staminaComponent.SetStamina(stamina, stamina);
        }
    }
}