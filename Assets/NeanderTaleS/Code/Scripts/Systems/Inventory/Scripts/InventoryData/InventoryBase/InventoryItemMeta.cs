using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.InventoryBase
{
    [Serializable]
    public sealed class InventoryItemMeta
    {
        [PreviewField]
        public Sprite Icon;
        public string Name;
        public string Description;
    }
}