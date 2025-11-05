using System;
using NeanderTaleS.Code.Scripts.Core.EnemiesComponents;
using NeanderTaleS.Code.Scripts.Interfaces.WeaponInterfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Components
{
    public class PointHitDamageListener: MonoBehaviour
    {
        public event Action<Vector3> OnHitPoint;
        
        [SerializeField] private OnCollisionComponent _onCollision;
        [SerializeField] private DealDamageComponent _takeDamageEvents;
        private Vector3 _hitPointPosition;

        public Vector3 GetLastHitPointPosition => _hitPointPosition;

        private void Awake()
        {
            _onCollision.OnEnterCollision += OnEnterCollision;
            _takeDamageEvents.OnDealDamageAction += HitPoint;
            Debug.Log($"Hit damage listener Awake, {_takeDamageEvents.gameObject.name}");
        }

        private void HitPoint(float _)
        {
            OnHitPoint?.Invoke(_hitPointPosition);
            Debug.Log($"OnHitPoints");
        }

        private void OnEnterCollision(Collision other)
        {
            var weapon = other.collider.gameObject.GetComponentInParent<IWeapon>();

            if (weapon != null && other.contacts.Length > 0)
            {
                _hitPointPosition = other.contacts[0].point;
            }
        }

        private void OnDestroy()
        {
            _onCollision.OnEnterCollision -= OnEnterCollision;
            _takeDamageEvents.OnDealDamageAction -= HitPoint;
        }
    }
}