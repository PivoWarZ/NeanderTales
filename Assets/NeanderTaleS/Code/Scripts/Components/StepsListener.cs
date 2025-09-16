using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Components
{
    public class StepsListener: MonoBehaviour
    {
        public event Action<Transform> OnStep;
        
        private Transform _stepTransform;

        private void Awake()
        {
            _stepTransform = transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            OnStep?.Invoke(_stepTransform);
        }
    }
}