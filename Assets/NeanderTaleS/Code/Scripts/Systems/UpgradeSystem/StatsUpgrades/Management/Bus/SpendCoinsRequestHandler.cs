using System;
using NeanderTaleS.Code.Scripts.Systems.Storages.UpgradeCoins;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Management.Bus
{
    public class SpendCoinsRequestHandler: IInitializable, IDisposable
    {
        private readonly IUpgradeCoinsStorage _storage;
        private readonly StatsUpgradeManager _manager;

        public SpendCoinsRequestHandler(IUpgradeCoinsStorage storage, StatsUpgradeManager manager)
        {
            _storage = storage;
            _manager = manager;
        }

        void IInitializable.Initialize()
        {
            _manager.OnSpendCoinsRequest += SetSpendCoinsResult;
        }
        
        void IDisposable.Dispose()
        {
            _manager.OnSpendCoinsRequest -= SetSpendCoinsResult;
        }

        private void SetSpendCoinsResult(int price)
        {
            bool canSpend = _storage.HasCoins(price);

            if (canSpend)
            {
                _storage.RemoveCoins(price);
            }
            
            _manager.SetSpendUpgradeCoinsResult(canSpend);
        }
    }
}