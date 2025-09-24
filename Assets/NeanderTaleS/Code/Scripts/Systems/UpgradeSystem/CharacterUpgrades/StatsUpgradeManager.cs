using System;
using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.Experience;
using NeanderTaleS.Code.Scripts.UI;
using NeanderTaleS.Code.Scripts.UI.Upgrades;
using R3;
using UnityEngine.UI;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.CharacterUpgrades
{
    public class StatsUpgradeManager: IInitializable, IDisposable
    {
        private readonly StatsUpgradePopupInstaller _popupInstaller;
        private readonly ICoinsStorage _storage;
        private readonly HudUI _hud;
        private List<Upgrade> _upgrades = new ();
        private List<UpgradeStatView> _views = new ();
        private Button _showPopupButton;
        private Button _hidePopupButton;
        IDisposable _disposable;

        public StatsUpgradeManager(StatsUpgradePopupInstaller popupInstaller, ICoinsStorage storage, HudUI hud)
        {
            _popupInstaller = popupInstaller;
            _storage = storage;
            _hud = hud;
        }

        void IInitializable.Initialize()
        {
            _popupInstaller.OnUpgradeBoxCreated += InitUpgradeBox;
            
            _showPopupButton = _hud.UpgradesPopup.ShowPopupButton;
            _hidePopupButton = _hud.UpgradesPopup.CloseButton;

            _showPopupButton.onClick.AddListener(ShowPopup);
            _hidePopupButton.onClick.AddListener(HidePopup);

            _disposable = _storage.Coins
                .Where(coins => coins > 0)
                .Subscribe(ShowUpgradeButton);

        }

        private void ShowUpgradeButton(int _)
        {
            _disposable.Dispose();
            _showPopupButton.gameObject.SetActive(true);
            
            _disposable = _storage.Coins
                .Where(coins => coins <= 0)
                .Subscribe(HideUpgradeButton);
        }

        private void HideUpgradeButton(int _)
        {
            _disposable.Dispose();
            _showPopupButton.gameObject.SetActive(false);
            
            _disposable = _storage.Coins
                .Where(coins => coins > 0)
                .Subscribe(HideUpgradeButton);
        }

        private void HidePopup()
        {
            _hud.UpgradesPopup.gameObject.SetActive(false);
        }

        private void ShowPopup()
        {
            _hud.UpgradesPopup.gameObject.SetActive(true);
        }

        private void InitUpgradeBox(UpgradeStatView view, Upgrade model)
        {
            _upgrades.Add(model);
            _views.Add(view);
            
            var viewModel = new UpgradeStatViewModel();
            viewModel.Init(model);
            view.Construct(viewModel);
            view.OnUpgradeRequest += TryUpgrade;
            view.OnAllUpgradeComplete += Unsubscribe;

        }

        private void TryUpgrade(string ID)
        {
            var isUpgrade = TryFindUpgrade(ID, out Upgrade model);

            if (isUpgrade)
            {
                var isBuyed = TrySpendCoins(model);

                if (isBuyed)
                {
                    model.LevelUp();
                }
            }
        }

        private bool TrySpendCoins(Upgrade upgrade)
        {
            var price = upgrade.NextPrice;
            var isSpendPossible = _storage.Coins.Value >= price;

            if (isSpendPossible)
            {
                _storage.Coins.Value -= price;
                return true;
            }
            
            return false;
        }

        private bool TryFindUpgrade(string ID, out Upgrade model)
        {
            var upgrade = _upgrades.FirstOrDefault(value => value.Id == ID);

            if (upgrade != null)
            {
                model = upgrade;
                return true;
            }
            
            model = null;
            return false;
        }

        private void Unsubscribe(UpgradeStatView view)
        {
            view.OnUpgradeRequest -= TryUpgrade;
            view.OnAllUpgradeComplete -= Unsubscribe;
        }

        void IDisposable.Dispose()
        {
            foreach (var view in _views)
            {
                view.OnUpgradeRequest -= TryUpgrade;
                view.OnAllUpgradeComplete -= Unsubscribe;
            }
            
            _views.Clear();
            _views = null;
            _upgrades.Clear();
            _upgrades = null;
            
            _showPopupButton.onClick.RemoveListener(ShowPopup);
            _hidePopupButton.onClick.RemoveListener(HidePopup);
            _disposable.Dispose();
        }
    }
}