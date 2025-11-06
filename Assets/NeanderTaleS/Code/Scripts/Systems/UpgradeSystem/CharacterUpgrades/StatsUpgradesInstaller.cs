using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Core.Services;
using NeanderTaleS.Code.Scripts.Interfaces.Systems;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades
{
    public sealed class StatsUpgradesInstaller: IInitializedAsPlayer
    {
        private readonly List<Upgrade> _upgrades = new();
        private PlayerService _servise;

        public StatsUpgradesInstaller(Upgrade[] upgrades)
        {
            for (var index = 0; index < upgrades.Length; index++)
            {
                var upgrade = upgrades[index];
                _upgrades.Add(upgrade);
            }
        }
        
        public void Initialize(GameObject player)
        {
            for (var index = 0; index < _upgrades.Count; index++)
            {
                var upgrade = _upgrades[index];
                
                if (upgrade is ICharacterStatUpgrade statsUpgrade)
                {
                    statsUpgrade.Construct(player);
                }
            }
        }

        public List<Upgrade> Upgrades => _upgrades;
    }
}