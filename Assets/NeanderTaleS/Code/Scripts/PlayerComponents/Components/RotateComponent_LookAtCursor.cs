using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.ComponentInterfaces;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.ServiceInterfaces;
using NeanderTaleS.Code.Scripts.Condition;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.PlayerComponents.Components
{
    public class RotateComponent_LookAtCursor: MonoBehaviour, ICursorFollower, IBreakable, IConditionComponent, IRotateAsync
    {
        public event Action<bool> OnRotate;
        public event Action OnRotateComplete;
        
        [SerializeField] private Transform _rotateTransform;
        [SerializeField] private float rotateSpeed = 5f;
        [SerializeField] private bool _canRotate = true;
        private CompositeCondition _condition = new ();
        private Quaternion _targetRotation;
        private const int OFFSET_ROTATION_ANGLE = 3;

        public float RotateSpeed => rotateSpeed;
        
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

            if (IsTargetRotation(_targetRotation))
            {
                _rotateTransform.rotation = _targetRotation;
                    
                OnRotateComplete?.Invoke();
                
                return;
            }

            _rotateTransform.rotation = Quaternion.Slerp( _rotateTransform.rotation, _targetRotation, RotateSpeed * Time.deltaTime);
            
        }

        public void SetRotateDirection(Vector3 hitPoint)
        {
            var direction = hitPoint - _rotateTransform.position;
            
            direction.y = 0;
            
            _targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            if (_rotateTransform.rotation == _targetRotation)
            {
                return;
            }

            if (IsTargetRotation(_targetRotation))
            {
                return;
            }
            
            bool isRightRotate = direction.x > 0;
            
            OnRotate?.Invoke(isRightRotate);
        }

        private Quaternion TargetRotation(Vector3 direction)
        {
            return Quaternion.LookRotation(direction, Vector3.up);
        }

        public void Rotate(Vector3 direction)
        {
            Quaternion targetRotation = TargetRotation(direction);
            _rotateTransform.rotation = Quaternion.Slerp(_rotateTransform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        public async UniTask<UniTask> RotateAsync(Vector3 direction, CancellationTokenSource cancell)
        {
            Quaternion targetRotation = TargetRotation(direction);
            bool isRightRotate = direction.x > 0;
            int cycleCount = 0;
            int looping = 50;
            
            OnRotate?.Invoke(isRightRotate);

            while (!IsTargetRotation(targetRotation) && cycleCount < looping && !cancell.IsCancellationRequested)
            {
                await UniTask.WaitForFixedUpdate();
                cycleCount++;

                if (cycleCount == looping)
                {
                    Debug.Log($"<color=yellow>WARNING! Rotate Component: RotateAsync is LOOPING!</color>");
                }
            }
            
            OnRotateComplete?.Invoke();
            
            return UniTask.CompletedTask;
        }

        private bool IsTargetRotation(Quaternion target)
        {
            return Quaternion.Angle( _rotateTransform.rotation, target) <= OFFSET_ROTATION_ANGLE;
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