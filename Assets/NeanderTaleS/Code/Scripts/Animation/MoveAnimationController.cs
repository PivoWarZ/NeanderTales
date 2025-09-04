using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation
{
    public class MoveAnimationController: MonoBehaviour, IAnimationController
    {
        [SerializeField] private Transform _inverseTransform;
        private Animator _animator;
        private IDisposable _disposable;
        
        public void Init(PlayerProvider playerProvider, AnimationEventDispatcher _)
        {
            var moveComponent = playerProvider.GetComponent<MoveComponent>();
            _animator = playerProvider.Animator;
            _disposable = moveComponent.MoveDirection.Subscribe(SetDirectionAxis);
        }
        
        public void SetDirectionAxis(Vector3 moveDirection)
        {
            var inverseDirection = _inverseTransform.InverseTransformDirection(moveDirection);
            
            _animator.SetFloat("XAxis", inverseDirection.x);
            _animator.SetFloat("YAxis", inverseDirection.z);
            _animator.SetBool("IsRotate_Left", false);
            _animator.SetBool("IsRotate_Right", false);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}