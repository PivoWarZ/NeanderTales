using System;
using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.Core.Services;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades
{
    public sealed class StatsUpgradesInstaller: IDisposable
    {
        public event Action<Upgrade> OnUpgradeConstructed;
        private readonly List<Upgrade> _upgrades;
        private PlayerService _servise;

        public StatsUpgradesInstaller(Upgrade[] upgrades)
        {
            _upgrades = upgrades.ToList();
        }
        
        public void Initialize(GameObject player)
        {
            for (var index = 0; index < _upgrades.Count; index++)
            {
                var upgrade = _upgrades[index];
                
                if (upgrade is IUpgradeSystemConstruct construct)
                {
                    construct.Construct(player);
                    OnUpgradeConstructed?.Invoke(upgrade);
                }
            }
        }

        public List<Upgrade> Upgrades => _upgrades;
        public void Dispose() => _upgrades.Clear();
    }
}