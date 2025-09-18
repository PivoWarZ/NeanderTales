using NeanderTaleS.Code.Scripts.Animation.Interfaces.Animations;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.EnemyAnimation
{
    public class JumpAnimationController: MonoBehaviour, IAnimationController
    {
        private Animator _animator;
        private JumpComponent _jumpComponent;
        
        public void Init(LocalProvider localProvider)
        {
            _animator = localProvider.Animator;
            _jumpComponent = localProvider.GetService<JumpComponent>();

            _jumpComponent.OnJumpAction += Jump;
        }

        private void Jump()
        {
            _animator.SetTrigger("Jump");
        }
    }
}