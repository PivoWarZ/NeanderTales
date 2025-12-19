using System;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Observers.GridObservers
{
    public sealed class GridClickObserver_LeftClick: IDisposable
    {
        public GridClickObserver_LeftClick()
        {
            SubscribeGridLeftClicked();
        }
        
        private void SubscribeGridLeftClicked()
        {
            foreach (var gridItem in Bag.Grids)
            {
                gridItem.OnGridLeftClicked += SwitchActiveGrid;
            }
        }
        
        public void Dispose()
        {
            Unsubscribe();
        }
        
        private void Unsubscribe()
        {
            foreach (var gridItem in Bag.Grids)
            {
                gridItem.OnGridLeftClicked -= SwitchActiveGrid;
            }
        }
        
        private void SwitchActiveGrid(GridItem grid)
        {   
            Bag.SwitchActiveGrid(grid);
        }
    }
}