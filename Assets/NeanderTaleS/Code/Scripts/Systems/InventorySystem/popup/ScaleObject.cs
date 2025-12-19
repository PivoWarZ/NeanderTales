using System;
using System.Collections.Generic;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.popup
{
    public class ScaleObject: MonoBehaviour
    {
        private Dictionary<RectTransform, Vector2> _scaleRecttransforms = new ();
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            
            foreach (var child in GetComponentsInChildren<RectTransform>())
            {
                if (child != _rectTransform)
                {
                    _scaleRecttransforms[child] = child.rect.size;
                }
            }
        }

        public void SetNewRecttransformSize(float deltaX, float deltaY)
        {
            foreach (var scaleRect in _scaleRecttransforms)
            {
                RectTransform child = scaleRect.Key;
                Vector2 initialSize = scaleRect.Value;

                float newWidth = initialSize.x * deltaX;
                float newHeight = initialSize.y * deltaY;

                child.sizeDelta = new Vector2(newWidth, newHeight);
            }
        }
    }
}