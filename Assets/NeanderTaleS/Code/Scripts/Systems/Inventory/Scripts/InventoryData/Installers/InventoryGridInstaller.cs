using System;
using System.Collections.Generic;
using Inventory.Scripts.Interfaces;
using Inventory.Scripts.InventoryData.components;
using Inventory.Scripts.InventoryData.EquipPopup;
using Inventory.Scripts.InventoryData.Grid;
using Inventory.Scripts.InventoryData.InventoryBase;
using Inventory.Scripts.InventoryData.Servises;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Inventory.Scripts.InventoryData.Installers
{
    public sealed class InventoryGridInstaller: IInitializable, IDisposable, IInventoryInitializable
    {
        public event Action<GridItem> OnGridItemAdded;
        private InventoryBase.Inventory _inventory;
        private InventoryPopupProvider _popupProvider;
        private GameObject _gridItemPrefab;
        private Transform _gridItemParent;
        private List<GridItem> _gridItems = new ();
        
        public InventoryGridInstaller(GameObject gridItemPrefab, InventoryPopupProvider popupProvider)
        {
            _gridItemPrefab = gridItemPrefab;
            _popupProvider = popupProvider;
        }

        void IInventoryInitializable.Initialize(IInventoryComponent inventoryComponent)
        {
            _inventory = inventoryComponent.Inventory;
            _inventory.OnItemAdded += OnItemAdded;
        }

        void IInitializable.Initialize()
        {
            _gridItemParent = _popupProvider.Content.transform;
        }

        public void OnItemAdded(InventoryItem item)
        {
            GridItem gridItem;
            gridItem = CreateGridItem(item);
            _gridItems.Add(gridItem);

            if (item.TryGetComponent<StackableComponent>(out StackableComponent stackableComponent))
            {
                gridItem.InitCountText(stackableComponent.Count);
            }
            
            OnGridItemAdded?.Invoke(gridItem);
        }

        private GridItem CreateGridItem(InventoryItem item)
        {
            var grid = Object.Instantiate(_gridItemPrefab, _gridItemParent);
            var gridItem = grid.GetComponent<GridItem>();
            gridItem.Initialize(item);
            
            return gridItem;
        }

        public void Dispose()
        {
            _inventory.OnItemAdded -= OnItemAdded;
        }
    }
}