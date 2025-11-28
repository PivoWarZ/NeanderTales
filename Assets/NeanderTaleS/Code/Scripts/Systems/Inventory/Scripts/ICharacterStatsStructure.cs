using System;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.EquipPopup;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts
{
    public interface ICharacterStatsStructure
    {
        event Action OnStatsValueChanged;
        StatsStruct GetStats();
    }
}