using System.Linq;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.configs;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryItemInfo;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Observers.GridObservers;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.popup
{
    public class InventoryPopup:MonoBehaviour
    {
        [SerializeField] private InventoryBagsCreator _bagsCreator;
        [SerializeField] private InventoryItemInfoView _itemInfoView;
        private Scripts.InventoryData.InventoryBase.Inventory _inventory;
        private CompositeDisposable _dispose = new ();

        public void SetInventory(Scripts.InventoryData.InventoryBase.Inventory inventory)
        {
            _inventory = inventory;
            _inventory.OnItemAdded += Refresh;
            BootsTrap();
        }

        void OnRectTransformDimensionsChange()
        {
            _bagsCreator.UpdateInventoryBags(_inventory);
        }

        private void Refresh(InventoryItem _)
        {
            _bagsCreator.UpdateInventoryBags(_inventory);
        }

        private void BootsTrap()
        {
            var config = Resources.Load<InventoryConfig>("InventoryConfig");
            
            _bagsCreator.Initialize(config, _inventory);

            CreateGridObservers();
        }

        private void CreateGridObservers()
        {
            var left_click = new GridClickObserver_LeftClick();
            _dispose.Add(left_click);
            
            var right_click = new GridObserver_RightClick(_itemInfoView);
            _dispose.Add(right_click);
        }

        private void OnDestroy()
        {
            _inventory.OnItemAdded -= Refresh;
            _dispose.Dispose();
            Bag.Grids.Clear();
        }
    }
}