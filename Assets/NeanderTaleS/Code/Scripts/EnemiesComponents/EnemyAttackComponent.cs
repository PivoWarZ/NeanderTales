using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.ComponentInterfaces;
using NeanderTaleS.Code.Scripts.Condition;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyAttackComponent: MonoBehaviour, IAttackable, IConditionComponent
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

        public void AddCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }
        
        public CompositeCondition GetCompositeCondition()
        {
            return _condition;
        }
    }
}