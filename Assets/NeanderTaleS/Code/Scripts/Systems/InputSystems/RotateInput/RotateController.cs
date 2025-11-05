using System;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems.RotateInput
{
    public class RotateController: IInitializable, IDisposable, IPlayerInput
    {
        private CursorPositionListener _listener;
        private ICursorFollower _cursorFollower;

        public RotateController(CursorPositionListener listener)
        {
            _listener = listener;
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

        public void Init(GameObject player)
        {
            _cursorFollower  = player.GetComponent<ICursorFollower>();

            if (_cursorFollower == null)
            {
                Debug.Log($"{player.GetType()} : {player.name} <color=red> ICursorFollower NOT FOUND </color>");
            }
        }
    }
}