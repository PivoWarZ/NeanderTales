using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.EnemiesComponents
{
    public class EnemyTargetController: MonoBehaviour
    {
        [SerializeField] private EnemyTargetComponent _targetComponent;
        [SerializeField] private DistanceToTargetComponent _distance;
        [SerializeField] private LocalProvider _localProvider;
        [SerializeField] private float _activatingDistance = 7f;
        private List<ITargetInitComponent> _targetInitComponents = new ();
        private IDisposable _dispose;

        private void Awake()
        {
            _targetComponent.OnTargetChanged += OnTargetDistance;
        }

        private void OnTargetDistance(GameObject target)
        {
            _distance.SetTarget(target);
            
           _dispose = _distance.TargetDistance
              .Where(distanceToTarget => distanceToTarget <= _activatingDistance)
                .Subscribe(Activating);
        }

        private void Activating(float _)
        {
            _dispose.Dispose();
            
            var target = _targetComponent.Target;
            _targetInitComponents = _localProvider.GetInterfaces<ITargetInitComponent>();
            
            if (_targetInitComponents.Count == 0)
            {
                return;
            }

            for (var i = 0; i < _targetInitComponents.Count; i++)
            {
                var targetInitComponent = _targetInitComponents[i];
                targetInitComponent.SetTarget(target);
            }
        }

        private void OnDestroy()
        {
            _targetComponent.OnTargetChanged -= OnTargetDistance;
            _dispose?.Dispose();
        }
    }
}