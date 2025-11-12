using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.UI.Upgrades
{
    public class StatsUpgradePopupsInstaller: IInitializable, IDisposable
    {
        public event Action <UpgradeStatView, Upgrade> OnUpgradeBoxCreated;
        private readonly List<Upgrade> _upgrades = new ();
        private readonly HudUI _hud;
        private readonly GameObject _box;

        public StatsUpgradePopupsInstaller(HudUI hud)
        {
            _hud = hud;
            _box = Resources.Load<GameObject>("UpgradeBox");
        }

        void IInitializable.Initialize()
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