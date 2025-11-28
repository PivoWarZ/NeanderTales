using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.Interfaces;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.EquipPopup;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Grid;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Installers;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.InventoryBase;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Servises;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Observers.GridObservers
{
    public sealed class GridObserver_DoubleClick: IInitializable, IDisposable, IInventoryInitializable
    {
        private readonly InventoryGridInstaller _gridInstaller;
        private readonly EquipItemAdapter _adapter;
        private readonly List<GridItem> _gridItems = new ();
        private readonly ActiveGridService _activeGridService;
        private InventoryBase.Inventory _inventory;

        public GridObserver_DoubleClick(InventoryGridInstaller gridInstaller, ActiveGridService activeGridService, EquipItemAdapter adapter)
        {
            _gridInstaller = gridInstaller;
            _activeGridService = activeGridService;
            _adapter = adapter;
        }

        void IInventoryInitializable.Initialize(IInventoryComponent inventoryComponent)
        {
            _inventory = inventoryComponent.Inventory;
        }
        
        void IInitializable.Initialize()
        {
            _gridInstaller.OnGridItemAdded += Subscribe;

        }

        private void Subscribe(GridItem grid)
        {
            grid.OnDoubleClick += DoubleClick;
            grid.OnGridDestroyed += Unsubscribes;
            _gridItems.Add(grid);
        }
        private void Unsubscribes(GridItem grid)
        {
            grid.OnDoubleClick -= DoubleClick;
            grid.OnGridDestroyed -= Unsubscribes;
            _gridItems.Remove(grid);
        }

        private void DoubleClick(GridItem grid)
        {
            var item = grid.InventoryItem;
            
            if (item.Flags.HasFlag(InventoryItemFlags.Consumable))
            {
                _inventory.ConsumeItem(item);
            }

            if (item.Flags.HasFlag(InventoryItemFlags.Equipable))
            {
                _adapter.EquipItem(item);
                _activeGridService.GetActiveGrid().DeactivateGrid();
            }
        }

        public void Dispose()
        {
            foreach (var gridItem in _gridItems)
            {
                gridItem.OnDoubleClick -= DoubleClick;
                gridItem.OnGridDestroyed -= Unsubscribes;
            }
        }
    }
}