using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using UnityEngine;

namespace NeanderTaleS.Code.Scripts.Core.Components
{
    public class ConditionInstaller: MonoBehaviour
    {
        [SerializeField] private LocalProvider _localProvider;
        [SerializeField] private bool _isHitPointsEmpty;
        [SerializeField] private bool _isAttackDistance;
        private ITakeDamageble _hitPointsComponent;
        private IAttackable _attackComponent;
        private IMovable _movableComponent;

        public void Init()
        {
            InstallConditions(_localProvider);
        }

        public void InstallConditions(LocalProvider localProvider)
        {
            _hitPointsComponent = localProvider.GetComponent<ITakeDamageble>();

            AddHitPointsEmptyCondition();
            AddAttackDistanceCondition();
        }
        
        public void AddCondition<T>(Func<bool> condition) where T : class
        {
            var conditionComponent = _localProvider.TryGetInterface<T>(out var value);

            if (conditionComponent)
            {
                if (value is IConditionComponent component)
                {
                    component.AddCondition(condition);
                }
            }
        }

        private void AddAttackDistanceCondition()
        {
            if (!_isAttackDistance)
            {
                return;
            }

            bool isRangeEntity = _localProvider.TryGetInterface<IAttackDistance>(out var attackDistance);

            if (!isRangeEntity)
            {
                Debug.Log("<color=yellow>IAttackDistance not found. Condition not added.</color>");
                return;
            }

            bool movable = _localProvider.TryGetInterface<IMovable>(out var moveComponent);

            if (!movable)
            {
                Debug.Log("<color=yellow>IMovable not found. Condition not added.</color>");
                return;
            }

            if (moveComponent is IConditionComponent condition)
            {
                condition.AddCondition(() => !attackDistance.IsAttackDistance);
            }
            else
            {
                Debug.Log("<color=yellow>IMovable is not IConditionComponent not found. Condition not added.</color>");
            }
            
            bool attackable = _localProvider.TryGetInterface<IAttackable>(out var attackComponent);

            if (!attackable)
            {
                Debug.Log("<color=yellow>IRotatable not found. Condition not added.</color>");
                return;
            }

            if (attackComponent is IConditionComponent conditionComponent)
            {
                conditionComponent.AddCondition(() => attackDistance.IsAttackDistance);
            }
            else
            {
                Debug.Log("<color=yellow>IRotatable is not IConditionComponent not found. Condition not added.</color>");
            }

        }

        private void AddHitPointsEmptyCondition()
        {
            if (!_isHitPointsEmpty)
            {
                return;
            }

            List<IConditionComponent> components = _localProvider.GetInterfaces<IConditionComponent>();

            for (var index = 0; index < components.Count; index++)
            {
                var conditionComponent = components[index];
                conditionComponent.AddCondition(IsAlive);
            }
        }

        private bool IsAlive()
        {
            float hitPoints = _hitPointsComponent.CurrentHitPoints.CurrentValue;
            return hitPoints > 0;
        }
    }
}