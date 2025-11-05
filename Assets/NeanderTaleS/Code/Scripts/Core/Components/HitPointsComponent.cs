using System;
using NeanderTaleS.Code.Scripts.Core.Condition;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Components
{
    public class HitPointsComponent: MonoBehaviour, ITakeDamageEvents, IHitPointsComponent, IDamageable
    {
        public event TakeDamageRequestHandler OnTakeDamageRequest;
        public event Action<float, IHitPointsComponent> OnTakeDamageAction;
        public event Action OnTakeDamageEvent;
        
        [SerializeField] private SerializableReactiveProperty<float> _maxHitPoints = new ();
        [SerializeField] private SerializableReactiveProperty<float> _currentHitPoints = new ();
        private bool _canTakeDamage = true;
        private CompositeCondition _condition  = new ();
        
        public ReadOnlyReactiveProperty<float> MaxHitPoints => _maxHitPoints;
        public ReadOnlyReactiveProperty<float> CurrentHitPoints => _currentHitPoints;

        private void Awake()
        {
            _condition.AddCondition(() => _canTakeDamage);
        }
        
        [Button]
        void IDamageable.TakeDamage(float damage)
        {
            OnTakeDamageRequest?.Invoke(ref damage);
            
            if (!_condition.IsTrue())
            {
                return;
            }
            
            OnTakeDamageAction?.Invoke(damage, this);
            
            var newValue = _currentHitPoints.Value -= damage;
            
            _currentHitPoints.Value = Math.Min(newValue, MaxHitPoints.CurrentValue);
            
            OnTakeDamageEvent?.Invoke();
        }

        void IHitPointsComponent.AddHitPoints(float currentHitPoints, float maxHitPoints)
        {
            _maxHitPoints.Value = Math.Max(0, _maxHitPoints.Value + maxHitPoints);
            _currentHitPoints.Value = Mathf.Clamp(_currentHitPoints.Value += currentHitPoints, 0, MaxHitPoints.CurrentValue);
        }

        void IHitPointsComponent.SetHitPoints(float currentHitPoints, float maxHitPoints)
        {
            _currentHitPoints.Value = Mathf.Clamp(currentHitPoints, 0, MaxHitPoints.CurrentValue);
            _maxHitPoints.Value = Math.Max(0, _maxHitPoints.Value);
        }

        public void SetCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }
    }
}