using System;
using NeanderTaleS.Code.Scripts.Condition;
using NeanderTaleS.Code.Scripts.Services;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyAttackComponent: MonoBehaviour
    {
        public event Action OnAttackRequest;
        public event Action OnAttackAction;
        public event Action OnAttackEvent;
        
        [SerializeField] private float _attackColdown = 2f;
        [SerializeField] private bool _canAttack = false;
        private Timer _attackTimer;
        private CompositeCondition _condition = new ();

        private void Awake()
        {
            _condition.AddCondition(() => _canAttack);
            
            _attackTimer = new Timer();
            _attackTimer.IsLoop = false;
            _attackTimer.Duration = _attackColdown;
            
            _attackTimer.OnEnded += AttackReady;
        }

        private void OnEnable()
        {
            _attackTimer.Start();
        }

        private void AttackReady()
        {
            _canAttack = true;
        }

        public void Attack()
        {
            OnAttackRequest?.Invoke();

            if (_condition.IsTrue())
            {
                OnAttackAction?.Invoke();
            }
        }

        public void AttackEVent()
        {
            OnAttackEvent?.Invoke();
            _canAttack = false;
            _attackTimer.Start();
        }

        public void SetCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }

        private void OnDestroy()
        {
            _attackTimer.OnEnded -= AttackReady;
        }
    }
}