using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.PlayerAnimation
{
    public class RotateAnimationController: MonoBehaviour, IAnimationController
    {
        private Animator _animator;
        private RotateComponent_LookAtCursor _rotateComponent;
        private IDisposable _disposable;
        private bool _isRotate = false;

        public void Init(LocalProvider localProvider)
        {
            _rotateComponent = localProvider.GetService<RotateComponent_LookAtCursor>();
            _animator = localProvider.Animator;
            
            _rotateComponent.OnRotate += RotateAnimation;
            _rotateComponent.OnRotateComplete += StopRotate;
        }

        private void RotateAnimation(bool isRightRotate)
        {
            _animator.SetBool("IsRotate_Right", isRightRotate);
            _animator.SetBool("IsRotate_Left", !isRightRotate);
        }

        private void StopRotate()
        {
            _animator.SetBool("IsRotate_Left", false);
            _animator.SetBool("IsRotate_Right", false);
        }

        private void OnDestroy()
        {
            _rotateComponent.OnRotate -= RotateAnimation;
            _rotateComponent.OnRotateComplete -= StopRotate;
        }
    }
}