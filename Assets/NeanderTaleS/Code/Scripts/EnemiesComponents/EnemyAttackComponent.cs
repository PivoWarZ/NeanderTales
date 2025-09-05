using System;
using NeanderTaleS.Code.Scripts.Condition;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyAttackComponent: MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private bool _canAttack = true;
        private CompositeCondition _condition = new ();

        private void Awake()
        {
            _condition.AddCondition(() => _canAttack);
        }

        public void DealDamage(GameObject target)
        {
            if (!_condition.IsTrue())
            {
                return;
            }

            IHitPointComponent hitPoint = target.TryGetComponent<IHitPointComponent>(out var hitPointComponent) ? hitPointComponent : null;

            if (hitPoint == null)
            {
                return;
            }
            
            hitPoint.TakeDamage(_damage);
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
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