using UnityEngine;

namespace Inventory.Scripts.InventoryData.Servises
{
    public sealed class InventoryPopupProvider: MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryPopup;
        [SerializeField] private GameObject _viewPort;
        [SerializeField] private GameObject _content;
        [SerializeField] private GameObject _itemInfo;
        [SerializeField] private GameObject _equipPopup;

        public GameObject InventoryPopup => _inventoryPopup;

        public GameObject ViewPort => _viewPort;

        public GameObject Content => _content;

        public GameObject ItemInfo => _itemInfo;

        public GameObject EquipPopup => _equipPopup;
    }
}