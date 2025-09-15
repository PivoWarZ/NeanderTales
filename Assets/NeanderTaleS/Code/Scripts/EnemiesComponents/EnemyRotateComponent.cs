using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Condition;
using NeanderTaleS.Code.Scripts.EnemiesComponents.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyRotateComponent: MonoBehaviour, ITargetInitComponent, ICursorFollower, IBreakable, IConditionComponent
    {
        [SerializeField] Transform _rotateTransform;
        [SerializeField] float _rotateSpeed;
        Transform _target;
        private bool _canRotate = true;
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
            
            SetRotateDirection(direction);
        }

        public void SetTarget(GameObject target)
        {
            _target = target.transform;
        }
        
        public void SetRotateDirection(Vector3 rotateDirection)
        {
            var targetRotation = Quaternion.LookRotation(rotateDirection, Vector3.up);
            
            _rotateTransform.rotation = Quaternion.Lerp(_rotateTransform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }

        public void AddCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }

        void IBreakable.EnabledMechanic()
        {
            _canRotate = true;
        }

        void IBreakable.DisablingMechanic()
        {
            _canRotate = false;
        }
    }
}