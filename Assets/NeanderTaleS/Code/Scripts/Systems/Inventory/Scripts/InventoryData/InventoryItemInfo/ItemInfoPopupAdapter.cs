using System;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.components;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.InventoryBase;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.InventoryItemInfo
{
    public sealed class ItemInfoPopupAdapter: IDisposable
    {
        public event Action<InventoryItem, bool> OnEquipButtonClick;
        public event Action<InventoryItem, bool> OnTrowAwayButtonClick;
        
        private InventoryItemInfoView _view;
        private InventoryItem _item;
        private bool _isEquipped;

        public ItemInfoPopupAdapter(InventoryItemInfoView view)
        {
            _view = view;
            _view.CloseButton.onClick.AddListener(HideInfoPopup);
            _view.EquipButton.onClick.AddListener(EquipButtonClick);
            _view.TrowAwayButton.onClick.AddListener(TrowInButtonClick);
        }

        private void EquipButtonClick()
        {
            OnEquipButtonClick?.Invoke(_item, _isEquipped);
        }

        private void TrowInButtonClick()
        {
            OnTrowAwayButtonClick?.Invoke(_item, _isEquipped);
        }

        public void HideInfoPopup()
        {
            _view.gameObject.SetActive(false);
        }

        public void ShowInfoPopup()
        {
            _view.gameObject.SetActive(true);
        }

        public void InitView(InventoryItem item, bool isEquip = false)
        {
            _item = item;
            _isEquipped = isEquip;
            InfoStruct info = new InfoStruct();

            if (item.Flags.HasFlag(InventoryItemFlags.Equipable))
            {
                info.EquipButtonText = "Equip";
            }
            else
            {
                info.EquipButtonText = "Use item";
            }

            if (isEquip)
            {
                info.EquipButtonText = "Unequip";
            }

            if (item.Flags.HasFlag(InventoryItemFlags.Effectable))
            {
                _view.EquipButton.gameObject.SetActive(false);
            }
            else
            {
                _view.EquipButton.gameObject.SetActive(true);
            }

            info.Name = item.Meta.Name;
            info.Description = item.Meta.Description;
            info.Icon = item.Meta.Icon;
            info.StatsText = SetStatsText(item);
            
            _view.Init(info);
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
            _view.CloseButton.onClick.RemoveListener(HideInfoPopup);
            _view.EquipButton.onClick.RemoveListener(EquipButtonClick);
            _view.TrowAwayButton.onClick.RemoveListener(TrowInButtonClick);
        }
    }
}