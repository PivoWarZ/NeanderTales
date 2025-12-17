using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Observers.GridObservers
{
    public sealed class GridClickObserver_LeftClick: IDisposable
    {
        private GridsStorage _storage;
        private readonly List<GridItem> _grids;

        public GridClickObserver_LeftClick(GridsStorage storage)
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
                gridItem.OnGridLeftClicked += ActivateAndSetAsLastSibling;
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
                gridItem.OnGridLeftClicked -= ActivateAndSetAsLastSibling;
            }
        }
        
        private void ActivateAndSetAsLastSibling(GridItem grid)
        {
            ActivateGrid(grid);
            SetAsLastSibling(grid.gameObject);
        }
        
        private void ActivateGrid(GridItem grid)
        {
            DeactivateGrids();
            grid.Activate();
        }
        
        private void DeactivateGrids()
        {
            foreach (var gridItem in _grids)
            {
                gridItem.Deactivate();
            }
        }
        
        private void SetAsLastSibling(GameObject gameObject)
        {
            gameObject.transform.SetAsLastSibling();
        }

        private void AddItem(GridItem gridItem)
        {
           _grids.Add(gridItem);
        }
    }
}