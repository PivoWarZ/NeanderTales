using System;
using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Animations;
using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Components;
using NeanderTaleS.Code.CoreScripts.Components;
using NeanderTaleS.Code.CoreScripts.PlayerComponents.Components;

namespace NeanderTaleS.Code.CoreScripts.Skills
{
    /*public class ComboAttack: IDisposable
    {
        private IHitAnimationListener _hitAnimation;
        private AttackComponent _attackComponent;
        private int _comboCounter = 3;
        private int _comboCounterCurrentValue;

        public void Init(AttackComponent attackComponent, ConditionInstaller conditionInstaller)
        {
            _comboCounterCurrentValue = _comboCounter;
            _attackComponent = attackComponent;
            
            conditionInstaller.AddCondition<IAttackable>(CanComboAttack);
            
            _attackComponent.OnAttackAction += StartCombo;
            _attackComponent.OnAttackEvent += RestoreCounter;
        }

        private bool CanComboAttack()
        {
            return _comboCounterCurrentValue > 0;
        }

        private void RestoreCounter()
        {
            _comboCounterCurrentValue = _comboCounter;
        }

        private void StartCombo()
        {
            _comboCounterCurrentValue--;

        }

        public void Dispose()
        {
            _hitAnimation.OnHitAnimation -= StartCombo;
            _attackComponent.OnAttackEvent -= RestoreCounter;
        }
    }*/
}