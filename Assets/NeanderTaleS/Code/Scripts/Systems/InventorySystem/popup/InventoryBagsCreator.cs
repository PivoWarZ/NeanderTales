using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.configs;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;


namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.popup
{
    [Serializable]
    public class InventoryBagsCreator: IDisposable
    {
        [SerializeField] private GameObject _bags;
        [SerializeField] private Image _bagsBackground;
        
        private List<GridItem> _items = new ();
        private InventoryConfig _config;
        private float _minSize;
        private float _minPadding;
        private bool _isInitializing = false;

        public void Initialize(InventoryConfig config, Scripts.InventoryData.InventoryBase.Inventory inventory)
        {
            _config = config;
            _isInitializing = true;
            
            UpdateInventoryBags(inventory);
        }

        public void UpdateInventoryBags(Scripts.InventoryData.InventoryBase.Inventory inventory)
        {
            if (!_isInitializing)
            {
                return;
            }
            
            DetermineDimensionsAndIndentsBag();
            CreateInventoryGrid();
            SetGridAnchorsTopLeftPosition();
            SetBagsBackgroundSize();
            InitializeInventoryGrid(inventory);
            HideSpritesFromNonActiveGrid();
        }

        private void DetermineDimensionsAndIndentsBag()
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
        
        [Button]
        private void CreateInventoryGrid()
        {
            float offsetY = _minPadding;
            Queue<GridItem> gridsQueue = new Queue<GridItem>(Bag.Grids);
            
            for (int i = 0; i < _config.HeightCount; i++)
            {
                var offsetX = _minPadding;

                for (int j = 0; j < _config.WidthCount; j++)
                {
                    gridsQueue.TryDequeue(out var item);
                    
                    var grid = !item ? Object.Instantiate(_config.Grid, _bagsBackground.transform) : item;
                    
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
                    
                    if(!item)
                        Bag.Grids.Add(grid);
                }
            }
        }

        private void SetGridAnchorsTopLeftPosition()
        {
            foreach (var gridItem in _items)
            {
                var rect = gridItem.GetComponent<RectTransform>();
                SetAnchorsTopLeftPosition(rect);
            }
        }

        private void SetAnchorsTopLeftPosition(RectTransform rect)
        {
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(0, 1);
        }

        private void SetBagsBackgroundSize()
        {
            var rect = _bagsBackground.GetComponent<RectTransform>();
            var padding = _config.BagsContentPadding * 2;
            
            SetAnchorsTopLeftPosition(rect);
            
            rect.sizeDelta = new Vector2(_minSize * _config.WidthCount + padding, _minSize * _config.HeightCount + padding);
        }

        private void InitializeInventoryGrid(Scripts.InventoryData.InventoryBase.Inventory inventory)
        {
            for (int i = 0; i < inventory.Items.Count; i++ )
            {
                _items[i].Initialize(inventory.Items[i]);
            }
        }

        private void HideSpritesFromNonActiveGrid()
        {
            foreach (var gridItem in _items)
            {
                if(gridItem.IsGridInitializing)
                    continue;
                
                gridItem.Icon.enabled = false;
            }
        }

        public void Dispose()
        {
            _items.Clear();
        }
    }
}