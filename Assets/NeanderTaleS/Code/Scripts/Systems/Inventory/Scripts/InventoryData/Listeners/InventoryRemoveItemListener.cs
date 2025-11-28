using System;
using System.Collections.Generic;
using Inventory.Scripts.Interfaces;
using Inventory.Scripts.InventoryData.EquipPopup;
using Inventory.Scripts.InventoryData.Grid;
using Inventory.Scripts.InventoryData.Installers;
using Inventory.Scripts.InventoryData.InventoryBase;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Inventory.Scripts.InventoryData.Listeners
{
    public sealed class InventoryRemoveItemListener: IDisposable, IInitializable, IInventoryInitializable
    {
        private InventoryBase.Inventory _inventory;
        private InventoryGridInstaller _gridInstaller;
        private Dictionary<InventoryItem, GameObject> _items = new ();
        
        public InventoryRemoveItemListener(InventoryGridInstaller gridInstaller)
        {
            _gridInstaller = gridInstaller;
        }
        
        public void Initialize(IInventoryComponent inventoryComponent)
        {
            _inventory = inventoryComponent.Inventory;
            _inventory.OnItemRemoved += OnItemRemoved;
        }

        void IInitializable.Initialize()
        {
            _gridInstaller.OnGridItemAdded += AddItem;
        }
        
        void IDisposable.Dispose()
        {
            _gridInstaller.OnGridItemAdded -= AddItem;
            _inventory.OnItemRemoved -= OnItemRemoved;
        }

        private void AddItem(GridItem gridItem)
        {
            _items[gridItem.InventoryItem] = gridItem.gameObject;
        }
        
        private void OnItemRemoved(InventoryItem item)
        {
            Object.Destroy(_items[item]);
        }
    }
}