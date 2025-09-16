using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.AnimationInterfaces;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.EnemyAnimation
{
    public class AttackAnimationController: MonoBehaviour, IAnimationController
    {
        public event Action OnBite;
        
        private EnemyAttackComponent _attackComponent;
        private Animator _animator;
        private AnimationEventDispatcher _event;
        public void Init(LocalProvider localProvider)
        {
            _attackComponent = localProvider.GetService<EnemyAttackComponent>();
            _animator = localProvider.Animator;
            _event = localProvider.GetService<AnimationEventDispatcher>();

            _attackComponent.OnAttackAction += Attack;
            _event.OnReceiveEvent += ReceiveEvent;
        }

        private void ReceiveEvent(string eventName)
        {
            if (eventName == "Bite")
            {
                OnBite?.Invoke();
            }

            if (eventName == "BiteAnimationComplete")
            {
                _attackComponent.AttackEVent();
            }
        }

        private void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        private void OnDestroy()
        {
            _attackComponent.OnAttackAction -= Attack;
            _event.OnReceiveEvent -= ReceiveEvent;
        }
    }
}