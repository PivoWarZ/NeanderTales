using System;
using NeanderTaleS.Code.Scripts.Systems.GameCycle;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems.RotateInput
{
    public sealed class CursorPositionListener: IGameCycleTick
    {
        public event Action<Vector3> OnRotatePointChanged;
        
        private Vector3 _mousePosition;
        private Ray _mouseRay;
        private RaycastHit _hit;
        private Vector3 _currentHitPoint = Vector3.zero;
        
        void IGameCycleTick.Tick(float deltatime)
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

            OnRotatePointChanged?.Invoke(_hit.point);
            _currentHitPoint = hit.point;
        }
    }
}