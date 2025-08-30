using System;
using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation
{
    public class AnimationControllerInstaller: MonoBehaviour
    {
        [SerializeField] private PlayerProvider _playerProvider;
        [SerializeField] private AnimationEventDispatcher _event;
        private List<IAnimationController> _animationControllers;

        private void Awake()
        {
            _animationControllers = new List<IAnimationController>();
            _animationControllers = gameObject.GetComponents<IAnimationController>().ToList();
            
            foreach (var animationController in _animationControllers)
            {
                animationController.Init(_playerProvider, _event);
            }
        }
    }
}