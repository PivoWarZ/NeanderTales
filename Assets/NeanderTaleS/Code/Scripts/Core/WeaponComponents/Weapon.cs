using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Interfaces.WeaponInterfaces;
using UnityEngine;
using DealDamageComponent = NeanderTaleS.Code.Scripts.Core.Components.DealDamageComponent;

namespace NeanderTaleS.Code.Scripts.Core.WeaponComponents
{
    public class Weapon: MonoBehaviour, IWeapon
    {
        [SerializeField] private float _damage;
        [SerializeField] Collider _collider;
        [SerializeField] private bool _isTrigger;
        [SerializeField] private bool _isPlayer;
        private List<ITakeDamageable> _hitPointsCpmponents = new ();
        private DealDamageComponent _damageComponent;
        private IAttackable _attackable;

        private void Awake()
        {
            if (_isTrigger)
            {
                _collider.isTrigger = true;
            }
            
            _collider.enabled = false;
            
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
            if (_isPlayer && other.gameObject.CompareTag("Player"))
            {
                return;
            }

            bool isDamageble = other.gameObject.TryGetComponent<ITakeDamageable>(out var hitPointsComponent);

            if (isDamageble)
            {
                TryDealDamage(hitPointsComponent);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_isTrigger)
            {
                return;
            }

            bool isDamageble = other.gameObject.TryGetComponent<ITakeDamageable>(out var hitPointsComponent);
            
            if (isDamageble)
            {
                TryDealDamage(hitPointsComponent);
            }
        }


        private void TryDealDamage(ITakeDamageable hitPointsComponent)
        {
            if (IsComponentDamaged(hitPointsComponent))
            {
                return;
            }

            _damageComponent.DealDamage(hitPointsComponent, _damage);
            _hitPointsCpmponents.Add(hitPointsComponent);
        }

        private bool IsComponentDamaged(ITakeDamageable component)
        {
            if (_hitPointsCpmponents.Count == 0)
            {
                return false;
            }

            return _hitPointsCpmponents.Any(hitPointsCpmponent => hitPointsCpmponent == component);
        }
        
        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        private void OnDestroy()
        {
            _attackable.OnAttackAction -= PrepareToAttack;
            _attackable.OnAttackEvent -= DisabledWeaponCollider;
        }
    }
}