using System;
using NeanderTaleS.Code.Scripts.Condition;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.PlayerComponents.Components
{
    public class JumpComponent: MonoBehaviour, IJumping
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _jumpForce;
        [SerializeField] private bool _canJump;
        [SerializeField] private LayerMask _groundLayer;
        private CompositeCondition _condition;

        private void Awake()
        {
            _condition = new CompositeCondition();
            _condition.AddCondition(() => _canJump);
            _condition.AddCondition(IsGrounded);
        }

        public void Jump()
        {
            if (!_condition.IsTrue())
            {
                return;
            }
            
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        public bool IsGrounded()
        {
            return Physics.CheckSphere(transform.position, 0.1f, _groundLayer, QueryTriggerInteraction.Ignore);
        }
    }
}