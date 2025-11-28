using System;
using Inventory.Scripts.Interfaces;
using Inventory.Scripts.InventoryData.components;
using Inventory.Scripts.InventoryData.EquipPopup;
using Inventory.Scripts.InventoryData.InventoryBase;
using Inventory.Scripts.InventoryData.Manager;

namespace Inventory.Scripts.InventoryData.Observers
{
    public sealed class MedicalKitObserver: IDisposable, IInventoryInitializable
    {
        private InventoryBase.Inventory _inventory;
        private ICharacterStatsSetter _setStats;

        void IInventoryInitializable.Initialize(IInventoryComponent inventoryComponent)
        {
            _inventory = inventoryComponent.Inventory;
            _setStats = inventoryComponent.CharacterStatsSetter;
            _inventory.OnItemConsumed += Healing;
        }

        void IDisposable.Dispose()
        {
            _inventory.OnItemConsumed -= Healing;
        }

        private void Healing(InventoryItem item)
        {
            if(item.TryGetComponent<HealItemComponent>(out var healItemComponent))
            {
                var heal = healItemComponent.Heal;
                _setStats.AddHitPoints(heal);
            }
        }
    }
}