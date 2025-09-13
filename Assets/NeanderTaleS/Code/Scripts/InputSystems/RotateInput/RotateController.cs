using System;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.InputSystems.RotateInput
{
    public class RotateController: IInitializable, IDisposable
    {
        private CursorPositionListener _listener;
        private IRotatable _rotatable;

        public RotateController(CursorPositionListener listener, IRotatable rotatable)
        {
            _listener = listener;
            _rotatable = rotatable;
        }


        void IInitializable.Initialize()
        {
            _listener.OnRotatePoinrChanged += Rotate;
        }

        private void Rotate(Vector3 hitPoint)
        {
            _rotatable.SetRotateDirection(hitPoint);
        }

        void IDisposable.Dispose()
        {
            _listener.OnRotatePoinrChanged -= Rotate;
        }
    }
}