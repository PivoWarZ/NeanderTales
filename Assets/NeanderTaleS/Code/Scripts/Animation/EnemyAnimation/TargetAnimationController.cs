using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.EnemyAnimation
{
    public class TargetAnimationController: MonoBehaviour, IEnemyAnimationController
    {
        private EnemyTargetComponent _target;
        private EnemyMoveComponent _move;
        private Animator _animator;
        private AnimationEventDispatcher _event;
        
        public void Init(EnemyProvider enemyProvider, AnimationEventDispatcher eventDispatcher)
        {
            _target = enemyProvider.TargetComponent;
            _animator = enemyProvider.Animator;
            _event = eventDispatcher;
            _move = enemyProvider.MoveComponent;

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