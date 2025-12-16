using System;
using System.Linq;
using NeanderTaleS.Code.Scripts.Systems.Inventory.configs;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Grid;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Observers.GridObservers;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.popup
{
    public class InventoryPopup:MonoBehaviour
    {
        [SerializeField] private InventoryBagsCreator _bagsCreator;
        [SerializeField] private Scaler _scaler;
        private readonly GridsStorage _gridsStorage = new ();
        private CompositeDisposable _dispose = new ();

        private void Awake()
        {
            _bagsCreator.OnGridCreated += _gridsStorage.AddItem;
            BootsTrap();
        }

        void OnRectTransformDimensionsChange()
        {
            _scaler.OnRectTransformDimensionsChange();
            _bagsCreator.UpdateInventoryBags();
        }

        private void BootsTrap()
        {
            var rect = GetComponent<RectTransform>();
            var config = Resources.Load<InventoryConfig>("InventoryConfig");
            var scalers = GetComponentsInChildren<Image>().ToList();
            
            _bagsCreator.Initialize(config);
            _scaler.Initialize(scalers, rect);

            CreateGridObservers(_gridsStorage);
            
            _dispose.Add(_gridsStorage);
            _dispose.Add(_scaler);
        }

        private void CreateGridObservers(GridsStorage storage)
        {
            var left_click = new GridObserver_LeftClick(storage);
            _dispose.Add(left_click);
        }

        private void OnDestroy()
        {
            _bagsCreator.OnGridCreated -= _gridsStorage.AddItem;
            _dispose.Dispose();
        }
    }
}