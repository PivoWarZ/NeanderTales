using System;
using NeanderTaleS.Code.Scripts.Systems.Inventory.configs;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.InventoryPopup
{
    public class InventoryPopup: MonoBehaviour
    {
        [SerializeField] private BagsProvider _bags;
        private InventoryConfig _config;

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
            
            var prefabWidth = (bagsWidth - padding*2) / _config.WidthCount;
            var prefabHeight = (bagsHeight - padding*2) / _config.HeightCount;
            
            float minSize = Mathf.Min(prefabWidth, prefabHeight);
            
            var paddingX = (bagsWidth - minSize * _config.WidthCount) / 2;
            var paddingY = (bagsHeight - minSize * _config.HeightCount) / 2;
            
            var minPadding = Mathf.Min(paddingX, paddingY);
            
            var offsetX = minPadding;
            var offsetY = minPadding;
            
            
            for (int i = 0; i < _config.HeightCount; i++)
            {
                offsetX = minPadding;
                
                for (int j = 0; j < _config.WidthCount; j++)
                {
                    var grid = Instantiate(_config.Grid, _bags.transform);
                    RectTransform gridRectTransform = grid.gameObject.GetComponent<RectTransform>();
                    gridRectTransform.sizeDelta = new Vector2(minSize, minSize);
                    gridRectTransform.anchoredPosition = new Vector2(offsetX, - offsetY);
                    
                    offsetX += gridRectTransform.sizeDelta.x;

                    if (j + 1 >= _config.WidthCount)
                    {
                        offsetY += gridRectTransform.sizeDelta.y;
                    }
                }
            }
            
            var bagsRectTransform = _bags.GetComponent<RectTransform>();
            var currentWidth = bagsRectTransform.rect.width;
            var newWidth = minPadding * 2 + minSize * _config.WidthCount;
            var sizeDelta = newWidth - currentWidth;
            var newOffsetMax = bagsRectTransform.offsetMax.x + sizeDelta;
            bagsRectTransform.offsetMax = new Vector2(newOffsetMax, bagsRectTransform.offsetMax.y);
        }
    }
}