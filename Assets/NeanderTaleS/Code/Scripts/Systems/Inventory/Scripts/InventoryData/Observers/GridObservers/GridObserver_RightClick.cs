using System;
using System.Collections.Generic;
using Inventory.Scripts.InventoryData.Grid;
using Inventory.Scripts.InventoryData.Installers;
using Inventory.Scripts.InventoryData.InventoryItemInfo;
using Inventory.Scripts.InventoryData.Servises;
using Zenject;

namespace Inventory.Scripts.InventoryData.Observers.GridObservers
{
    public sealed class GridObserver_RightClick: IInitializable, IDisposable
    {
        private readonly InventoryGridInstaller _gridInstaller;
        private readonly ActiveGridService _activeGridService;
        private readonly ItemInfoPopupAdapter _infoPopupAdapter;
        private readonly List<GridItem> _gridItems = new ();

        public GridObserver_RightClick(InventoryGridInstaller gridInstaller, ItemInfoPopupAdapter infoPopupAdapter, ActiveGridService activeGridService)
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
            grid.OnGridRightClicked += RightClick;
            grid.OnGridDestroyed += Unsubscribes;
            _gridItems.Add(grid);
        }

        private void Unsubscribes(GridItem grid)
        {
            grid.OnGridRightClicked -= RightClick;
            grid.OnGridDestroyed -= Unsubscribes;
            _gridItems.Remove(grid);
        }

        private void RightClick(GridItem grid)
        {
            var item = grid.InventoryItem;
            _activeGridService.ActivateGrid(grid);
            _infoPopupAdapter.InitView(item);
            _infoPopupAdapter.ShowInfoPopup();
        }

        public void Dispose()
        {
            for (var index = 0; index < _gridItems.Count; index++)
            {
                var gridItem = _gridItems[index];
                Unsubscribes(gridItem);
            }
        }
    }
}