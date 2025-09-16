using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.AnimationInterfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;

namespace NeanderTaleS.Code.Scripts.Skills
{
    public class ComboAttack: IDisposable
    {
        private IHitAnimationListener _hitAnimation;
        private AttackComponent _attackComponent;
        private int _comboCounter = 3;
        private int _comboCounterCurrentValue;

        public void Init(AttackComponent attackComponent, IHitAnimationListener hitAnimationListener)
        {
            _attackComponent = attackComponent;
            _hitAnimation = hitAnimationListener;
            
            _hitAnimation.OnHitAnimation += StartCombo;
            _attackComponent.OnAttackEvent += RestoreCounter;
        }

        private void RestoreCounter()
        {
            _comboCounterCurrentValue = _comboCounter;
        }

        private void StartCombo()
        {
            if (_comboCounterCurrentValue > 0)
            {
                _attackComponent.SetCanAttack(true);
                _comboCounterCurrentValue--;
            }
        }

        public void Dispose()
        {
            _hitAnimation.OnHitAnimation -= StartCombo;
            _attackComponent.OnAttackEvent -= RestoreCounter;
        }
    }
}