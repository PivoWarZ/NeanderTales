using System;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.RotateSystem
{
    public class CursorPositionListener: ITickable
    {
        public event Action<Vector3> OnRotatePoinrChanged;
        
        private Vector3 _mousePosition;
        private Ray _mouseRay;
        private RaycastHit _hit;
        public void Tick()
        {
            _mousePosition = Input.mousePosition;
            _mousePosition.z = Camera.main.nearClipPlane;
            _mouseRay = Camera.main.ScreenPointToRay(_mousePosition);
            
            if (Physics.Raycast(_mouseRay, out RaycastHit hit, Mathf.Infinity, 3 << LayerMask.NameToLayer("Ground")))
            {
                _hit = hit;
            }
            
            OnRotatePoinrChanged?.Invoke(_hit.point);
        }
    }
}