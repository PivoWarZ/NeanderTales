using System;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.InputSysytem
{
    public class PlayerInputListener: ITickable
    {
        public event Action<Vector3> OnDirectionChanged;
        private Vector3 _direction = Vector3.zero;
        
        public void Tick()
        {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");

            OnDirectionChanged?.Invoke(_direction);
        }
    }
}