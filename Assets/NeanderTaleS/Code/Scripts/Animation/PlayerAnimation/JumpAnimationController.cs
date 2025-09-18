using NeanderTaleS.Code.Scripts.Animation.Interfaces.Animations;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.PlayerAnimation
{
    public class JumpAnimationController: MonoBehaviour, IAnimationController
    {
        private JumpComponent _jumpComponent;
        private Animator _animator;
        private AnimationEventDispatcher _event;
        private bool _isLanding;
        public void Init(LocalProvider localProvider)
        {
            _jumpComponent = localProvider.GetService<JumpComponent>();
            _animator = localProvider.Animator;
            _event = localProvider.GetService<AnimationEventDispatcher>();
            
            var conditionInstaller = localProvider.GetService<ConditionInstaller>();
            conditionInstaller.AddCondition<IMovable>(JumpOver);

            _jumpComponent.OnJumpAction += JumpAnimation;
            _event.OnReceiveEvent += ReceiveEvent;
        }

        private bool JumpOver()
        {
            return !_isLanding;
        }

        private void ReceiveEvent(string eventName)
        {
            if (eventName == "Jump")
            {
                _jumpComponent.Jump();
            }

            if (eventName == "Landing")
            {
                _isLanding = true;
            }

            if (eventName == "JumpComplete")
            {
                _animator.SetBool("IsJump", false);
                _isLanding = false;
            }
        }

        private void JumpAnimation()
        {
            _animator.SetBool("IsJump", true);
        }
    }
}