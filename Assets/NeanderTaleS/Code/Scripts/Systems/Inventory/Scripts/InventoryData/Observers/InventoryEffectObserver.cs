using System;
using Inventory.Scripts.Interfaces;
using Inventory.Scripts.InventoryData.components;
using Inventory.Scripts.InventoryData.EquipPopup;
using Inventory.Scripts.InventoryData.InventoryBase;
using Inventory.Scripts.InventoryData.Manager;

namespace Inventory.Scripts.InventoryData.Observers
{
    public sealed class InventoryEffectObserver: IInventoryEffectObserver, IDisposable, IInventoryInitializable
    {
        private ICharacterStatsSetter _setStats;
        private InventoryBase.Inventory _inventory;
        
        void IInventoryInitializable.Initialize(IInventoryComponent inventoryComponent)
        {
            _setStats = inventoryComponent.CharacterStatsSetter;
            _inventory = inventoryComponent.Inventory;
            _inventory.OnItemAdded += OnItemAdded;
            _inventory.OnItemRemoved += OnItemRemoved;
        }
        
        public void Dispose()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemRemoved -= OnItemRemoved;
        }

        public void OnItemAdded(InventoryItem item)
        {
            if (!item.Flags.HasFlag(InventoryItemFlags.Effectable))
            {
                return;
            }

            if (item.TryGetComponent<DamageItemComponent>(out var damageItemComponent))
            {
                var damage = damageItemComponent.Damage;
                _setStats.AddDamage(damage);
            }
            
            if (item.TryGetComponent<ArmorComponent>(out var armorComponent))
            {
                var armor = armorComponent.Armor;
                _setStats.AddArmor(armor);
            }
            
            if (item.TryGetComponent<DexterityComponent>(out var dexterityComponent))
            {
                var dexterity = dexterityComponent.Dexterity;
                _setStats.AddDexterity(dexterity);
            }
        }

        public void OnItemRemoved(InventoryItem item)
        {
            if (!item.Flags.HasFlag(InventoryItemFlags.Effectable))
            {
                return;
            }
            
            if (item.TryGetComponent<DamageItemComponent>(out var damageItemComponent))
            {
                var damage = damageItemComponent.Damage;
                _setStats.RemoveDamage(damage);
            }
            
                        
            if (item.TryGetComponent<ArmorComponent>(out var armorComponent))
            {
                var armor = armorComponent.Armor;
                _setStats.RemoveArmor(armor);
            }
            
            if (item.TryGetComponent<DexterityComponent>(out var dexterityComponent))
            {
                var dexterity = dexterityComponent.Dexterity;
                _setStats.RemoveDexterity(dexterity);
            }
        }
    }
}