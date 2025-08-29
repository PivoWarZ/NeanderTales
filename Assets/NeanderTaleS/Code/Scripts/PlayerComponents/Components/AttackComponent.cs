using System;
using NeanderTaleS.Code.Scripts.Condition;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.PlayerComponents.Components
{
    public class AttackComponent: MonoBehaviour, IAttackable
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

        public void AttackRequest()
        {
            if (!_condition.IsTrue())
            {
                return;
            }

            OnAttackRequest?.Invoke();
        }

        public void AttackAction()
        {
            OnAttackAction?.Invoke();
        }

        public void AttackEvent()
        {
            OnAttackEvent?.Invoke();
        }

        public void SetCanAttack(bool value)
        {
            _canAttack = value;
        }

        private void SetCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        private void RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }
    }
}