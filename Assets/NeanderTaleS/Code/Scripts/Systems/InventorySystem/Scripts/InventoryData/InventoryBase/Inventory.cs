using System;
using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase
{
    [Serializable]
    public sealed class Inventory
    {
        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem> OnItemRemoved;
        public event Action<InventoryItem> OnItemConsumed;
        public event Action OnInventoryUpdated;
        
        public List<InventoryItem> Items = new ();

        public void AddStub()
        {
            Items.Add(new InventoryItem());
        }

        public void AddItem(InventoryItem item)
        {
            if (!TryAddStackableItem(item) && !TryReplaceStubItem(item))
            {
                Items.Add(item);
            }
            
            OnItemAdded?.Invoke(item);
        }

        public void ReplaceItems(int index1, int index2)
        {
            if (index1 == index2 || index1 < 0 || index2 < 0)
            {
                return;
            }

            (Items[index1], Items[index2]) = (Items[index2], Items[index1]);
            
            OnInventoryUpdated?.Invoke();
        }

        public void Reset(InventoryItem prototype)
        {
            var index = Items.IndexOf(prototype);
            Items[index] = new InventoryItem();
            OnItemRemoved?.Invoke(prototype);
        }

        public void Remove(InventoryItem prototype)
        {
            Items.Remove(prototype);
            OnItemRemoved?.Invoke(prototype);
        }

        private int GetIndexOfItem(InventoryItem item)
        {
            return Items.IndexOf(item);
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

        private bool TryReplaceStubItem(InventoryItem item)
        {
            var isStubItem = TryGetStubItem(out var stub);

            if (isStubItem)
            {
                Items[GetIndexOfItem(stub)] = item;
                return true;
            }
            
            return false;
        }

        private bool TryGetStubItem(out InventoryItem stub)
        {
            var stubItem = Items.FirstOrDefault(stub => stub.IsStub());
            stub = stubItem;
            
            return stubItem != null;
        }

        private bool TryFindNotCompleteStack(List<InventoryItem> items, out StackableComponent stackNotComplete)
        {
            foreach (var inventoryItem in items)
            {
                inventoryItem.TryGetComponent<StackableComponent>(out var stack);
                bool isMaxStack = stack.Count >= stack.MaxCount;

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
                
                if (stack.Count <= 0)
                {
                   Reset(item);
                }
            }
            else
            {
                Reset(item);
            }

            ItemConsumed(item);
        }

        private void ItemConsumed(InventoryItem item)
        {
            OnItemConsumed?.Invoke(item);
        }
    }
}