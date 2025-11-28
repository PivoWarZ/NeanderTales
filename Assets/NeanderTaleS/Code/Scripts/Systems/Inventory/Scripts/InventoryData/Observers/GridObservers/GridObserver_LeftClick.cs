using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Grid;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Installers;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.InventoryItemInfo;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Servises;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Observers.GridObservers
{
    public sealed class GridObserver_LeftClick: IInitializable, IDisposable
    {
        private readonly InventoryGridInstaller _gridInstaller;
        private readonly ActiveGridService _activeGridService;
        private readonly ItemInfoPopupAdapter _infoPopupAdapter;
        private readonly List<GridItem> _gridItems = new ();

        public GridObserver_LeftClick(InventoryGridInstaller gridInstaller, ItemInfoPopupAdapter infoPopupAdapter, ActiveGridService activeGridService)
        {
            _gridInstaller = gridInstaller;
            _infoPopupAdapter = infoPopupAdapter;
            _activeGridService = activeGridService;
        }

        public void Initialize()
        {
            _gridInstaller.OnGridItemAdded += Subscribe;
        }

        private void Subscribe(GridItem grid)
        {
            grid.OnGridLeftClicked += LeftClicked;
            grid.OnGridDestroyed += Unsubscribes;
            _gridItems.Add(grid);
        }

        private void Unsubscribes(GridItem grid)
        {
            grid.OnGridLeftClicked -= LeftClicked;
            grid.OnGridDestroyed -= Unsubscribes;
            _gridItems.Remove(grid);
        }

        private void LeftClicked(GridItem grid)
        {
            var isActiveGrid = _activeGridService.GetActiveGrid() == grid;

            if (!isActiveGrid)
            {
                _infoPopupAdapter.HideInfoPopup();
            }

            _activeGridService.ActivateGrid(grid);
        }

        public void Dispose()
        {
            for (var index = 0; index < _gridItems.Count; index++)
            {
                var gridItem = _gridItems[index];
                gridItem.OnGridLeftClicked -= LeftClicked;
                gridItem.OnGridDestroyed -= Unsubscribes;
            }
        }
    }
}