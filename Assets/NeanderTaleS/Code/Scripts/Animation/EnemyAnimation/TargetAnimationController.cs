using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.EnemyAnimation
{
    public class TargetAnimationController: MonoBehaviour, IEnemyAnimationController
    {
        private EnemyTargetComponent _target;
        private Animator _animator;
        private AnimationEventDispatcher _event;
        public void Init(EnemyProvider enemyProvider, AnimationEventDispatcher eventDispatcher)
        {
            _target = enemyProvider.TargetComponent;
            _animator = enemyProvider.Animator;
            _event = eventDispatcher;

            _event.OnReceiveEvent += ReceiveEvent;
            _target.OnTargetChanged += TargetChanged;
        }

        private void TargetChanged(GameObject obj)
        {
            throw new System.NotImplementedException();
        }

        private void ReceiveEvent(string obj)
        {
            throw new System.NotImplementedException();
        }
    }
}