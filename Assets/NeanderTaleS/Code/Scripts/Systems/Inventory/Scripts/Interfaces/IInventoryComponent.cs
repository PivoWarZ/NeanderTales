using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Manager;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.Interfaces
{
    public interface IInventoryComponent: IInventory, ICharacterStatsSetter, ICharacterStatsStructure
    {
        ICharacterStatsSetter CharacterStatsSetter { get; }
        ICharacterStatsStructure CharacterStatsStructure { get; }
    }
}