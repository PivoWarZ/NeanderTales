using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Manager;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.Interfaces
{
    public interface IInventoryComponent: IInventory, ICharacterStatsSetter, ICharacterStatsStructure
    {
        ICharacterStatsSetter CharacterStatsSetter { get; }
        ICharacterStatsStructure CharacterStatsStructure { get; }
    }
}