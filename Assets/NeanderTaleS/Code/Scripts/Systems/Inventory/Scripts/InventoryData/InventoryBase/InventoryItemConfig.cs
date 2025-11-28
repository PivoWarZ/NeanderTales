using UnityEngine;

namespace Inventory.Scripts.InventoryData.InventoryBase
{
    [CreateAssetMenu(fileName = "InventoryItemConfig", menuName = "Inventory/InventoryItem/New InventoryItemConfig")]
    public sealed class InventoryItemConfig: ScriptableObject
    {
        [SerializeField] private InventoryItem _item;

        public InventoryItem Clone()
        {
            return _item.Clone();
        }
    }
}