using System;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.Interfaces;
using R3;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.components
{
    [Serializable]
    public sealed class StackableComponent: IItemComponent
    {
        public int Count = 1;
        public int MaxCount;
        
        public IItemComponent Clone()
        {
            return new StackableComponent()
            {
                Count = Count,
                MaxCount = MaxCount,
            };
        }

        public void IncrementCount()
        {
            Count++;
        }

        public void DecrementCount()
        {
            Count--;
        }
    }
}