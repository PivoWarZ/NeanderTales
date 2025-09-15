using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.EnemiesComponents.Interfaces;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.EnemyAnimation
{
    public class DyingAnimationController: MonoBehaviour, IAnimationController, IDyingAnimation
    {
        public event Action<Vector3> OnDyingAnimationComplete;
        
        private Animator _animator;
        private IMechanicsBreaker _breaker;
        private AnimationEventDispatcher _eventDispatcher;
        private ITakeDamageble _damageble;
        private IDisposable _disposable;
        public void Init(LocalProvider localProvider)
        {
            _animator = localProvider.Animator;
            _breaker = localProvider.GetInterface<IMechanicsBreaker>();
            _eventDispatcher = localProvider.GetService<AnimationEventDispatcher>();
            _damageble = localProvider.GetInterface<ITakeDamageble>();

            _eventDispatcher.OnReceiveEvent += ReceiveEvent;
            _disposable = _damageble.HitPoints.Where(hp => hp <= 0).Subscribe(DyingAnimation);
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
            _animator.SetTrigger("Dying");
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
            _eventDispatcher.OnReceiveEvent -= ReceiveEvent;
        }
    }
}