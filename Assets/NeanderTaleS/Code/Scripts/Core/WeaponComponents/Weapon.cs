using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Interfaces.WeaponInterfaces;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.WeaponComponents
{
    public class Weapon: MonoBehaviour, IWeapon
    {
        [SerializeField] private float _damage;
        [SerializeField] Collider _collider;
        [SerializeField] ParticleSystem _hitParticle;
        [SerializeField] private float _particleSize = 0.01f;
        [SerializeField] Transform _firePoint;
        [SerializeField] private bool _isTrigger;
        [SerializeField] private bool _isPlayer;
        private ParticleSystem _hitEffect;
        private readonly List<IDamageable> _damagedTargets = new ();
        private IDealDamage _dealDamageComponent;
        private IAttackEvents _attackEvents;

        private void Awake()
        {
            if (_isTrigger)
            {
                _collider.isTrigger = true;
            }
            
            _collider.enabled = false;
            _hitEffect =  Instantiate(_hitParticle, _firePoint.transform);
            _firePoint.gameObject.transform.localScale = new Vector3(_particleSize, _particleSize, _particleSize);
            
            DisabledWeaponCollider();

            _attackEvents = GetComponentInParent<IAttackEvents>();
            _dealDamageComponent = GetComponentInParent<IDealDamage>();
            
            _attackEvents.OnAttackAction += PrepareToAttack;
            _attackEvents.OnAttackEvent += DisabledWeaponCollider;
        }
        
        private void OnDestroy()
        {
            _attackEvents.OnAttackAction -= PrepareToAttack;
            _attackEvents.OnAttackEvent -= DisabledWeaponCollider;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_isPlayer && other.gameObject.CompareTag("Player"))
            {
                return;
            }

            var target = other.gameObject;

            if (!IsDamageable(target, out var damageable))
            {
                return;
            }
            
            var isDamageDealing = TryDealDamage(damageable);
            
            if (isDamageDealing)
            {
               PlayHitParticle(other);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var target = other.gameObject;

            if (!IsDamageable(target, out var damageable))
            {
                return;
            }
            
            var isDamageDealing = TryDealDamage(damageable);
            
            if (isDamageDealing)
            {
                PlayHitParticle();
            }
        }
        
        private void DisabledWeaponCollider()
        {
            _collider.enabled = false;
        }

        private void PrepareToAttack()
        {
            _damagedTargets.Clear();
            _collider.enabled = true;
        }

        private void PlayHitParticle(Collision other = null)
        {
            if (_isTrigger)
            {
                _hitEffect.transform.position = _firePoint.position;
                _hitEffect.Play();
                return;
            }

            if (other == null)
            {
                return;
            }
            
            _hitEffect.transform.position = other.contacts[0].point;
            _hitEffect.Play();
        }

        private bool IsDamageable(GameObject target, out IDamageable damageableComponent)
        {
            bool isDamageble = target.TryGetComponent<IDamageable>(out var damageable);

            if (isDamageble)
            {
                damageableComponent = damageable;
                return true;
            }
            
            damageableComponent = null;
            return false;
        }

        private bool TryDealDamage(IDamageable damageable)
        {
            if (IsComponentDamaged(damageable))
            {
                return false;
            }

            _dealDamageComponent.DealDamage(damageable, _damage); 
            _damagedTargets.Add(damageable);
            return true;
        }

        private bool IsComponentDamaged(IDamageable component)
        {
            if (_damagedTargets.Count == 0)
            {
                return false;
            }

            return _damagedTargets.Any(damageable => damageable == component);
        }
        
        public void SetDamage(float damage)
        {
            _damage = damage;
        }
    }
}