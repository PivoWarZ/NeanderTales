using System;
using NeanderTaleS.Code.Scripts.Core.Condition;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Components
{
    public class DealDamageComponent: MonoBehaviour
    {
        public event DealDamageRequestHandler OnDealDamageRequest;
        public event Action<float> OnDealDamageAction;
        public event Action OnDealDamageEvent;

        private bool _canDealDamage;
        private CompositeCondition _condition = new ();

        public void DealDamage(ITakeDamageble hitPointsComponent, float damage)
        {
            OnDealDamageRequest?.Invoke(ref damage);

            if (!_condition.IsTrue())
            {
                return;
            }
            
            OnDealDamageAction?.Invoke(damage);
            
            hitPointsComponent.TakeDamage(damage);
            
            hitPointsComponent.TakeDamageEvent();
            
            OnDealDamageEvent?.Invoke();
        }
    }
}