using System;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.InputSystems.JumpIntput
{
    public class JumpInputListener: IInitializable, ITickable
    {
        public event Action OnJump;
        private KeyCode _jumpKey;


        void IInitializable.Initialize()
        {
            _jumpKey = KeyCode.Space;
        }

        void ITickable.Tick()
        {
            if (Input.GetKeyDown(_jumpKey))
            {
                OnJump?.Invoke();
            }
        }
    }
}