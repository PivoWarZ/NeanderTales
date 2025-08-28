using System;
using NeanderTaleS.Code.Scripts.Condition;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.PlayerComponents.Components
{
    public class MoveComponent: MonoBehaviour, IMovable
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private bool _canMove = true;
        CompositeCondition _condition;

        private void Awake()
        {
            _condition = new CompositeCondition();
            _condition.AddCondition(() => _canMove);
        }

        public void Move(Vector3 direction)
        {
            if (!_condition.IsTrue())
            {
                StopRotation(direction);
                return;
            }

            _rigidbody.linearVelocity = direction * _speed;
        }

        private void StopRotation(Vector3 direction)
        {
            if (direction == Vector3.zero || !_condition.IsTrue())
            {
                _rigidbody.linearVelocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
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