using System;
using Inventory.Scripts.DI;
using Inventory.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Inventory.Scripts.Helpers
{
    public class InitializerView: MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryObject;
        private InventorySystemInitializer _inventorySystemInitializer;

        [Inject]
        public void Construct(InventorySystemInitializer inventorySystemInitializer, DiContainer container)
        {
            _inventorySystemInitializer = inventorySystemInitializer;
            _inventoryObject.TryGetComponent<IInventoryComponent>(out var inventoryComponent);
            container.BindInstance(inventoryComponent);
        }

        private void Start()
        {
            _inventorySystemInitializer.Initialize();
        }
    }
}