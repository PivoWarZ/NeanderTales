using System;
using NeanderTaleS.Code.Scripts.Systems.GameCycle;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems.AttackInput
{
    public class AttackInputListener: ITickable, IStartGameListener, IFinishGameListener, IPauseGameListener, IResumeGameListener
    {
        public event Action OnAttackInput;
        private const int LEFT_BUTTON = 0;
        private bool _canAttack = false;

        public void Tick()
        {
            if (!_canAttack)
            {
                return;
            }

            if (Input.GetMouseButtonDown(LEFT_BUTTON))
            {
                OnAttackInput?.Invoke();
            }
        }

        void IStartGameListener.OnStartGame()
        {
            _canAttack = true;
        }

        void IFinishGameListener.OnFinishGame()
        {
            _canAttack = false;
        }

        void IPauseGameListener.OnPauseGame()
        {
            _canAttack = false;
        }

        void IResumeGameListener.OnResumeGame()
        {
            _canAttack = true;
        }
    }
}