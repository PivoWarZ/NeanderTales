using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using UnityEngine;
using DealDamageComponent = NeanderTaleS.Code.Scripts.Components.DealDamageComponent;

namespace NeanderTaleS.Code.Scripts.WeaponComponents
{
    public class Weapon: MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] OnCollisionComponent _onCollision;
        private DealDamageComponent _damageComponent;

        private void Awake()
        {
            _onCollision.OnEnterCollision += OnCollisionEnter;
        }

        public void Init(DealDamageComponent damageComponent)
        {
            _damageComponent = damageComponent;
        }

        private void OnCollisionEnter(Collision other)
        {
            bool isDamageble = other.gameObject.TryGetComponent<ITakeDamageble>(out var hitPointsComponent);

            if (isDamageble)
            {
                _damageComponent.DealDamage(hitPointsComponent, _damage);
            }
        }
    }
}