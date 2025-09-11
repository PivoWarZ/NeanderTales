using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Condition;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyRotateComponent: MonoBehaviour, ITargetInitComponent
    {
        [SerializeField] Transform _rotateTransform;
        [SerializeField] float _rotateSpeed;
        Transform _target;
        private bool _canRotate = true;
        private CompositeCondition _condition;

        private void Awake()
        {
            _condition = new CompositeCondition();
            _condition.AddCondition(() => _canRotate);
        }

        private void LateUpdate()
        {
            if (!_condition.IsTrue())
            {
                return;
            }

            if (_target == null)
            {
                return;
            }

            var direction = _target.position - _rotateTransform.position;
            
            direction.y = 0;
            
            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            
            _rotateTransform.rotation = Quaternion.Lerp(_rotateTransform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }

        public void SetTarget(GameObject target)
        {
            _target = target.transform;
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