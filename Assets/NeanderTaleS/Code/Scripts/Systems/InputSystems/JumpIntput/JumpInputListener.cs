using System;
using NeanderTaleS.Code.Scripts.Systems.GameCycle;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems.JumpIntput
{
    public sealed class JumpInputListener: IInitializable, ITickable, IStartGameListener, IFinishGameListener, IPauseGameListener, IResumeGameListener
    {
        public event Action OnJump;
        private KeyCode _jumpKey;
        private bool _canJump = false;


        void IInitializable.Initialize()
        {
            _jumpKey = KeyCode.Space;
        }

        void ITickable.Tick()
        {
            if (!_canJump)
            {
                return;
            }

            if (Input.GetKeyDown(_jumpKey))
            {
                OnJump?.Invoke();
            }
        }

        void IStartGameListener.OnStartGame()
        {
            _canJump = true;
        }

        void IFinishGameListener.OnFinishGame()
        {
            _canJump = false;
        }

        void IPauseGameListener.OnPauseGame()
        {
            _canJump = false;
        }

        void IResumeGameListener.OnResumeGame()
        {
            _canJump = true;
        }
    }
}