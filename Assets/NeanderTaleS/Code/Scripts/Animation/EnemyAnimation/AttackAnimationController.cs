using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.EnemyAnimation
{
    public class AttackAnimationController: MonoBehaviour, IEnemyAnimationController
    {
        public event Action OnBite;
        
        private EnemyAttackComponent _attackComponent;
        private Animator _animator;
        private AnimationEventDispatcher _event;
        public void Init(EnemyProvider enemyProvider, AnimationEventDispatcher eventDispatcher)
        {
            _attackComponent = enemyProvider.AttackComponent;
            _animator = enemyProvider.Animator;
            _event = eventDispatcher;

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
                Debug.Log("AnimationBiteComplete");
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