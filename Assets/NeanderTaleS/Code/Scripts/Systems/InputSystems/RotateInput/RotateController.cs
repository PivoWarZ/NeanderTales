using System;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Systems.InputSystems.Interfaces;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems.RotateInput
{
    public sealed class RotateController: IInitializable, IDisposable, IPlayerInput
    {
        private readonly CursorPositionListener _listener;
        private ICursorFollower _cursorFollower;

        public RotateController(CursorPositionListener listener)
        {
            _listener = listener;
        }


        void IInitializable.Initialize()
        {
            _listener.OnRotatePointChanged += Rotate;
        }

        private void Rotate(Vector3 hitPoint)
        {
            if (_cursorFollower is not null)
                _cursorFollower.SetRotateDirection(hitPoint);
        }

        void IDisposable.Dispose()
        {
            _listener.OnRotatePointChanged -= Rotate;
        }

        public void Construct(GameObject player)
        {
            _cursorFollower  = player.GetComponent<ICursorFollower>();

            if (_cursorFollower == null)
            {
                Debug.Log($"{player.GetType()} : {player.name} <color=red> ICursorFollower NOT FOUND </color>");
            }
        }
    }
}