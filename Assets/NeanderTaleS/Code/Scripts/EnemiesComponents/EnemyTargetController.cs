using System;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class EnemyTargetController: MonoBehaviour
    {
        [SerializeField] EnemyTargetComponent _targetComponent;
        [SerializeField] private EnemyRotateComponent _rotateComponent;

        private void Awake()
        {
            _targetComponent.OnTargetChanged += SetTarget;
        }

        private void SetTarget(GameObject target)
        {
            _rotateComponent.SetTarget(target.transform);
        }

        private void OnDestroy()
        {
            _targetComponent.OnTargetChanged -= SetTarget;
        }
    }
}