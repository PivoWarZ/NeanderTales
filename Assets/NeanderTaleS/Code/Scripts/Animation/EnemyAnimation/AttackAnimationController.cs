using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.AnimationInterfaces;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.ComponentInterfaces;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.EnemyAnimation
{
    public class AttackAnimationController: MonoBehaviour, IAnimationController
    {
        private EnemyAttackComponent _attackComponent;
        private Animator _animator;
        private AnimationEventDispatcher _event;
        private bool isBite;
        public void Init(LocalProvider localProvider)
        {
            _attackComponent = localProvider.GetService<EnemyAttackComponent>();
            _animator = localProvider.Animator;
            _event = localProvider.GetService<AnimationEventDispatcher>();
            
            AddCondition<IRotatable>(localProvider, () => !isBite);
            AddCondition<IMovable>(localProvider, () => !isBite);

            _attackComponent.OnAttackAction += Attack;
            _event.OnReceiveEvent += ReceiveEvent;
        }
        
        private void ReceiveEvent(string eventName)
        {
            if (eventName == "BiteStarted")
            {
                isBite = true;
            }

            if (eventName == "Bite")
            {
            }

            if (eventName == "BiteAnimationComplete")
            {
                isBite = false;
                _attackComponent.AttackEVent();
            }
        }

        private void Attack()
        {
            _animator.SetTrigger("Attack");
        }
        
        private void AddCondition<T>(LocalProvider localProvider, Func<bool> condition) where T : class
        {
            var conditionComponent = localProvider.TryGetInterface<IRotatable>(out var rotatable);

            if (conditionComponent)
            {
                if (rotatable is IConditionComponent component)
                {
                    component.AddCondition(condition);
                }
            }
        }


        private void OnDestroy()
        {
            _attackComponent.OnAttackAction -= Attack;
            _event.OnReceiveEvent -= ReceiveEvent;
        }
    }
}