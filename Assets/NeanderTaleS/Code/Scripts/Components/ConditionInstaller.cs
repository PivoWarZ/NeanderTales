using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Components
{
    public class ConditionInstaller: MonoBehaviour
    {
        [SerializeField] private LocalProvider _localProvider;
        private ITakeDamageble _hitPointsComponent;

        private void Start()
        {
            _hitPointsComponent = _localProvider.GetComponent<ITakeDamageble>();

            AddHitPointsEmptyCondition();
        }

        private void AddHitPointsEmptyCondition()
        {
            List<IConditionComponent> components = _localProvider.GetInterfaces<IConditionComponent>();

            for (var index = 0; index < components.Count; index++)
            {
                var conditionComponent = components[index];
                conditionComponent.AddCondition(IsAlive);
            }
        }

        private bool IsAlive()
        {
            float hitPoints = _hitPointsComponent.HitPoints.CurrentValue;
            return hitPoints > 0;
        }
    }
}