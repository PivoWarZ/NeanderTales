using System;
using NeanderTaleS.Code.Scripts.Condition;
using NeanderTaleS.Code.Scripts.EnemiesComponents.Interfaces;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Components
{
    public class HitPointsComponent: MonoBehaviour, ITakeDamageble, IBreakable
    {
        public event TakeDamageRequestHandler OnTakeDamageRequest;
        public event Action<float> OnTakeDamageAction;
        public event Action OnTakeDamageEvent;
        
        [SerializeField] private SerializableReactiveProperty<float> _hitPoints = new ();
        private bool _canTakeDamage = true;
        private CompositeCondition _condition  = new ();
        
        public ReadOnlyReactiveProperty<float> HitPoints => _hitPoints;

        private void Awake()
        {
            _condition.AddCondition(() => _canTakeDamage);
        }
        
        [Button]
        public void TakeDamage(float damage)
        {
            OnTakeDamageRequest?.Invoke(ref damage);
            
            if (!_condition.IsTrue())
            {
                return;
            }
            
            OnTakeDamageAction?.Invoke(damage);
            
            _hitPoints.Value -= damage;
            
            Debug.Log($"Hit: {_hitPoints.Value}");
        }

        public void TakeDamageEvent()
        {
            OnTakeDamageEvent?.Invoke();
        }
        
        public void SetCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        public void RemoveCondition(Func<bool> condition)
        {
            _condition.RemoveCondition(condition);
        }

        void IBreakable.EnabledMechanic()
        {
            _canTakeDamage = true;
        }

        void IBreakable.DisablingMechanic()
        {
            _canTakeDamage = false;
        }
    }
}