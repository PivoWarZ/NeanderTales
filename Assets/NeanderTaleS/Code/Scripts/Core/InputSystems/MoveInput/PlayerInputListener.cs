using System;
using NeanderTaleS.Code.Scripts.Systems.GameCycle;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Core.InputSystems.MoveInput
{
    public class PlayerInputListener: ITickable, IStartGameListener, IFinishGameListener, IPauseGameListener, IResumeGameListener
    {
        public event Action<Vector3> OnDirectionChanged;
        private Vector3 _direction = Vector3.zero;
        private bool _canMove;
        
        public void Tick()
        {
            if (!_canMove)
            {
                return;
            }

            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");

            OnDirectionChanged?.Invoke(_direction);
        }
        
        void IStartGameListener.OnStartGame()
        {
            _canMove = true;
        }

        void IFinishGameListener.OnFinishGame()
        {
            _canMove = false;
        }

        void IPauseGameListener.OnPauseGame()
        {
            _canMove = false;
        }

        void IResumeGameListener.OnResumeGame()
        {
            _canMove = true;
        }
    }
}