using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.configs;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.components;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;
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
        private Inventory _inventory;
        private float _minSize;
        private float _minPadding;
        private bool _isInitializing = false;

        public void Initialize(InventoryConfig config, Inventory inventory)
        {
            _config = config;
            _isInitializing = true;
            _inventory = inventory;
            
            UpdateInventoryBags();
        }

        public void UpdateInventoryBags()
        {
            if (!_isInitializing)
            {
                return;
            }
            
            InstallGrids();
            InitializeGrids();
        }

        private void InstallGrids()
        {
            DetermineDimensionsAndIndentsBag();
            CreateInventoryGrid();
            SetGridAnchorsTopLeftPosition();
            SetBagsBackgroundSize();
        }

        public void InitializeGrids()
        {
            DeinitializeGrid();
            InitializeInventoryGrid(_inventory);
            HideSpritesFromNonActiveGrid();
            ActivateStackableCounter();
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
            
            Bag.GridSize = _minSize;
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

        private void InitializeInventoryGrid(Inventory inventory)
        {
            for (int i = 0; i < inventory.Items.Count; i++ )
            {
                if (inventory.Items[i].Id == String.Empty)
                    continue;
                
                _items[i].Initialize(inventory.Items[i]);
            }
        }

        private void ActivateStackableCounter()
        {
            foreach (var gridItem in Bag.Grids)
            {
                if (!gridItem.IsInitialising)
                {
                    continue;
                }

                var item = gridItem.InventoryItem;

                if (item.Id == String.Empty)
                {
                    continue;
                }

                var isStackable = item.TryGetComponent<StackableComponent>(out var stackableComponent);

                if (isStackable)
                {
                    gridItem.InitCountText(stackableComponent.Count);
                }
            }
        }

        private void HideSpritesFromNonActiveGrid()
        {
            foreach (var gridItem in _items)
            {
                if(gridItem.IsInitialising)
                    continue;
                
                gridItem.Icon.enabled = false;
            }
        }

        private void DeinitializeGrid()
        {
            foreach (var gridItem in _items)
            {
                gridItem.Reset();
            }
        }

        public void Dispose()
        {
            _items.Clear();
        }
    }
}