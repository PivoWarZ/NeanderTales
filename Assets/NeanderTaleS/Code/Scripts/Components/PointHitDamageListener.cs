using System;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.WeaponInterfaces;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using NeanderTaleS.Code.Scripts.WeaponComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Components
{
    public class PointHitDamageListener: MonoBehaviour
    {
        public event Action<Vector3> OnHitPointLocation;
        
        [SerializeField] private OnCollisionComponent _onCollision;
        private Vector3 _hitPointPosition;

        public Vector3 GetLastHitPointPosition => _hitPointPosition;

        private void Awake()
        {
            _onCollision.OnEnterCollision += OnEnterCollision;
        }

        private void OnEnterCollision(Collision other)
        {
            Debug.Log(other.collider.name);
            var isWeapon = other.collider.gameObject.GetComponentInParent<IWeapon>();

            if (isWeapon != null && other.contacts.Length > 0)
            {
                _hitPointPosition = other.contacts[0].point;
                OnHitPointLocation?.Invoke(_hitPointPosition);
                Debug.Log(_hitPointPosition);
            }
        }

        private void OnDestroy()
        {
            _onCollision.OnEnterCollision -= OnEnterCollision;
        }
    }
}