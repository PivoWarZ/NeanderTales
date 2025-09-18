using System;
using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Components;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.CoreScripts.InputSystems.MoveInput
{
    public class PlayerInputController: IInitializable, IDisposable
    {
        private PlayerInputListener _input;
        private IMovable _movable;

        public PlayerInputController(PlayerInputListener input, IMovable movable)
        {
            _input = input;
            _movable = movable;
        }

        public void Initialize()
        {
            _input.OnDirectionChanged += Move;
        }

        private void Move(Vector3 direction)
        {
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