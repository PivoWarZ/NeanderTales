using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Power
{
    public class PowerUpgrade: Upgrade, ICharacterStatUpgrade
    {
        private readonly PowerUpgradeConfig _config;
        private IAdditionalDamage _damage;

        public PowerUpgrade(PowerUpgradeConfig config) : base(config)
        {
            _config = config;
        }
        
        public void Construct(GameObject player)
        {
            _damage = player.GetComponent<IAdditionalDamage>();
            OnUpgrade();
        }

        protected override void OnUpgrade()
        {
            int level = Level.CurrentValue;
            var addDamage = _config.GetPower(level);
            _damage.AdditionalPercentDamage += addDamage;
        }
    }
}