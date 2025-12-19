using System;
using System.Collections.Generic;
using System.Linq;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid
{
    public static class Bag
    {
        public static Action<GridItem> OnGridActivated;
        
        public static List<GridItem> Grids = new ();

        public static GridItem ActiveGrid => Grids.FirstOrDefault(grid => grid.IsActive());
        
        public static void SwitchActiveGrid(GridItem gridItem)
        {
            DeactivateActiveGrid();
            
            ActivateGrid(gridItem);
            
            SetAsLastSibling(ActiveGrid);
        }
        
        private static void DeactivateActiveGrid()
        {
            var activeGrid = ActiveGrid;
            
            if(activeGrid)
                activeGrid.Deactivate();
        }
        
        private static void ActivateGrid(GridItem gridItem)
        {
            var grid = FindGrid(gridItem);
            grid.Activate();
            OnGridActivated?.Invoke(gridItem);
        }

        private static void SetAsLastSibling(GridItem gridItem)
        {
            gridItem.gameObject.transform.SetAsLastSibling();
        }
        
        private static GridItem FindGrid(GridItem gridItem)
        {
            return Grids.FirstOrDefault(grid => grid == gridItem);
        }

        public static void DeactivateGrid(GridItem gridItem)
        {
            var grid = FindGrid(gridItem);
            grid.Deactivate();
        }
    }
}