using System;
using System.Linq;
using NeanderTaleS.Code.Scripts.Systems.Inventory.configs;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.popup
{
    public class InventoryPopup:MonoBehaviour
    {
        [SerializeField] private InventoryBagsCreator _bagsCreator;
        [SerializeField] private Scaler _scaler;

        private void Awake()
        {
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
        }
    }
}