using NeanderTaleS.Code.Scripts.Core.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Effects.StepsFX
{
    public class OnStepFX: MonoBehaviour
    {
        public float Scale = 3f;
        
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private float _offsetY = -0.1f;
        private ParticleSystem _effectRight;
        private ParticleSystem _effectLeft;
        private StepsListener _lefsStep;
        private StepsListener _rightStep;
        
        [Button]
        public void CreateEffects()
        {
            _particle.playOnAwake = false;
            
            _effectRight = Instantiate(_particle, transform).GetComponent<ParticleSystem>();
            _effectRight.gameObject.transform.localScale *= Scale;
            var offsetRight = _effectRight.gameObject.transform.position.y;
            _effectRight.transform.position = new Vector3(_effectRight.transform.position.x, offsetRight, _effectRight.transform.position.z);
            
            _effectLeft = Instantiate(_particle, transform).GetComponent<ParticleSystem>();
            _effectLeft.gameObject.transform.localScale *= Scale;
            var offsetLeft = _effectRight.gameObject.transform.position.y;
            _effectRight.transform.position = new Vector3(_effectRight.transform.position.x, offsetLeft, _effectRight.transform.position.z);
        }

        public void Initialize(StepsListener lefsStep, StepsListener rightStep)
        {
            _lefsStep = lefsStep;
            _rightStep = rightStep;
            CreateEffects();

            _lefsStep.OnStep += StepFXLeft;
            _rightStep.OnStep += StepFXRight;
        }

        private void StepFXRight(Transform footTransform)
        {
            _effectRight.transform.position = footTransform.position;
            _effectRight.Play();
        }

        private void StepFXLeft(Transform footTransform)
        {
            _effectLeft.transform.position = footTransform.position;
            _effectLeft.Play();
        }

        private void OnDestroy()
        {
            _lefsStep.OnStep -= StepFXLeft;
            _rightStep.OnStep -= StepFXRight;
        }
    }
}