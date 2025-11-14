using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Core.Services;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades
{
    public sealed class StatsUpgradesSystemInitializer: IDisposable
    {
        public event Action<Upgrade> OnUpgradeConstructed;
        private PlayerService _servise;
        private StatsUpgradeSystemInstaller _installer = new ();
        
        public void Initialize(GameObject player)
        {
            _installer.Install();
            
            List<Upgrade> upgrades = _installer.Upgrades;
            
            for (var index = 0; index < upgrades.Count; index++)
            {
                var upgrade = upgrades[index];
                
                if (upgrade is IUpgradeSystemConstruct construct)
                {
                    construct.Construct(player);
                    OnUpgradeConstructed?.Invoke(upgrade);
                }
            }
        }

        public void Dispose()
        {
            _installer.Dispose();
        }
    }
}