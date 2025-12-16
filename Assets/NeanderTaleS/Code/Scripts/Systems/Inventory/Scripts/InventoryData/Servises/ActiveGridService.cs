using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Grid;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Servises
{
    public sealed class ActiveGridService
    {
        private GridItem _activeGrid;

        public void ActivateGrid(GridItem gridItem)
        {
            if (_activeGrid == null)
            {
                _activeGrid = gridItem;
                _activeGrid.Activate();
                return;
            }
            
            _activeGrid.Deactivate();
            _activeGrid = gridItem;
            _activeGrid.Activate();
        }

        public GridItem GetActiveGrid()
        {
            return _activeGrid;
        }
    }
}