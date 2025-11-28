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
                _activeGrid.ActivateGrid();
                return;
            }
            
            _activeGrid.DeactivateGrid();
            _activeGrid = gridItem;
            _activeGrid.ActivateGrid();
        }

        public GridItem GetActiveGrid()
        {
            return _activeGrid;
        }
    }
}