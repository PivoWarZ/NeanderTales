using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.WeaponInterfaces;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Components
{
    public class PointHitDamageListener: MonoBehaviour
    {
        public event Action<Vector3> OnHitPoint;
        
        [SerializeField] private OnCollisionComponent _onCollision;
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        private Vector3 _hitPointPosition;

        public Vector3 GetLastHitPointPosition => _hitPointPosition;

        private void Awake()
        {
            _onCollision.OnEnterCollision += OnEnterCollision;
            _hitPointsComponent.OnTakeDamageAction += HitPoint;
        }

        private void HitPoint(float obj)
        {
            OnHitPoint?.Invoke(_hitPointPosition);
        }

        private void OnEnterCollision(Collision other)
        {
            var isWeapon = other.collider.gameObject.GetComponentInParent<IWeapon>();

            if (isWeapon != null && other.contacts.Length > 0)
            {
                _hitPointPosition = other.contacts[0].point;
            }
        }

        private void OnDestroy()
        {
            _onCollision.OnEnterCollision -= OnEnterCollision;
        }
    }
}