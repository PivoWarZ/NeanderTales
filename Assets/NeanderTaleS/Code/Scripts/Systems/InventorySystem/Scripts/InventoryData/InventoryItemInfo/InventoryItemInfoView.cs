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

        public Button EquipButton => _equipButton;

        public Button TrowAwayButton => _trowAwayButton;

        public Button CloseButton => _closeButton;

        public void Init(ItemInfo itemInfo)
        {
            _name.text = itemInfo.Name;
            _description.text = itemInfo.Description;
            _icon.sprite = itemInfo.Icon;
            _equipButtonText.text = itemInfo.EquipButtonText;
            _statsText.text = itemInfo.StatsText;
        }
    }
}