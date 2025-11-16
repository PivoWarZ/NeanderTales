using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.Scripts.UI;
using NeanderTaleS.Code.Scripts.UI.Upgrades;
using UnityEngine.UI;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.UpgradeSystem.StatsUpgrades.Management
{
    public sealed class StatsUpgradeManager: IInitializable, IDisposable
    {
        public event Action<int> OnSpendCoinsRequest;
        
        private readonly StatsUpgradePopupsInstaller _popupsInstaller;
        private readonly UpgradesPopup _upgradesPopup;
        private List<Upgrade> _upgrades = new ();
        private List<UpgradeStatView> _views = new ();
        private Button _showPopupButton;
        private Button _hidePopupButton;
        private UniTaskCompletionSource<bool> _task = new ();
        private CancellationTokenSource _cancell = new ();

        public StatsUpgradeManager(StatsUpgradePopupsInstaller popupsInstaller, HudUI hud)
        {
            _popupsInstaller = popupsInstaller;
            _upgradesPopup = hud.UpgradesPopup;
        }

        void IInitializable.Initialize()
        {
            _popupsInstaller.OnUpgradeBoxCreated += CreateStatsViewModel;
            
            _showPopupButton = _upgradesPopup.ShowPopupButton;
            _hidePopupButton = _upgradesPopup.CloseButton;

            _showPopupButton.onClick.AddListener(ShowPopup);
            _hidePopupButton.onClick.AddListener(HidePopup);
        }

        public void ShowUpgradeButton()
        {
            _showPopupButton.gameObject.SetActive(true);
        }

        public void HideUpgradeButton()
        {
            _showPopupButton.gameObject.SetActive(false);
        }

        private void HidePopup()
        {
            _upgradesPopup.gameObject.SetActive(false);
        }

        private void ShowPopup()
        {
            _upgradesPopup.gameObject.SetActive(true);
        }

        public void CreateStatsViewModel(UpgradeStatView view, Upgrade model)
        {
            view.gameObject.transform.SetParent(_upgradesPopup.Content);
            
            _upgrades.Add(model);
            _views.Add(view);
            
            var viewModel = new UpgradeStatViewModel();
            viewModel.Init(model);
            view.Construct(viewModel);
            view.OnClickUpgradeButton += OnUpgradeRequest;
            view.OnAllUpgradesComplete += Unsubscribe;
        }

        private void OnUpgradeRequest(string ID)
        {
            var issUpgradeFinded = TryFindUpgrade(ID, out Upgrade model);

            if (!issUpgradeFinded)
            {
                return;
            }
            
            TryUpgradeAsync(model, _cancell).Forget();
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
        
        private async UniTask<bool> TryUpgradeAsync(Upgrade model, CancellationTokenSource token)
        {
            var price = model.NextPrice;
            bool isBuy = await TrySpendCoinsAsync(price, _cancell);

            if (isBuy)
            {
                model.TryLevelUp();
                return true;
            }
                
            return false;
        }

        private async UniTask<bool> TrySpendCoinsAsync(int price, CancellationTokenSource token)
        {
            _task = new UniTaskCompletionSource<bool>();
            OnSpendCoinsRequest?.Invoke(price);
            
            var canSpendCoins = await _task.Task;
            return canSpendCoins;
        }

        public void SetSpendUpgradeCoinsResult(bool result)
        {
            _task.TrySetResult(result);
        }

        private void Unsubscribe(UpgradeStatView view)
        {
            view.OnClickUpgradeButton -= OnUpgradeRequest;
            view.OnAllUpgradesComplete -= Unsubscribe;
        }

        void IDisposable.Dispose()
        {
            foreach (var view in _views)
            {
                view.OnClickUpgradeButton -= OnUpgradeRequest;
                view.OnAllUpgradesComplete -= Unsubscribe;
            }
            
            _cancell.Cancel();
            _cancell.Dispose();
            
            _views.Clear();
            _views = null;
            _upgrades.Clear();
            _upgrades = null;
            
            _showPopupButton.onClick.RemoveListener(ShowPopup);
            _hidePopupButton.onClick.RemoveListener(HidePopup);
        }
    }
}