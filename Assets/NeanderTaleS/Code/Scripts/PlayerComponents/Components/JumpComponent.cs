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
            while (!IsGrounded() && !cancell.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromMilliseconds(10));
                Debug.Log("Cycle");
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
        
        private void SetCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        private void RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }

        private void OnDestroy()
        {
            _cancell.Cancel();
        }
    }
}