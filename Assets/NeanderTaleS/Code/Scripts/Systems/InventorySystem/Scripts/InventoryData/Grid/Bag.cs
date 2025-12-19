using System.Collections.Generic;
using System.Linq;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid
{
    public static class Bag
    {
        public static List<GridItem> Grids = new ();

        public static GridItem ActiveGrid => Grids.FirstOrDefault(grid => grid.IsActive());

        public static void ActivateGrid(GridItem gridItem)
        {
            var grid = FindGrid(gridItem);
            grid.Activate();
        }

        public static void DeactivateGrid(GridItem gridItem)
        {
            var grid = FindGrid(gridItem);
            grid.Deactivate();
        }

        public static void SwitchActiveGrid(GridItem gridItem)
        {
            var grid = FindGrid(gridItem);
            var activeGrid = ActiveGrid;
            
            if (activeGrid)
                activeGrid.Deactivate();
            
            grid.Activate();
            
            SetAsLastSibling(gridItem);
        }

        public static void SetAsLastSibling(GridItem gridItem)
        {
            gridItem.gameObject.transform.SetAsLastSibling();
        }

        private static GridItem FindGrid(GridItem gridItem)
        {
            return Grids.FirstOrDefault(grid => grid == gridItem);
        }
    }
}