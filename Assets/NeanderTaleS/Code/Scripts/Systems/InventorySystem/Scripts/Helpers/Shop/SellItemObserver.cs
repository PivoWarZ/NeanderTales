using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.Helpers.Shop
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
            Debug.Log($"{GetType().Name}: Added to inventory {item.Id}");
        }

        private void OnDestroy()
        {
            _shop.OnItemSold -= AddInventoryItem;
        }
    }
}