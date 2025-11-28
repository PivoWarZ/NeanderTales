namespace Inventory.Scripts.DI
{
    public interface IInventory
    {
        InventoryData.InventoryBase.Inventory Inventory { get; }
    }
}