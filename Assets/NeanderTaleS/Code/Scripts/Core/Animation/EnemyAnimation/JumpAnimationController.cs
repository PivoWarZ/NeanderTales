using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Core.PlayerComponents.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Animations;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Animation.EnemyAnimation
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