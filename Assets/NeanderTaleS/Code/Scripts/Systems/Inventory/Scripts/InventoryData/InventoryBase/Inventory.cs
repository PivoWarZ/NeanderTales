using System;
using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.components;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.InventoryBase
{
    [Serializable]
    public sealed class Inventory
    {
        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem> OnItemRemoved;
        public event Action<InventoryItem> OnItemConsumed;
        public List<InventoryItem> Items = new ();

        public void AddItem(InventoryItem item)
        {
            if (!TryAddStackableItem(item))
            {
                Items.Add(item);
                ItemAdded(item);
            }
        }

        private void ItemAdded(InventoryItem item)
        {
            OnItemAdded?.Invoke(item);
        }

        public void RemoveItem(InventoryItem prototype)
        {
              Items.Remove(prototype);
              OnItemRemoved?.Invoke(prototype);
        }

        private bool TryAddStackableItem(InventoryItem item)
        {
            if (item.TryGetComponent<StackableComponent>(out _))
            {
                List<InventoryItem> stackableItems = GetAllCurrenItems(item.Id);

                if (TryFindNotCompleteStack(stackableItems, out var stack))
                {
                    stack.IncrementCount();
                    return true;
                }
            }
            
            return false;
        }

        private bool TryFindNotCompleteStack(List<InventoryItem> items, out StackableComponent stackNotComplete)
        {
            foreach (var inventoryItem in items)
            {
                inventoryItem.TryGetComponent<StackableComponent>(out var stack);
                bool isMaxStack = stack.Count.Value >= stack.MaxCount;

                if (!isMaxStack)
                {
                    stackNotComplete = stack;
                    return true;
                }
            }
            
            stackNotComplete = null;
            return false;
        }

        private List<InventoryItem> GetAllCurrenItems(string id)
        {
            List<InventoryItem> items = new();

            foreach (var inventoryItem in Items)
            {
                if (inventoryItem.Id == id)
                {
                    items.Add(inventoryItem);
                }
            }
            
            return items;
        }

        public bool TryFindInventoryItem(string id, out InventoryItem item)
        {
            item = Items.FirstOrDefault(item => item.Id == id);
            return item != null;
        }

        private bool HasItem(string id)
        {
            return Items.Any(item => item.Id == id);
        }

        public void ConsumeItem(InventoryItem item)
        {
            if (!TryFindInventoryItem(item.Id, out var inventoryItem))
            {
                return;
            }

            if (!inventoryItem.Flags.HasFlag(InventoryItemFlags.Consumable))
            {
                return;
            }

            if (item.TryGetComponent<StackableComponent>(out var stack))
            {
                stack.DecrementCount();
                
                if (stack.Count.Value <= 0)
                {
                   RemoveItem(item);
                }
            }
            else
            {
                RemoveItem(item);
            }

            ItemConsumed(item);
        }

        private void ItemConsumed(InventoryItem item)
        {
            OnItemConsumed?.Invoke(item);
        }
    }
}