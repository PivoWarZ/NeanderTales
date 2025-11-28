using System;
using Inventory.Scripts.Interfaces;
using R3;

namespace Inventory.Scripts.InventoryData.components
{
    [Serializable]
    public sealed class StackableComponent: IItemComponent
    {
        public SerializableReactiveProperty<int> Count = new (1);
        public int MaxCount;
        
        public IItemComponent Clone()
        {
            return new StackableComponent()
            {
                Count = new SerializableReactiveProperty<int>(Count.Value),
                MaxCount = MaxCount,
            };
        }

        public void IncrementCount()
        {
            Count.Value++;
        }

        public void DecrementCount()
        {
            Count.Value--;
        }
    }
}