using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Core.Services;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades
{
    public class StatsUpgradesInstaller
    {
        private List<Upgrade> _upgrades = new();
        private PlayerService _servise;

        public StatsUpgradesInstaller(Upgrade[] upgrades, PlayerService servise)
        {
            foreach (var upgrade in upgrades)
            {
                if (upgrade is ICharacterStatUpgrade statsUpgrade)
                {
                    _upgrades.Add(upgrade);
                    statsUpgrade.Construct(servise);
                }
            }
        }
        
        public List<Upgrade> Upgrades => _upgrades;
    }
}