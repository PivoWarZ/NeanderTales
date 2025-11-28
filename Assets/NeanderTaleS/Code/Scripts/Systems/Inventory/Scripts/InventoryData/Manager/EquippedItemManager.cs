using System;
using System.Collections.Generic;
using Inventory.Scripts.Interfaces;
using Inventory.Scripts.InventoryData.EquipPopup;
using Inventory.Scripts.InventoryData.Grid;
using Inventory.Scripts.InventoryData.InventoryBase;
using Inventory.Scripts.InventoryData.InventoryItemInfo;
using Inventory.Scripts.InventoryData.Observers.EquipItemClickObservers;
using Inventory.Scripts.InventoryData.Servises;
using Zenject;

namespace Inventory.Scripts.InventoryData.Manager
{
    public sealed class EquippedItemManager: IInitializable, IDisposable, IInventoryInitializable
    {
        private EquipPopupView _view;
        private ActiveGridService _activeGridService;
        private ItemInfoPopupAdapter _infoPopupAdapter;
        private EquipItemEffectObserver _equipItemEffectObserver;
        private InventoryBase.Inventory _inventory;
        private EquipClickObserver _clickObserver;
        private Dictionary<InventoryItem, GridItem> _equippedItems = new ();

        public EquippedItemManager(ItemInfoPopupAdapter infoPopupAdapter, EquipPopupView view, ActiveGridService activeGridService)
        {
            _infoPopupAdapter = infoPopupAdapter;
            _view = view;
            _activeGridService = activeGridService;
        }

        void IInventoryInitializable.Initialize(IInventoryComponent inventoryComponent)
        {
            _inventory = inventoryComponent.Inventory;
            var statsSetter = inventoryComponent.CharacterStatsSetter;
            _equipItemEffectObserver = new EquipItemEffectObserver(statsSetter);
        }

        void IInitializable.Initialize()
        {
            _clickObserver = new EquipClickObserver();
            _clickObserver.Init(_infoPopupAdapter, _activeGridService);
            _clickObserver.OnUnequipItem += UnequipItem;
            
            _view.OnItemEquipped += EquipItem;
        }
        
        private void EquipItem(GridItem grid)
        {
            var item = grid.InventoryItem;
            
            _inventory.RemoveItem(item);
            _clickObserver.AddEquipItem(grid);
            _equipItemEffectObserver.OnItemAdded(item);
            _infoPopupAdapter.InitView(item, true);
            _equippedItems.Add(item, grid);
        }

        private void UnequipItem(GridItem grid)
        {
            var item = grid.InventoryItem;
            
            grid.gameObject.SetActive(false);
            _equippedItems.Remove(item);
            _equipItemEffectObserver.OnItemRemoved(item);
            _infoPopupAdapter.HideInfoPopup();
            _inventory.AddItem(item.Clone());
        }

        public void TrowAwayItem(InventoryItem item)
        {
            _equippedItems[item].gameObject.SetActive(false);
            _equippedItems.Remove(item);
            _equipItemEffectObserver.OnItemRemoved(item);
            _infoPopupAdapter.HideInfoPopup();
        }

        public void Unequip(InventoryItem inventoryItem)
        {
            UnequipItem(_equippedItems[inventoryItem]);
        }

        public void Dispose()
        {
            _clickObserver.Dispose();
            _view.OnItemEquipped -= EquipItem;
        }
    }
}