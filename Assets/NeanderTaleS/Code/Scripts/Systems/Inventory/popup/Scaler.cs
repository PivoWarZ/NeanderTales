using System.Collections.Generic;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.popup
{
    public class Scaler: MonoBehaviour
    {
        [SerializeField] RectTransform[] _scaleObjects;
        private Dictionary<RectTransform, Vector2> _scaleSize = new Dictionary<RectTransform, Vector2>();
        private RectTransform _rect;
        private RectTransform _popupRect;
        private Vector2 _size;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _size = _rect.sizeDelta;

            foreach (var gridItem in _scaleObjects)
            {
                _scaleSize[gridItem] = gridItem.sizeDelta;
            }
        }

        void OnRectTransformDimensionsChange()
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
    }
}