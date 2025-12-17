using System;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.EquipPopup;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts
{
    public interface ICharacterStatsStructure
    {
        event Action OnStatsValueChanged;
        StatsStruct GetStats();
    }
}