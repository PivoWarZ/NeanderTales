using System;
using System.Threading;
using NeanderTaleS.Code.Scripts.Condition;
using NeanderTaleS.Code.Scripts.EnemiesComponents.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyAttackComponent: MonoBehaviour, IAttackable, IBreakable
    {
        public event Action OnAttackRequest;
        public event Action OnAttackAction;
        public event Action OnAttackEvent;
        

        [SerializeField] private bool _canAttack = false;
        private CompositeCondition _condition = new ();

        private void Awake()
        {
            _condition.AddCondition(() => _canAttack);
        }

        public void Attack()
        {
            OnAttackRequest?.Invoke();

            if (_condition.IsTrue())
            {
                OnAttackAction?.Invoke();
            }
        }

        public bool IsAttackReady()
        {
            return _condition.IsTrue();
        }

        public void AttackEVent()
        {
            OnAttackEvent?.Invoke();
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