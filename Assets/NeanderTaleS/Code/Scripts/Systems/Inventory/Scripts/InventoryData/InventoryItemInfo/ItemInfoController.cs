using Inventory.Scripts.DI;
using Inventory.Scripts.Interfaces;
using Inventory.Scripts.InventoryData.EquipPopup;
using Inventory.Scripts.InventoryData.InventoryBase;
using Inventory.Scripts.InventoryData.Manager;
using Zenject;

namespace Inventory.Scripts.InventoryData.InventoryItemInfo
{
    public sealed class ItemInfoController: IInitializable, IInventoryInitializable
    {
        private readonly ItemInfoPopupAdapter _infoPopupAdapter;
        private readonly EquipItemAdapter _equipAdapter;
        private readonly EquippedItemManager _equippedItemManager;
        private InventoryBase.Inventory _inventory;

        public ItemInfoController(ItemInfoPopupAdapter infoPopupAdapter, EquipItemAdapter equipAdapter, EquippedItemManager equippedItemManager)
        {
            _infoPopupAdapter = infoPopupAdapter;
            _equipAdapter = equipAdapter;
            _equippedItemManager = equippedItemManager;
        }

        void IInventoryInitializable.Initialize(IInventoryComponent inventoryComponent)
        {
            _inventory = inventoryComponent.Inventory;
        }

        void IInitializable.Initialize()
        {
            _infoPopupAdapter.OnEquipButtonClick += EquipButtonClick;
            _infoPopupAdapter.OnTrowAwayButtonClick += TrowAwayButtonClick;
        }

        private void TrowAwayButtonClick(InventoryItem item, bool isEquipped)
        {
            if (isEquipped)
            {
                _equippedItemManager.TrowAwayItem(item);
            }
            else
            {
                _inventory.RemoveItem(item);
            }
            
            _infoPopupAdapter.HideInfoPopup();
        }

        private void EquipButtonClick(InventoryItem item, bool isEquipped)
        {
            if (isEquipped)
            {
                _equippedItemManager.Unequip(item);
            }
            else
            {
                _equipAdapter.EquipItem(item);
            }

            if (item.Flags.HasFlag(InventoryItemFlags.Consumable))
            {
                _inventory.ConsumeItem(item);
            }
        }
    }
}