using System;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryItemInfo;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Observers.GridObservers
{
    public sealed class GridObserver_RightClick: IDisposable
    {
        private ItemInfoPopupAdapter _infoAdapter;

        public GridObserver_RightClick(ItemInfoPopupAdapter infoAdapter)
        {
            _infoAdapter = infoAdapter;
            Subscribe();
        }

        private void Subscribe()
        {
            foreach (var gridItem in Bag.Grids)
            {
                gridItem.OnGridRightClicked += SwitchActiveGridAndInitializeInfoPopup;
            }
        }

        private void Unsubscribe()
        {
            foreach (var gridItem in Bag.Grids)
            {
                gridItem.OnGridRightClicked -= SwitchActiveGridAndInitializeInfoPopup;
            }
        }

        private void SwitchActiveGridAndInitializeInfoPopup(GridItem item)
        {
            Bag.SwitchActiveGrid(item);
            InitializeItemInfoPupup(item);
        }

        private void InitializeItemInfoPupup(GridItem item)
        {
            _infoAdapter.ShowInfoPopup();
            _infoAdapter.RefreshItemInfoPopup(item);
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}