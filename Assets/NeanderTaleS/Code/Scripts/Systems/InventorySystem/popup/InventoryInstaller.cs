using System;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.Interfaces;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.popup
{
    public class InventoryInstaller: MonoBehaviour
    {
        [SerializeField] private InventoryPopup _popup;
        [SerializeField] private Hero _hero;

        private void Awake()
        {
            var inventory = _hero.GetComponent<IInventory>().Inventory;
            _popup.SetInventory(inventory);
        }
    }
}