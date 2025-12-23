using System;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.popup;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid
{
    public class DropHandler: MonoBehaviour, IDropHandler
    {
        private Inventory _inventory;

        private void Start()
        {
            _inventory = GetComponentInParent<InventoryPopup>().Inventory;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var gridItem = eventData.pointerEnter.GetComponentInParent<GridItem>();
            var dragIndex = eventData.pointerDrag.gameObject.GetComponent<DragHandler>().Index;
            var dropIndex = Bag.Grids.IndexOf(gridItem);
            
            _inventory.ReplaceItems(dragIndex, dropIndex);
        }
    }
}