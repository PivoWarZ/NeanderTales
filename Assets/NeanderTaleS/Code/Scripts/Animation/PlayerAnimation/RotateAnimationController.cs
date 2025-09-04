using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Animation.PlayerAnimation
{
    public class RotateAnimationController: MonoBehaviour, IAnimationController
    {
        private PlayerProvider _playerProvider;
        private AnimationEventDispatcher _event;
        private Animator _animator;
        private RotateComponent_LookAtCursor _rotateComponent;
        private IDisposable _disposable;
        private bool _isRotate = false;

        public void Init(PlayerProvider playerProvider, AnimationEventDispatcher eventDispatcher)
        {
            _playerProvider = playerProvider;
            _event = eventDispatcher;
            _rotateComponent = _playerProvider.RotateComponent;
            _animator = _playerProvider.Animator;
            
            _rotateComponent.OnRotate += RotateAnimation;
            _rotateComponent.OnRotateComplete += StopRotate;
            _event.OnReceiveEvent += ReceiveEvent;
        }

        private void ReceiveEvent(string eventName)
        {
            // if (eventName == "LeftRotateComplete")
            // {
            //     _animator.SetBool("IsRotate_Left", false);
            // }
            //
            // if (eventName == "RightRotateComplete")
            // {
            //     _animator.SetBool("IsRotate_Right", false);
            // }
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