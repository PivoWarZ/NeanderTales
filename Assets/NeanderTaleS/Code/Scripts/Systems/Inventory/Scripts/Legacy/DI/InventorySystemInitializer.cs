/*using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.Interfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.DI
{
    public sealed class InventorySystemInitializer
    {
        private readonly List<IInventoryInitializable> _initializables;
        private readonly IContext _context;

        public InventorySystemInitializer(IInventoryInitializable[] initializables, IContext context)
        {
            _context = context;
            _initializables = initializables.ToList();
        }

        public void Initialize()
        {
            
            var inventoryComponent = _context.GetService<IInventoryComponent>();

            if (inventoryComponent != null)
            {
                foreach (var iInventoryInitializables in _initializables)
                {
                    iInventoryInitializables.Initialize(inventoryComponent);
                }   
            }
            else
            {
                Debug.LogError($"<color=red>Couldn't find any IInventoryComponent</color>");
                throw new System.NullReferenceException();
            }
        }
    }
}*/