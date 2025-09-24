using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.EnemiesComponents
{
    public class DistanceToTargetComponent: MonoBehaviour
    {
        [SerializeField] private SerializableReactiveProperty<float> _targetDistance = new (50);
        private Transform _target;
        
        public ReadOnlyReactiveProperty<float> TargetDistance => _targetDistance;

        public void SetTarget(GameObject target)
        {
            _target = target.transform;
        }

        private void Update()
        {
            if (!_target)
            {
                return;
            }

            var distance = _target.position - transform.position;
            _targetDistance.Value = distance.magnitude;
        }
    }
}