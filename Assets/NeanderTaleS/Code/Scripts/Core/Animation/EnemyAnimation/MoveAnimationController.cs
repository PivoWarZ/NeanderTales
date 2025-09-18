using System;
using NeanderTaleS.Code.Scripts.Core.Animation.Interfaces.Animations;
using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Core.EnemiesComponents;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Animation.EnemyAnimation
{
    public class MoveAnimationController: MonoBehaviour, IAnimationController
    {
        private EnemyMoveComponent _moveComponent;
        private Animator _animator;
        private AnimationEventDispatcher _event;
        private IDisposable _dispose;
        public void Init(LocalProvider localProvider)
        {
            _moveComponent = localProvider.GetService<EnemyMoveComponent>();
            _event = localProvider.GetService<AnimationEventDispatcher>();
            _animator = localProvider.Animator;

            _dispose = _moveComponent.IsMoving.Subscribe(MoveAnimation);
        }

        private void MoveAnimation(bool isMoving)
        {
            _animator.SetBool("IsMoving", isMoving);
        }

        private void OnDestroy()
        {
            _dispose.Dispose();
        }
    }
}