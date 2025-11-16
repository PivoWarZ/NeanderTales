using NeanderTaleS.Code.Scripts.Core.Components;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using R3;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.PlayerComponents.Components
{
    public sealed class StaminaComponent: MonoBehaviour, IStaminaComponent
    {
        [SerializeField] private StaminaUser _user;
        [SerializeField] private ConditionInstaller _conditionInstaller;
        [SerializeField] private SerializableReactiveProperty<float> _maxStamina = new (5);
        [SerializeField] private SerializableReactiveProperty<float> _stamina = new (5);
        
        public ReadOnlyReactiveProperty<float> MaxStamina => _maxStamina;
        public ReadOnlyReactiveProperty<float> Stamina => _stamina;

        private void Awake()
        {
            _user.OnSpend += Spend;
        }
        
        public void Init(IAttackEvents attackEvents)
        {
            _conditionInstaller.AddCondition<IAttackEvents>(CanStaminaPrice);
        }

        void IStaminaComponent.SetStamina(float stamina, float maxStamina)
        {
            
            _maxStamina.Value = Mathf.Max(0, _maxStamina.Value + maxStamina);
            _stamina.Value = Mathf.Clamp(_stamina.Value += stamina, 0, MaxStamina.CurrentValue);
        }

        private void Spend(float price)
        {
            _stamina.Value -= price;
        }

        public void AddStamina(float stamina)
        {
            var newValue = Stamina.CurrentValue + stamina;
            _stamina.Value = Mathf.Min(newValue, MaxStamina.CurrentValue);
        }

        public void AddMaxStamina(float stamina)
        {
            _maxStamina.Value = stamina > 0 ? _maxStamina.Value + stamina : _maxStamina.Value;
        }

        private bool CanStaminaPrice()
        {
            return Stamina.CurrentValue >= _user.CurrentStaminaPrice;
        }

        private void OnDestroy()
        {
            _user.OnSpend -= Spend;
        }
    }
}