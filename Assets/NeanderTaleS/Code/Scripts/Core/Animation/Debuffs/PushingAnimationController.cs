using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.Scripts.Core.Animation.Interfaces.Animations;
using NeanderTaleS.Code.Scripts.Core.Animation.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Core.PlayerComponents.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Animation.Debuffs
{
    public class PushingAnimationController: MonoBehaviour, IAnimationController
    {
        private DebuffsComponent _debuff;
        private AnimationEventDispatcher _eventDispatcher;
        private Animator _animator;
        private RotateComponent_LookAtCursor _rotateComponent;
        private Rigidbody _rigidbody;
        private bool _isPushing;
        private IDisposable _dispose;
        private CancellationTokenSource _cancell = new ();
        
        public void Init(LocalProvider localProvider)
        {
            _debuff = localProvider.GetService<DebuffsComponent>();
            _eventDispatcher = localProvider.GetService<AnimationEventDispatcher>();
            _rotateComponent = localProvider.GetService<RotateComponent_LookAtCursor>();
            _rigidbody = localProvider.Rigidbody;
            _animator = localProvider.Animator;
            
            var conDitionInstaller = localProvider.GetService<ConditionInstaller>();
            conDitionInstaller.AddCondition<IRotatable>(IsPushingOver);
            conDitionInstaller.AddCondition<IMovable>(IsPushingOver);
            conDitionInstaller.AddCondition<IAttackable>(IsPushingOver);
            
            _dispose = _debuff.Pushing.Where(isPush => isPush).Subscribe(Push);
            _eventDispatcher.OnReceiveEvent += ReceiveEvent;
        }

        private bool IsPushingOver()
        {
            return !_isPushing;
        }

        private void ReceiveEvent(string eventName)
        {
            if (eventName == "Standing")
            {
                _debuff.Pushing.Value = false;
                _isPushing = false;
                
                _animator.SetTrigger("Push");
            }
        }

        private void Push(bool isPushing)
        {
            Vector3 rotateDirection = -_rigidbody.linearVelocity.normalized;
            rotateDirection.y = 0;
            
            _isPushing = true;
            _rotateComponent.RotateAsync(rotateDirection, _cancell).Forget();
            
            _animator.SetBool("Push", _isPushing);
        }

        private void OnDestroy()
        {
            _dispose.Dispose();
            _cancell.Cancel();
            _eventDispatcher.OnReceiveEvent -= ReceiveEvent;
        }
    }
}