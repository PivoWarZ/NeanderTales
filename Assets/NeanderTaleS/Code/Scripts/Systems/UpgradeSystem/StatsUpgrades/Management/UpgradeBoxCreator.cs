using System;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.EntryPoint;
using NeanderTaleS.Code.Scripts.UI.Upgrades;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Management
{
    public class UpgradeBoxCreator: IDisposable
    {
        private StatsUpgradesSystemInitializer _initializer;
        private StatsUpgradeManager _manager;
        private UpgradeStatView _prefab;

        public UpgradeBoxCreator(StatsUpgradesSystemInitializer initializer, StatsUpgradeManager manager)
        {
            _initializer = initializer;
            _manager = manager;
            
            _prefab = Resources.Load<UpgradeStatView>("UpgradeBox");
            _initializer.OnUpgradeConstructed += CreateViewModel;
        }
        
        void IDisposable.Dispose()
        {
            _initializer.OnUpgradeConstructed -= CreateViewModel;
        }

        private void CreateViewModel(Upgrade model)
        {
            var view = Object.Instantiate(_prefab);
            _manager.CreateStatsViewModel(view, model);
        }
    }
}