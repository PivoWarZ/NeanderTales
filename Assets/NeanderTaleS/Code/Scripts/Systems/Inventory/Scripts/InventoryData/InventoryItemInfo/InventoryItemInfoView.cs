using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.InventoryItemInfo
{
    public sealed class InventoryItemInfoView: MonoBehaviour
    {
        
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _equipButtonText;
        [SerializeField] private TMP_Text _staText;
        [SerializeField] private Button _equipButton;
        [SerializeField] private Button _trowAwayButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Image _icon;

        public Button EquipButton => _equipButton;

        public Button TrowAwayButton => _trowAwayButton;

        public Button CloseButton => _closeButton;

        public void Init(InfoStruct info)
        {
            _name.text = info.Name;
            _description.text = info.Description;
            _icon.sprite = info.Icon;
            _equipButtonText.text = info.EquipButtonText;
            _staText.text = info.StatsText;
        }
    }
}