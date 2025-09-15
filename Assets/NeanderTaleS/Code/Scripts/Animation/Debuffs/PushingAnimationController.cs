using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.EnemiesComponents.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.Debuffs
{
    public class PushingAnimationController: MonoBehaviour, IAnimationController
    {
        private DebuffsComponent _debuff;
        private MechanicsBreaker _breaker;
        private AnimationEventDispatcher _eventDispatcher;
        private Animator _animator;
        private RotateComponent_LookAtCursor _rotateComponent;
        private Rigidbody _rigidbody;
        private IDisposable _dispose;
        private CancellationTokenSource _cancell = new ();
        
        public void Init(LocalProvider localProvider)
        {
            _debuff = localProvider.GetService<DebuffsComponent>();
            _eventDispatcher = localProvider.GetService<AnimationEventDispatcher>();
            _rotateComponent = localProvider.GetService<RotateComponent_LookAtCursor>();
            _rigidbody = localProvider.Rigidbody;
            _animator = localProvider.Animator;
            _breaker = localProvider.MechanicsBreaker;
            
            _dispose = _debuff.Pushing.Where(isPush => isPush).Subscribe(Push);
            _eventDispatcher.OnReceiveEvent += ReceiveEvent;
        }

        private void ReceiveEvent(string eventName)
        {
            if (eventName == "Standing")
            {
                _debuff.Pushing.Value = false;
                _breaker.EnabledCoreMechanics();
            }
        }

        private void Push(bool isPushing)
        {
            Vector3 rotateDirection = -_rigidbody.linearVelocity.normalized;
            rotateDirection.y = 0;
            
            _breaker.BanCoreMechanics();
            _rotateComponent.RotateAsync(rotateDirection, _cancell).Forget();
            
            _animator.SetTrigger("Pushing");
        }

        private void OnDestroy()
        {
            _dispose.Dispose();
            _cancell.Cancel();
            _eventDispatcher.OnReceiveEvent -= ReceiveEvent;
        }
    }
}