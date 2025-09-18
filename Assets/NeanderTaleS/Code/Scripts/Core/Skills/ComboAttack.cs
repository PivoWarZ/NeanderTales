namespace NeanderTaleS.Code.Scripts.Core.Skills
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