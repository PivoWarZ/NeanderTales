using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEditor.MPE;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation
{
    public class AnimationController: MonoBehaviour, IAnimationController
    {
        [SerializeField] private PlayerProvider _playerProvider;
        [SerializeField] private AnimationEventDispatcher _event;
        [SerializeField] private Transform _inverseTransform;
        private Animator _animator;
        private AttackComponent _attackComponent;

        private void Awake()
        {
            _animator = _playerProvider.Animator;
            _attackComponent = _playerProvider.AttackComponent;
            
            _event.OnReceiveEvent += ProcessEvent;

            _attackComponent.OnAttackRequest += Attack;
        }

        private void ProcessEvent(string eventName)
        {
            if (eventName == "AttackStarted")
            {
                _attackComponent.AttackAction();
            }

            if (eventName == "AttackComplete")
            {
                _attackComponent.AttackEvent();
                _attackComponent.SetCanAttack(true);
            }
        }

        public void SetDirectionAxis(Vector3 moveDirection)
        {
            var inverseDirection = _inverseTransform.InverseTransformDirection(moveDirection);
            
            _animator.SetFloat("XAxis", inverseDirection.x);
            _animator.SetFloat("YAxis", inverseDirection.z);
        }

        public void Attack()
        {
            _animator.SetTrigger("Attack");
            _attackComponent.SetCanAttack(false);
        }
    }
}