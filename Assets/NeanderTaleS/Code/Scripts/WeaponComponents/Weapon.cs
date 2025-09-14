using System;
using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.EnemiesComponents;
using NeanderTaleS.Code.Scripts.PlayerComponents.Interfaces;
using UnityEngine;
using DealDamageComponent = NeanderTaleS.Code.Scripts.Components.DealDamageComponent;

namespace NeanderTaleS.Code.Scripts.WeaponComponents
{
    public class Weapon: MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] OnCollisionComponent _onCollision;
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
            _onCollision.gameObject.SetActive(false);
        }

        private void PrepareToAttack()
        {
            _hitPointsCpmponents.Clear();
            _onCollision.gameObject.SetActive(true);
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