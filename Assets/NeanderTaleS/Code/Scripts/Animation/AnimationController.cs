using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation
{
    public class AnimationController: MonoBehaviour, IAnimationController, IHitAnimationListener
    {
        public event Action OnHitAnimation;
        [SerializeField] private PlayerProvider _playerProvider;
        [SerializeField] private AnimationEventDispatcher _event;
        [SerializeField] private Transform _inverseTransform;
        private int _comboAttack;
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
                Debug.Log($"Attack Started {_comboAttack}");
            }

            if (eventName == "AttackComplete")
            {
                _comboAttack = 0;
                _animator.SetBool("FirstAttack", false);
                _animator.SetBool("SecondAttack", false);
                _animator.SetBool("ThirdAttack", false);
                _attackComponent.AttackEvent();
                _attackComponent.SetCanAttack(true);
                Debug.Log($"Attack Complete {_comboAttack}");
            }

            if (eventName == "Hit")
            {
                _comboAttack++;
                OnHitAnimation?.Invoke();
                Debug.Log($"Hit {_comboAttack}");
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
            _attackComponent.SetCanAttack(false);
            
            if (_comboAttack == 0)
            {
                _animator.SetBool("FirstAttack", true);
            }
            else if (_comboAttack == 1)
            {
                _animator.SetBool("FirstAttack", false);
                _animator.SetBool("SecondAttack", true);
            }
            else if (_comboAttack == 2)
            {
                _animator.SetBool("FirstAttack", false);
                _animator.SetBool("SecondAttack", false);
                _animator.SetBool("ThirdAttack", true);
            }
        }
    }
}