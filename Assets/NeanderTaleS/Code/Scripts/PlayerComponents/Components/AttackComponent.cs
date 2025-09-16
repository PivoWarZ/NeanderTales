using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.ComponentInterfaces;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.ServiceInterfaces;
using NeanderTaleS.Code.Scripts.Condition;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.PlayerComponents.Components
{
    public class AttackComponent: MonoBehaviour, IAttackable, IBreakable
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

        public void SetCanAttack(bool value)
        {
            _canAttack = value;
        }

        public void SetCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }

        void IBreakable.EnabledMechanic()
        {
            _canAttack = true;
        }

        void IBreakable.DisablingMechanic()
        {
            _canAttack = false;
        }
    }
}