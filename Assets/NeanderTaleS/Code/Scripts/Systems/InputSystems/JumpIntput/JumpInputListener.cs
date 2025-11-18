using System;
using NeanderTaleS.Code.Scripts.Systems.GameCycle;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems.JumpIntput
{
    public sealed class JumpInputListener: IInitializable, IGameCycleTick
    {
        public event Action OnJump;
        private KeyCode _jumpKey;
        
        void IInitializable.Initialize()
        {
            _jumpKey = KeyCode.Space;
        }

        void IGameCycleTick.Tick(float deltaTime)
        {
            if (Input.GetKeyDown(_jumpKey))
            {
                OnJump?.Invoke();
            }
        }
    }
}