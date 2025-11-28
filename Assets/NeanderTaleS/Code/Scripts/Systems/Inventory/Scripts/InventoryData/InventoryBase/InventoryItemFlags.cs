using System;

namespace Inventory.Scripts.InventoryData.InventoryBase
{
    [Flags]
    public enum InventoryItemFlags
    {
        None = 0,
        Effectable = 1 << 1,
        Consumable = 1 << 2,
        Equipable = 1 << 3,
        EquipableTypeHead = 1 << 4,
        EquipableTypeBody = 1 << 5,
        EquipableTypeLegs = 1 << 6,
        EquipableTypeWeapon = 1 << 7,
        EquipableTypeShield = 1 << 8,
    }
}