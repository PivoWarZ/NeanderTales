using System;
using NeanderTaleS.Code.Scripts.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Effects.StepsFX
{
    public class OnStepFX: MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private float _scale = 1f;
        [SerializeField] private float _offsetY = -0.1f;
        private ParticleSystem _effectRight;
        private ParticleSystem _effectLeft;
        private StepsListener _lefsStep;
        private StepsListener _rightStep;

        private void Awake()
        {
            _particle.playOnAwake = false;
            
            _effectRight = Instantiate(_particle, transform).GetComponent<ParticleSystem>();
            _effectRight.gameObject.transform.localScale *= _scale;
            
            _effectLeft = Instantiate(_particle, transform).GetComponent<ParticleSystem>();
            _effectLeft.gameObject.transform.localScale *= _scale;
        }

        public void Initialize(StepsListener lefsStep, StepsListener rightStep)
        {
            _lefsStep = lefsStep;
            _rightStep = rightStep;

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