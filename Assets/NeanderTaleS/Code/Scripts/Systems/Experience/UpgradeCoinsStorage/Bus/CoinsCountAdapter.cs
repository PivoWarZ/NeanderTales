using System;
using NeanderTaleS.Code.Scripts.UI;
using NeanderTaleS.Code.Scripts.UI.Upgrades;
using R3;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.Storages.UpgradeCoins.Bus
{
    public sealed class CoinsCountAdapter: IInitializable, IDisposable
    {
        private readonly IUpgradeCoinsStorage _storage;
        private readonly UpgradesPopup _popup;
        IDisposable _disposable;

        public CoinsCountAdapter(HudUI hud, IUpgradeCoinsStorage storage)
        {
            _storage = storage;
            _popup = hud.UpgradesPopup;
        }

        void IInitializable.Initialize()
        {
            _disposable = _storage.Coins.Subscribe(SetCoinsValueText);
        }
        
        void IDisposable.Dispose()
        {
            _disposable.Dispose();
        }

        private void SetCoinsValueText(int value)
        {
            _popup.StarsCount.text = $" X {value}";
        }
    }
}