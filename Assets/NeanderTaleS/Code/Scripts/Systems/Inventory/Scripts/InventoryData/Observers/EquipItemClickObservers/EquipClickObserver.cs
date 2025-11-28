using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Grid;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.InventoryItemInfo;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Servises;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Observers.EquipItemClickObservers
{
    public sealed class EquipClickObserver
    {
        public event Action<GridItem> OnUnequipItem;
        private ActiveGridService _activeGridService;
        private ItemInfoPopupAdapter _infoPopupAdapter;
        private List<GridItem> _items = new ();

        public void Init(ItemInfoPopupAdapter popupAdapter, ActiveGridService activeGridService)
        {
            _infoPopupAdapter = popupAdapter;
            _activeGridService = activeGridService;
        }

        public void AddEquipItem(GridItem grid)
        {
            grid.OnGridLeftClicked += ActivateGrid;
            grid.OnGridRightClicked += ShowInfoPopup;
            grid.OnDoubleClick += UnequipItem;
        }

        private void ActivateGrid(GridItem grid)
        {
            _activeGridService.ActivateGrid(grid);
        }

        private void UnequipItem(GridItem grid)
        {
            OnUnequipItem?.Invoke(grid);
            RemoveEquipItem(grid);
        }

        public void RemoveEquipItem(GridItem grid)
        {
            grid.OnGridLeftClicked -= ActivateGrid;
            grid.OnGridRightClicked -= ShowInfoPopup;
            grid.OnDoubleClick -= UnequipItem;
            _items.Remove(grid);
        }

        private void ShowInfoPopup(GridItem grid)
        {
            var item = grid.InventoryItem;
            
            _infoPopupAdapter.InitView(item, true);
            _infoPopupAdapter.ShowInfoPopup();
            
            ActivateGrid(grid);
        }

        public void Dispose()
        {
            foreach (var grid in _items)
            {
                grid.OnGridLeftClicked -= ActivateGrid;
                grid.OnGridRightClicked -= ShowInfoPopup;
            }
        }
    }
}