namespace NeanderTaleS.Code.Scripts.Core.Components
{
    /*public class HitPointsEmptyConditionInstaller
    {
        private LocalProvider _localProvider;
        private ITakeDamageEvents _hitPointsComponent;

        public HitPointsEmptyConditionInstaller(LocalProvider localProvider)
        {
            _localProvider = localProvider;
            _hitPointsComponent = localProvider.GetInterface<ITakeDamageEvents>();
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
            float hitPoints = _hitPointsComponent.CurrentHitPoints.CurrentValue;
            return hitPoints > 0;
        }
    }*/
}