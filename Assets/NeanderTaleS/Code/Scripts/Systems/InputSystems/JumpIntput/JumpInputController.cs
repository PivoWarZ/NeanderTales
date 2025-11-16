using System;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Systems.InputSystems.Interfaces;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems.JumpIntput
{
    public sealed class JumpInputController: IInitializable, IDisposable, IPlayerInput
    {
        private readonly JumpInputListener _listener;
        private IJumping _jumping;

        public JumpInputController(JumpInputListener listener)
        {
            _listener = listener;
        }

        void IInitializable.Initialize()
        {
            _listener.OnJump += Jump;
        }

        private void Jump()
        {
            _jumping.Jump();
        }

        public void Construct(GameObject player)
        {
            _jumping = player.GetComponent<IJumping>();
            
            if (_jumping == null)
            {
                Debug.Log($"{player.GetType()} : {player.name} <color=red> IJumping NOT FOUND </color>");
            }
        }
        
        void IDisposable.Dispose()
        {
            _listener.OnJump -= Jump;
        }

    }
}