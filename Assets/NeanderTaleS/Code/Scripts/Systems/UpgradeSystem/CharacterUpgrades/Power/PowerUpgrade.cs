using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Power
{
    public class PowerUpgrade: Upgrade, IUpgradeSystemConstruct
    {
        private readonly PowerUpgradeConfig _config;
        private IAdditionalDamage _damage;

        public PowerUpgrade(PowerUpgradeConfig config) : base(config)
        {
            _config = config;
        }

        void IUpgradeSystemConstruct.Construct(GameObject player)
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