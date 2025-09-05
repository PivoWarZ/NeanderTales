using System;
using NeanderTaleS.Code.Scripts.Condition;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyMoveComponent: MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private bool _canMove;
        ReactiveProperty<bool> _isMoving = new ();
        private Transform _target;
        private CompositeCondition _condition = new();
        
        public ReadOnlyReactiveProperty<bool> IsMoving => _isMoving;

        private void Awake()
        {
            _condition.AddCondition(() => _canMove);
        }

        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            if (!_condition.IsTrue())
            {
                _isMoving.Value = false;
                return;
            }
            
            _isMoving.Value = true;
            var direction = _target.position - transform.position;
            _rb.linearVelocity = direction * _moveSpeed;
        }
        
        private void SetCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        private void RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}