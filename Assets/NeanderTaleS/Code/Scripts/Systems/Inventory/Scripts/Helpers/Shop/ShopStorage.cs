using System;
using System.Collections.Generic;
using Inventory.Scripts.InventoryData.InventoryBase;
using UnityEngine;

namespace Inventory.Scripts.Helpers.Shop
{
    public sealed class ShopStorage: MonoBehaviour
    {
        public event Action<InventoryItem> OnItemSold;
        [SerializeField] private Transform _contentTransform;
        [SerializeField] private GameObject _sellItemPrefab;
        [SerializeField] private InventoryItemConfig[] _itemConfigs;
        private readonly Dictionary<GameObject, InventoryItem> _sellItems = new ();

        private void Awake()
        {
            for (var i = 0; i < _itemConfigs.Length; i++)
            {
                var itemPrefab = Instantiate(_sellItemPrefab, _contentTransform);
                var inventoryItem = _itemConfigs[i].Clone();
                var sprite = inventoryItem.Meta.Icon;
                var sellItem = itemPrefab.GetComponent<SellItem>();
                sellItem.Image.sprite = sprite;
                _sellItems[itemPrefab] = inventoryItem;
                sellItem.OnItemSelected += SoldItem;
            }
        }

        private void SoldItem(GameObject item)
        {
            var sellItem = _sellItems[item].Clone();
            OnItemSold?.Invoke(sellItem);
        }

        private void OnDestroy()
        {
            foreach (var sellItemsKey in _sellItems.Keys)
            {
                sellItemsKey.GetComponent<SellItem>().OnItemSelected -= SoldItem;
            }
        }
    }
}