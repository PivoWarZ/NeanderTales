using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.ComponentInterfaces;
using NeanderTaleS.Code.Scripts.Condition;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyRotateComponent: MonoBehaviour, ITargetInitComponent, IConditionComponent, IRotatable
    {
        [SerializeField] Transform _rotateTransform;
        [SerializeField] float _rotateSpeed;
        [SerializeField] private bool _canRotate = true;
        Transform _target;
        private CompositeCondition _condition = new ();

        private void Awake()
        {
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
            
            Rotate(direction);
        }

        public void SetTarget(GameObject target)
        {
            _target = target.transform;
        }
        
        public void Rotate(Vector3 rotateDirection)
        {
            var targetRotation = Quaternion.LookRotation(rotateDirection, Vector3.up);
            
            _rotateTransform.rotation = Quaternion.Lerp(_rotateTransform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }

        public void AddCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }
        
        public CompositeCondition GetCompositeCondition()
        {
            return _condition;
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }
    }
}