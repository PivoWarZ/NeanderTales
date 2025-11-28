using System;
using Inventory.Scripts.InventoryData.EquipPopup;

namespace Inventory.Scripts
{
    public interface ICharacterStatsStructure
    {
        event Action OnStatsValueChanged;
        StatsStruct GetStats();
    }
}