
using System;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryItemInfo;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Observers.GridObservers
{
    public sealed class GridObserver_RightClick: IDisposable
    {
        private InventoryItemInfoView _infoView;
        private ItemInfoPopupAdapter _infoAdapter;

        public GridObserver_RightClick(InventoryItemInfoView infoView)
        {
            _infoView = infoView;
            _infoAdapter = new ItemInfoPopupAdapter(_infoView);
            Subscribe();
        }

        private void Subscribe()
        {
            foreach (var gridItem in Bag.Grids)
            {
                gridItem.OnGridRightClicked += InitializeItemInfoPupup;
            }
        }

        private void Unsubscribe()
        {
            foreach (var gridItem in Bag.Grids)
            {
                gridItem.OnGridRightClicked -= InitializeItemInfoPupup;
            }
        }

        private void InitializeItemInfoPupup(GridItem item)
        {
            _infoAdapter.ShowInfoPopup();
            _infoAdapter.InitView(item.InventoryItem);
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}