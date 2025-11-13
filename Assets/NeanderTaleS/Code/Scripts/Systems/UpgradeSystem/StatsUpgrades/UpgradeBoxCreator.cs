using System;
using NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext;
using NeanderTaleS.Code.Scripts.UI.Upgrades;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades
{
    public class UpgradeBoxCreator: IDisposable
    {
        private StatsUpgradesInstaller _installer;
        private StatsUpgradeManager _manager;
        private UpgradeStatView _prefab;

        public UpgradeBoxCreator(StatsUpgradesInstaller installer, StatsUpgradeManager manager)
        {
            _installer = installer;
            _manager = manager;
            
            _prefab = Resources.Load<UpgradeStatView>("UpgradeBox");
            _installer.OnUpgradeConstructed += CreateViewModel;
        }
        
        void IDisposable.Dispose()
        {
            _installer.OnUpgradeConstructed -= CreateViewModel;
        }

        private void CreateViewModel(Upgrade model)
        {
            var view = Object.Instantiate(_prefab);
            _manager.CreateStatsViewModel(view, model);
            Debug.Log("Create View Prefab");
        }
    }
}