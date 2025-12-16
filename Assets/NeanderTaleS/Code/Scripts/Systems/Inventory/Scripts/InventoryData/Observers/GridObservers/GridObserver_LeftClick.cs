using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Grid;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Observers.GridObservers
{
    public sealed class GridObserver_LeftClick: IDisposable
    {
        private GridsStorage _storage;
        private readonly List<GridItem> _grids;

        public GridObserver_LeftClick(GridsStorage storage)
        {
            _grids = storage.Items;
            _storage = storage;

            _storage.OnGridAdded += AddItem;
            Subscribe();
        }
        
        private void Subscribe()
        {
            foreach (var gridItem in _grids)
            {
                gridItem.OnGridLeftClicked += ActivateGrid;
            }
        }
        
        public void Dispose()
        {
            _storage.OnGridAdded -= AddItem;
            Unsubscribe();
        }
        
        private void Unsubscribe()
        {
            foreach (var gridItem in _grids)
            {
                gridItem.OnGridLeftClicked -= ActivateGrid;
            }
        }

        private void AddItem(GridItem gridItem)
        {
           _grids.Add(gridItem);
        }

        private void DeactivateGrids()
        {
            foreach (var gridItem in _grids)
            {
                gridItem.Deactivate();
            }
        }

        private void ActivateGrid(GridItem grid)
        {
            DeactivateGrids();
            grid.Activate();
        }
    }
}