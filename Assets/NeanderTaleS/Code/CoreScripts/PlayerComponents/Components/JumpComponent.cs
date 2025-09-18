using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Components;
using NeanderTaleS.Code.CoreScripts.Condition;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.PlayerComponents.Components
{
    public class JumpComponent: MonoBehaviour, IJumping, IConditionComponent
    {
        public event Action OnJumpRequest;
        public event Action OnJumpAction;
        public event Action OnJumpEvent;

        [SerializeField] private Vector3 _jumpVector;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _jumpForce;
        [SerializeField] private bool _canJump;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private bool _isJump;
        private CancellationTokenSource _cancell;
        private CompositeCondition _condition;

        private void Awake()
        {
            _condition = new CompositeCondition();
            _condition.AddCondition(() => _canJump);
            _condition.AddCondition(IsGrounded);
            _condition.AddCondition(() => !_isJump);
            _cancell = new CancellationTokenSource();
        }
        
        [Button]
        public void Jump()
        {
            OnJumpRequest?.Invoke();
            
            if (!_condition.IsTrue())
            {
                return;
            }
            
            OnJumpAction?.Invoke();
            OnJumpImpulse();
        }

        private async UniTaskVoid IsJumping(CancellationTokenSource cancell)
        {
            while (IsGrounded() && !cancell.IsCancellationRequested)
            {
                await UniTask.WaitForFixedUpdate();
            }

            while (!IsGrounded() && !_cancell.IsCancellationRequested)
            {
                await UniTask.WaitForFixedUpdate();
            }
            
            OnJumpEvent?.Invoke();
            _isJump = false;
        }
        
        private void OnJumpImpulse()
        {
            _isJump = true;
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            var forceVector = transform.TransformDirection(_jumpVector);
            _rigidbody.AddForce(forceVector * _jumpForce, ForceMode.Impulse);
            IsJumping(_cancell).Forget();
        }

        public bool IsGrounded()
        {
            return Physics.CheckSphere(transform.position, 0.1f, _groundLayer, QueryTriggerInteraction.Ignore);
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

        private void OnDestroy()
        {
            _cancell.Cancel();
        }
    }
}