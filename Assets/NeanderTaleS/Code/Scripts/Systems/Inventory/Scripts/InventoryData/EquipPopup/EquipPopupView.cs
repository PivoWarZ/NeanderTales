using System;
using Inventory.Scripts.InventoryData.Grid;
using Inventory.Scripts.InventoryData.InventoryBase;
using UnityEngine;

namespace Inventory.Scripts.InventoryData.EquipPopup
{
    public sealed class EquipPopupView: MonoBehaviour
    {
        public event Action<GridItem> OnItemEquipped;
        
        [SerializeField] private GridItem _head;
        [SerializeField] private GridItem _body;
        [SerializeField] private GridItem _legs;
        [SerializeField] private GridItem _weapon;
        [SerializeField] private GridItem _shield;

        public void EuipItem(InventoryItem item)
        {
            if (!item.Flags.HasFlag(InventoryItemFlags.Equipable))
            {
                return;
            }

            if (item.Flags.HasFlag(InventoryItemFlags.EquipableTypeWeapon))
            {
                EquipItem(_weapon, item);
            }
            
            if (item.Flags.HasFlag(InventoryItemFlags.EquipableTypeBody))
            {
                EquipItem(_body, item);
            }
            
            if (item.Flags.HasFlag(InventoryItemFlags.EquipableTypeShield))
            {
                EquipItem(_shield, item);
            }
            
            if (item.Flags.HasFlag(InventoryItemFlags.EquipableTypeHead))
            {
                EquipItem(_head, item);
            }
            
            if (item.Flags.HasFlag(InventoryItemFlags.EquipableTypeLegs))
            {
                EquipItem(_legs, item);
            }
        }

        private void EquipItem(GridItem grid, InventoryItem item)
        {
            grid.Initialize(item);
            grid.gameObject.SetActive(true);
            ItemEquipped(grid);
        }

        private void ItemEquipped(GridItem item)
        {
            OnItemEquipped?.Invoke(item);
        }

    }
}