using System;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.CoreScripts.InputSystems.RotateInput
{
    public class CursorPositionListener: ITickable
    {
        public event Action<Vector3> OnRotatePoinrChanged;
        
        private Vector3 _mousePosition;
        private Ray _mouseRay;
        private RaycastHit _hit;
        private Vector3 _currentHitPoint = Vector3.zero;
        public void Tick()
        {
            _mousePosition = Input.mousePosition;
            _mousePosition.z = Camera.main.nearClipPlane;
            _mouseRay = Camera.main.ScreenPointToRay(_mousePosition);
            
            if (Physics.Raycast(_mouseRay, out RaycastHit hit, Mathf.Infinity, 3 << LayerMask.NameToLayer("Ground")))
            {
                _hit = hit;
            }

            if (hit.point == _currentHitPoint)
            {
                return;
            }

            OnRotatePoinrChanged?.Invoke(_hit.point);
            _currentHitPoint = hit.point;
        }
    }
}