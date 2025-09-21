using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Interfaces.WeaponInterfaces;
using UnityEngine;
using DealDamageComponent = NeanderTaleS.Code.Scripts.Core.Components.DealDamageComponent;

namespace NeanderTaleS.Code.Scripts.Core.WeaponComponents
{
    public class Weapon: MonoBehaviour, IWeapon, IStartValueSetter
    {
        [SerializeField] private float _damage;
        [SerializeField] Collider _collider;
        [SerializeField] private bool _isTrigger;
        [SerializeField] private bool _isPlayer;
        private List<ITakeDamageble> _hitPointsCpmponents = new ();
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

            bool isDamageble = other.gameObject.TryGetComponent<ITakeDamageble>(out var hitPointsComponent);

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

            bool isDamageble = other.gameObject.TryGetComponent<ITakeDamageble>(out var hitPointsComponent);
            
            if (isDamageble)
            {
                TryDealDamage(hitPointsComponent);
            }
        }


        private void TryDealDamage(ITakeDamageble hitPointsComponent)
        {
            if (IsComponentDamaged(hitPointsComponent))
            {
                return;
            }

            _damageComponent.DealDamage(hitPointsComponent, _damage);
            _hitPointsCpmponents.Add(hitPointsComponent);
        }

        private bool IsComponentDamaged(ITakeDamageble component)
        {
            if (_hitPointsCpmponents.Count == 0)
            {
                return false;
            }

            return _hitPointsCpmponents.Any(hitPointsCpmponent => hitPointsCpmponent == component);
        }
        
        public void SetStartValue(float currentValue, float maxValue = 0)
        {
           _damage = currentValue;
        }

        private void OnDestroy()
        {
            _attackable.OnAttackAction -= PrepareToAttack;
            _attackable.OnAttackEvent -= DisabledWeaponCollider;
        }
    }
}