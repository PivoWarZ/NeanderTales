using System;
using NeanderTaleS.Code.Scripts.Condition;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.PlayerComponents.Components
{
    public class RotateComponent_LooAtCursor: MonoBehaviour, IRotatable
    {
        [SerializeField] private Transform _rotateTransform;
        [SerializeField] private float rotateSpeed = 5f;
        [SerializeField] private bool _canRotate = true;
        private CompositeCondition _condition;

        private void Awake()
        {
            _condition = new CompositeCondition();
            _condition.AddCondition(() => _canRotate);
        }

        public void Rotate(Vector3 hitPoint)
        {
            if (!_condition.IsTrue())
            {
                return;
            }

            var direction = hitPoint - _rotateTransform.position;
            direction.y = 0;
            var rotateDirection = Quaternion.LookRotation(direction, Vector3.up);
            _rotateTransform.rotation = Quaternion.Lerp(_rotateTransform.rotation, rotateDirection, rotateSpeed * Time.deltaTime);
        }

        public void AddCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }
    }
}