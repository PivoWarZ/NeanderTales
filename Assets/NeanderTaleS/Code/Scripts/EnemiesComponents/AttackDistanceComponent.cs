using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class AttackDistanceComponent: MonoBehaviour
    {
        [SerializeField] private float _attackDistance;
        private Transform _target;
        private ReactiveProperty<bool> _isAttackDistance = new (false);

        public ReadOnlyReactiveProperty<bool> IsAttackDistance => _isAttackDistance;

        private void FixedUpdate()
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
    }
}