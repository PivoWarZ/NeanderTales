using System;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.popup;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Listeners
{
    public class InventoryUpdateListener_InitializeGrid: IDisposable
    {
        private Inventory _inventory;
        private InventoryBagsCreator _creator;

        public InventoryUpdateListener_InitializeGrid(Inventory inventory, InventoryBagsCreator creator)
        {
            _inventory = inventory;
            _creator = creator;

            _inventory.OnInventoryUpdated += InitializeGrid;
        }

        private void InitializeGrid()
        {
            _creator.InitializeGrids();
        }

        public void Dispose()
        {
            _inventory.OnInventoryUpdated -= InitializeGrid;
        }
    }
}