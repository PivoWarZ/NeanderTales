using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.Inventory.configs;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Grid;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.InventoryPopup
{
    public class InventoryPopup: MonoBehaviour
    {
        [SerializeField] private BagsProvider _bags;
        private List<GridItem> _items = new ();
        private InventoryConfig _config;
        private float _minSize;
        private float _minPadding;

        private void Awake()
        {
            CreateNonPaddingInventory();
        }

        private void CreateNonPaddingInventory()
        {
            _config = Resources.Load<InventoryConfig>("InventoryConfig");

            var padding = _config.BagsContentPadding;
            
            var bagsWidth = _bags.GetComponent<RectTransform>().rect.width;
            var bagsHeight = _bags.GetComponent<RectTransform>().rect.height;
            
            Debug.Log($"{bagsWidth}x{bagsHeight}");
            
            var prefabWidth = (bagsWidth - padding*2) / _config.WidthCount;
            var prefabHeight = (bagsHeight - padding*2) / _config.HeightCount;
            
            _minSize = Mathf.Min(prefabWidth, prefabHeight);
            
            var paddingX = (bagsWidth - _minSize * _config.WidthCount) / 2;
            var paddingY = (bagsHeight - _minSize * _config.HeightCount) / 2;
            
            _minPadding = Mathf.Min(paddingX, paddingY);
            
            var offsetX = _minPadding;
            var offsetY = _minPadding;
            
            
           CreateInventoryGrid();
            
          ChangeBagsAnchors();
        }

        private void ChangeBagsAnchors()
        {
            var bagsRectTransform = _bags.GetComponent<RectTransform>();
           // Debug.Log($"CurrentAnchorMax: {bagsRectTransform.anchorMax} / CurrentAnchorMin: {bagsRectTransform.anchorMin}");
            var currentWidth = bagsRectTransform.rect.width;
            var newWidth = _minPadding * 2 + _minSize * _config.WidthCount;
            var currentHeight = bagsRectTransform.rect.height;
            var newHeight = _minPadding * 2 + _minSize * _config.HeightCount;
            var sizeDelta = newWidth - currentWidth;
            var widthDeltaPercent = newWidth / currentWidth;
            var hightDeltaPercent = newHeight / currentHeight;
            //var newOffsetMax = bagsRectTransform.offsetMax.x + sizeDelta;
            //bagsRectTransform.offsetMax = new Vector2(newOffsetMax, bagsRectTransform.offsetMax.y);
            var newAnchorMax = (bagsRectTransform.anchorMax.x * widthDeltaPercent, bagsRectTransform.anchorMax.y);
            var newAnchorMin = (bagsRectTransform.anchorMin.x * widthDeltaPercent, bagsRectTransform.anchorMin.y);
            //bagsRectTransform.anchorMin = new Vector2(bagsRectTransform.anchorMin.x * widthDeltaPercent, bagsRectTransform.anchorMin.y * hightDeltaPercent);
            //bagsRectTransform.anchorMax = new Vector2(bagsRectTransform.anchorMax.x * widthDeltaPercent, bagsRectTransform.anchorMax.y * hightDeltaPercent);
            //Debug.Log($"NewAnchorMax: {bagsRectTransform.anchorMax} / NewAnchorMin: {bagsRectTransform.anchorMin}");
        }

        private void CreateInventoryGrid()
        {
            float offsetY = _minPadding;
            
            for (int i = 0; i < _config.HeightCount; i++)
            {
                var offsetX = _minPadding;

                for (int j = 0; j < _config.WidthCount; j++)
                {
                    var grid = Instantiate(_config.Grid, _bags.transform);
                    RectTransform gridRectTransform = grid.gameObject.GetComponent<RectTransform>();
                    
                    var currentWidth = gridRectTransform.rect.width;
                    var currentHeight = gridRectTransform.rect.height;
                    var startAnchorMax = gridRectTransform.anchorMax;
                    var startAnchorMin = gridRectTransform.anchorMin;
                    
                    var widthDeltaPercent = _minSize / currentWidth;
                    var hightDeltaPercent = _minSize / currentHeight;
                    
                    //gridRectTransform.anchorMin = new Vector2(0, 1);
                    //gridRectTransform.anchorMax = new Vector2(0, 1);
                    gridRectTransform.sizeDelta = new Vector2(_minSize, _minSize);
                    gridRectTransform.anchoredPosition = new Vector2(offsetX, - offsetY);
                    
                   //gridRectTransform.anchorMin = new Vector2(startAnchorMin.x, startAnchorMin.y);
                    //gridRectTransform.anchorMax = new Vector2(startAnchorMax.x, startAnchorMax.y);
                    
                    //gridRectTransform.anchorMin = new Vector2(gridRectTransform.anchorMin.x * widthDeltaPercent, gridRectTransform.anchorMin.y * hightDeltaPercent);
                    //gridRectTransform.anchorMax = new Vector2(gridRectTransform.anchorMax.x * widthDeltaPercent, gridRectTransform.anchorMax.y * hightDeltaPercent);
                    
                    offsetX += gridRectTransform.sizeDelta.x;
                    
                    _items.Add(grid);
                    
                    if (j + 1 >= _config.WidthCount)
                    {
                        offsetY += gridRectTransform.sizeDelta.y;
                    }
                }
            }
        }

        private void Clear()
        {
            foreach (var gridItem in _items)
            {
                Destroy(gridItem.gameObject);
            }
            
            _items.Clear();
        }

        void OnRectTransformDimensionsChange()
        {
            // Clear();
            // CreateNonPaddingInventory();
            // CreateInventoryGrid();
            // ChangeBagsAnchors();
        }
    }
}