using NeanderTaleS.Code.Scripts.Core.Effects.StepsFX;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Components
{
    public class StepsFXInstaller: MonoBehaviour
    {
        [SerializeField] private StepsListener _listener;
        [SerializeField] private RootTransforms _root;
        [SerializeField] private OnStepFX _stepFX;
        [SerializeField] private float _scale;
        private StepsListener _rightFoot;
        private StepsListener _leftFoot;

        public StepsListener RightFoot => _rightFoot;

        public StepsListener LeftFoot => _leftFoot;

        public void Init()
        {
            var leftFootTransform = _root.LeftFoot;
            var rightFootTransform = _root.RightFoot;
            
            _rightFoot = Instantiate(_listener, rightFootTransform);
            _rightFoot.gameObject.transform.localScale *= _scale;
            
            _leftFoot = Instantiate(_listener, leftFootTransform);
            _leftFoot.gameObject.transform.localScale *= _scale;
            
            _stepFX.Initialize(_leftFoot, _rightFoot);
        }
    }
}