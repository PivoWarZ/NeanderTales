using System.Linq;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.configs;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Observers.GridObservers;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.popup
{
    public class InventoryPopup:MonoBehaviour
    {
        [SerializeField] private InventoryBagsCreator _bagsCreator;
        [SerializeField] private Scaler _scaler;
        private Inventory _inventory;
        private readonly GridsStorage _gridsStorage = new ();
        private CompositeDisposable _dispose = new ();

        private void Awake()
        {
            _bagsCreator.OnGridCreated += _gridsStorage.AddItem;
        }

        public void SetInventory(Inventory inventory)
        {
            _inventory = inventory;
            _inventory.OnItemAdded += Refresh;
            BootsTrap();
        }

        void OnRectTransformDimensionsChange()
        {
            _scaler.OnRectTransformDimensionsChange();
            _bagsCreator.UpdateInventoryBags(_inventory);
        }

        private void Refresh(InventoryItem _)
        {
            _bagsCreator.UpdateInventoryBags(_inventory);
        }

        private void BootsTrap()
        {
            var rect = GetComponent<RectTransform>();
            var config = Resources.Load<InventoryConfig>("InventoryConfig");
            var scalers = GetComponentsInChildren<Image>().ToList();
            
            _bagsCreator.Initialize(config, _inventory);
            _scaler.Initialize(scalers, rect);

            CreateGridObservers(_gridsStorage);
            
            _dispose.Add(_gridsStorage);
            _dispose.Add(_scaler);
        }

        private void CreateGridObservers(GridsStorage storage)
        {
            var left_click = new GridClickObserver_LeftClick(storage);
            _dispose.Add(left_click);
        }

        private void OnDestroy()
        {
            _bagsCreator.OnGridCreated -= _gridsStorage.AddItem;
            _inventory.OnItemAdded -= Refresh;
            _dispose.Dispose();
        }
    }
}