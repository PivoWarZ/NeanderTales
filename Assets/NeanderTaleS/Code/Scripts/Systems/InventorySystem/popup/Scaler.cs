using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.popup
{
    public class Scaler: MonoBehaviour
    {
        private List<ScaleObject> _objects = new List<ScaleObject>();
        private RectTransform _rectTransform;
        private Vector2 _scale;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _objects = GetComponentsInChildren<ScaleObject>().ToList();
            _scale = _rectTransform.sizeDelta;
            Debug.Log(_scale);
        }

        void OnRectTransformDimensionsChange()
        {
            if (!_rectTransform)
            {
                return;
            }

            Vector2 size = _rectTransform.sizeDelta;
            
            //Vector2 baseSize = new Vector2(100, 100);
            
            var scale = new Vector2(size.x / _scale.x, size.y / _scale.y);
            Debug.Log(scale);
            

            foreach (var scaleObject in _objects)
            {
                scaleObject.SetNewRecttransformSize(scale.x, scale.y);
            }
            
           // _scale = new Vector2(size.x, size.y);
        }
    }
}