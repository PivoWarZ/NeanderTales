using System;
using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Animations;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Animation.EnemyAnimation
{
    public class DyingAnimationController: MonoBehaviour, IAnimationController, IDyingAnimation
    {
        public event Action<Vector3> OnDyingAnimationComplete;
        
        private Animator _animator;
        private AnimationEventDispatcher _eventDispatcher;
        private IHitPointsComponent _hitPoints;
        private IDisposable _disposable;
        
        public void Init(LocalProvider localProvider)
        {
            _animator = localProvider.Animator;
            _eventDispatcher = localProvider.GetService<AnimationEventDispatcher>();
            _hitPoints = localProvider.GetInterface<IHitPointsComponent>();


            _eventDispatcher.OnReceiveEvent += ReceiveEvent;
            _disposable = _hitPoints.CurrentHitPoints.Where(hp => hp <= 0).Subscribe(DyingAnimation);
        }

        private void ReceiveEvent(string eventName)
        {
            if (eventName == "Dying")
            {
                OnDyingAnimationComplete?.Invoke(transform.position);
            }
        }

        private void DyingAnimation(float _)
        {
            // Vector3 hitPointPosition = _pointHit.GetLastHitPointPosition;
            // bool isDyingForward = hitPointPosition.z > 0.6f;
            //
            // if (isDyingForward)
            // {
            //     _animator.SetTrigger("DyingForward");
            // }
            // else
            // {
            //     _animator.SetTrigger("DyingBackward");
            // }
            
            _animator.SetTrigger("DyingBackward");

        }

        private void OnDestroy()
        {
            _disposable.Dispose();
            _eventDispatcher.OnReceiveEvent -= ReceiveEvent;
        }
    }
}