using System;
using NeanderTaleS.Code.Scripts.Systems.Storages.UpgradeCoins;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades;
using R3;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.Bus
{
    public class UpdateCoinsListener_UpgradeButtonVisibility: IInitializable, IDisposable
    {
        private readonly IUpgradeCoinsStorage _storage;
        private readonly StatsUpgradeManager _manager;
        private IDisposable _disposable;

        public UpdateCoinsListener_UpgradeButtonVisibility(IUpgradeCoinsStorage storage, StatsUpgradeManager manager)
        {
            _storage = storage;
            _manager = manager;
        }

        void IInitializable.Initialize()
        {
            _disposable = _storage.Coins.Subscribe(SetUpgradeButtonVisibility);
        }

        void IDisposable.Dispose()
        { 
            _disposable.Dispose();
        }

        private void SetUpgradeButtonVisibility(int coins)
        {
            if (coins > 0)
            {
                _manager.ShowUpgradeButton();
            }
            else
            {
                _manager.HideUpgradeButton();
            }
        }
    }
}