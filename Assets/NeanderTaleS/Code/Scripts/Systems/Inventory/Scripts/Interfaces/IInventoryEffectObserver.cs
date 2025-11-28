using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.InventoryBase;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.Interfaces
{
    public interface IInventoryEffectObserver
    {
        void OnItemAdded(InventoryItem item);
        void OnItemRemoved(InventoryItem item);
    }
}