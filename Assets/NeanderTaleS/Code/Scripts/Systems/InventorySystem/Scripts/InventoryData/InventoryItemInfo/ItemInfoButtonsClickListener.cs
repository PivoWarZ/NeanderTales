using System;
using DG.Tweening;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryItemInfo
{
    public class ItemInfoButtonsClickListener: IDisposable
    {
        private InventoryItemInfoView _view;
        private Inventory _inventory;

        public ItemInfoButtonsClickListener(InventoryItemInfoView view, Inventory inventory)
        {
            _view = view;
            _inventory = inventory;
            Subscribes();
        }

        private void Subscribes()
        {
            _view.CloseButton.onClick.AddListener(HideInfoPopup);
            _view.EquipButton.onClick.AddListener(EquipButtonClick);
            _view.TrowAwayButton.onClick.AddListener(TrowAwayButtonClick);
        }

        private void Unsubscribes()
        {
            _view.CloseButton.onClick.RemoveListener(HideInfoPopup);
            _view.EquipButton.onClick.RemoveListener(EquipButtonClick);
            _view.TrowAwayButton.onClick.RemoveListener(TrowAwayButtonClick);
        }

        private void EquipButtonClick()
        {
            var activeGrid = Bag.ActiveGrid;
            Equipment.Add(activeGrid.InventoryItem);
        }

        private void TrowAwayButtonClick()
        {
            DeleteItemFromInventory();
            HideInfoPopup();
        }

        private void DeleteItemFromInventory()
        {
           _inventory.Reset(_view.Item);
        }

        private void HideInfoPopup()
        {
            FadeOut(_view.gameObject);
        }
        
        public void FadeOut(GameObject view)
        {
            var canvasGroup = view.GetComponent<CanvasGroup>();
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, 0.3f);
        }

        public void Dispose()
        {
            Unsubscribes();
        }
    }
}