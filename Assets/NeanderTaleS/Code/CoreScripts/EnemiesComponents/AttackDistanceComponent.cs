using System;
using NeanderTaleS.Code.CoreScripts.Animation.Interfaces.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.EnemiesComponents
{
    public class AttackDistanceComponent: MonoBehaviour, IAttackDistance
    {
        
        [SerializeField] private float _attackDistance;
        [SerializeField] private DistanceToTargetComponent _distanceComponent;
        [SerializeField] public bool _isAttackDistance;
        private float _targetDistance;
        private IDisposable _dispose;
        

        public bool IsAttackDistance => _isAttackDistance;
        
        private void Awake()
        {
            _dispose = _distanceComponent.TargetDistance.Where(distance => distance <= _attackDistance).Subscribe(IsDistanceAttack);
        }

        private void IsDistanceAttack(float distance)
        {
            _dispose?.Dispose();
            _dispose = _distanceComponent.TargetDistance.Where(distance => distance > _attackDistance).Subscribe(IsDistanceMoving);
            
            _isAttackDistance = true;
        }

        private void IsDistanceMoving(float distance)
        {
            _dispose?.Dispose();
            _dispose = _distanceComponent.TargetDistance.Where(distance => distance <= _attackDistance).Subscribe(IsDistanceAttack);
            
            _isAttackDistance = false;
        }

        private void OnDestroy()
        {
            _dispose?.Dispose();
        }
    }
}