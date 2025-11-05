using System;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems.MoveInput
{
    public class MoveInputController: IInitializable, IDisposable, IPlayerInput
    {
        private MoveInputListener _input;
        private IMovable _movable;

        public MoveInputController(MoveInputListener input)
        {
            _input = input;
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

        public void Init(GameObject player)
        {
            _movable = player.GetComponent<IMovable>();
            
            if (_movable == null)
            {
                Debug.Log($"{player.GetType()} : {player.name} <color=red> IMovable NOT FOUND </color>");
            }
        }
        
        void IDisposable.Dispose()
        {
            _input.OnDirectionChanged -= Move;
        }
    }
}