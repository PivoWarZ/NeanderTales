using System;
using NeanderTaleS.Code.Scripts.Effects.StepsFX;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Components
{
    public class StepsFXInstaller: MonoBehaviour
    {
        [SerializeField] private StepsListener _listener;
        [SerializeField] private RootTransforms _root;
        [SerializeField] private OnStepFX _stepFX;
        private StepsListener _rightFoot;
        private StepsListener _leftFoot;

        public StepsListener RightFoot => _rightFoot;

        public StepsListener LeftFoot => _leftFoot;

        private void Awake()
        {
            var leftFootTransform = _root.LeftFoot;
            var rightFootTransform = _root.RightFoot;
            
            _rightFoot = Instantiate(_listener, rightFootTransform);
            _leftFoot = Instantiate(_listener, leftFootTransform);
            
            _stepFX.Initialize(_leftFoot, _rightFoot);
        }
    }
}