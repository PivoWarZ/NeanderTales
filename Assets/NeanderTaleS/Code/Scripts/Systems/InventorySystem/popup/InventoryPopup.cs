using NeanderTaleS.Code.Scripts.Systems.InventorySystem.configs;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryItemInfo;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Observers.GridObservers;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.popup
{
    public class InventoryPopup:MonoBehaviour
    {
        [SerializeField] private InventoryBagsCreator _bagsCreator;
        [SerializeField] private InventoryItemInfoView _itemInfoView;
        private Inventory _inventory;
        private CompositeDisposable _dispose = new ();

        public void SetInventory(Inventory inventory)
        {
            _inventory = inventory;
            _inventory.OnItemAdded += InitializeGrids;
            BootsTrap();
        }

        void OnRectTransformDimensionsChange()
        {
            _bagsCreator.UpdateInventoryBags();
        }

        private void InitializeGrids(InventoryItem _)
        {
            _bagsCreator.InitializeGrids();
        }

        private void BootsTrap()
        {
            var config = Resources.Load<InventoryConfig>("InventoryConfig");
            
            _bagsCreator.Initialize(config, _inventory);
            
            var infoPopupAdapter = new ItemInfoPopupAdapter(_itemInfoView);
            _dispose.Add(infoPopupAdapter);
            
            var itemInfoButtonCliclListener = new ItemInfoButtonsClickListener(_itemInfoView, _inventory);
            _dispose.Add(itemInfoButtonCliclListener);

            CreateGridObservers(infoPopupAdapter, _bagsCreator, _inventory);
        }

        private void CreateGridObservers(ItemInfoPopupAdapter infoPopupAdapter, InventoryBagsCreator bagsCreator, Inventory inventory)
        {
            var left_click = new GridClickObserver_LeftClick();
            _dispose.Add(left_click);
            
            var right_click = new GridObserver_RightClick(infoPopupAdapter);
            _dispose.Add(right_click);

            var gridReset = new RemoveItemObserver_UpdateInventoryBags(bagsCreator, inventory);
            _dispose.Add(gridReset);
        }

        private void OnDestroy()
        {
            _inventory.OnItemAdded -= InitializeGrids;
            _dispose.Dispose();
            Bag.Grids.Clear();
            _bagsCreator.Dispose();
        }
    }
}