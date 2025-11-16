using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Core.EnemiesComponents;
using NeanderTaleS.Code.Scripts.Interfaces.Animations;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Animation.EnemyAnimation
{
    public sealed class TargetAnimationController: MonoBehaviour, IAnimationController
    {
        private EnemyTargetComponent _target;
        private EnemyMoveComponent _move;
        private Animator _animator;
        private AnimationEventDispatcher _event;
        
        public void Init(LocalProvider localProvider)
        {
            _target = localProvider.GetService<EnemyTargetComponent>();
            _animator = localProvider.Animator;
            _event = localProvider.GetService<AnimationEventDispatcher>();
            _move = localProvider.GetService<EnemyMoveComponent>();

            _event.OnReceiveEvent += ReceiveEvent;
            _target.OnTargetChanged += TargetChanged;
        }

        private void TargetChanged(GameObject _)
        {
            _animator.SetTrigger("Cry");
        }

        private void ReceiveEvent(string eventName)
        {
            if (eventName == "CryComplete")
            {
                _animator.SetTrigger("Roar");
            }

            if (eventName == "RoarComplete")
            {
                _move.CanMove = true;
            }
        }
    }
}