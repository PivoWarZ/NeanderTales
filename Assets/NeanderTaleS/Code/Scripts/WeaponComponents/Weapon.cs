using System.Collections.Generic;
using System.Linq;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Animation.Interfaces.WeaponInterfaces;
using NeanderTaleS.Code.Scripts.Components;
using UnityEngine;
using DealDamageComponent = NeanderTaleS.Code.Scripts.Components.DealDamageComponent;

namespace NeanderTaleS.Code.Scripts.WeaponComponents
{
    public class Weapon: MonoBehaviour, IWeapon
    {
        [SerializeField] private float _damage;
        //[SerializeField] OnCollisionComponent _onCollision;
        [SerializeField] Collider _collider;
        [SerializeField] private bool _isTrigger;
        private List<ITakeDamageble> _hitPointsCpmponents = new ();
        private DealDamageComponent _damageComponent;
        private IAttackable _attackable;

        private void Awake()
        {
            if (_isTrigger)
            {
               // _onCollision.OnEnterTrigger += EnterTrigger;
                _collider.isTrigger = true;
            }
            
            _collider.enabled = false;

           // _onCollision.OnEnterCollision += EnterCollision;
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
            Debug.Log($"{gameObject.name} OnCollisionEnter: {other.gameObject.name}");

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

        private void OnDestroy()
        {
            _attackable.OnAttackAction -= PrepareToAttack;
            _attackable.OnAttackEvent -= DisabledWeaponCollider;
          //  _onCollision.OnEnterCollision -= EnterCollision;
           // _onCollision.OnEnterTrigger -= EnterTrigger;
        }
    }
}