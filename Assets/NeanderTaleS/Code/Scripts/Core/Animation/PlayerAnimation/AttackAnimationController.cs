using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Core.PlayerComponents.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Animations;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Animation.PlayerAnimation
{
    public class AttackAnimationController: MonoBehaviour, IAnimationController
    {
        private AnimationEventDispatcher _event;
        private AttackComponent _attackComponent;
        private Animator _animator;
        private int _comboAttackCount;
        private bool _isAttack;
        private const int FIRST_ATTACK = 0;
        private const int SECOND_ATTACK = 1;
        private const int THIRD_ATTACK = 2;
        
        public void Init(LocalProvider localProvider)
        {
            _event = localProvider.GetService<AnimationEventDispatcher>();
            _animator = localProvider.Animator;
            _attackComponent = localProvider.GetService<AttackComponent>();
            
            var conditionInstaller = localProvider.GetService<ConditionInstaller>();
            conditionInstaller.AddCondition<IAttackable>(IsAttackOver);
            
            _event.OnReceiveEvent += ProcessEvent;
            _attackComponent.OnAttackAction += Attack;
        }

        private bool IsAttackOver()
        {
            return !_isAttack;
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
                _isAttack = false;
            }

            if (eventName == "Hit")
            {
                _isAttack = false;
                _comboAttackCount++;
            }
        }
        
        public void Attack()
        {
            _isAttack = true;
            
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