using System;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.Interfaces;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.EquipPopup;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Manager;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts
{
    public sealed class Hero: MonoBehaviour, IInventoryComponent
    {
        public event Action OnStatsValueChanged;
        public float Health;
        public float Damage;
        public float Dexterity;
        public float Armor;
        
        [SerializeField] InventoryData.InventoryBase.Inventory _inventory;
        private ICharacterStatsStructure _characterStatsStructure;

        public InventoryData.InventoryBase.Inventory Inventory => _inventory;
        
        ICharacterStatsSetter IInventoryComponent.CharacterStatsSetter => this;

        ICharacterStatsStructure IInventoryComponent.CharacterStatsStructure => this;

        private void StatsValueChanged()
        {
            OnStatsValueChanged?.Invoke();
        }

        StatsStruct ICharacterStatsStructure.GetStats()
        {
            StatsStruct stats = new StatsStruct
            {
                Health = $"HitPoints: {Health.ToString()}",
                Damage = $"Damage: {Damage.ToString()}",
                Dexterity = $"Dexterity: {Dexterity.ToString()}",
                Armor = $"Armor: {Armor.ToString()}"
            };
            
            return stats;
        }

        void ICharacterStatsSetter.AddHitPoints(int hitPoints)
        {
            Health += hitPoints;
            StatsValueChanged();
        }

        void ICharacterStatsSetter.RemoveHitPoints(int hitPoints)
        {
            Health -= hitPoints;
            StatsValueChanged();
        }

        void ICharacterStatsSetter.AddArmor(int armor)
        {
            Armor += armor;
            StatsValueChanged();
        }

        void ICharacterStatsSetter.RemoveArmor(int armor)
        {
            Armor -= armor;
            StatsValueChanged();
        }

        void ICharacterStatsSetter.AddDexterity(int dexterity)
        {
            Dexterity += dexterity;
            StatsValueChanged();
        }

        void ICharacterStatsSetter.RemoveDexterity(int dexterity)
        {
            Dexterity -= dexterity;
            StatsValueChanged();
        }

        void ICharacterStatsSetter.AddDamage(int damage)
        {
            Damage += damage;
            StatsValueChanged();
        }

        void ICharacterStatsSetter.RemoveDamage(int damage)
        {
            Damage -= damage;
            StatsValueChanged();
        }
    }
}