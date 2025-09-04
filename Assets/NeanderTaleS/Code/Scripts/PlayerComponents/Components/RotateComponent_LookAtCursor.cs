using System;
using NeanderTaleS.Code.Scripts.Condition;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.PlayerComponents.Components
{
    public class RotateComponent_LookAtCursor: MonoBehaviour, IRotatable
    {
        public event Action<bool> OnRotate;
        public event Action OnRotateComplete;
        
        [SerializeField] private Transform _rotateTransform;
        [SerializeField] private float rotateSpeed = 5f;
        [SerializeField] private bool _canRotate = true;
        private bool _isRotate;
        private CompositeCondition _condition;
        private Quaternion _targetRotation;
        private const int OFFSET_ROTATION_ANGLE = 3;
        private const float ROTATE_ANGLE = 10f;
        

        private void Awake()
        {
            _condition = new CompositeCondition();
            _condition.AddCondition(() => _canRotate);
        }

        private void LateUpdate()
        {
            if (!_isRotate)
            {
                return;
            }

            if (_rotateTransform.rotation != _targetRotation)
            {
                var offset = Quaternion.Angle( _rotateTransform.rotation, _targetRotation);

                if (offset <= OFFSET_ROTATION_ANGLE)
                {
                    _rotateTransform.rotation = _targetRotation;
                    _isRotate = false;
                    
                    OnRotateComplete?.Invoke();
                }

                _rotateTransform.rotation = Quaternion.Lerp( _rotateTransform.rotation, _targetRotation, rotateSpeed * Time.deltaTime);
            }
        }

        public void Rotate(Vector3 hitPoint)
        {
            if (!_condition.IsTrue())
            {
                return;
            }
            
            var direction = hitPoint - _rotateTransform.position;
            
            direction.y = 0;
            
            _targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            if (_rotateTransform.rotation == _targetRotation)
            {
                return;
            }

            if (Quaternion.Angle(_rotateTransform.rotation, _targetRotation) < ROTATE_ANGLE)
            {
                return;
            }

            _isRotate = true;
            
            bool isRightRotate = direction.x > 0;
            
            OnRotate?.Invoke(isRightRotate);
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