using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Core.EnemiesComponents;
using NeanderTaleS.Code.Scripts.Interfaces.Animations;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Animation.EnemyAnimation
{
    public class AttackAnimationController: MonoBehaviour, IAnimationController
    {
        private EnemyAttackComponent _attackComponent;
        private Animator _animator;
        private AnimationEventDispatcher _event;
        private ITakeDamageable _hitPointsComponent;
        private bool _isBite = false;


        public void Init(LocalProvider localProvider)
        {
            _attackComponent = localProvider.GetService<EnemyAttackComponent>();
            _hitPointsComponent = localProvider.GetInterface<ITakeDamageable>();
            _animator = localProvider.Animator;
            _event = localProvider.GetService<AnimationEventDispatcher>();

            var conDitionInstaller = localProvider.GetService<ConditionInstaller>();
            conDitionInstaller.AddCondition<IRotatable>(IsBiteOver);
            conDitionInstaller.AddCondition<IMovable>(IsBiteOver);

            _attackComponent.OnAttackAction += Attack;
            _event.OnReceiveEvent += ReceiveEvent;
            _hitPointsComponent.OnTakeDamageEvent += ReloadAttack;
        }
        
        private bool IsBiteOver()
        {
            return !_isBite;
        }

        private void ReceiveEvent(string eventName)
        {
            if (eventName == "BiteStarted")
            {
                _isBite = true;
            }

            if (eventName == "Bite")
            {
            }

            if (eventName == "BiteAnimationComplete")
            {
                _isBite = false;
                _attackComponent.AttackEVent();
            }
        }

        private void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        private void ReloadAttack()
        {
            _isBite = false;
            _attackComponent.AttackEVent();
        }
        
        private void OnDestroy()
        {
            _attackComponent.OnAttackAction -= Attack;
            _event.OnReceiveEvent -= ReceiveEvent;
            _hitPointsComponent.OnTakeDamageEvent -= ReloadAttack;
        }
    }
}