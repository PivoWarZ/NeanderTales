using System;
using NeanderTaleS.Code.Scripts.Core.Condition;
using NeanderTaleS.Code.Scripts.Core.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.PlayerComponents.Components
{
    public class AttackComponent: MonoBehaviour, IAttackable, IConditionComponent
    {
        public event Action OnAttackRequest;
        public event Action OnAttackAction;
        public event Action OnAttackEvent;
        
        [SerializeField] private int _damage = 5;
        [SerializeField] private bool _canAttack = true;
        private CompositeCondition _condition;

        private void Awake()
        {
            _condition = new CompositeCondition();
            _condition.AddCondition(() => _canAttack);
        }

        public int Damage => _damage;

        public void Attack()
        {
            OnAttackRequest?.Invoke();
            
            if (!_condition.IsTrue())
            {
                return;
            }
            
            AttackAction();
        }

        public void AttackAction()
        {
            OnAttackAction?.Invoke();
        }

        public void AttackEvent()
        {
            OnAttackEvent?.Invoke();
        }

        void IConditionComponent.AddCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        void IConditionComponent.RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }

        CompositeCondition IConditionComponent.GetCompositeCondition()
        {
            return _condition;
        }
    }
}