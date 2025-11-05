using System;
using NeanderTaleS.Code.Scripts.Systems.GameCycle;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems.RotateInput
{
    public class CursorPositionListener: ITickable, IStartGameListener, IFinishGameListener, IPauseGameListener, IResumeGameListener
    {
        public event Action<Vector3> OnRotatePoinrChanged;
        
        private Vector3 _mousePosition;
        private Ray _mouseRay;
        private RaycastHit _hit;
        private Vector3 _currentHitPoint = Vector3.zero;
        private bool _canRotate = false;
        public void Tick()
        {
            if (!_canRotate)
            {
                return;
            }

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
        
        void IStartGameListener.OnStartGame()
        {
            _canRotate = true;
        }

        void IFinishGameListener.OnFinishGame()
        {
            _canRotate = false;
        }

        void IPauseGameListener.OnPauseGame()
        {
            _canRotate = false;
        }

        void IResumeGameListener.OnResumeGame()
        {
            _canRotate = true;
        }
    }
}