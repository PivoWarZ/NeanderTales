using System;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.InputSystems.RotateInput
{
    public class RotateController: IInitializable, IDisposable
    {
        private CursorPositionListener _listener;
        private ICursorFollower _cursorFollower;

        public RotateController(CursorPositionListener listener, ICursorFollower cursorFollower)
        {
            _listener = listener;
            _cursorFollower = cursorFollower;
        }


        void IInitializable.Initialize()
        {
            _listener.OnRotatePoinrChanged += Rotate;
        }

        private void Rotate(Vector3 hitPoint)
        {
            _cursorFollower.SetRotateDirection(hitPoint);
        }

        void IDisposable.Dispose()
        {
            _listener.OnRotatePoinrChanged -= Rotate;
        }
    }
}