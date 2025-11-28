using Inventory.Scripts.Interfaces;

namespace Inventory.Scripts.InventoryData.EquipPopup
{
    public interface IInventoryInitializable
    {
        void Initialize(IInventoryComponent inventoryComponent);
    }
}