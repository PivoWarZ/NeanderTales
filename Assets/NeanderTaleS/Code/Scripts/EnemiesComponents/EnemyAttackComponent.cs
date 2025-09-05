using System;
using NeanderTaleS.Code.Scripts.Condition;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyAttackComponent: MonoBehaviour
    {
        public event Action AttackRequest;
        public event Action AttackAction;
        public event Action AttackEvent;
        
        [SerializeField] private float _attackColdown = 1f;
        [SerializeField] private bool _canAttack = true;
        private CompositeCondition _condition = new ();

        private void Awake()
        {
            _condition.AddCondition(() => _canAttack);
        }

        public void Attack()
        {
            AttackRequest?.Invoke();

            if (_condition.IsTrue())
            {
                AttackAction?.Invoke();
            }
        }

        public void SetCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }
    }
}