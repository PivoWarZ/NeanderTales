using System;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems.JumpIntput
{
    public class JumpInputController: IInitializable, IDisposable, IPlayerInput
    {
        private JumpInputListener _listener;
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

        public void Init(GameObject player)
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