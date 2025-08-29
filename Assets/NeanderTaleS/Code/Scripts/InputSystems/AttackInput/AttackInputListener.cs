using System;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.InputSystems.AttackInput
{
    public class AttackInputListener: ITickable
    {
        public event Action OnAttackInput;
        private const int LEFT_BUTTON = 0;

        public void Tick()
        {
            if (Input.GetMouseButtonDown(LEFT_BUTTON))
            {
                OnAttackInput?.Invoke();
            }
        }
    }
}