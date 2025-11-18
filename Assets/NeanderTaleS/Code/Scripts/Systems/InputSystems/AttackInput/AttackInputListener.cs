using System;
using NeanderTaleS.Code.Scripts.Systems.GameCycle;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems.AttackInput
{
    public sealed class AttackInputListener: IGameCycleTick
    {
        public event Action OnAttackInput;
        private const int LEFT_BUTTON = 0;

        public void Tick(float deltatime)
        {
            if (Input.GetMouseButtonDown(LEFT_BUTTON))
            {
                OnAttackInput?.Invoke();
            }
        }
    }
}