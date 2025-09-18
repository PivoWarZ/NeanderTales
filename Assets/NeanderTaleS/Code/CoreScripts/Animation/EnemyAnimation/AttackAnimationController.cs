using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Animations;
using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Components;
using NeanderTaleS.Code.CoreScripts.Components;
using NeanderTaleS.Code.CoreScripts.EnemiesComponents;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.Animation.EnemyAnimation
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
            Debug.Log($"IS Bite {_isBite}");
        }
        
        private void OnDestroy()
        {
            _attackComponent.OnAttackAction -= Attack;
            _event.OnReceiveEvent -= ReceiveEvent;
            _hitPointsComponent.OnTakeDamageEvent -= ReloadAttack;
        }
    }
}