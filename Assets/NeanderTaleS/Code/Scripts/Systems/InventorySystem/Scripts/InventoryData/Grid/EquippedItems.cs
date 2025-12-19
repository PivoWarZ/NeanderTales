using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid
{
    public static class EquippedItems
    {
        public static event Action<InventoryItem> OnItemEquipped;
        public static event Action<InventoryItem> OnItemUnequipped;
        
        private static List<InventoryItem> _equippedItems = new ();

        public static bool IsItemEquipped(InventoryItem item)
        {
            return _equippedItems.Contains(item);
        }

        public static void Add(InventoryItem item)
        {
            _equippedItems.Add(item);
            OnItemEquipped?.Invoke(item);
        }

        public static void Remove(InventoryItem item)
        {
            _equippedItems.Remove(item);
            OnItemUnequipped?.Invoke(item);
        }
    }
}