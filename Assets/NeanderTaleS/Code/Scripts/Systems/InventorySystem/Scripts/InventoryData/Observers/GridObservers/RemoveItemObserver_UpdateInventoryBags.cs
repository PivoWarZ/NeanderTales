using System;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.popup;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Observers.GridObservers
{
    public class RemoveItemObserver_UpdateInventoryBags: IDisposable
    {
        private InventoryBagsCreator _inventoryBagsCreator;
        private Inventory _inventory;

        public RemoveItemObserver_UpdateInventoryBags(InventoryBagsCreator inventoryBagsCreator, Inventory inventory)
        {
            _inventoryBagsCreator = inventoryBagsCreator;
            _inventory = inventory;
            Subscribes();
        }

        private void Subscribes()
        {
            _inventory.OnItemRemoved += UpdateInventoryBags;
        }
        
        private void Unsubscribes()
        {
            _inventory.OnItemRemoved -= UpdateInventoryBags;
        }

        private void UpdateInventoryBags(InventoryItem _)
        {
            _inventoryBagsCreator.InitializeGrids();
        }

        public void Dispose()
        {
            Unsubscribes();
        }
    }
}