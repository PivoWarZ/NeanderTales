using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents;
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
        public void Init(PlayerProvider playerProvider, AnimationEventDispatcher eventDispatcher)
        {
            _jumpComponent = playerProvider.JumpComponent;
            _animator = playerProvider.Animator;
            _event = eventDispatcher;
            
            playerProvider.MoveComponent.SetCondition(() => !_isLanding);

            _jumpComponent.OnJumpAction += JumpAnimation;
            _event.OnReceiveEvent += ReceiveEvent;
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