using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation
{
    public class AnimationControllersInstaller: MonoBehaviour
    {
        [SerializeField] private LocalProvider _localProvider;
        private List<IAnimationController> _animationControllers = new ();

        private void Start()
        {
            _animationControllers = _localProvider.GetInterfaces<IAnimationController>();

            for (var index = 0; index < _animationControllers.Count; index++)
            {
                var animationController = _animationControllers[index];
                animationController.Init(_localProvider);
            }
        }
    }
}