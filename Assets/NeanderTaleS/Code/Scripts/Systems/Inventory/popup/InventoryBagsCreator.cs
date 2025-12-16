using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.Inventory.configs;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Grid;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.popup
{
    [Serializable]
    public class InventoryBagsCreator
    {
        public event Action<GridItem> OnGridCreated;
        
        [SerializeField] private BagsProvider _bags;
        
        private List<GridItem> _items = new ();
        private InventoryConfig _config;
        private float _minSize;
        private float _minPadding;
        private bool _isInitializing = false;

        public void Initialize(InventoryConfig config)
        {
            _config = config;
            _isInitializing = true;
            
            UpdateInventoryBags();
        }

        public void UpdateInventoryBags()
        {
            if (!_isInitializing)
            {
                return;
            }
            
            Clear();
            
            CreateInventoryBags();
            CreateInventoryGrid();
            SetAnchorsTopLeftPosition();
            SetBagsBackgroundSize();
        }

        private void CreateInventoryBags()
        {
            var padding = _config.BagsContentPadding;
            
            var bagsWidth = _bags.GetComponent<RectTransform>().rect.width;
            var bagsHeight = _bags.GetComponent<RectTransform>().rect.height;
            
            var prefabWidth = (bagsWidth - padding*2) / _config.WidthCount;
            var prefabHeight = (bagsHeight - padding*2) / _config.HeightCount;
            
            _minSize = Mathf.Min(prefabWidth, prefabHeight);
            
            var paddingX = (bagsWidth - _minSize * _config.WidthCount) / 2;
            var paddingY = (bagsHeight - _minSize * _config.HeightCount) / 2;
            
            _minPadding = Mathf.Min(paddingX, paddingY);
        }

        private void CreateInventoryGrid()
        {
            float offsetY = _minPadding;
            
            for (int i = 0; i < _config.HeightCount; i++)
            {
                var offsetX = _minPadding;

                for (int j = 0; j < _config.WidthCount; j++)
                {
                    var grid = Object.Instantiate(_config.Grid, _bags.transform);
                    RectTransform gridRectTransform = grid.gameObject.GetComponent<RectTransform>();

                    gridRectTransform.sizeDelta = new Vector2(_minSize, _minSize);
                    gridRectTransform.anchoredPosition = new Vector2(offsetX, - offsetY);
                  
                    offsetX += gridRectTransform.sizeDelta.x;
                    
                    _items.Add(grid);
                    
                    bool isEndOfHorizontalRow = j + 1 >= _config.WidthCount;
                    
                    if (isEndOfHorizontalRow)
                    {
                        offsetY += gridRectTransform.sizeDelta.y;
                    }
                    
                    OnGridCreated?.Invoke(grid);
                }
            }
        }
        
        [Button]
        private void SetAnchorsTopLeftPosition()
        {
            foreach (var gridItem in _items)
            {
                var rect = gridItem.GetComponent<RectTransform>();
                rect.anchorMin = new Vector2(0, 1);
                rect.anchorMax = new Vector2(0, 1);
            }
        }

        private void SetBagsBackgroundSize()
        {
            var image = _bags.Background;
            var rect = image.GetComponent<RectTransform>();
            var padding = _config.BagsContentPadding * 2;
            
            rect.sizeDelta = new Vector2(_minSize * _config.WidthCount + padding, _minSize * _config.HeightCount + padding);
        }

        private void Clear()
        {
            foreach (var gridItem in _items)
            {
                Object.Destroy(gridItem.gameObject);
            }
            
            _items.Clear();
        }

        private void OnDestroy()
        {
            Clear();
        }
    }
}