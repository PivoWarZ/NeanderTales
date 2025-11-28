using Inventory.Scripts.InventoryData.InventoryBase;

namespace Inventory.Scripts.Interfaces
{
    public interface IInventoryEffectObserver
    {
        void OnItemAdded(InventoryItem item);
        void OnItemRemoved(InventoryItem item);
    }
}