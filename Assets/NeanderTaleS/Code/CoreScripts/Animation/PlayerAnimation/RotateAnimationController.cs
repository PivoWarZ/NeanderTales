using System;
using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Animations;
using NeanderTaleS.Code.CoreScripts.Components;
using NeanderTaleS.Code.CoreScripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.Animation.PlayerAnimation
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