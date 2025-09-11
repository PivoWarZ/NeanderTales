using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using UnityEngine;
using R3;

namespace NeanderTaleS.Code.Scripts.Animation.EnemyAnimation
{
    public class MoveAnimationController: MonoBehaviour, IEnemyAnimationController
    {
        private EnemyMoveComponent _moveComponent;
        private Animator _animator;
        private AnimationEventDispatcher _event;
        private IDisposable _dispose;
        public void Init(EnemyProvider enemyProvider)
        {
            _moveComponent = enemyProvider.MoveComponent;
            _event = enemyProvider.AnimationEvent;
            _animator = enemyProvider.Animator;

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