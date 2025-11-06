using System;
using NeanderTaleS.Code.Scripts.Core.Condition;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.PlayerComponents.Components
{
    public class MoveComponent: MonoBehaviour, IMovable, IConditionComponent
    {
        private readonly ReactiveProperty<Vector3> _moveDirection = new (Vector3.zero);
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private bool _canMove = true;
        readonly CompositeCondition _condition = new ();
        public ReadOnlyReactiveProperty<Vector3> MoveDirection => _moveDirection;

        private void Awake()
        {
            _condition.AddCondition(() => _canMove);
        }

        void IMovable.Move(Vector3 direction)
        {
            _moveDirection.Value = direction;
            
            if (!_condition.IsTrue())
            {
                return;
            }

            Vector3 velocity = _rigidbody.linearVelocity;
            velocity.x = direction.x * _speed;
            velocity.z = direction.z * _speed;
            _rigidbody.linearVelocity = velocity;
        }

        void IMovable.SetSpeed(float speed)
        {
            _speed = speed;
        }

        private void StopRotation(Vector3 direction)
        {
            if (direction == Vector3.zero || !_condition.IsTrue())
            {
                _rigidbody.linearVelocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
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