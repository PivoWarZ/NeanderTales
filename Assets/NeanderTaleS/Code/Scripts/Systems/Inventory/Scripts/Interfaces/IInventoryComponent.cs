using Inventory.Scripts.DI;
using Inventory.Scripts.InventoryData.Manager;

namespace Inventory.Scripts.Interfaces
{
    public interface IInventoryComponent: IInventory, ICharacterStatsSetter, ICharacterStatsStructure
    {
        ICharacterStatsSetter CharacterStatsSetter { get; }
        ICharacterStatsStructure CharacterStatsStructure { get; }
    }
}