using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.ComponentInterfaces;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.WeaponInterfaces;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using UnityEngine;
using DealDamageComponent = NeanderTaleS.Code.Scripts.Components.DealDamageComponent;

namespace NeanderTaleS.Code.Scripts.WeaponComponents
{
    public class Weapon: MonoBehaviour, IWeapon
    {
        [SerializeField] private float _damage;
        [SerializeField] OnCollisionComponent _onCollision;
        [SerializeField] Collider _collider;
        private List<ITakeDamageble> _hitPointsCpmponents = new ();
        private DealDamageComponent _damageComponent;
        private IAttackable _attackable;

        private void Awake()
        {
            _onCollision.OnEnterCollision += OnCollisionEnter;
            DisabledWeaponCollider();
        }

        public void Init(DealDamageComponent damageComponent, IAttackable attackable)
        {
            _damageComponent = damageComponent;
            _attackable = attackable;

            _attackable.OnAttackAction += PrepareToAttack;
            _attackable.OnAttackEvent += DisabledWeaponCollider;
        }

        private void DisabledWeaponCollider()
        {
            _collider.enabled = false;
        }

        private void PrepareToAttack()
        {
            _hitPointsCpmponents.Clear();
            _collider.enabled = true;
        }

        private void OnCollisionEnter(Collision other)
        {
            bool isDamageble = other.gameObject.TryGetComponent<ITakeDamageble>(out var hitPointsComponent);

            if (isDamageble)
            {
                if (IsComponentDamaged(hitPointsComponent))
                {
                    return;
                }

                _damageComponent.DealDamage(hitPointsComponent, _damage);
                _hitPointsCpmponents.Add(hitPointsComponent);
            }
        }

        private bool IsComponentDamaged(ITakeDamageble component)
        {
            if (_hitPointsCpmponents.Count == 0)
            {
                return false;
            }

            return _hitPointsCpmponents.Any(hitPointsCpmponent => hitPointsCpmponent == component);
        }

        private void OnDestroy()
        {
            _attackable.OnAttackAction -= PrepareToAttack;
            _attackable.OnAttackEvent -= DisabledWeaponCollider;
            _onCollision.OnEnterCollision -= OnCollisionEnter;
        }
    }
}