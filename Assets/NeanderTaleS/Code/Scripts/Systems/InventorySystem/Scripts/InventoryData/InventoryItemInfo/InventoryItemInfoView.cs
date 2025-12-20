using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryItemInfo
{
    public sealed class InventoryItemInfoView: MonoBehaviour
    {
        
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _equipButtonText;
        [SerializeField] private TMP_Text _statsText;
        [SerializeField] private Button _equipButton;
        [SerializeField] private Button _trowAwayButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Image _icon;
        private InventoryItem _item;

        public Button EquipButton => _equipButton;

        public Button TrowAwayButton => _trowAwayButton;

        public Button CloseButton => _closeButton;

        public InventoryItem Item => _item;

        public void Init(ItemViewInfo itemViewInfo)
        {
            _item = itemViewInfo.Item;
            _name.text = itemViewInfo.Item.Meta.Name;
            _description.text = itemViewInfo.Item.Meta.Description;
            _icon.sprite = itemViewInfo.Item.Meta.Icon;
            _equipButtonText.text = itemViewInfo.EquipButtonText;
            _statsText.text = itemViewInfo.StatsText;
        }
    }
}