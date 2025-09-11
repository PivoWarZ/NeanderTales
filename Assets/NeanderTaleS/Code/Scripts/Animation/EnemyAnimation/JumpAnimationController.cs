using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.EnemyAnimation
{
    public class JumpAnimationController: MonoBehaviour, IEnemyAnimationController
    {
        private Animator _animator;
        private JumpComponent _jumpComponent;
        
        public void Init(EnemyProvider enemyProvider)
        {
            _animator = enemyProvider.Animator;
            _jumpComponent = enemyProvider.JumpComponent;

            _jumpComponent.OnJumpAction += Jump;
        }

        private void Jump()
        {
            _animator.SetTrigger("Jump");
        }
    }
}