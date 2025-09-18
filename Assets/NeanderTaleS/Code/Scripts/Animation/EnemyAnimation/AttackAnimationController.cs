using NeanderTaleS.Code.Scripts.Animation.Interfaces.Animations;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.EnemyAnimation
{
    public class AttackAnimationController: MonoBehaviour, IAnimationController
    {
        private EnemyAttackComponent _attackComponent;
        private Animator _animator;
        private AnimationEventDispatcher _event;
        private ITakeDamageble _hitPointsComponent;
        private bool _isBite = false;


        public void Init(LocalProvider localProvider)
        {
            _attackComponent = localProvider.GetService<EnemyAttackComponent>();
            _hitPointsComponent = localProvider.GetInterface<ITakeDamageble>();
            _animator = localProvider.Animator;
            _event = localProvider.GetService<AnimationEventDispatcher>();

            var conDitionInstaller = localProvider.GetService<ConditionInstaller>();
            conDitionInstaller.AddCondition<IRotatable>(IsBiteOver);
            conDitionInstaller.AddCondition<IMovable>(IsBiteOver);

            _attackComponent.OnAttackAction += Attack;
            _event.OnReceiveEvent += ReceiveEvent;
            _hitPointsComponent.OnTakeDamageEvent += Reset;
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

        private void Reset()
        {
            _isBite = false;
            Debug.Log($"IS Bite {_isBite}");
        }
        
        private void OnDestroy()
        {
            _attackComponent.OnAttackAction -= Attack;
            _event.OnReceiveEvent -= ReceiveEvent;
            _hitPointsComponent.OnTakeDamageEvent -= Reset;
        }
    }
}