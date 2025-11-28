using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.Interfaces;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.components;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.InventoryBase;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Manager;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Observers.EquipItemClickObservers
{
    public sealed class EquipItemEffectObserver: IInventoryEffectObserver
    {
        private readonly ICharacterStatsSetter _setStats;

        public EquipItemEffectObserver(ICharacterStatsSetter setStats)
        {
           _setStats = setStats;
        }

        public void OnItemAdded(InventoryItem item)
        {
            if (!item.Flags.HasFlag(InventoryItemFlags.Equipable))
            {
                return;
            }

            if (item.TryGetComponent<DamageItemComponent>(out var damageComponent))
            {
                var damage = damageComponent.Damage;
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
            if (!item.Flags.HasFlag(InventoryItemFlags.Equipable))
            {
                return;
            }

            if (item.TryGetComponent<DamageItemComponent>(out var damageComponent))
            {
                var damage = damageComponent.Damage;
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