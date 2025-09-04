using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.Scripts.Condition;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.PlayerComponents.Components
{
    public class JumpComponent: MonoBehaviour, IJumping
    {
        public event Action OnJumpRequest;
        public event Action OnJumpAction;
        public event Action OnJumpEvent;
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _jumpForce;
        [SerializeField] private bool _canJump;
        [SerializeField] private LayerMask _groundLayer;
        private CancellationTokenSource _cancell;
        private CompositeCondition _condition;
        [ShowInInspector] private bool _isJump;

        private void Awake()
        {
            _condition = new CompositeCondition();
            _condition.AddCondition(() => _canJump);
            _condition.AddCondition(IsGrounded);
            _condition.AddCondition(() => !_isJump);
            _cancell = new CancellationTokenSource();
        }

        public void Jump()
        {
            OnJumpRequest?.Invoke();
            
            if (!_condition.IsTrue())
            {
                return;
            }
            
            OnJumpAction?.Invoke();
        }

        private async UniTaskVoid IsJumping(CancellationTokenSource cancell)
        {
            while (_isJump && !cancell.IsCancellationRequested)
            {
                await UniTask.WaitForFixedUpdate();
                
                bool isGrounded = IsGrounded();
                
                if (isGrounded)
                {
                    _isJump = false;
                }
            }
        }

        public void OnJumpImpulse()
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isJump = true;
            IsJumping(_cancell).Forget();
        }

        public bool IsGrounded()
        {
            return Physics.CheckSphere(transform.position, 0.1f, _groundLayer, QueryTriggerInteraction.Ignore);
        }
        
        private void SetCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        private void RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }

        [Button]
        private void Cancellation()
        {
            _cancell.Cancel();
        }

        private void OnDestroy()
        {
            _cancell.Cancel();
        }
    }
}