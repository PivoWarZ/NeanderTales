using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.Debuffs
{
    public class PushingAnimationController: MonoBehaviour, IEnemyAnimationController
    {
        private DebuffsComponent _debuff;
        private AnimationEventDispatcher _eventDispatcher;
        private EnemyRotateComponent _rotateComponent;
        private Rigidbody _rigidbody;
        private IDisposable _dispose;
        public void Init(EnemyProvider enemyProvider)
        {
            _debuff = enemyProvider.DebuffComponent;
            _eventDispatcher = enemyProvider.AnimationEvent;
            _rigidbody = enemyProvider.Rigidbody;

            _dispose = _debuff.Pushing.Where(isPush => true).Subscribe(Push);
        }

        private void Push(bool isPushing)
        {
            Vector3 rotateDirection = - _rigidbody.linearVelocity.normalized;
            rotateDirection.y = 0;
            
            _rotateComponent.Rotate(rotateDirection);
            
            
        }
    }
}