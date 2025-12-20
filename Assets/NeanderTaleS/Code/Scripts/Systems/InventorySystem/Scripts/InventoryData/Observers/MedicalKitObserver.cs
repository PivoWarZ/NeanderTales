using System;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.Interfaces;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.components;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Manager;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Observers
{
    public sealed class MedicalKitObserver: IDisposable, IInventoryInitializable
    {
        private Inventory _inventory;
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