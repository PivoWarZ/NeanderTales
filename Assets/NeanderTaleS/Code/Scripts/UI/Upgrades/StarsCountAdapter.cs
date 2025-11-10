using System;
using NeanderTaleS.Code.Scripts.Systems.ExperienceSystem;
using R3;
using Zenject;

namespace NeanderTaleS.Code.Scripts.UI.Upgrades
{
    public class StarsCountAdapter: IInitializable, IDisposable
    {
        private readonly ICoinsStorage _storage;
        private readonly UpgradesPopup _popup;
        IDisposable _disposable;

        public StarsCountAdapter(HudUI hud, ICoinsStorage storage)
        {
            _storage = storage;
            _popup = hud.UpgradesPopup;
        }

        void IInitializable.Initialize()
        {
            _disposable = _storage.Coins.Subscribe(SetCoinsValueText);
        }

        private void SetCoinsValueText(int value)
        {
            _popup.StarsCount.text = $" X {value}";
        }

        void IDisposable.Dispose()
        {
            _disposable.Dispose();
        }
    }
}