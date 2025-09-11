using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Condition;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyMoveComponent: MonoBehaviour, ITargetInitComponent
    {
        public bool CanMove;
        
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Rigidbody _rb;
        ReactiveProperty<bool> _isMoving = new ();
        private Transform _target;
        private CompositeCondition _condition = new();
        
        public ReadOnlyReactiveProperty<bool> IsMoving => _isMoving;

        private void Awake()
        {
            _condition.AddCondition(() => CanMove);
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
            _rb.linearVelocity = direction.normalized * _moveSpeed;
        }
        
        public void SetCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }

        public void SetTarget(GameObject target)
        {
            _target = target.transform;
        }
    }
}