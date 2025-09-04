using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.PlayerAnimation
{
    public class AttackAnimationController: MonoBehaviour, IHitAnimationListener, IAnimationController
    {
        public event Action OnHitAnimation;
        
        private PlayerProvider _playerProvider;
        private AnimationEventDispatcher _event;
        private AttackComponent _attackComponent;
        private Animator _animator;
        private int _comboAttackCount;
        private const int FIRST_ATTACK = 0;
        private const int SECOND_ATTACK = 1;
        private const int THIRD_ATTACK = 2;
        
        public void Init(PlayerProvider playerProvider, AnimationEventDispatcher eventDispatcher)
        {
            _playerProvider = playerProvider;
            _event = eventDispatcher;
            _animator = _playerProvider.Animator;
            _attackComponent = _playerProvider.AttackComponent;
            
            _event.OnReceiveEvent += ProcessEvent;
            _attackComponent.OnAttackAction += Attack;
        }
        
        private void ProcessEvent(string eventName)
        {
            if (eventName == "AttackStarted")
            {
                
            }

            if (eventName == "AttackComplete")
            {
                _comboAttackCount = FIRST_ATTACK;
                _animator.SetBool("FirstAttack", false);
                _animator.SetBool("SecondAttack", false);
                _animator.SetBool("ThirdAttack", false);
                _attackComponent.AttackEvent();
                _attackComponent.SetCanAttack(true);
            }

            if (eventName == "Hit")
            {
                _comboAttackCount++;
                OnHitAnimation?.Invoke();
            }
        }
        
        public void Attack()
        {
            _attackComponent.SetCanAttack(false);
            
            if (_comboAttackCount == FIRST_ATTACK)
            {
                _animator.SetBool("FirstAttack", true);
            }
            else if (_comboAttackCount == SECOND_ATTACK)
            {
                _animator.SetBool("FirstAttack", false);
                _animator.SetBool("SecondAttack", true);
            }
            else if (_comboAttackCount == THIRD_ATTACK)
            {
                _animator.SetBool("FirstAttack", false);
                _animator.SetBool("SecondAttack", false);
                _animator.SetBool("ThirdAttack", true);
            }
        }
    }
}