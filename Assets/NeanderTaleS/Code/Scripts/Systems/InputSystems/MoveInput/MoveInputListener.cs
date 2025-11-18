using System;
using NeanderTaleS.Code.Scripts.Systems.GameCycle;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems.MoveInput
{
    public sealed class MoveInputListener: IGameCycleTick
    {
        public event Action<Vector3> OnDirectionChanged;
        private Vector3 _direction = Vector3.zero;
        
        void IGameCycleTick.Tick(float deltaTime)
        {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");

            OnDirectionChanged?.Invoke(_direction);
        }
    }
}