using System;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.components;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryItemInfo
{
    public sealed class ItemInfoPopupAdapter: IDisposable
    {
        private InventoryItemInfoView _view;
        private InventoryItem _item;
        private bool _isEquipped;

        public ItemInfoPopupAdapter(InventoryItemInfoView view)
        {
            _view = view;

            Bag.OnGridActivated += RefreshItemInfoPopup;
        }

        public void RefreshItemInfoPopup(GridItem grid)
        {
            if(grid.IsInitialising)
                InitView(grid.InventoryItem);
        }

        public void ShowInfoPopup()
        {
            _view.gameObject.SetActive(true);
        }

        private void InitView(InventoryItem item)
        {
            _item = item;

            ItemViewInfo itemViewInfo = new ItemViewInfo();
            itemViewInfo.Item = item;

            if (item.Flags.HasFlag(InventoryItemFlags.Equipable))
            {
                itemViewInfo.EquipButtonText = "Equip";
            }
            else
            {
                itemViewInfo.EquipButtonText = "Use item";
            }

            if (EquippedItems.IsItemEquipped(_item))
            {
                itemViewInfo.EquipButtonText = "Unequip";
            }

            if (item.Flags.HasFlag(InventoryItemFlags.Effectable))
            {
                _view.EquipButton.gameObject.SetActive(false);
            }
            else
            {
                _view.EquipButton.gameObject.SetActive(true);
            }
            
            itemViewInfo.StatsText = SetStatsText(item);
            
            _view.Init(itemViewInfo);
        }
        
        private string SetStatsText(InventoryItem item)
        {
            string stats = string.Empty;
            
            var isDamage = item.TryGetComponent<DamageItemComponent>(out var damageComponent);
            var isHeal = item.TryGetComponent<HealItemComponent>(out var healComponent);
            var isArmor = item.TryGetComponent<ArmorComponent>(out var armorComponent);
            var isDexterity = item.TryGetComponent<DexterityComponent>(out var dexterityComponent);

            if (isDamage)
            {
                var damage = damageComponent.Damage;
                stats += CreateStatString(damage, "Damage");
            }

            if (isHeal)
            {
                var heal = healComponent.Heal;
                stats += CreateStatString(heal, "Healing");
            }

            if (isArmor)
            {
                var armor = armorComponent.Armor;
                stats += CreateStatString(armor, "Armor");
            }
            
            if (isDexterity)
            {
                var dexterity = dexterityComponent.Dexterity;
                stats += CreateStatString(dexterity, "Dexterity");
            }

            return stats;
        }

        private string CreateStatString(int value, string statName)
        {
            string stats = string.Empty;
            string color = value > 0 ? "green" : "red";
            string sign = value > 0 ? "+" : string.Empty;
            
            stats += $"{statName}: <color={color}>{sign}{value.ToString()}</color><br>";
            
            return stats;
        }

        public void Dispose()
        {
            Bag.OnGridActivated -= RefreshItemInfoPopup;
        }
    }
}