using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.Debuffs
{
    public class PushingAnimationController: MonoBehaviour, IAnimationController
    {
        private DebuffsComponent _debuff;
        private AnimationEventDispatcher _eventDispatcher;
        private EnemyRotateComponent _rotateComponent;
        private Rigidbody _rigidbody;
        private IDisposable _dispose;
        public void Init(LocalProvider localProvider)
        {
            _debuff = localProvider.GetComponent<DebuffsComponent>();
            _eventDispatcher = localProvider.GetComponent<AnimationEventDispatcher>();
            _rigidbody = localProvider.Rigidbody;

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