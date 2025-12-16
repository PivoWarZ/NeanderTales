using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.popup
{
    [Serializable]
    public class Scaler: IDisposable
    {
        [SerializeField] List<RectTransform> _scaleObjects;
        private Dictionary<RectTransform, Vector2> _scaleSize = new Dictionary<RectTransform, Vector2>();
        private RectTransform _rect;
        private RectTransform _popupRect;
        private Vector2 _size;

        public void Initialize(List<Image> objs, RectTransform inventiryPopupRect)
        {
            _rect = inventiryPopupRect;
            _size = _rect.sizeDelta;
            
            AddScaleObjects(objs);

            foreach (var gridItem in _scaleObjects)
            {
                _scaleSize[gridItem] = gridItem.sizeDelta;
            }
        }

        public void OnRectTransformDimensionsChange()
        {
            if (!_rect || _size == Vector2.zero)
            {
                return;
            }

            var deltaX = _rect.sizeDelta.x / _size.x;
            var deltaY = _rect.sizeDelta.y / _size.y;
            
            foreach (var gridItem in _scaleObjects)
            {
                var scale = _scaleSize[gridItem];
                gridItem.sizeDelta = new Vector2(scale.x * deltaX, scale.y * deltaY);
            }
        }

        public void AddScaleObject(RectTransform obj)
        {
            _scaleObjects.Add(obj);
        }

        public void AddScaleObjects(List<Image> objs)
        {
            foreach (var image in objs)
            {
                var imageRectTransform = image.GetComponent<RectTransform>();
                _scaleObjects.Add(imageRectTransform);
            }
        }

        public void Dispose()
        {
            _scaleObjects.Clear();
        }
    }
}