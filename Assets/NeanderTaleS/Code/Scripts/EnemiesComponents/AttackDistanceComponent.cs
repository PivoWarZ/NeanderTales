using NeanderTaleS.Code.Scripts.Animation.Interfaces;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.ComponentInterfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.EnemiesComponents
{
    public class AttackDistanceComponent: MonoBehaviour, ITargetInitComponent, IAttackDistance
    {
        public bool IsAttackDistance { get; private set; }
        
        [SerializeField] private float _attackDistance;
        [SerializeField] private DistanceToTargetComponent _distanceComponent;
        private float _targetDistance;
        private Transform _target;

        private void Update()
        {
            if (_target == null)
            {
                return;
            }
            
            _targetDistance = _distanceComponent.TargetDistance.CurrentValue;

            if (_targetDistance <= _attackDistance)
            {
                IsAttackDistance = true;
            }
            else
            {
                IsAttackDistance = false;
            }
        }
        
        public void SetTarget(GameObject target)
        {
            _target = target.transform;
        }
    }
}