using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class AttackDistanceComponent: MonoBehaviour, ITargetInitComponent
    {
        [SerializeField] private float _attackDistance;
        private Transform _target;
        [SerializeField] private SerializableReactiveProperty<bool> _isAttackDistance = new (false);
        
        public ReadOnlyReactiveProperty<bool> IsAttackDistance => _isAttackDistance;

        private void Update()
        {
            if (_target == null)
            {
                return;
            }
            
            var distanceToTarget = _target.position - transform.position;
            var distance = distanceToTarget.magnitude;

            if (distance <= _attackDistance)
            {
                _isAttackDistance.Value = true;
            }
            else
            {
                _isAttackDistance.Value = false;
            }
        }
        
        public void SetTarget(GameObject target)
        {
            _target = target.transform;
        }
    }
}