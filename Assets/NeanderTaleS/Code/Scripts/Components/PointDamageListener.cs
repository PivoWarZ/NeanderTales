using NeanderTaleS.Code.Scripts.EnemiesComponents;
using NeanderTaleS.Code.Scripts.WeaponComponents;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Components
{
    public class PointDamageListener: MonoBehaviour
    {
        [SerializeField] private OnCollisionComponent _onCollision;
        private Vector3 _hitPoint;

        public Vector3 WeaponHitPoint => _hitPoint;

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
                _hitPoint = other.contacts[0].point;
                Debug.Log(_hitPoint);
            }
        }

        private void OnDestroy()
        {
            _onCollision.OnEnterCollision -= OnEnterCollision;
        }
    }
}