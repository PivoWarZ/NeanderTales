using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.EnemyAnimation
{
    public class EnemyAnimatorControllerInstaller : MonoBehaviour
    {
        [SerializeField] private EnemyProvider _enemyProvider;
        [SerializeField] private AnimationEventDispatcher _event;
        private List<IEnemyAnimationController> _animationControllers;

        private void Awake()
        {
            _animationControllers = new List<IEnemyAnimationController>();
            _animationControllers = gameObject.GetComponents<IEnemyAnimationController>().ToList();

            foreach (var animationController in _animationControllers)
            {
                animationController.Init(_enemyProvider, _event);
            }
        }
    }
}