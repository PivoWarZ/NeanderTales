using System;
using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Animations;
using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Components;
using NeanderTaleS.Code.CoreScripts.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.Animation.EnemyAnimation
{
    public class DyingAnimationController: MonoBehaviour, IAnimationController, IDyingAnimation
    {
        public event Action<Vector3> OnDyingAnimationComplete;
        
        private Animator _animator;
        private AnimationEventDispatcher _eventDispatcher;
        private ITakeDamageble _damageble;
        private PointHitDamageListener _pointHit;
        private IDisposable _disposable;
        
        public void Init(LocalProvider localProvider)
        {
            _animator = localProvider.Animator;
            _eventDispatcher = localProvider.GetService<AnimationEventDispatcher>();
            _damageble = localProvider.GetInterface<ITakeDamageble>();
            _pointHit = localProvider.GetService<PointHitDamageListener>();

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
            Vector3 hitPointPosition = _pointHit.GetLastHitPointPosition;
            bool isDyingForward = hitPointPosition.z > 0.6f;

            if (isDyingForward)
            {
                _animator.SetTrigger("DyingForward");
            }
            else
            {
                _animator.SetTrigger("DyingBackward");
            }

        }

        private void OnDestroy()
        {
            _disposable.Dispose();
            _eventDispatcher.OnReceiveEvent -= ReceiveEvent;
        }
    }
}