using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Interfaces.Components;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Stamina
{
    public class StaminaUpgrade: Upgrade, ICharacterStatUpgrade
    {
        private readonly StaminaUpgradeConfig _config;
        private IStamina _stamina;

        public StaminaUpgrade(StaminaUpgradeConfig config) : base(config)
        {
            _config = config;
        }
        
        public void Construct(PlayerService service)
        {
            _stamina = service.GetPlayer().GetComponent<IStamina>();
            OnUpgrade();
        }

        protected override void OnUpgrade()
        {
            int level = Level.CurrentValue;
            float notAddedCurrentValue = 0;
            var addStamina = _config.GetStamina(level);
            _stamina.AddedStamina(notAddedCurrentValue, addStamina);
        }
    }
}