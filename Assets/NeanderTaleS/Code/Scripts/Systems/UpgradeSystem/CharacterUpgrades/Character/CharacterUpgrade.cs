using NeanderTaleS.Code.Scripts.Core.PlayerComponents;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades.Character
{
    public class CharacterUpgrade: Upgrade
    {
        private readonly CharacterUpgradesConfig _config;
        private IUpgradePlayer _upgradePlayer;

        public CharacterUpgrade(CharacterUpgradesConfig config) : base(config)
        {
            _config = config;
        }
        
        public void Construct(IUpgradePlayer upgradePlayer)
        {
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
}