using System;
using System.Collections.Generic;
using NeanderTaleS.Code.Scripts.Components;
using NeanderTaleS.Code.Scripts.PlayerComponents.Components;

namespace NeanderTaleS.Code.Scripts.Animation.EnemyAnimation
{
    public class HitPointsEmptyConditionInstaller
    {
        private LocalProvider _localProvider;
        private ITakeDamageble _hitPointsComponent;

        public HitPointsEmptyConditionInstaller(LocalProvider localProvider)
        {
            _localProvider = localProvider;
            _hitPointsComponent = localProvider.GetInterface<ITakeDamageble>();
        }

        public void SetHitPpointsEmptyCondition()
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