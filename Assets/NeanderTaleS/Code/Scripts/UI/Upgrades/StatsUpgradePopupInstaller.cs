using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.UI.Upgrades
{
    public class StatsUpgradePopupInstaller: IInitializable, IDisposable
    {
        public event Action <UpgradeStatView, Upgrade> OnUpgradeBoxCreated;
        private readonly StatsUpgradesInstaller _upgradesInstaller;
        private readonly List<Upgrade> _upgrades = new ();
        private readonly HudUI _hud;
        private readonly GameObject _box;

        public StatsUpgradePopupInstaller(StatsUpgradesInstaller upgradesInstaller, HudUI hud)
        {
            _upgradesInstaller = upgradesInstaller;
            _hud = hud;
            _upgrades = _upgradesInstaller.Upgrades;
            _box = Resources.Load<GameObject>("UpgradeBox");
        }

        public void Initialize()
        {
            var parent = _hud.UpgradesPopup.Content.transform;
            
            foreach (var upgrade in _upgrades)
            {
                var box = GameObject.Instantiate(_box.gameObject, parent);
                UpgradeStatView upgradeStatView = box.GetComponent<UpgradeStatView>();
                
                OnUpgradeBoxCreated?.Invoke(upgradeStatView, upgrade);
            }
        }

        public void Dispose()
        {
            _upgrades.Clear();
        }
    }
}