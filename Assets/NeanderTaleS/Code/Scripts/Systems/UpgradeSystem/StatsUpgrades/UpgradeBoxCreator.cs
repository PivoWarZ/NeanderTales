using NeanderTaleS.Code.Scripts.DI_Zenject.ProjectContext;
using NeanderTaleS.Code.Scripts.UI.Upgrades;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades
{
    public class UpgradeBoxCreator: IInitializable
    {
        private StatsUpgradesInstaller _installer;
        private StatsUpgradeManager _manager;
        private UpgradeStatView _prefab;

        public UpgradeBoxCreator(StatsUpgradesInstaller installer, StatsUpgradeManager manager)
        {
            _installer = installer;
            _manager = manager;
        }

        public void Initialize()
        {
            _installer.OnUpgradeConstructed += CreateViewModel;
            _prefab = Resources.Load<UpgradeStatView>("UpgradeBox");
            Debug.Log(_prefab);
        }

        private void CreateViewModel(Upgrade model)
        {
            var view = Object.Instantiate(_prefab);
            _manager.CreateStatsViewModel(view, model);
        }
    }
}