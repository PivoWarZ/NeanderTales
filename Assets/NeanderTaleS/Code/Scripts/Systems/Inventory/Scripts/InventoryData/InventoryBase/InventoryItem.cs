using System;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.Interfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.InventoryBase
{
    [Serializable]
    public sealed class InventoryItem
    {
        public string Id;
        public InventoryItemMeta Meta;
        public InventoryItemFlags Flags;
        
        [SerializeReference]
        public IItemComponent[] Components;

        public bool TryGetComponent<T>(out T itemComponent) where T : IItemComponent
        {
            foreach (var component in Components)
            {
                if (component is T targetComponent)
                {
                    itemComponent = targetComponent;
                    return true;
                }
            }
            
            itemComponent = default;
            return false;
        }

        public InventoryItem Clone()
        {
            var copiesComponent = Array.Empty<IItemComponent>();

            if (Components != null)
            {
                copiesComponent = new IItemComponent[Components.Length];

                for (var index = 0; index < Components.Length; index++)
                {
                    var itemComponent = Components[index].Clone();
                    copiesComponent[index] = itemComponent;
                }
            }

            return new InventoryItem
            {
                Id = Id,
                Meta = new InventoryItemMeta
                {
                    Icon = Meta.Icon,
                    Name = Meta.Name,
                    Description = Meta.Description,
                },
                
                Flags = Flags,
                Components = copiesComponent
            };
        }
    }
}


