using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation
{
    public class AnimationController: MonoBehaviour, IAnimationController
    {
        [SerializeField] private PlayerProvider _playerProvider;
        [SerializeField] private AnimationEventDispatcher _event;
        [SerializeField] private Transform _inverseTransform;
        private Animator _animator;

        private void Awake()
        {
            _animator = _playerProvider.Animator;
        }

        public void SetDirectionAxis(Vector3 moveDirection)
        {
            var inverseDirection = _inverseTransform.InverseTransformDirection(moveDirection);
            
            _animator.SetFloat("XAxis", inverseDirection.x);
            _animator.SetFloat("YAxis", inverseDirection.z);
        }
    }
}