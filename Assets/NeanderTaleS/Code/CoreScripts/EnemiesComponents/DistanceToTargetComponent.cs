using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.EnemiesComponents
{
    public class DistanceToTargetComponent: MonoBehaviour, ITargetInitComponent
    {
        [SerializeField] private SerializableReactiveProperty<float> _targetDistance = new ();
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