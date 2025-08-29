using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.InputSystems.MoveInput
{
    public class PlayerInputController: IInitializable, IDisposable
    {
        private PlayerInputListener _input;
        private IMovable _movable;
        private IAnimationController _animationController;

        public PlayerInputController(PlayerInputListener input, IMovable movable, IAnimationController animationController)
        {
            _input = input;
            _movable = movable;
            _animationController = animationController;
        }

        public void Initialize()
        {
            _input.OnDirectionChanged += Move;
        }

        private void Move(Vector3 direction)
        {
            _animationController.SetDirectionAxis(direction);
            
            if (direction == Vector3.zero)
            {
                return;
            }
            
            _movable.Move(direction);
        }

        void IDisposable.Dispose()
        {
            _input.OnDirectionChanged -= Move;
        }
    }
}