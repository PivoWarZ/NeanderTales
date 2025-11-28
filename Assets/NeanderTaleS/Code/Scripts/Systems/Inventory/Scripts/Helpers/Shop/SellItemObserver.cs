using Inventory.Scripts.InventoryData.InventoryBase;
using UnityEngine;

namespace Inventory.Scripts.Helpers.Shop
{
    public sealed class SellItemObserver: MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        [SerializeField] private ShopStorage _shop;

        private void Awake()
        {
            _shop.OnItemSold += AddInventoryItem;
        }

        private void AddInventoryItem(InventoryItem item)
        {
            _hero.Inventory.AddItem(item.Clone());
        }

        private void OnDestroy()
        {
            _shop.OnItemSold -= AddInventoryItem;
        }
    }
}