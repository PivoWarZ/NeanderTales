using System;
using UnityEngine;

namespace NeanderTaleS.Code.CoreScripts.PlayerComponents.Components
{
    public class StaminaUser: MonoBehaviour
    {
        public event Action<float> OnSpend;
        
        [SerializeField] private AttackComponent _attackComponent;
        [SerializeField] private float _staminaPrice;
        [SerializeField] private float _comboStaminaModifier;
        private float _currentStaminaPrice;

        public float CurrentStaminaPrice => _currentStaminaPrice;

        private void Awake()
        {
            _currentStaminaPrice = _staminaPrice;
            
            _attackComponent.OnAttackAction += SpendStamina;
            _attackComponent.OnAttackEvent += Reload;
        }

        private void SpendStamina()
        {
            OnSpend?.Invoke(_currentStaminaPrice);
            _currentStaminaPrice *= _comboStaminaModifier;
        }
        
        private void Reload()
        {
            _currentStaminaPrice = _staminaPrice;
        }
    }
}